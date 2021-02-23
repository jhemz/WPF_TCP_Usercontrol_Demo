using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Demo_Usercontrols.UserControls.TimeLine
{
    /// <summary>
    /// Interaction logic for TimelineScaleShuttle.xaml
    /// </summary>
    public partial class TimelineScaleShuttle : UserControl
    {
        public event MouseButtonEventHandler RightExpanderClicked;
        public event MouseButtonEventHandler LeftExpanderClicked;
        public event MouseButtonEventHandler MoverClicked;

        public TimelineScaleShuttle()
        {
            InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public static readonly DependencyProperty ItemWidthProperty =
    DependencyProperty.Register("ItemWidth", typeof(double), typeof(TimelineScaleShuttle), new
     PropertyMetadata(100.0, new PropertyChangedCallback(OnItemWidthChanged)));

        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }
        private static void OnItemWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineScaleShuttle tlss = d as TimelineScaleShuttle;
            
        }


        public static readonly DependencyProperty StartProperty =
 DependencyProperty.Register("Start", typeof(double), typeof(TimelineScaleShuttle), new
  FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnStartChanged)));

        public double Start
        {
            get { return (double)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }
        private static void OnStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineScaleShuttle tlss = d as TimelineScaleShuttle;
            double newLeft = ((double)e.NewValue);
            tlss.Item.Margin = new Thickness(newLeft, 0, 0, 0);

            double width = (tlss.End) - (tlss.Start);
            tlss.Item.Width = width;
            tlss.Offset = ((tlss.End - (double)e.NewValue) / 2) + (double)e.NewValue;
        }

        public static readonly DependencyProperty OffsetProperty =
 DependencyProperty.Register("Offset", typeof(double), typeof(TimelineScaleShuttle), new
  FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnOffsetChanged)));

        public double Offset
        {
            get { return (double)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }
        private static void OnOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineScaleShuttle tlss = d as TimelineScaleShuttle;
            tlss.Title.Text = e.NewValue.ToString();
        }

        public static readonly DependencyProperty EndProperty =
DependencyProperty.Register("End", typeof(double), typeof(TimelineScaleShuttle), new
 PropertyMetadata(100.0, new PropertyChangedCallback(OnEndChanged)));

        public double End
        {
            get { return (double)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }
        private static void OnEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineScaleShuttle tlss = d as TimelineScaleShuttle;
            double width = ((double)e.NewValue) - (tlss.Start);
            tlss.Item.Width = width;
            tlss.Offset = (((double)e.NewValue - tlss.Start) / 2) + tlss.Start;
        }



        public static readonly DependencyProperty ItemColourProperty =
   DependencyProperty.Register("ItemColour", typeof(Color), typeof(TimelineScaleShuttle), new
    PropertyMetadata(Colors.Transparent, new PropertyChangedCallback(OnItemColourChanged)));

        public Color ItemColour
        {
            get { return (Color)GetValue(ItemColourProperty); }
            set { SetValue(ItemColourProperty, value); }
        }
        private static void OnItemColourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineScaleShuttle tlss = d as TimelineScaleShuttle;
            //tle.Item.Background = new SolidColorBrush((Color)e.NewValue);
        }

        public static readonly DependencyProperty TextProperty =
     DependencyProperty.Register("Text", typeof(string), typeof(TimelineScaleShuttle), new
      PropertyMetadata("", new PropertyChangedCallback(OnTextChanged)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimelineScaleShuttle tlss = d as TimelineScaleShuttle;
            tlss.Title.Text = e.NewValue.ToString();
        }

        public static readonly DependencyProperty IdProperty =
   DependencyProperty.Register("Id", typeof(Guid), typeof(TimelineScaleShuttle), new
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
