using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        public DigitColour DigitColour
        {
            get { return (DigitColour)GetValue(DigitColourDep); }
            set { SetValue(DigitColourDep, value); }
        }


        public static readonly DependencyProperty DigitColourDep =
            DependencyProperty.Register("DigitColour", typeof(DigitColour), typeof(UCDigitalScreen),
                new PropertyMetadata(DigitColour.Red, new PropertyChangedCallback(DigitColourChanged)));

        private static void DigitColourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCDigit digit = d as UCDigit;
            
        }

        public int Digits
        {
            get { return (int)GetValue(DigitsProperty); }
            set { SetValue(DigitsProperty, value); }
        }

        public static readonly DependencyProperty DigitsProperty =
            DependencyProperty.Register("Digits", typeof(int), typeof(UCDigitalScreen),
               new PropertyMetadata(0, new PropertyChangedCallback(DigitsChanged)));

        private static void DigitsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCDigitalScreen display = d as UCDigitalScreen;
            display.Screen.Children.Clear();
            for (int i = 1; i <= (int)display.Digits; i++)
            {
                display.Screen.Children.Add(new Viewbox() { StretchDirection = StretchDirection.Both, Stretch = Stretch.Uniform, Child = new UCDigit() {DisplayDigit="0", Colour = display.DigitColour, Name = "Digit_" + i } });
            }
            UpdateDisplay(display.DisplayValue, display);
        }

        public bool IsOn
        {
            get { return (bool)GetValue(IsOnProperty); }
            set { SetValue(IsOnProperty, value); }
        }

        public static readonly DependencyProperty IsOnProperty =
            DependencyProperty.Register("IsOn", typeof(bool), typeof(UCDigitalScreen),
               new PropertyMetadata(true, new PropertyChangedCallback(IsOnChanged)));

        private static void IsOnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCDigitalScreen display = d as UCDigitalScreen;
            UpdateDisplay(display.DisplayValue, display);
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
                if(display.Digits > 0)
                {
                    UpdateDisplay(e.NewValue.ToString(), display);
                }
            }
        }

        private static void UpdateDisplay(string displayString, UCDigitalScreen display)
        {
          

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

                if (display.IsOn)
                {
                    int spacer = display.Digits - displayString.Length;

                    for (int i = displayString.Length - 1; i >= 0; i--)
                    {
                        string value = displayString.Substring(i, 1);
                        _digits[i + spacer].DisplayDigit = value;
                    }
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

