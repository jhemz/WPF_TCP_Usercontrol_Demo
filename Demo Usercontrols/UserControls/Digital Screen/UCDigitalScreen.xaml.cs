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

namespace Demo_Usercontrols.UserControls.Digital_Screen
{
    /// <summary>
    /// Interaction logic for UCDigitalScreen.xaml
    /// </summary>
    public partial class UCDigitalScreen : UserControl
    {


        public UCDigitalScreen()
        {
            InitializeComponent();
        }

        public int Digits
        {
            get { return (int)GetValue(DigitsProperty); }
            set { SetValue(DigitsProperty, value); }
        }

        public static readonly DependencyProperty DigitsProperty =
            DependencyProperty.Register("Digits", typeof(int), typeof(UCDigitalScreen),
               new PropertyMetadata(11, new PropertyChangedCallback(DigitsChanged)));

        private static void DigitsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCDigitalScreen display = d as UCDigitalScreen;
            display.Screen.Children.Clear();
            for (int i = 1; i <= (int)display.Digits; i++)
            {
                display.Screen.Children.Add(new Viewbox() { StretchDirection = StretchDirection.Both, Stretch = Stretch.Uniform, Child = new UCDigit() { Name = "Digit_" + i } });
            }
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(UCDigitalScreen),
               new PropertyMetadata("", new PropertyChangedCallback(LabelChanged)));

        private static void LabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCDigitalScreen display = d as UCDigitalScreen;

            if (e.NewValue != null)
            {
                display.DisplayLabel.Content = e.NewValue;
            }
        }

        public string DisplayValue
        {
            get { return (string)GetValue(DisplayValueProperty); }
            set { SetValue(DisplayValueProperty, value); }
        }

        public static readonly DependencyProperty DisplayValueProperty =
            DependencyProperty.Register("DisplayValue", typeof(string), typeof(UCDigitalScreen),
               new PropertyMetadata("", new PropertyChangedCallback(DisplayValueChanged)));

        private static void DisplayValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCDigitalScreen display = d as UCDigitalScreen;

            if (e.NewValue != null)
            {
                //if(display.Screen.Children.Count == 0)
                //{
                //    for (int i = 1; i <= (int)display.Digits; i++)
                //    {
                //        display.Screen.Children.Add(new Viewbox() { StretchDirection = StretchDirection.Both, Stretch = Stretch.Uniform, Child = new UCDigit() { Name = "Digit_" + i } });
                //    }
                //}

                string displayString = e.NewValue.ToString();

                List<UCDigit> _digits; _digits = new List<UCDigit>();

                foreach (Viewbox digitContainer in display.Screen.Children)
                {
                    _digits.Add((UCDigit)digitContainer.Child);
                }

                if (displayString.Length <= _digits.Count)
                {
                    for (int i = 0; i < _digits.Count; i++)
                    {

                        _digits[i].DisplayDigit = "";
                    }

                    int spacer = display.Digits - displayString.Length;

                    for (int i = displayString.Length - 1; i >= 0; i--)
                    {
                        string value = displayString.Substring(i, 1);
                        _digits[i + spacer].DisplayDigit = value;
                    }
                }
                else
                {
                    _digits[0].DisplayDigit = "R";
                    _digits[1].DisplayDigit = "R";
                    _digits[2].DisplayDigit = "E";
                }
            }
        }
    }
}
