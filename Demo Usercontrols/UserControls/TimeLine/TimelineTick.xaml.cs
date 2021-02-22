using System.Windows;
using System.Windows.Controls;

namespace Demo_Usercontrols.UserControls.TimeLine
{
    /// <summary>
    /// Interaction logic for TimelineTick.xaml
    /// </summary>
    public partial class TimelineTick : UserControl
    {
        public TimelineTick()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TimeLabelProperty =
 DependencyProperty.Register("TimeLabel", typeof(string), typeof(TimelineTick), new
  PropertyMetadata("", new PropertyChangedCallback(OnTimeLabelChanged)));

        public string TimeLabel
        {
            get { return (string)GetValue(TimeLabelProperty); }
            set { SetValue(TimeLabelProperty, value); }
        }
        private static void OnTimeLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineTick tle = d as TimelineTick;
            tle.Text.Text = e.NewValue.ToString();
        }
    }
}
