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

namespace Demo_Usercontrols.UserControls
{
    public enum PointerColorEnum { Yellow, Red, Blue, White, Green}
    /// <summary>
    /// Interaction logic for UCRotationalSwitch.xaml
    /// </summary>
    public partial class UCRotationalSwitch : UserControl
    {
        double y = 0;
        double newY = 0;

        bool rotatingButton = false;

        public UCRotationalSwitch()
        {
            InitializeComponent();

            (this.Content as FrameworkElement).DataContext = this;
        }

        private void button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this);
            y = p.Y;// - ButtomAngle.Angle;
            rotatingButton = true;
        }

        private void button_MouseMove(object sender, MouseEventArgs e)
        {
            if (rotatingButton)
            {
                Point p = e.GetPosition(this);
                newY = p.Y + ButtomAngle.Angle;
                double newAngle = (newY - y);
                if (newAngle <= MinAngle)
                {
                    ButtomAngle.Angle = MinAngle;
                }
                else if (newAngle >= MaxAngle)
                {
                    ButtomAngle.Angle = MaxAngle;
                }
                else
                {
                    ButtomAngle.Angle = newAngle;
                }



            }


        }

        private void button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            FinishRotation();
        }

        private void button_MouseLeave(object sender, MouseEventArgs e)
        {
            FinishRotation();
        }

        private void FinishRotation()
        {
            int newAngle = ((int)Math.Round((newY - y) / 45)) * 45;
            if (newAngle <= MinAngle)
            {
                ButtomAngle.Angle = MinAngle;
            }
            else if (newAngle >= MaxAngle)
            {
                ButtomAngle.Angle = MaxAngle;
            }
            else
            {
                ButtomAngle.Angle = newAngle;
            }
            Value = (int)ButtomAngle.Angle / 45;
            rotatingButton = false;
           
        }

        public static readonly DependencyProperty PointerColorProperty =
    DependencyProperty.Register("PointerColor", typeof(PointerColorEnum), typeof(UCRotationalSwitch), new
     PropertyMetadata(PointerColorEnum.Yellow, new PropertyChangedCallback(PointerColorChanged)));

        public PointerColorEnum PointerColor
        {
            get { return (PointerColorEnum)GetValue(PointerColorProperty); }
            set { SetValue(PointerColorProperty, value); }
        }
        private static void PointerColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCRotationalSwitch uc = d as UCRotationalSwitch;
            PointerColorEnum value = (PointerColorEnum)e.NewValue;
            switch (value)
            {
                case PointerColorEnum.Yellow:
                    uc.YellowPointer.Visibility = Visibility.Visible;
                    uc.RedPointer.Visibility = Visibility.Hidden;
                    uc.BluePointer.Visibility = Visibility.Hidden;
                    uc.WhitePointer.Visibility = Visibility.Hidden;
                    uc.GreenPointer.Visibility = Visibility.Hidden;
                    break;
                case PointerColorEnum.Red:
                    uc.YellowPointer.Visibility = Visibility.Hidden;
                    uc.RedPointer.Visibility = Visibility.Visible;
                    uc.BluePointer.Visibility = Visibility.Hidden;
                    uc.WhitePointer.Visibility = Visibility.Hidden;
                    uc.GreenPointer.Visibility = Visibility.Hidden;
                    break;
                case PointerColorEnum.Blue:
                    uc.YellowPointer.Visibility = Visibility.Hidden;
                    uc.RedPointer.Visibility = Visibility.Hidden;
                    uc.BluePointer.Visibility = Visibility.Visible;
                    uc.WhitePointer.Visibility = Visibility.Hidden;
                    uc.GreenPointer.Visibility = Visibility.Hidden;
                    break;
                case PointerColorEnum.White:
                    uc.YellowPointer.Visibility = Visibility.Hidden;
                    uc.RedPointer.Visibility = Visibility.Hidden;
                    uc.BluePointer.Visibility = Visibility.Hidden;
                    uc.WhitePointer.Visibility = Visibility.Visible;
                    uc.GreenPointer.Visibility = Visibility.Hidden;
                    break;
                case PointerColorEnum.Green:
                    uc.YellowPointer.Visibility = Visibility.Hidden;
                    uc.RedPointer.Visibility = Visibility.Hidden;
                    uc.BluePointer.Visibility = Visibility.Hidden;
                    uc.WhitePointer.Visibility = Visibility.Hidden;
                    uc.GreenPointer.Visibility = Visibility.Visible;
                    break;

            }
        }

        public static readonly DependencyProperty ValueProperty =
     DependencyProperty.Register("Value", typeof(int), typeof(UCRotationalSwitch), new
      FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(ValueChanged)));

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sri = Application.GetResourceStream(new Uri("/Demo_Usercontrols;component/SoundEffects/mixkit-typewriter-soft-click-1125.wav", UriKind.RelativeOrAbsolute));

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

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }


        public static readonly DependencyProperty MinAngleProperty =
      DependencyProperty.Register("MinAngle", typeof(int), typeof(UCRotationalSwitch), new
       PropertyMetadata(-45, new PropertyChangedCallback(MinAngleChanged)));

        public int MinAngle
        {
            get { return (int)GetValue(MinAngleProperty); }
            set { SetValue(MinAngleProperty, value); }
        }
        private static void MinAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCRotationalSwitch uc = d as UCRotationalSwitch;

        }

        public static readonly DependencyProperty MaxAngleProperty =
     DependencyProperty.Register("MaxAngle", typeof(int), typeof(UCRotationalSwitch), new
      PropertyMetadata(-45, new PropertyChangedCallback(MaxAngleChanged)));

        public int MaxAngle
        {
            get { return (int)GetValue(MaxAngleProperty); }
            set { SetValue(MaxAngleProperty, value); }
        }
        private static void MaxAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCRotationalSwitch uc = d as UCRotationalSwitch;

        }

        public static readonly DependencyProperty Label1Property =
   DependencyProperty.Register("Label1", typeof(string), typeof(UCRotationalSwitch), new
    PropertyMetadata("", new PropertyChangedCallback(Label1Changed)));

        public string Label1
        {
            get { return (string)GetValue(Label1Property); }
            set { SetValue(Label1Property, value); }
        }
        private static void Label1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCRotationalSwitch uc = d as UCRotationalSwitch;

        }

        public static readonly DependencyProperty Label2Property =
  DependencyProperty.Register("Label2", typeof(string), typeof(UCRotationalSwitch), new
   PropertyMetadata("", new PropertyChangedCallback(Label2Changed)));

        public string Label2
        {
            get { return (string)GetValue(Label2Property); }
            set { SetValue(Label2Property, value); }
        }
        private static void Label2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCRotationalSwitch uc = d as UCRotationalSwitch;

        }

        public static readonly DependencyProperty Label3Property =
 DependencyProperty.Register("Label3", typeof(string), typeof(UCRotationalSwitch), new
  PropertyMetadata("", new PropertyChangedCallback(Label3Changed)));

        public string Label3
        {
            get { return (string)GetValue(Label3Property); }
            set { SetValue(Label3Property, value); }
        }
        private static void Label3Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCRotationalSwitch uc = d as UCRotationalSwitch;

        }

        public static readonly DependencyProperty Label4Property =
 DependencyProperty.Register("Label4", typeof(string), typeof(UCRotationalSwitch), new
  PropertyMetadata("", new PropertyChangedCallback(Label4Changed)));

        public string Label4
        {
            get { return (string)GetValue(Label4Property); }
            set { SetValue(Label4Property, value); }
        }
        private static void Label4Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCRotationalSwitch uc = d as UCRotationalSwitch;

        }


        public static readonly DependencyProperty Label5Property =
 DependencyProperty.Register("Label5", typeof(string), typeof(UCRotationalSwitch), new
  PropertyMetadata("", new PropertyChangedCallback(Label5Changed)));

        public string Label5
        {
            get { return (string)GetValue(Label5Property); }
            set { SetValue(Label5Property, value); }
        }
        private static void Label5Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCRotationalSwitch uc = d as UCRotationalSwitch;

        }


        public static readonly DependencyProperty Label6Property =
 DependencyProperty.Register("Label6", typeof(string), typeof(UCRotationalSwitch), new
  PropertyMetadata("", new PropertyChangedCallback(Label6Changed)));

        public string Label6
        {
            get { return (string)GetValue(Label6Property); }
            set { SetValue(Label6Property, value); }
        }
        private static void Label6Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCRotationalSwitch uc = d as UCRotationalSwitch;

        }

        public static readonly DependencyProperty Label7Property =
 DependencyProperty.Register("Label7", typeof(string), typeof(UCRotationalSwitch), new
  PropertyMetadata("", new PropertyChangedCallback(Label7Changed)));

        public string Label7
        {
            get { return (string)GetValue(Label7Property); }
            set { SetValue(Label7Property, value); }
        }
        private static void Label7Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCRotationalSwitch uc = d as UCRotationalSwitch;

        }

        public static readonly DependencyProperty Label8Property =
 DependencyProperty.Register("Label8", typeof(string), typeof(UCRotationalSwitch), new
  PropertyMetadata("", new PropertyChangedCallback(Label8Changed)));

        public string Label8
        {
            get { return (string)GetValue(Label8Property); }
            set { SetValue(Label8Property, value); }
        }
        private static void Label8Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCRotationalSwitch uc = d as UCRotationalSwitch;

        }


    }


}
