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
            double time = (double)e.NewValue;
            double sceneTime = time - tle.StartTime;
            if (time >= tle.StartTime && sceneTime < tle.EndTime)
            {
                
                if (tle.stackSceneTimeline.Children.Count == 0)
                {
                    switch (tle.CurrentStateType)
                    {
                        case "Boolean":
                            if (Convert.ToBoolean(tle.CurrentState) == true)
                            {
                                tle.stackSceneTimeline.Children.Add(tle.TrueBlock());
                            }
                            else
                            {
                                tle.stackSceneTimeline.Children.Add(tle.FalseBlock());
                            }
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
                            Border block = (Border)item;
                            if (index != lastIndex)//only add up the previous ones
                            {
                                totalLengthSoFar += block.Width;
                            }

                            if (index == lastIndex)
                            {
                                //length = time * 50
                                double endTimeOfLastBlock = totalLengthSoFar / 50;
                                double newRight = (sceneTime * 50) - (endTimeOfLastBlock * 50);
                                block.Width = newRight;
                            }
                            index++;
                        }

                    }
                }
            }

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
        DependencyProperty.Register("EndTime", typeof(double), typeof(TimeLineElement), new
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
                    if (Convert.ToBoolean((double)e.NewValue) == true)
                    {
                        tl.stackSceneTimeline.Children.Add(tl.TrueBlock());
                    }
                    else
                    {
                        tl.stackSceneTimeline.Children.Add(tl.FalseBlock());
                    }
                    break;
                case "Double":
                    break;
            }
           
        }

        private Border TrueBlock()
        {
            return new Border() { Width = 0, Height = 10, Background = new SolidColorBrush(Colors.LightGreen) };
        }

        private Border FalseBlock()
        {
            return new Border() { Width = 0, Height = 2, Background = new SolidColorBrush(Colors.Red) };
        }
    }
}
