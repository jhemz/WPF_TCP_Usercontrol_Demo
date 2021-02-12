using ClientWPFDemo.Managers;
using ClientWPFDemo.Services;
using ClientWPFDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ClientWPFDemo
{
  //https://codescratcher.com/wpf/custom-slider-control-in-wpf/
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {

    protected void App_Startup(object sender, StartupEventArgs e)
    {
      RegisterDependencies();
      MainWindow mw = new MainWindow(DependencyManager.Instance.Resolve<IMessageCore>());
      mw.Show();
    }

    // from Mastering Windows Presentation Foundation 2nd ed, p111
    public void RegisterDependencies()
    {
      DependencyManager.Instance.ClearRegistrations();
      DependencyManager.Instance.Register<IMessageCore, TCPClientService>();
    }

  }

}
