﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Demo_Usercontrols.UserControls.TimeLine
{
    /// <summary>
    /// Interaction logic for TimeLineElement.xaml
    /// </summary>
    public partial class TimeLineElement : UserControl
    {
        public event MouseButtonEventHandler RightExpanderClicked;
        public event MouseButtonEventHandler LeftExpanderClicked;
        public event MouseButtonEventHandler MoverClicked;

        public TimeLineElement()
        {
            InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public static readonly DependencyProperty Element_TickWidthProperty =
     DependencyProperty.Register("Element_TickWidth", typeof(double), typeof(TimeLineElement), new
     PropertyMetadata(1.0, new PropertyChangedCallback(OnElement_TickWidthChanged)));

        public double Element_TickWidth
        {
            get { return (double)GetValue(Element_TickWidthProperty); }
            set { SetValue(Element_TickWidthProperty, value); }
        }
        private static void OnElement_TickWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeLineElement tle = d as TimeLineElement;
            tle.RefreshScene(tle, tle.Start, tle.End, (double)e.NewValue);
            foreach(TimelineSceneTimeline tlst in tle.stackEvents.Children)
            {
                tlst.ElementTimeline_TickWidth = (double)e.NewValue;
            }
        }

        public static readonly DependencyProperty ItemHeightProperty =
    DependencyProperty.Register("ItemHeight", typeof(double), typeof(TimeLineElement), new
     PropertyMetadata(30.0, new PropertyChangedCallback(OnItemHeightChanged)));

        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }
        private static void OnItemHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeLineElement tle = d as TimeLineElement;
           
        }


        public static readonly DependencyProperty StartProperty =
 DependencyProperty.Register("Start", typeof(double), typeof(TimeLineElement), new
  PropertyMetadata(0.0, new PropertyChangedCallback(OnStartChanged)));

        public double Start
        {
            get { return (double)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }
        private static void OnStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeLineElement tle = d as TimeLineElement;
            tle.RefreshScene(tle, (double)e.NewValue, tle.End, tle.Element_TickWidth);

        }

        public static readonly DependencyProperty EndProperty =
DependencyProperty.Register("End", typeof(double), typeof(TimeLineElement), new
 PropertyMetadata(100.0, new PropertyChangedCallback(OnEndChanged)));

        public double End
        {
            get { return (double)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }
        private static void OnEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeLineElement tle = d as TimeLineElement;
            tle.RefreshScene(tle, tle.Start, (double)e.NewValue, tle.Element_TickWidth);
        }

        private void RefreshScene(TimeLineElement tle, double start, double end, double tickWidth)
        {
            double left = start * tle.Element_TickWidth;
            tle.Item.Margin = new Thickness(left, 0, 0, 0);

            double width = (tle.End * tle.Element_TickWidth) - (tle.Start * tle.Element_TickWidth);
            tle.Item.Width = width;
        }

        public static readonly DependencyProperty ItemColourProperty =
   DependencyProperty.Register("ItemColour", typeof(Color), typeof(TimeLineElement), new
    PropertyMetadata(Colors.Transparent, new PropertyChangedCallback(OnItemColourChanged)));

        public Color ItemColour
        {
            get { return (Color)GetValue(ItemColourProperty); }
            set { SetValue(ItemColourProperty, value); }
        }
        private static void OnItemColourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeLineElement tle = d as TimeLineElement;
            //tle.Item.Background = new SolidColorBrush((Color)e.NewValue);
        }

        public static readonly DependencyProperty TextProperty =
     DependencyProperty.Register("Text", typeof(string), typeof(TimeLineElement), new
      PropertyMetadata("", new PropertyChangedCallback(OnTextChanged)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeLineElement tle = d as TimeLineElement;
            tle.Title.Text = e.NewValue.ToString();
        }

        public static readonly DependencyProperty IdProperty =
   DependencyProperty.Register("Id", typeof(Guid), typeof(TimeLineElement), new
   PropertyMetadata(Guid.Empty, new PropertyChangedCallback(OnIdChanged)));

        public Guid Id
        {
            get { return (Guid)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        private static void OnIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }


        private void Title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MouseButtonEventHandler handler = MoverClicked;
            handler?.Invoke(this, e);
        }

        private void LeftExpander_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MouseButtonEventHandler handler = LeftExpanderClicked;
            handler?.Invoke(this, e);
        }

        private void RightExpander_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MouseButtonEventHandler handler = RightExpanderClicked;
            handler?.Invoke(this, e);
        }
    }
}
