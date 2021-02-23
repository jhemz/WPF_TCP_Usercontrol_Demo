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

        public static readonly DependencyProperty IncrementProperty =
 DependencyProperty.Register("Increment", typeof(double), typeof(TimelineTick), new
  PropertyMetadata(1.0, new PropertyChangedCallback(OnIncrementChanged)));

        public double Increment
        {
            get { return (double)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }
        private static void OnIncrementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineTick tle = d as TimelineTick;

        }

        public static readonly DependencyProperty TickWidthProperty =
 DependencyProperty.Register("TickWidth", typeof(double), typeof(TimelineTick), new
  PropertyMetadata(50.0, new PropertyChangedCallback(OnTickWidthChanged)));

        public double TickWidth
        {
            get { return (double)GetValue(TickWidthProperty); }
            set { SetValue(TickWidthProperty, value); }
        }
        private static void OnTickWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineTick tlt = d as TimelineTick;
            double newWidth = (double)e.NewValue;
            tlt.Tick.Width = newWidth * tlt.Increment;
        }

        public static readonly DependencyProperty IsHeaderProperty =
DependencyProperty.Register("IsHeader", typeof(bool), typeof(TimelineTick), new
PropertyMetadata(true, new PropertyChangedCallback(OnIsHeaderChanged)));

        public bool IsHeader
        {
            get { return (bool)GetValue(IsHeaderProperty); }
            set { SetValue(IsHeaderProperty, value); }
        }
        private static void OnIsHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineTick tlt = d as TimelineTick;
            if((bool)e.NewValue == false)
            {
                tlt.Text.Visibility = Visibility.Hidden;
                tlt.tick1.Visibility = Visibility.Hidden;
                tlt.tick2.Visibility = Visibility.Hidden;
                tlt.tick3.Visibility = Visibility.Hidden;
                tlt.tick4.Visibility = Visibility.Hidden;
                tlt.tick5.Margin = new Thickness(0);
            }
           
        }
    }
}
