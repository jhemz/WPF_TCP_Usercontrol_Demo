using ServerWPFDemo.Modelling;
using ServerWPFDemo.Services;
using ServerWPFDemo.ViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace ServerWPFDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new vmMain();


            StartServerAsync();

            InitalizeShipModel();
        }

        private void InitalizeShipModel()
        {
            App currentApp = Application.Current as App;
            currentApp.ShipModel = new ShipModel();
        }

        private async Task StartServerAsync()
        {
            App currentApp = Application.Current as App;
            currentApp.TCPServer = new TCPServerService();
            await currentApp.TCPServer.SetupServerAsync(100);
        }
    }
}
