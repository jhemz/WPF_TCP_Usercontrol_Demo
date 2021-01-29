using ClientWPFDemo.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace ClientWPFDemo.Services
{
    public class TCPClientService
    {
        private TcpClient client;
        private NetworkStream stream;
        private bool connected;
        private Timer timer;
        private int port = 100;

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
            // Create a timer with a 0.1 second interval.
            timer = new Timer(100);
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += SendUpdate;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void SendUpdate(object sender, ElapsedEventArgs e)
        {
            try
            {
                string json = "";
                Application.Current.Dispatcher.Invoke(() =>
                {
                    App currentApp = Application.Current as App;
                    vmMain vm = (vmMain)currentApp.MainWindow.DataContext;
                    json = JsonConvert.SerializeObject(vm.Model);
                });

                string separator = "&";


                Message(json + separator);
            }
            catch(Exception ex)
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
