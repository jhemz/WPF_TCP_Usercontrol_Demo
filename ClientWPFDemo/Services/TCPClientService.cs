using ClientWPFDemo.EventHandlers;
using ClientWPFDemo.Models;
using ClientWPFDemo.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ClientWPFDemo.Services
{
    public class TCPClientService
    {
        private TcpClient client;
        private NetworkStream stream;
        private bool connected;
        private DispatcherTimer timer;
        private int port = 100;

        private BackgroundWorker backgroundWorker;

        public event UpdateModelHandler UpdateModelEvent;

        public TCPClientService()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(Sync);
        }

       

        public void Disconnect()
        {
            client.Close();
            //set class variable to false for the puposes of the switch statement in 'Main'
            connected = false;
        }

        public async Task Connect()
        {
            //create client
            client = new TcpClient();

            //connect
            await client.ConnectAsync(GetLocalIP(), port);

            //initialise the network stream upon connection
            stream = client.GetStream();

            //set buffer to read into
            byte[] b = new byte[10000];

            //read the connection message sent by the server into the buffer, we wont set a timout, as we know the server will send something
            int k = stream.Read(b, 0, 10000);//k is the length of the message
            string data = Encoding.Default.GetString(b, 0, k);//decode the byte array of length k into a human readable ascii string

            //set class variable to true for the puposes of the switch statement in 'Main'
            connected = true;

        }

        public void Start()
        {
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void Sync(object sender, DoWorkEventArgs e)
        {
            try
            {
                Queue queueToPassToServer = new Queue();
                string json = "";
                Application.Current.Dispatcher.Invoke(() =>
                {
                    App currentApp = Application.Current as App;
                    vmMain vm = (vmMain)currentApp.MainWindow.DataContext;

                    //remove items from the main queue, and pass to a tempory queue to pass over to the server

                    while (vm.UpdateQueue.Count > 0)
                    {
                        var queueItem = vm.UpdateQueue.Dequeue();
                        queueToPassToServer.Enqueue(queueItem);
                    }

                    json = JsonConvert.SerializeObject(queueToPassToServer.ToArray());
                });

                //add a separator, incase more than 1 queue ends up in the buffer, so we know where one ends
                string separator = "&";


                Message(json + separator);


            }
            catch (Exception ex)
            {
                Disconnect();
            }

        }
       

        private void Message(string message)
        {
            if (client != null)
            {
                //convert string message to byte array
                byte[] messageBytes = Encoding.Default.GetBytes(message);

                //write to the listener
                stream.Write(messageBytes, 0, messageBytes.Length);


                byte[] b = new byte[10000];

                //read reply
                int k = stream.Read(b, 0, 10000); //code will hang here waiting for a reply, we need a reply from the server to continue, so we know whats going on
                string data = Encoding.Default.GetString(b, 0, k);
                KeyValuePair<string, object>[] queueArray = JsonConvert.DeserializeObject<KeyValuePair<string, object>[]>(data);
                Queue queue = new Queue();
                foreach (var queueArrayItem in queueArray)
                {
                    queue.Enqueue(queueArrayItem);
                }

                Application.Current.Dispatcher.Invoke(() =>
               {
                   UpdateModelEvent e1 = new UpdateModelEvent(queue);
                   if (UpdateModelEvent != null)
                   {
                       UpdateModelEvent(this, e1);
                   }
               });
            }
        }

        private string GetLocalIP()
        {
            //get local ip address
            List<string> IPAddresses = new List<string>();
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] ip = host.AddressList;

            for (int j = 0; j < ip.Length; j++)
            {
                if (ip[j].ToString().Contains("."))
                {
                    IPAddresses.Add(ip[j].ToString());
                }
            }

            return IPAddresses.First();
        }
    }
}
