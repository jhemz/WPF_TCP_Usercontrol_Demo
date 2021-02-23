using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Demo_Usercontrols.UserControls.TimeLine
{
    /// <summary>
    /// Interaction logic for TimeLine.xaml
    /// </summary>
    public partial class TimeLine : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool play = false;

        private int startTime = 0;

        private DateTime? startDateTime = null;
        private DateTime? pauseDateTime = null;
        private DateTime? playDateTime = null;
        private DateTime? currentDateTime = null;
        private double pausedInterval = 0;
        private double offset = 0;

        bool movingItem = false;
        double x = 0;
        double startLeft = 0;
        TimeLineElement itemToMove;

        bool RightExpandingItem = false;
        bool LeftExpandingItem = false;
        double startRight = 0;
        TimeLineElement itemToExpand;

        public event EventHandler AddNewScene;
        public event SceneUpdateHandler SceneStarted;
        public event SceneUpdateHandler SceneEnded;


        private DispatcherTimer timer;

        public TimeLine()
        {
            InitializeComponent();

            scrollViewer.ScrollToTop();

            timer = new DispatcherTimer();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);

            Binding offsetBinding = new Binding("Timeline_ShuttleOffset");
            offsetBinding.Source = this;
            timelineScaleBar.SetBinding(TimelineScaleBar.TimelineBar_ShuttleOffsetProperty, offsetBinding);


            Binding startBinding = new Binding("Timeline_ShuttleStart");
            startBinding.Source = this;
            timelineScaleBar.SetBinding(TimelineScaleBar.TimelineBar_ShuttleStartProperty, startBinding);
        }


        private double currentTime = 0;
        public double CurrentTime
        {
            get
            {
                return currentTime;
            }
            set
            {
                currentTime = value;
                OnPropertyChanged();
            }

        }

        public static readonly DependencyProperty ScaleProperty =
      DependencyProperty.Register("Scale", typeof(double), typeof(TimeLine), new
      PropertyMetadata(0.0, new PropertyChangedCallback(OnScaleChanged)));

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }
        private static void OnScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeLine tl = d as TimeLine;
            double scale = (double)e.NewValue;

            double width = tl.ActualWidth - 300;
            if (width <= 0)
            {
                UIElement parent = (UIElement)tl.Parent;
                parent.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                Size s = parent.DesiredSize;
                width = s.Width - 300;// s.Width;
            }

            tl.Timeline_TickWidth = (width) / scale / tl.Duration;
        }

        public static readonly DependencyProperty Timeline_ShuttleOffsetProperty =
DependencyProperty.Register("Timeline_ShuttleOffset", typeof(double), typeof(TimeLine), new
  PropertyMetadata(0.0, new PropertyChangedCallback(OnTimeline_ShuttleOffsetChanged)));
        public double Timeline_ShuttleOffset
        {
            get { return (double)GetValue(Timeline_ShuttleOffsetProperty); }
            set { SetValue(Timeline_ShuttleOffsetProperty, value); }
        }
        private static void OnTimeline_ShuttleOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeLine tl = d as TimeLine;
            double offset = (double)e.NewValue;
            double start = tl.Timeline_ShuttleStart;
            double halfShuttleWidth = offset - start;
            double end = start + (2 * halfShuttleWidth);
            double barWidth = tl.ActualWidth - 300;
            double maxEnd = barWidth - halfShuttleWidth;
            double minStart = halfShuttleWidth;
            double availableBarlength = maxEnd - minStart;

            if (availableBarlength > 0)
            {
                double scrollOffset = (offset - halfShuttleWidth) / availableBarlength;// offset / availableBarlength;

                
                tl.scrollViewHeader.ScrollToHorizontalOffset(scrollOffset * tl.scrollViewHeader.ScrollableWidth);
                tl.scrollView.ScrollToHorizontalOffset(scrollOffset * tl.scrollViewHeader.ScrollableWidth);
            }

        }

        public static readonly DependencyProperty Timeline_ShuttleStartProperty =
DependencyProperty.Register("Timeline_ShuttleStart", typeof(double), typeof(TimeLine), new
 PropertyMetadata(0.0, new PropertyChangedCallback(OnTimeline_ShuttleStartChanged)));
        public double Timeline_ShuttleStart
        {
            get { return (double)GetValue(Timeline_ShuttleStartProperty); }
            set { SetValue(Timeline_ShuttleStartProperty, value); }
        }
        private static void OnTimeline_ShuttleStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeLine tl = d as TimeLine;
        }

        public static readonly DependencyProperty Timeline_IncrementProperty =
DependencyProperty.Register("Timeline_Increment", typeof(double), typeof(TimeLine), new
 PropertyMetadata(1.0, new PropertyChangedCallback(OnTimeline_IncrementChanged)));

        public double Timeline_Increment
        {
            get { return (double)GetValue(Timeline_IncrementProperty); }
            set { SetValue(Timeline_IncrementProperty, value); }
        }
        private static void OnTimeline_IncrementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeLine tl = d as TimeLine;
            double newIncrement = (double)e.NewValue;
            tl.timerTicksHeader.Children.Clear();
            tl.timerTicks.Children.Clear();
            for (int i = 0; i < tl.Duration; i++)
            {
                if ((i + 1) % newIncrement == 0)//if the label is not divisible by 5, the hide the label to stop crowding
                {
                    TimelineTick tickHeader = new TimelineTick() { Increment = newIncrement, TimeLabel = (i + 1).ToString(), TickWidth = tl.Timeline_TickWidth };
                    Binding tickHeaderBinding = new Binding("Timeline_TickWidth");
                    tickHeaderBinding.Source = tl;
                    tickHeader.SetBinding(TimelineTick.TickWidthProperty, tickHeaderBinding);
                    tl.timerTicksHeader.Children.Add(tickHeader);

                    //TimelineTick tick = new TimelineTick() { IsHeader = false, Increment = newIncrement, TimeLabel = (i + 1).ToString(), TickWidth = tl.Timeline_TickWidth };
                    //Binding tickBinding = new Binding("Timeline_TickWidth");
                    //tickBinding.Source = tl;
                    //tick.SetBinding(TimelineTick.TickWidthProperty, tickBinding);
                    //tl.timerTicks.Children.Add(tick);
                }

            }
            tl.timerTicks.Children.Add(new Grid() { Width = tl.timerTicksHeader.Children.Count * newIncrement * tl.Timeline_TickWidth });
        }

        public static readonly DependencyProperty Timeline_TickWidthProperty =
 DependencyProperty.Register("Timeline_Timeline_TickWidth", typeof(double), typeof(TimeLine), new
  PropertyMetadata(50.0, new PropertyChangedCallback(OnTimeline_TickWidthChanged)));

        public double Timeline_TickWidth
        {
            get { return (double)GetValue(Timeline_TickWidthProperty); }
            set { SetValue(Timeline_TickWidthProperty, value); }
        }
        private static void OnTimeline_TickWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeLine tle = d as TimeLine;
            double newWidth = (double)e.NewValue;

            tle.tickWidth.Content = newWidth.ToString() + ", count: " + tle.timerTicksHeader.Children.Count.ToString();
            if (newWidth < 4)//if we go to this scale, clear the stack and add larger ticks
            {
                tle.Timeline_Increment = 30;
            }
            else if (newWidth < 10)//if we go to this scale, clear the stack and add larger ticks
            {
                tle.Timeline_Increment = 10;
            }
            else if (newWidth < 35)//if we go to this scale, clear the stack and add larger ticks
            {
                tle.Timeline_Increment = 5;
            }
            else if(newWidth >= 35)
            {
                tle.Timeline_Increment = 1;
            }

            //foreach (TimelineTick tick in tle.timerTicks.Children)
            //{
            //    tick.TickWidth = (double)e.NewValue;
            //}
            foreach (TimelineTick tick in tle.timerTicksHeader.Children)
            {
                tick.TickWidth = newWidth;
            }
            foreach (TimeLineElement element in tle.timelineItems.Children)
            {
                element.Element_TickWidth = (double)e.NewValue;
            }
            tle.timeBar.Margin = new Thickness(tle.CurrentTime * (double)e.NewValue, 0, 0, 0);
        }

        public static readonly DependencyProperty DurationProperty =
        DependencyProperty.Register("Duration", typeof(int), typeof(TimeLine), new
        PropertyMetadata(5, new PropertyChangedCallback(OnDurationChanged)));

        public int Duration
        {
            get { return (int)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }
        private static void OnDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeLine tl = d as TimeLine;
            //tl.timerTicks.Children.Clear();
            tl.timerTicksHeader.Children.Clear();

            Binding scaleBinding = new Binding("Scale");
            scaleBinding.Source = tl;
            scaleBinding.Mode = BindingMode.TwoWay;
            tl.timelineScaleBar.SetBinding(TimelineScaleBar.BarScaleProperty, scaleBinding);

            for (int i = 0; i < (int)e.NewValue; i++)
            {
                //TimelineTick tick = new TimelineTick() { TimeLabel = (i + 1).ToString(), IsHeader = false };
                //Binding tickBinding = new Binding("Timeline_TickWidth");
                //tickBinding.Source = tl;
                //tick.SetBinding(TimelineTick.TickWidthProperty, tickBinding);
                //tl.timerTicks.Children.Add(tick);


                TimelineTick tickHeader = new TimelineTick() { TimeLabel = (i + 1).ToString() };
                Binding tickHeaderBinding = new Binding("Timeline_TickWidth");
                tickHeaderBinding.Source = tl;
                tickHeader.SetBinding(TimelineTick.TickWidthProperty, tickHeaderBinding);
                tl.timerTicksHeader.Children.Add(tickHeader);
            }

        }

        public static readonly DependencyProperty ScenesProperty =
       DependencyProperty.Register("Scenes", typeof(ObservableCollection<vmTimelineScene>), typeof(TimeLine), new
       PropertyMetadata(new ObservableCollection<vmTimelineScene>(), new PropertyChangedCallback(OnScenesChanged)));

        public ObservableCollection<vmTimelineScene> Scenes
        {
            get { return (ObservableCollection<vmTimelineScene>)GetValue(ScenesProperty); }
            set { SetValue(ScenesProperty, value); }
        }
        private static void OnScenesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeLine tl = d as TimeLine;
            tl.timelineItems.Children.Clear();
            tl.SceneView.Items.Clear();

            ObservableCollection<vmTimelineScene> list = (ObservableCollection<vmTimelineScene>)e.NewValue;
            list.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs ev) { Scenes_CollectionChanged(sender, ev, tl); };
        }

        private static void Scenes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e, TimeLine tl)
        {
            ObservableCollection<vmTimelineScene> list = (ObservableCollection<vmTimelineScene>)sender;

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (vmTimelineScene vm in e.NewItems)
                {
                    //add the scene to the treeview with all its descendants

                    TimelineTreeviewScene treeViewItem = new TimelineTreeviewScene() { Id = vm.Id };

                    Binding Binding = new Binding("Header");
                    Binding.Source = vm;
                    Binding.Mode = BindingMode.TwoWay;
                    treeViewItem.SetBinding(TimelineTreeviewScene.HeaderProperty, Binding);

                    treeViewItem.RenameClicked += delegate (object s, EventArgs ev) { tl.TreeViewItem_Rename(s, ev, vm); };
                    treeViewItem.DeleteClicked += delegate (object s, EventArgs ev) { tl.TreeViewItem_Delete(s, ev, vm); };
                    foreach (vmTimelinePanel panel in vm.Panels)
                    {

                        TreeViewItem treeViewPanelItem = new TreeViewItem()
                        {
                            Header = panel.Header,
                            FontSize = 15,
                            Foreground = new SolidColorBrush(Colors.White)
                        };

                        Type panelType = typeof(vmTimelinePanel);


                        //proeprties
                        foreach (var prop in panel.GetType().GetProperties())
                        {
                            string propName = prop.Name;
                            var propValue = prop.GetValue(panel, null);

                            PropertyAttribute MyAttribute = (PropertyAttribute)Attribute.GetCustomAttribute(prop, typeof(PropertyAttribute));

                            string propType = prop.PropertyType.Name;
                            if (MyAttribute.PropertyType == PropertyType.Display)
                            {
                                //binding the ischecked property to the proeprty_defaultValue in the vm
                                TimelineTreeviewPanelProperty tltpp = new TimelineTreeviewPanelProperty() { Header = MyAttribute.DisplayName };
                                Binding tltppBinding = new Binding(propName);
                                tltppBinding.Source = panel;
                                tltppBinding.Mode = BindingMode.TwoWay;
                                switch (propType)
                                {
                                    case "Boolean":
                                        tltpp.txtBox.Visibility = Visibility.Collapsed;
                                        tltpp.chkBx.SetBinding(CheckBox.IsCheckedProperty, tltppBinding);
                                        break;
                                    case "Double":
                                        tltpp.chkBx.Visibility = Visibility.Collapsed;
                                        tltpp.txtBox.SetBinding(TextBox.TextProperty, tltppBinding);
                                        break;
                                }
                                treeViewPanelItem.Items.Add(tltpp);
                            }
                        }

                        treeViewItem.Main.Items.Add(treeViewPanelItem);
                    }

                    tl.SceneView.Items.Add(treeViewItem);

                    //add the timeline element
                    TimeLineElement tle = new TimeLineElement() { Id = vm.Id };
                    tle.LeftExpanderClicked += tl.TimeLineElement_LeftExpanderClicked;
                    tle.RightExpanderClicked += tl.TimeLineElement_RightExpanderClicked;
                    tle.MoverClicked += tl.TimeLineElement_MouseDown;
                    tle.ItemColour = vm.Color;

                    Binding scaleBinding = new Binding("Timeline_TickWidth");
                    scaleBinding.Source = tl;
                    tle.SetBinding(TimeLineElement.Element_TickWidthProperty, scaleBinding);

                    Binding myBinding = new Binding("ActualHeight");
                    myBinding.Source = treeViewItem;
                    tle.SetBinding(TimeLineElement.HeightProperty, myBinding);

                    Binding myBinding2 = new Binding("ActualHeight");
                    myBinding2.Source = treeViewItem;
                    tle.SetBinding(TimeLineElement.ItemHeightProperty, myBinding2);

                    Binding myBinding3 = new Binding("Header");
                    myBinding3.Source = treeViewItem.Main;
                    tle.SetBinding(TimeLineElement.TextProperty, myBinding3);

                    Binding myBinding4 = new Binding("Start");
                    myBinding4.Source = vm;
                    myBinding4.Mode = BindingMode.TwoWay;
                    tle.SetBinding(TimeLineElement.StartProperty, myBinding4);

                    Binding myBinding5 = new Binding("End");
                    myBinding5.Source = vm;
                    myBinding5.Mode = BindingMode.TwoWay;
                    tle.SetBinding(TimeLineElement.EndProperty, myBinding5);

                    //we want to add a flag to indicate the change in user accesible property here
                    foreach (vmTimelinePanel panel in vm.Panels)
                    {
                        //proeprties
                        foreach (var prop in panel.GetType().GetProperties())
                        {
                            string propName = prop.Name;
                            var propValue = prop.GetValue(panel, null);
                            string propType = prop.PropertyType.Name;
                            PropertyAttribute MyAttribute = (PropertyAttribute)Attribute.GetCustomAttribute(prop, typeof(PropertyAttribute));

                            if (MyAttribute.PropertyType == PropertyType.Display)
                            {
                                //binding the ischecked property to the proeprty_defaultValue in the vm
                                TimelineSceneTimeline tlstl = new TimelineSceneTimeline() { CurrentStateType = propType };

                                Binding tlstStartBinding = new Binding("Start");
                                tlstStartBinding.Source = tle;
                                tlstl.SetBinding(TimelineSceneTimeline.StartTimeProperty, tlstStartBinding);

                                Binding tlstEndBinding = new Binding("End");
                                tlstEndBinding.Source = tle;
                                tlstl.SetBinding(TimelineSceneTimeline.EndTimeProperty, tlstEndBinding);

                                Binding tlstlStateBinding = new Binding(MyAttribute.DisplayName);
                                tlstlStateBinding.Source = panel;
                                tlstl.SetBinding(TimelineSceneTimeline.CurrentStateProperty, tlstlStateBinding);

                                Binding tlstTimeBinding = new Binding("CurrentTime");
                                tlstTimeBinding.Source = tl;
                                tlstl.SetBinding(TimelineSceneTimeline.CurrentTimeProperty, tlstTimeBinding);

                                tle.stackEvents.Children.Add(tlstl);
                            }
                        }
                    }
                    tl.timelineItems.Children.Add(tle);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {

                List<TimelineTreeviewScene> treeViewItemsToRemove = new List<TimelineTreeviewScene>();
                List<TimeLineElement> timelineItemsToRemove = new List<TimeLineElement>();
                foreach (vmTimelineScene oldItem in e.OldItems)
                {
                    foreach (TimelineTreeviewScene item in tl.SceneView.Items)
                    {
                        if (item.Id == oldItem.Id)
                        {
                            treeViewItemsToRemove.Add(item);
                        }
                    }
                    foreach (TimeLineElement item in tl.timelineItems.Children)
                    {
                        if (item.Id == oldItem.Id)
                        {
                            timelineItemsToRemove.Add(item);
                        }
                    }
                }
                //remove the treeview item
                foreach (TimelineTreeviewScene item in treeViewItemsToRemove)
                {
                    tl.SceneView.Items.Remove(item);
                }
                //remove the timeline item
                foreach (TimeLineElement item in timelineItemsToRemove)
                {
                    tl.timelineItems.Children.Remove(item);
                }
            }
        }

        private void TreeViewItem_Delete(object sender, EventArgs e, vmTimelineScene vm)
        {
            Scenes.Remove(vm);
        }

        private void TreeViewItem_Rename(object sender, EventArgs e, vmTimelineScene vm)
        {
            TimelineTreeviewScene tvi = (TimelineTreeviewScene)sender;

            string name = tvi.Header.ToString();

            var textBox = new TextBox()
            {
                Text = name,
            };
            tvi.Main.Header = textBox;

            textBox.LostFocus += (o, e2) =>
            {
                vm.Header = textBox.Text;
                name = textBox.Text;
            };
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (startDateTime == null)//its not been started yet
            {
                startDateTime = DateTime.Now;
            }

            if (CurrentTime <= Duration)
            {
                currentDateTime = DateTime.Now;
                double difference = ((DateTime)currentDateTime - (DateTime)startDateTime).TotalMilliseconds;


                if (playDateTime > pauseDateTime)//there is a pause interval
                {
                    pausedInterval += ((DateTime)playDateTime - (DateTime)pauseDateTime).TotalMilliseconds;
                    pauseDateTime = playDateTime = currentDateTime;
                }

                difference -= pausedInterval;
                difference += offset * 1000;
                double newLeft = (difference / 1000) * Timeline_TickWidth;
                if (newLeft > Duration * Timeline_TickWidth)
                {
                    newLeft = Duration * Timeline_TickWidth;
                }
                timeBar.Margin = new System.Windows.Thickness(newLeft, 0, 0, 0);

                CurrentTime = difference / 1000;
                if (CurrentTime > Duration)
                {
                    CurrentTime = Duration;
                }
                TimeSpan t = TimeSpan.FromMilliseconds(CurrentTime * 1000);
                Time.Content = new DateTime(t.Ticks).ToString("mm:ss:ffff");

                //check for start and end events to raise
                double currentTimeSeconds = Math.Round(CurrentTime * 4, MidpointRounding.ToEven) / 4;
                //check for starts
                List<vmTimelineScene> scenesStarting = Scenes.Where(x => x.Start == currentTimeSeconds).ToList();
                if (scenesStarting.Any())
                {
                    foreach (vmTimelineScene scene in scenesStarting)
                    {
                        SceneUpdateHandler handler = SceneStarted;
                        handler?.Invoke(this, new SceneUpdateEvent(SceneEvent.Started, scene));
                    }
                }
                //check for ends
                List<vmTimelineScene> scenesEnding = Scenes.Where(x => x.End == currentTimeSeconds).ToList();
                if (scenesEnding.Any())
                {
                    foreach (vmTimelineScene scene in scenesEnding)
                    {
                        SceneUpdateHandler handler = SceneEnded;
                        handler?.Invoke(this, new SceneUpdateEvent(SceneEvent.Ended, scene));
                    }
                }

            }
            else
            {
                timer.Stop();
            }
        }

        private void btnPlayPause_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            play = !play;
            if (play)
            {
                timer.Start();
                playDateTime = DateTime.Now;
                if (pauseDateTime == null)
                {
                    pauseDateTime = playDateTime;
                }
            }
            else
            {
                timer.Stop();
                pauseDateTime = DateTime.Now;
            }
        }

        private void btnSkipBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //if there are no scenes, skip to the end
            if (!Scenes.Any())
            {
                SkipToTime(startTime);
            }
            else //we skip to the start of the next scene
            {
                List<vmTimelineScene> scenesBehindFromThisPoint = Scenes.Where(x => x.Start < CurrentTime).ToList();
                if (scenesBehindFromThisPoint.Any())
                {
                    SkipToTime(scenesBehindFromThisPoint.LastOrDefault().Start);
                }
                else
                {
                    SkipToTime(startTime);
                }
            }
            startDateTime = null;
            pausedInterval = 0;
            playDateTime = startDateTime;
            pauseDateTime = startDateTime;
        }

        private void btnSkipNext_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //if there are no scenes, skip to the end
            if (!Scenes.Any())
            {
                SkipToTime(Duration);
            }
            else //we skip to the start of the next scene
            {
                List<vmTimelineScene> scenesForwardFromThisPoint = Scenes.Where(x => x.Start > CurrentTime).ToList();
                if (scenesForwardFromThisPoint.Any())
                {
                    SkipToTime(scenesForwardFromThisPoint.FirstOrDefault().Start);
                }
                else
                {
                    SkipToTime(Duration);
                }
            }


        }

        private void SkipToTime(double time)
        {
            double newLeft = time * Timeline_TickWidth;
            timeBar.Margin = new System.Windows.Thickness(newLeft, 0, 0, 0);
            startDateTime = DateTime.Now;
            offset = time;
            CurrentTime = time;
            pauseDateTime = DateTime.Now;
            playDateTime = DateTime.Now;
            TimeSpan t = TimeSpan.FromMilliseconds(CurrentTime * 1000);
            Time.Content = new DateTime(t.Ticks).ToString("mm:ss:ffff");
        }

        private void TimeLineElement_MouseMove(object sender, MouseEventArgs e)
        {
            timelineScaleBar.TimeLineScaleShuttle_MouseMove(sender, e);

            

            if (movingItem)
            {
                Point p = e.GetPosition(Main);
                double changeX = p.X - x;
                double newStart = (changeX / Timeline_TickWidth) + startLeft;
                double newEnd = (changeX / Timeline_TickWidth) + startRight;
                if (newStart >= startTime * Timeline_TickWidth && newEnd <= Duration * Timeline_TickWidth)
                {
                    double roundedNewEnd = Math.Round(newEnd * 4, MidpointRounding.ToEven) / 4;
                    double roundedNewStart = Math.Round(newStart * 4, MidpointRounding.ToEven) / 4;
                    itemToMove.Start = roundedNewStart;
                    itemToMove.End = roundedNewEnd;
                }
            }
            if (RightExpandingItem)
            {
                Point p = e.GetPosition(Main);
                double changeX = p.X - x;
                double newEnd = (changeX / Timeline_TickWidth) + startRight;
                double roundedNewEnd = Math.Round(newEnd * 4, MidpointRounding.ToEven) / 4;
                if (roundedNewEnd <= Duration * Timeline_TickWidth && roundedNewEnd > startLeft)
                {
                    itemToExpand.End = roundedNewEnd;
                }
            }
            if (LeftExpandingItem)
            {
                Point p = e.GetPosition(Main);
                double changeX = p.X - x;
                double newStart = (changeX / Timeline_TickWidth) + startLeft;
                double roundedNewStart = Math.Round(newStart * 4, MidpointRounding.ToEven) / 4;

                if (roundedNewStart >= startTime * Timeline_TickWidth && roundedNewStart < startRight)
                {
                    itemToExpand.Start = roundedNewStart;
                }
            }

        }

        private void InitializeCursorMonitoring()
        {
            var point = Mouse.GetPosition(Application.Current.MainWindow);
            var timer = new System.Windows.Threading.DispatcherTimer();

            timer.Tick += delegate
            {
                Application.Current.MainWindow.CaptureMouse();
                if (point != Mouse.GetPosition(Application.Current.MainWindow))
                {
                    point = Mouse.GetPosition(Application.Current.MainWindow);
                    Console.WriteLine(String.Format("X:{0}  Y:{1}", point.X, point.Y));
                }
                Application.Current.MainWindow.ReleaseMouseCapture();
            };

            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
        }

        private void TimeLineElement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            timelineScaleBar.TimeLineScaleShuttle_MouseUp(sender, e);
            itemToMove = null;
            itemToExpand = null;
            movingItem = false;
            RightExpandingItem = false;
            LeftExpandingItem = false;
        }

        private void TimeLineElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TimeLineElement tle = (TimeLineElement)sender;
            itemToMove = tle;
            Point p = e.GetPosition(Main);
            x = p.X;
            startLeft = tle.Start;
            startRight = tle.End;
            movingItem = true;
        }

        private void TimeLineElement_LeftExpanderClicked(object sender, MouseButtonEventArgs e)
        {
            TimeLineElement tle = (TimeLineElement)sender;
            itemToExpand = tle;
            Point p = e.GetPosition(Main);
            x = p.X;
            startLeft = tle.Start;
            startRight = tle.End;
            LeftExpandingItem = true;
        }

        private void TimeLineElement_RightExpanderClicked(object sender, MouseButtonEventArgs e)
        {
            TimeLineElement tle = (TimeLineElement)sender;
            itemToExpand = tle;
            Point p = e.GetPosition(Main);
            x = p.X;
            startLeft = tle.Start;
            startRight = tle.End;
            RightExpandingItem = true;
        }

        private void btnAddScene_Click(object sender, RoutedEventArgs e)
        {
            EventHandler handler = AddNewScene;
            handler?.Invoke(this, e);
        }

        private void Main_MouseLeave(object sender, MouseEventArgs e)
        {
            //timelineScaleBar.TimeLineScaleShuttle_MouseLeave(sender, e);
            //itemToMove = null;
            //itemToExpand = null;
            //movingItem = false;
            //RightExpandingItem = false;
            //LeftExpandingItem = false;
        }


    }
}
