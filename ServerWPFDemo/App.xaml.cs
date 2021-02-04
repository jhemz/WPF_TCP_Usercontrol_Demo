using ServerWPFDemo.Modelling;
using ServerWPFDemo.Services;
using System.Windows;

namespace ServerWPFDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public TCPServerService TCPServer;

        public ShipModel ShipModel;
    }
}
