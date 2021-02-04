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

        public string DisplayValue
        {
            get { return (string)GetValue(DisplayValueDep); }
            set { SetValue(DisplayValueDep, value); }
        }

        public static readonly DependencyProperty DisplayValueDep =
            DependencyProperty.Register("DisplayValue", typeof(string), typeof(UCDigitalScreen),
               new PropertyMetadata("", new PropertyChangedCallback(DisplayValueChanged)));

        private static void DisplayValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCDigitalScreen display = d as UCDigitalScreen;

            string displayString = e.NewValue.ToString();

            List<UCDigit> _digits; _digits = new List<UCDigit>();
            _digits.Add(display.Digit_1);
            _digits.Add(display.Digit_2);
            _digits.Add(display.Digit_3);
            _digits.Add(display.Digit_4);
            _digits.Add(display.Digit_5);
            _digits.Add(display.Digit_6);
            _digits.Add(display.Digit_7);
            _digits.Add(display.Digit_8);
            _digits.Add(display.Digit_9);
            _digits.Add(display.Digit_10);
            _digits.Add(display.Digit_11);


            if (displayString.Length <= _digits.Count)
            {
                for (int i = 0; i < _digits.Count; i++)
                {

                    _digits[i].DisplayDigit = "";
                }

                for (int i = 0; i < displayString.Length; i++)
                {
                    string value = displayString.Substring(i, 1);
                    _digits[displayString.Length - 1 - i].DisplayDigit = value;
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
