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
    public partial class TimelineTreeviewPanelProperty : UserControl
    {
        public TimelineTreeviewPanelProperty()
        {
            InitializeComponent();

            (this.Content as FrameworkElement).DataContext = this;
        }

        public static readonly DependencyProperty HeaderProperty =
      DependencyProperty.Register("Header", typeof(string), typeof(TimelineTreeviewPanelProperty), new
      PropertyMetadata("", new PropertyChangedCallback(OnHeaderChanged)));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineTreeviewPanelProperty tlpp = d as TimelineTreeviewPanelProperty;
            tlpp.lblHeader.Text = e.NewValue.ToString();
        }

        public static readonly DependencyProperty IsCheckedProperty =
    DependencyProperty.Register("IsChecked", typeof(bool), typeof(TimelineTreeviewPanelProperty), new
    PropertyMetadata(false, new PropertyChangedCallback(OnIsCheckedChanged)));

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineTreeviewPanelProperty tlpp = d as TimelineTreeviewPanelProperty;
            tlpp.chkBx.IsChecked = (bool)e.NewValue;
        }
    }
}
