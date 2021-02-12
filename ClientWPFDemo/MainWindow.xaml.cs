using ClientWPFDemo.EventHandlers;
using ClientWPFDemo.Managers;
using ClientWPFDemo.Models;
using ClientWPFDemo.Services;
using ClientWPFDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace ClientWPFDemo
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private vmMain vm;

    private int frameRate = 0;
    private Stopwatch stopWatch;
    long elapsedMilliseconds;
    private Timer timer;
    //private DispatcherTimer timer;

    private IMessageCore TCPClient;
    public MainWindow(IMessageCore _TCPClient)
    {
      TCPClient = _TCPClient;
      InitializeComponent();

      vm = new vmMain(TCPClient);
      DataContext = vm;

      StartClientAsync();

      stopWatch = new Stopwatch();
      stopWatch.Start();
      timer = new Timer();

    }

    private void Tick(object sender, ElapsedEventArgs e)
    {
      long currentEllapsed = stopWatch.ElapsedMilliseconds;
      long differnece = currentEllapsed - elapsedMilliseconds;
      frameRate = 1000 / (int)differnece;
      elapsedMilliseconds = currentEllapsed;
      vm.FrameRate = (int)differnece;
    }

    private async Task StartClientAsync()
    {
      UpdateModelHandler handler = new UpdateModelHandler(HandleUpdate);
      TCPClient.UpdateModelEvent += handler;
    }

    private void HandleUpdate(object sender, UpdateModelEvent e)
    {
      long currentEllapsed = stopWatch.ElapsedMilliseconds;
      long differnece = currentEllapsed - elapsedMilliseconds;
      frameRate = 1000 / (int)differnece;

      elapsedMilliseconds = currentEllapsed;

      while (e.UpdateQueue.Count > 0)
      {
        KeyValuePair<string, object> queueValue = (KeyValuePair<string, object>)e.UpdateQueue.Dequeue();
        vm.GetType().GetProperty(queueValue.Key).SetValue(vm, queueValue.Value);
      }
      vm.FrameRate = frameRate;
    }

    private async void BtnConnect_Checked(object sender, RoutedEventArgs e)
    {
      await TCPClient.Connect();
      TCPClient.Start();
    }

    private void BtnConnect_Unchecked(object sender, RoutedEventArgs e)
    {
      App currentApp = Application.Current as App;
      TCPClient.Disconnect();
    }


  }
}
