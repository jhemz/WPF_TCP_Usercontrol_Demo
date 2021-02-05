using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace Demo_Usercontrols
{
    public enum GaugeType { Round, Square }
    public enum GaugeDialBackground { Weathered, Clean }
    public enum GaugeScaleType { Scale1, Scale2 }

    /// <summary>
    /// Interaction logic for UCGuage.xaml
    /// </summary>
    public partial class UCGuage : UserControl
    {
        private int minValue = 0;
        private int maxValue = 100;
        public UCGuage()
        {
            InitializeComponent();

            (this.Content as FrameworkElement).DataContext = this;
        }

        public static readonly DependencyProperty TypeProperty =
       DependencyProperty.Register("Type", typeof(GaugeType), typeof(UCGuage), new
        PropertyMetadata(GaugeType.Round, new PropertyChangedCallback(OnTypeChanged)));

        public GaugeType Type
        {
            get { return (GaugeType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCGuage gauge = d as UCGuage;
            GaugeType type = (GaugeType)e.NewValue;
            switch (type)
            {
                case GaugeType.Round:
                    gauge.Surround.CornerRadius = new CornerRadius(200);
                    gauge.ScrewsRound.Visibility = Visibility.Visible;
                    gauge.ScrewsSquare.Visibility = Visibility.Hidden;
                    break;
                case GaugeType.Square:
                    gauge.Surround.CornerRadius = new CornerRadius(20);
                    gauge.ScrewsRound.Visibility = Visibility.Hidden;
                    gauge.ScrewsSquare.Visibility = Visibility.Visible;
                    break;
            }
        }

        public static readonly DependencyProperty DialBackgroundProperty =
      DependencyProperty.Register("DialBackground", typeof(GaugeDialBackground), typeof(UCGuage), new
       PropertyMetadata(GaugeDialBackground.Weathered, new PropertyChangedCallback(OnDialBackgroundChanged)));

        public GaugeDialBackground DialBackground
        {
            get { return (GaugeDialBackground)GetValue(DialBackgroundProperty); }
            set { SetValue(DialBackgroundProperty, value); }
        }
        private static void OnDialBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCGuage gauge = d as UCGuage;
            GaugeDialBackground DialBackground = (GaugeDialBackground)e.NewValue;
            switch (DialBackground)
            {
                case GaugeDialBackground.Weathered:
                    gauge.background1.Visibility = Visibility.Visible;
                    gauge.background2.Visibility = Visibility.Collapsed;
                    break;
                case GaugeDialBackground.Clean:
                    gauge.background1.Visibility = Visibility.Collapsed;
                    gauge.background2.Visibility = Visibility.Visible;
                   
                    break;
            }
        }

        public static readonly DependencyProperty ScaleTypeProperty =
     DependencyProperty.Register("ScaleType", typeof(GaugeScaleType), typeof(UCGuage), new
      PropertyMetadata(GaugeScaleType.Scale2, new PropertyChangedCallback(OnScaleTypeChanged)));

        public GaugeScaleType ScaleType
        {
            get { return (GaugeScaleType)GetValue(ScaleTypeProperty); }
            set { SetValue(ScaleTypeProperty, value); }
        }
        private static void OnScaleTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCGuage gauge = d as UCGuage;
            GaugeScaleType ScaleType = (GaugeScaleType)e.NewValue;
            switch (ScaleType)
            {
                case GaugeScaleType.Scale1:
                    gauge.Scale1.Visibility = Visibility.Visible;
                    gauge.Scale2.Visibility = Visibility.Collapsed;
                    break;
                case GaugeScaleType.Scale2:
                    gauge.Scale1.Visibility = Visibility.Collapsed;
                    gauge.Scale2.Visibility = Visibility.Visible;

                    break;
            }
        }


        public static readonly DependencyProperty Zone1Property =
       DependencyProperty.Register("Zone1", typeof(Color), typeof(UCGuage), new
        PropertyMetadata(Colors.Transparent, new PropertyChangedCallback(OnZone1Changed)));

        public Color Zone1
        {
            get { return (Color)GetValue(Zone1Property); }
            set { SetValue(Zone1Property, value); }
        }
        private static void OnZone1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCGuage gauge = d as UCGuage;
            gauge.zone1.Stroke = new SolidColorBrush() { Color = (Color)e.NewValue };
        }

        public static readonly DependencyProperty Zone2Property =
     DependencyProperty.Register("Zone2", typeof(Color), typeof(UCGuage), new
      PropertyMetadata(Colors.Transparent, new PropertyChangedCallback(OnZone2Changed)));

        public Color Zone2
        {
            get { return (Color)GetValue(Zone2Property); }
            set { SetValue(Zone2Property, value); }
        }
        private static void OnZone2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCGuage gauge = d as UCGuage;
            gauge.zone2.Stroke = new SolidColorBrush() { Color = (Color)e.NewValue };
        }

        public static readonly DependencyProperty Zone3Property =
     DependencyProperty.Register("Zone3", typeof(Color), typeof(UCGuage), new
      PropertyMetadata(Colors.Transparent, new PropertyChangedCallback(OnZone3Changed)));

        public Color Zone3
        {
            get { return (Color)GetValue(Zone3Property); }
            set { SetValue(Zone3Property, value); }
        }
        private static void OnZone3Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCGuage gauge = d as UCGuage;
            gauge.zone3.Stroke = new SolidColorBrush() { Color = (Color)e.NewValue };
        }

        public static readonly DependencyProperty Zone4Property =
     DependencyProperty.Register("Zone4", typeof(Color), typeof(UCGuage), new
      PropertyMetadata(Colors.Transparent, new PropertyChangedCallback(OnZone4Changed)));

        public Color Zone4
        {
            get { return (Color)GetValue(Zone4Property); }
            set { SetValue(Zone4Property, value); }
        }
        private static void OnZone4Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCGuage gauge = d as UCGuage;
            gauge.zone4.Stroke = new SolidColorBrush() { Color = (Color)e.NewValue };
        }

        public static readonly DependencyProperty Zone5Property =
     DependencyProperty.Register("Zone5", typeof(Color), typeof(UCGuage), new
      PropertyMetadata(Colors.Red, new PropertyChangedCallback(OnZone5Changed)));

        public Color Zone5
        {
            get { return (Color)GetValue(Zone5Property); }
            set { SetValue(Zone5Property, value); }
        }
        private static void OnZone5Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCGuage gauge = d as UCGuage;
            gauge.zone5.Stroke = new SolidColorBrush() { Color = (Color)e.NewValue };
        }


        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(UCGuage), new
         PropertyMetadata("Title", new PropertyChangedCallback(OnTitleChanged)));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           
        }

        public static readonly DependencyProperty UnitsProperty =
        DependencyProperty.Register("Units", typeof(string), typeof(UCGuage), new
         PropertyMetadata("Units", new PropertyChangedCallback(OnUnitsChanged)));

        public string Units
        {
            get { return (string)GetValue(UnitsProperty); }
            set { SetValue(UnitsProperty, value); }
        }
        private static void OnUnitsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public static readonly DependencyProperty CrackIsVisibleProperty =
      DependencyProperty.Register("CrackIsVisible", typeof(bool), typeof(UCGuage), new
       PropertyMetadata(false, new PropertyChangedCallback(OnCrackIsVisibleChanged)));

        public bool CrackIsVisible
        {
            get { return (bool)GetValue(CrackIsVisibleProperty); }
            set { SetValue(CrackIsVisibleProperty, value); }
        }
        private static void OnCrackIsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCGuage gauge = d as UCGuage;
            if((bool)e.NewValue == true)
            {
                gauge.Crack.Visibility = Visibility.Visible;

                var sri = Application.GetResourceStream(new Uri("/Demo_Usercontrols;component/UserControls/crack.wav", UriKind.RelativeOrAbsolute));

                if ((sri != null))
                {
                    using (var s = sri.Stream)
                    {
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(s);
                        player.Load();
                        player.Play();
                    }
                }

               
            }
            else
            {
                gauge.Crack.Visibility = Visibility.Hidden;
            }
        }

        public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(int), typeof(UCGuage), new
           PropertyMetadata(0, new PropertyChangedCallback(OnValueChanged)));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCGuage guage = d as UCGuage;
            int value = (int)e.NewValue;
            double multiplier = (double)value / (double)guage.maxValue;
            guage.Needle.Angle = (multiplier * 140) + 20;
        }

        public static readonly DependencyProperty MinValueProperty =
       DependencyProperty.Register("MinValue", typeof(int), typeof(UCGuage), new
          PropertyMetadata(0, new PropertyChangedCallback(OnMinValueChanged)));

        private static void OnMinValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCGuage guage = d as UCGuage;
            guage.minValue = (int)e.NewValue;
        }

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public static readonly DependencyProperty MaxValueProperty =
     DependencyProperty.Register("MaxValue", typeof(int), typeof(UCGuage), new
        PropertyMetadata(100, new PropertyChangedCallback(OnMaxValueChanged)));

        private static void OnMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCGuage guage = d as UCGuage;
            guage.maxValue = (int)e.NewValue;
        }

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }
    }
}
