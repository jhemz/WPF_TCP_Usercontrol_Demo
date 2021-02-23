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
    /// Interaction logic for TimelineSceneTimelineBar.xaml
    /// </summary>
    public partial class TimelineSceneTimelineBar : UserControl
    {
        public TimelineSceneTimelineBar()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty BarStartTimeProperty =
       DependencyProperty.Register("BarStartTime", typeof(double), typeof(TimelineSceneTimelineBar), new
       PropertyMetadata(0.0, new PropertyChangedCallback(OnBarStartTimeChanged)));

        public double BarStartTime
        {
            get { return (double)GetValue(BarStartTimeProperty); }
            set { SetValue(BarStartTimeProperty, value); }
        }
        private static void OnBarStartTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineSceneTimelineBar tle = d as TimelineSceneTimelineBar;
        }

        public static readonly DependencyProperty BarEndTimeProperty =
        DependencyProperty.Register("BarEndTime", typeof(double), typeof(TimelineSceneTimelineBar), new
         PropertyMetadata(100.0, new PropertyChangedCallback(OnBarEndTimeChanged)));

        public double BarEndTime
        {
            get { return (double)GetValue(BarEndTimeProperty); }
            set { SetValue(BarEndTimeProperty, value); }
        }
        private static void OnBarEndTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineSceneTimelineBar tle = d as TimelineSceneTimelineBar;
        }

        public static readonly DependencyProperty ColorProperty =
       DependencyProperty.Register("Color", typeof(Color), typeof(TimelineSceneTimelineBar), new
        PropertyMetadata(Colors.Green, new PropertyChangedCallback(OnColorChanged)));

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineSceneTimelineBar tle = d as TimelineSceneTimelineBar;
            tle.Bar.Background = new SolidColorBrush((Color)e.NewValue);
        }

        public static readonly DependencyProperty BarWidthProperty =
      DependencyProperty.Register("BarWidth", typeof(double), typeof(TimelineSceneTimelineBar), new
       PropertyMetadata(0.0, new PropertyChangedCallback(OnBarWidthChanged)));

        public double BarWidth
        {
            get { return (double)GetValue(BarWidthProperty); }
            set { SetValue(BarWidthProperty, value); }
        }
        private static void OnBarWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineSceneTimelineBar tle = d as TimelineSceneTimelineBar;
            tle.Bar.Width = (double)e.NewValue;
        }
    }
}
