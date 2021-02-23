using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Demo_Usercontrols.UserControls.TimeLine
{
    /// <summary>
    /// Interaction logic for TimelineScaleBar.xaml
    /// </summary>
    public partial class TimelineScaleBar : UserControl
    {
        bool movingItem = false;
        double x = 0;
        double startlssft = 0;
        TimelineScaleShuttle itemToMove;

        bool RightExpandingItem = false;
        bool LeftExpandingItem = false;
        double startRight = 0;

        TimelineScaleShuttle itemToExpand;
        public TimelineScaleBar()
        {
            InitializeComponent();

            Binding offsetBinding = new Binding("TimelineBar_ShuttleOffset");
            offsetBinding.Source = this;
            offsetBinding.Mode = BindingMode.TwoWay;
            shuttle.SetBinding(TimelineScaleShuttle.OffsetProperty, offsetBinding);

            Binding startBinding = new Binding("TimelineBar_ShuttleStart");
            startBinding.Source = this;
            startBinding.Mode = BindingMode.TwoWay;
            shuttle.SetBinding(TimelineScaleShuttle.StartProperty, startBinding);

            shuttle.End = 1230;
        }

        public static readonly DependencyProperty BarScaleProperty =
      DependencyProperty.Register("BarScale", typeof(double), typeof(TimelineScaleBar), new
       FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnBarScaleChanged)));

        public double BarScale
        {
            get { return (double)GetValue(BarScaleProperty); }
            set { SetValue(BarScaleProperty, value); }
        }
        private static void OnBarScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineScaleBar tl = d as TimelineScaleBar;
            
        }

       

        private void TimeLineScaleShuttle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TimelineScaleShuttle tlss = (TimelineScaleShuttle)sender;
            itemToMove = tlss;
            Point p = e.GetPosition(this);
            x = p.X;
            startlssft = tlss.Start;
            startRight = tlss.End;
            movingItem = true;
        }

        private void TimeLineScaleShuttle_LeftExpanderClicked(object sender, MouseButtonEventArgs e)
        {
            TimelineScaleShuttle tlss = (TimelineScaleShuttle)sender;
            itemToExpand = tlss;
            Point p = e.GetPosition(this);
            x = p.X;
            startlssft = tlss.Start;
            startRight = tlss.End;
            LeftExpandingItem = true;
        }

        private void TimeLineScaleShuttle_RightExpanderClicked(object sender, MouseButtonEventArgs e)
        {
            TimelineScaleShuttle tlss = (TimelineScaleShuttle)sender;
            itemToExpand = tlss;
            Point p = e.GetPosition(this);
            x = p.X;
            startlssft = tlss.Start;
            startRight = tlss.End;
            RightExpandingItem = true;
        }

        public void TimeLineScaleShuttle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            itemToMove = null;
            itemToExpand = null;
            movingItem = false;
            RightExpandingItem = false;
            LeftExpandingItem = false;
        }
        public void TimeLineScaleShuttle_MouseLeave(object sender, MouseEventArgs e)
        {
            itemToMove = null;
            itemToExpand = null;
            movingItem = false;
            RightExpandingItem = false;
            LeftExpandingItem = false;
        }

        public static readonly DependencyProperty TimelineBar_ShuttleStartProperty =
DependencyProperty.Register("TimelineBar_ShuttleStart", typeof(double), typeof(TimelineScaleBar), new
 FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnTimelineBar_ShuttleStartChanged)));
        public double Timeline_ShuttleStart
        {
            get { return (double)GetValue(TimelineBar_ShuttleStartProperty); }
            set { SetValue(TimelineBar_ShuttleStartProperty, value); }
        }
        private static void OnTimelineBar_ShuttleStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineScaleBar tl = d as TimelineScaleBar;
        }

        public static readonly DependencyProperty TimelineBar_ShuttleOffsetProperty =
DependencyProperty.Register("TimelineBar_ShuttleOffset", typeof(double), typeof(TimelineScaleBar), new
  FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnTimelineBar_ShuttleOffsetChanged)));
        public double TimelineBar_ShuttleOffset
        {
            get { return (double)GetValue(TimelineBar_ShuttleOffsetProperty); }
            set { SetValue(TimelineBar_ShuttleOffsetProperty, value); }
        }
        private static void OnTimelineBar_ShuttleOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineScaleShuttle tlss = d as TimelineScaleShuttle;
        }

        public void TimeLineScaleShuttle_MouseMove(object sender, MouseEventArgs e)
        {
            if (movingItem)
            {

                Point p = e.GetPosition(this);
                double changeX = p.X - x;
                double newStart = (changeX ) + startlssft;
                double newEnd = (changeX ) + startRight;
                if (newStart >= 0 && newEnd <= Bar.ActualWidth )
                {
                    double roundedNewEnd = Math.Round(newEnd * 4, MidpointRounding.ToEven) / 4;
                    double roundedNewStart = Math.Round(newStart * 4, MidpointRounding.ToEven) / 4;
                    itemToMove.Start = roundedNewStart;
                    itemToMove.End = roundedNewEnd;
                }
            }
            if (RightExpandingItem)
            {

                Point p = e.GetPosition(this);
                double changeX = p.X - x;
                double newEnd = (changeX ) + startRight;
                double roundedNewEnd = Math.Round(newEnd * 4, MidpointRounding.ToEven) / 4;
                if (roundedNewEnd <= Bar.ActualWidth  && roundedNewEnd > startlssft)
                {
                    itemToExpand.End = roundedNewEnd;
                    double scale = (itemToExpand.End - itemToExpand.Start) / Bar.ActualWidth;
                    BarScale = scale;
                    //when its 1, the actual width must be the position of the end of the timeline
                }
            }
            if (LeftExpandingItem)
            {
                Point p = e.GetPosition(this);
                double changeX = p.X - x;
                double newStart = (changeX ) + startlssft;
                double roundedNewStart = Math.Round(newStart * 4, MidpointRounding.ToEven) / 4;

                if (roundedNewStart >= 0 && roundedNewStart < startRight)
                {
                    itemToExpand.Start = roundedNewStart;
                }
            }

        }

    }


}
