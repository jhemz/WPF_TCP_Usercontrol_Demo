using ClientWPFDemo.EventHandlers;
using ClientWPFDemo.Models;
using ClientWPFDemo.Services;
using ClientWPFDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ClientWPFDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private vmMain vm;

        public MainWindow()
        {
            InitializeComponent();

            vm = new vmMain();
            DataContext = vm;

            StartClientAsync();
        }

        private async Task StartClientAsync()
        {
            App currentApp = Application.Current as App;
            currentApp.TCPClient = new TCPClientService();

            UpdateModelHandler handler = new UpdateModelHandler(HandleUpdate);
            currentApp.TCPClient.UpdateModelEvent += handler;
        }

        private void HandleUpdate(object sender, UpdateModelEvent e)
        {
            while (e.UpdateQueue.Count > 0)
            {
                KeyValuePair<string, object> queueValue = (KeyValuePair<string, object>)e.UpdateQueue.Dequeue();
                vm.GetType().GetProperty(queueValue.Key).SetValue(vm, queueValue.Value);
            }
            vm.FrameRate = e.FrameRate;
        }

        private async void BtnConnect_Checked(object sender, RoutedEventArgs e)
        {
            App currentApp = Application.Current as App;
            await currentApp.TCPClient.Connect();
            currentApp.TCPClient.Start();
        }

        private void BtnConnect_Unchecked(object sender, RoutedEventArgs e)
        {
            App currentApp = Application.Current as App;
            currentApp.TCPClient.Disconnect();
        }

        private void UCGuage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UCGuage_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
