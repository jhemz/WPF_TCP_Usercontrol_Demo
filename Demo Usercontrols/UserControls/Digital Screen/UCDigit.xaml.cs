using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo_Usercontrols.UserControls.Digital_Screen
{
    public enum DigitColour { None, Red, Green, Yellow }
    /// <summary>
    /// Interaction logic for UCDigit.xaml
    /// </summary>
    public partial class UCDigit : UserControl
    {
        private List<KeyValuePair<string, int>> digits = new List<KeyValuePair<string, int>>();

        public UCDigit()
        {
            InitializeComponent();

            digits.Add(new KeyValuePair<string, int>("a", 8064));
            digits.Add(new KeyValuePair<string, int>("b", 8128));
            digits.Add(new KeyValuePair<string, int>("c", 5440));
            digits.Add(new KeyValuePair<string, int>("d", 8000));
            digits.Add(new KeyValuePair<string, int>("e", 5568));
            digits.Add(new KeyValuePair<string, int>("f", 5504));
            digits.Add(new KeyValuePair<string, int>("g", 5952));
            digits.Add(new KeyValuePair<string, int>("h", 7808));
            digits.Add(new KeyValuePair<string, int>("i", 368));
            digits.Add(new KeyValuePair<string, int>("j", 3648));
            digits.Add(new KeyValuePair<string, int>("k", 5768));
            digits.Add(new KeyValuePair<string, int>("l", 5184));
            digits.Add(new KeyValuePair<string, int>("m", 7984));
            digits.Add(new KeyValuePair<string, int>("n", 7936));
            digits.Add(new KeyValuePair<string, int>("o", 8000));
            digits.Add(new KeyValuePair<string, int>("p", 7552));
            digits.Add(new KeyValuePair<string, int>("q", 7040));
            digits.Add(new KeyValuePair<string, int>("r", 7553));
            digits.Add(new KeyValuePair<string, int>("s", 5056));
            digits.Add(new KeyValuePair<string, int>("t", 304));
            digits.Add(new KeyValuePair<string, int>("u", 7744));
            digits.Add(new KeyValuePair<string, int>("v", 5130));
            digits.Add(new KeyValuePair<string, int>("w", 7792));
            digits.Add(new KeyValuePair<string, int>("x", 15));
            digits.Add(new KeyValuePair<string, int>("y", 6848));
            digits.Add(new KeyValuePair<string, int>("z", 330));

            digits.Add(new KeyValuePair<string, int>("0", 8000));
            digits.Add(new KeyValuePair<string, int>("1", 2560));
            digits.Add(new KeyValuePair<string, int>("2", 3520));
            digits.Add(new KeyValuePair<string, int>("3", 3008));
            digits.Add(new KeyValuePair<string, int>("4", 6784));
            digits.Add(new KeyValuePair<string, int>("5", 5056));
            digits.Add(new KeyValuePair<string, int>("6", 6080));
            digits.Add(new KeyValuePair<string, int>("7", 2816));
            digits.Add(new KeyValuePair<string, int>("8", 8128));
            digits.Add(new KeyValuePair<string, int>("9", 7040));

            digits.Add(new KeyValuePair<string, int>("-", 128));

            (this.Content as FrameworkElement).DataContext = this;
        }



        public DigitColour Colour
        {
            get { return (DigitColour)GetValue(ColourDep); }
            set { SetValue(ColourDep, value); }
        }

        public static readonly DependencyProperty ColourDep =
            DependencyProperty.Register("Colour", typeof(DigitColour), typeof(UCDigit),
                new PropertyMetadata(DigitColour.None, new PropertyChangedCallback(ColourChanged)));

        private static void ColourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCDigit digit = d as UCDigit;
            digit.UpdateDigit(digit, digit.DisplayDigit);
        }

        public string DisplayDigit
        {
            get { return (string)GetValue(DisplayDigitDep); }
            set { SetValue(DisplayDigitDep, value); }
        }


        public static readonly DependencyProperty DisplayDigitDep =
            DependencyProperty.Register("DisplayDigit", typeof(string), typeof(UCDigit),
                new PropertyMetadata("", new PropertyChangedCallback(DisplayDigitChanged)));

        private static void DisplayDigitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCDigit digit = d as UCDigit;
            digit.UpdateDigit(digit, e.NewValue.ToString());
        }

        private void UpdateDigit(UCDigit digit, string displayValue)
        {
            switch (digit.Colour)
            {
                case DigitColour.Red:
                    digit.color1.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0000"));//glow
                    digit.color2.Color = (Color)ColorConverter.ConvertFromString("#FF890F0F");//main
                    digit.color3.Color = (Color)ColorConverter.ConvertFromString("#FFDD3E3E");//secondary
                    digit.color4.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#337C0F0F"));//dim
                    break;
                case DigitColour.Yellow:
                    digit.color1.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e3a736"));
                    digit.color2.Color = (Color)ColorConverter.ConvertFromString("#e3a736");
                    digit.color3.Color = (Color)ColorConverter.ConvertFromString("#f1e3b6");
                    digit.color4.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#19F6FF86"));
                    break;
                case DigitColour.Green:
                    digit.color1.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#55b541"));
                    digit.color2.Color = (Color)ColorConverter.ConvertFromString("#55b541");
                    digit.color3.Color = (Color)ColorConverter.ConvertFromString("#91db8c");
                    digit.color4.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#19F0BF63"));
                    break;
                default:
                    digit.color1.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0000"));//glow
                    digit.color2.Color = (Color)ColorConverter.ConvertFromString("#FF890F0F");//main
                    digit.color3.Color = (Color)ColorConverter.ConvertFromString("#FFDD3E3E");//secondary
                    digit.color4.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#337C0F0F"));//dim
                    break;
            }

            if (displayValue != ":" && displayValue != ".")
            {
                string bits = "0000000000000";
                KeyValuePair<string, int> displayDigit = digits.Where(x => x.Key == displayValue.ToLower()).FirstOrDefault();
                if (displayDigit.Key != null)
                {
                    bits = ToBinary(displayDigit.Value);
                    while (bits.Length < 13)
                    {
                        bits = "0" + bits;
                    }
                }
                digit.Piece_1.Visibility = ToVisibility(bits.Substring(0, 1));
                digit.Piece_2.Visibility = ToVisibility(bits.Substring(1, 1));
                digit.Piece_3.Visibility = ToVisibility(bits.Substring(2, 1));
                digit.Piece_4.Visibility = ToVisibility(bits.Substring(3, 1));
                digit.Piece_5.Visibility = ToVisibility(bits.Substring(4, 1));
                digit.Piece_6.Visibility = ToVisibility(bits.Substring(5, 1));
                digit.Piece_7.Visibility = ToVisibility(bits.Substring(6, 1));
                digit.Piece_8.Visibility = ToVisibility(bits.Substring(7, 1));
                digit.Piece_9.Visibility = ToVisibility(bits.Substring(8, 1));
                digit.Piece_10.Visibility = ToVisibility(bits.Substring(9, 1));
                digit.Piece_11.Visibility = ToVisibility(bits.Substring(10, 1));
                digit.Piece_12.Visibility = ToVisibility(bits.Substring(11, 1));
                digit.Piece_13.Visibility = ToVisibility(bits.Substring(12, 1));
                digit.Point_1.Visibility = Visibility.Hidden;
                digit.Point_2.Visibility = Visibility.Hidden;
            }
            else if (displayValue == ":")
            {
                digit.Piece_1.Visibility = Visibility.Hidden;
                digit.Piece_2.Visibility = Visibility.Hidden;
                digit.Piece_3.Visibility = Visibility.Hidden;
                digit.Piece_4.Visibility = Visibility.Hidden;
                digit.Piece_5.Visibility = Visibility.Hidden;
                digit.Piece_6.Visibility = Visibility.Hidden;
                digit.Piece_7.Visibility = Visibility.Hidden;
                digit.Piece_8.Visibility = Visibility.Hidden;
                digit.Piece_9.Visibility = Visibility.Hidden;
                digit.Piece_10.Visibility = Visibility.Hidden;
                digit.Piece_11.Visibility = Visibility.Hidden;
                digit.Piece_12.Visibility = Visibility.Hidden;
                digit.Piece_13.Visibility = Visibility.Hidden;
                digit.Point_1.Visibility = Visibility.Visible;
                digit.Point_2.Visibility = Visibility.Visible;
            }
            else if (displayValue == ".")
            {
                digit.Piece_1.Visibility = Visibility.Hidden;
                digit.Piece_2.Visibility = Visibility.Hidden;
                digit.Piece_3.Visibility = Visibility.Hidden;
                digit.Piece_4.Visibility = Visibility.Hidden;
                digit.Piece_5.Visibility = Visibility.Hidden;
                digit.Piece_6.Visibility = Visibility.Hidden;
                digit.Piece_7.Visibility = Visibility.Hidden;
                digit.Piece_8.Visibility = Visibility.Hidden;
                digit.Piece_9.Visibility = Visibility.Hidden;
                digit.Piece_10.Visibility = Visibility.Hidden;
                digit.Piece_11.Visibility = Visibility.Hidden;
                digit.Piece_12.Visibility = Visibility.Hidden;
                digit.Piece_13.Visibility = Visibility.Hidden;
                digit.Point_1.Visibility = Visibility.Visible;
                digit.Point_2.Visibility = Visibility.Hidden;
            }


        }

        private string ToBinary(int n)
        {
            if (n < 2) return n.ToString();

            var divisor = n / 2;
            var remainder = n % 2;
            return ToBinary(divisor) + remainder;
        }

        private Visibility ToVisibility(string input)
        {
            Visibility result = Visibility.Visible;
            if (input == "0")
            {
                result = Visibility.Hidden;
            }
            return result;
        }

    }
}
