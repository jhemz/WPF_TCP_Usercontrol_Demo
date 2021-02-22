using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo_Usercontrols.UserControls.TimeLine
{
    /// <summary>
    /// Interaction logic for TimelinePanelProperty.xaml
    /// </summary>
    public partial class TimelineTreeviewScene : UserControl
    {
        public event EventHandler RenameClicked;
        public event EventHandler DeleteClicked;

        public TimelineTreeviewScene()
        {
            InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public static readonly DependencyProperty HeaderProperty =
      DependencyProperty.Register("Header", typeof(string), typeof(TimelineTreeviewScene), new
      PropertyMetadata("", new PropertyChangedCallback(OnHeaderChanged)));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineTreeviewScene tlpp = d as TimelineTreeviewScene;
            tlpp.Main.Header = e.NewValue.ToString();
        }

        public static readonly DependencyProperty IdProperty =
     DependencyProperty.Register("Id", typeof(Guid), typeof(TimelineTreeviewScene), new
     PropertyMetadata(Guid.Empty, new PropertyChangedCallback(OnIdChanged)));

        public Guid Id
        {
            get { return (Guid)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        private static void OnIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineTreeviewScene tlpp = d as TimelineTreeviewScene;
           
        }

        private void btnRename_Click(object sender, RoutedEventArgs e)
        {
            EventHandler handler = RenameClicked;
            handler?.Invoke(this, e);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            EventHandler handler = DeleteClicked;
            handler?.Invoke(this, e);
        }
    }
}
