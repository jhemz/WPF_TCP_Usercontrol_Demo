using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo_Usercontrols.UserControls.TimeLine
{
    /// <summary>
    /// Interaction logic for TimelineElementPropertyTracker.xaml
    /// </summary>
    public partial class TimelineSceneTimeline : UserControl
    {
        public TimelineSceneTimeline()
        {
            InitializeComponent();

            (this.Content as FrameworkElement).DataContext = this;
        }

        public static readonly DependencyProperty CurrentTimeProperty =
      DependencyProperty.Register("CurrentTime", typeof(double), typeof(TimelineSceneTimeline), new
      PropertyMetadata(0.0, new PropertyChangedCallback(OnCurrentTimeChanged)));

        public double CurrentTime
        {
            get { return (double)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }
        private static void OnCurrentTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineSceneTimeline tle = d as TimelineSceneTimeline;
            tle.RefreshTimelineBars(tle, (double)e.NewValue);
        }

        private void RefreshTimelineBars(TimelineSceneTimeline tle, double time)
        {
            double sceneTime = time - tle.StartTime;
            if (time >= tle.StartTime && sceneTime < tle.EndTime)
            {

                if (tle.stackSceneTimeline.Children.Count == 0)
                {
                    switch (tle.CurrentStateType)
                    {
                        case "Boolean":
                            tle.AddBlock(tle, (Convert.ToBoolean(tle.CurrentState)));
                            break;
                        case "Double":
                            break;
                    }
                }//if the stack is empty, add initial blocks
                if (time == tle.StartTime)
                {
                    tle.stackSceneTimeline.Children.Clear();
                }//iff its the begining, its been rewound, so remove all blocks
                else//only active between this elements start and end times
                {
                    if (tle.stackSceneTimeline.Children.Count > 0)
                    {
                        int lastIndex = tle.stackSceneTimeline.Children.Count - 1;

                        int index = 0;
                        double totalLengthSoFar = 0;
                        foreach (var item in tle.stackSceneTimeline.Children)
                        {

                            TimelineSceneTimelineBar block = (TimelineSceneTimelineBar)item;
                            if (index != lastIndex)//only add up the previous ones
                            {
                                double startTime = block.BarStartTime;
                                double oldWidth = block.BarWidth;
                                double timeLength = block.BarEndTime - block.BarStartTime;
                                double newWidth = timeLength * tle.ElementTimeline_TickWidth;
                                block.BarWidth = newWidth;

                                totalLengthSoFar += block.BarWidth;
                            }

                            if (index == lastIndex)
                            {
                                double endTimeOfLastBlock = totalLengthSoFar / tle.ElementTimeline_TickWidth;
                                double newWidth = (sceneTime - block.BarStartTime) * tle.ElementTimeline_TickWidth;
                                block.BarWidth = newWidth;
                            }
                            index++;
                        }

                    }
                }
            }
        }

        public static readonly DependencyProperty ElementTimeline_TickWidthProperty =
   DependencyProperty.Register("ElementTimeline_TickWidth", typeof(double), typeof(TimelineSceneTimeline), new
   PropertyMetadata(1.0, new PropertyChangedCallback(OnElementTimeline_TickWidthChanged)));

        public double ElementTimeline_TickWidth
        {
            get { return (double)GetValue(ElementTimeline_TickWidthProperty); }
            set { SetValue(ElementTimeline_TickWidthProperty, value); }
        }
        private static void OnElementTimeline_TickWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineSceneTimeline tle = d as TimelineSceneTimeline;
            tle.RefreshTimelineBars(tle, tle.CurrentTime);
        }

        public static readonly DependencyProperty StartTimeProperty =
        DependencyProperty.Register("StartTime", typeof(double), typeof(TimelineSceneTimeline), new
        PropertyMetadata(0.0, new PropertyChangedCallback(OnStartTimeChanged)));

        public double StartTime
        {
            get { return (double)GetValue(StartTimeProperty); }
            set { SetValue(StartTimeProperty, value); }
        }
        private static void OnStartTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineSceneTimeline tle = d as TimelineSceneTimeline;
        }

        public static readonly DependencyProperty EndTimeProperty =
        DependencyProperty.Register("EndTime", typeof(double), typeof(TimelineSceneTimeline), new
         PropertyMetadata(100.0, new PropertyChangedCallback(OnEndTimeChanged)));

        public double EndTime
        {
            get { return (double)GetValue(EndTimeProperty); }
            set { SetValue(EndTimeProperty, value); }
        }
        private static void OnEndTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineSceneTimeline tle = d as TimelineSceneTimeline;
        }

        public static readonly DependencyProperty CurrentStateTypeProperty =
      DependencyProperty.Register("CurrentStateType", typeof(string), typeof(TimelineSceneTimeline), new
      PropertyMetadata("Double", new PropertyChangedCallback(OnCurrentStateTypeChanged)));

        public string CurrentStateType
        {
            get { return (string)GetValue(CurrentStateTypeProperty); }
            set { SetValue(CurrentStateTypeProperty, value); }
        }
        private static void OnCurrentStateTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineSceneTimeline tl = d as TimelineSceneTimeline;
        }

        public static readonly DependencyProperty CurrentStateProperty =
      DependencyProperty.Register("CurrentState", typeof(double), typeof(TimelineSceneTimeline), new
      PropertyMetadata(0.0, new PropertyChangedCallback(OnCurrentStateChanged)));

        public double CurrentState
        {
            get { return (double)GetValue(CurrentStateProperty); }
            set { SetValue(CurrentStateProperty, value); }
        }
        private static void OnCurrentStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineSceneTimeline tl = d as TimelineSceneTimeline;

            switch (tl.CurrentStateType)
            {
                case "Boolean":
                    tl.AddBlock(tl, (Convert.ToBoolean((double)e.NewValue)));
                    break;
                case "Double":
                    break;
            }

        }

        private void AddBlock(TimelineSceneTimeline tlst, bool state)
        {
            int lastIndex = tlst.stackSceneTimeline.Children.Count - 1;
            int index = 0;
            foreach (var item in tlst.stackSceneTimeline.Children)
            {
                TimelineSceneTimelineBar block = (TimelineSceneTimelineBar)item;
                if (index == lastIndex)//we are setting the endtime for the last one
                {
                    block.BarEndTime = tlst.CurrentTime;
                }
                index++;
            }
            if (state == true)
            {
                tlst.stackSceneTimeline.Children.Add(tlst.TrueBlock(tlst.CurrentTime));
            }
            else
            {
                tlst.stackSceneTimeline.Children.Add(tlst.FalseBlock(tlst.CurrentTime));
            }
        }

        private TimelineSceneTimelineBar TrueBlock(double time)
        {

            return new TimelineSceneTimelineBar() { Color = Colors.Green, BarStartTime = time };
        }

        private TimelineSceneTimelineBar FalseBlock(double time)
        {
            return new TimelineSceneTimelineBar() { Color = Colors.Red, BarStartTime = time };
        }
    }
}
