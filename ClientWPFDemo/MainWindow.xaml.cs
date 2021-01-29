using ClientWPFDemo.Services;
using ClientWPFDemo.ViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace ClientWPFDemo
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

            StartClientAsync();
        }

        private async Task StartClientAsync()
        {
            App currentApp = Application.Current as App;
            currentApp.TCPClient = new TCPClientService();
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
    }
}
