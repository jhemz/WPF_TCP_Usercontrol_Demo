using Newtonsoft.Json;
using ServerWPFDemo.Models;
using ServerWPFDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace ServerWPFDemo.Services
{
    public class TCPServerService
    {
        private TcpListener server;

        public async Task SetupServerAsync(int port)
        {
            Console.Write("Starting server.." + Environment.NewLine);
            server = new TcpListener(IPAddress.Any, port);

            var listeningThread = new Thread(async () =>
            {
                while (true)
                {
                    server.Start();
                    Console.Write("New server listener socket opened, listening for incoming connections.." + Environment.NewLine);
                    Socket socket = await server.AcceptSocketAsync();

                    //send initial message to client, to identify etc
                    byte[] connectionMessage = Encoding.Default.GetBytes("You are now connected to the messaging server, at: " + DateTime.Now.ToString("HH:mm:ss:fff"));
                    socket.Send(connectionMessage);

                    //update ui
                    Console.Write("Connection made, reading data.." + Environment.NewLine);


                    //set timeout
                    //socket.ReceiveTimeout = 30000;

                    byte[] b = new byte[10000];

                    //initial read
                    int k = socket.Receive(b);

                    while (true)
                    {
                        string data = Encoding.Default.GetString(b, 0, k);
                        Console.Write(data + Environment.NewLine);

                        string[] models = Regex.Split(data, "&");

                        foreach(string item in models.Where(x => x.Length > 0).ToList())
                        {
                            modelMain model = JsonConvert.DeserializeObject<modelMain>(item);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                App currentApp = Application.Current as App;
                                ((vmMain)currentApp.MainWindow.DataContext).Model = model;
                            });
                        }

                        //when we have read a packet, respond
                        byte[] responseMessageBytes = Encoding.Default.GetBytes("Hey there!");
                        socket.Send(responseMessageBytes);

                        //re-read while data is available
                        k = socket.Receive(b);
                    }

                    socket.Close();
                    Console.Write("Server listener socket closed.." + Environment.NewLine);
                }
            });
            listeningThread.Start();


        }
    }
}
