using System.Windows;
using System.Windows.Controls;

namespace Demo_Usercontrols.UserControls.Digital_Screen
{
    /// <summary>
    /// Interaction logic for UCDigit.xaml
    /// </summary>
    public partial class UCDigit : UserControl
    {
        public UCDigit()
        {
            InitializeComponent();

            (this.Content as FrameworkElement).DataContext = this;
        }


        public string DisplayDigit
        {
            get { return (string)GetValue(DisplayDigitDep); }
            set { SetValue(DisplayDigitDep, value); }
        }


        public static readonly DependencyProperty DisplayDigitDep =
            DependencyProperty.Register("DisplayDigit", typeof(string), typeof(UCDigit),
                new PropertyMetadata("*", new PropertyChangedCallback(DisplayDigitChanged)));

        private static void DisplayDigitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UCDigit digit = d as UCDigit;

            switch (e.NewValue.ToString().ToLower())
            {
                case "":
                    digit.Piece_1.Visibility = Visibility.Collapsed;
                    digit.Piece_2.Visibility = Visibility.Collapsed;
                    digit.Piece_3.Visibility = Visibility.Collapsed;
                    digit.Piece_4.Visibility = Visibility.Collapsed;
                    digit.Piece_5.Visibility = Visibility.Collapsed;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;
                case "0":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "1":
                    digit.Piece_1.Visibility = Visibility.Collapsed;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Collapsed;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Collapsed;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;
                case "2":
                    digit.Piece_1.Visibility = Visibility.Collapsed;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Collapsed;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "3":
                    digit.Piece_1.Visibility = Visibility.Collapsed;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Collapsed;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "4":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Collapsed;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Collapsed;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;
                case "5":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Collapsed;
                    digit.Piece_3.Visibility = Visibility.Collapsed;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "6":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Collapsed;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "7":
                    digit.Piece_1.Visibility = Visibility.Collapsed;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Collapsed;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;
                case "8":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "9":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Collapsed;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;

                case "-":
                    digit.Piece_1.Visibility = Visibility.Collapsed;
                    digit.Piece_2.Visibility = Visibility.Collapsed;
                    digit.Piece_3.Visibility = Visibility.Collapsed;
                    digit.Piece_4.Visibility = Visibility.Collapsed;
                    digit.Piece_5.Visibility = Visibility.Collapsed;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;

               

                case "a":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;
                case "b":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "c":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Collapsed;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Collapsed;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "d":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "e":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Collapsed;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Collapsed;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "f":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Collapsed;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Collapsed;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;
                case "g":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Collapsed;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "h":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Collapsed;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;
                case "i":
                    digit.Piece_1.Visibility = Visibility.Collapsed;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Collapsed;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Collapsed;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;
                case "j":
                    digit.Piece_1.Visibility = Visibility.Collapsed;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Collapsed;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Collapsed;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "k":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Collapsed;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Collapsed;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;
                case "l":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Collapsed;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Collapsed;
                    digit.Piece_5.Visibility = Visibility.Collapsed;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "m":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;
                case "n":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;

                case "o":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "p":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Collapsed;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;
                case "q":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Collapsed;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;

                case "r":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Collapsed;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Collapsed;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;
                case "s":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Collapsed;
                    digit.Piece_3.Visibility = Visibility.Collapsed;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "t":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Collapsed;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Collapsed;
                    digit.Piece_5.Visibility = Visibility.Collapsed;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "u":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Collapsed;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "v":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Collapsed;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "w":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Collapsed;
                    digit.Piece_6.Visibility = Visibility.Collapsed;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;
                case "x":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Collapsed;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;
                case "y":
                    digit.Piece_1.Visibility = Visibility.Visible;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Collapsed;
                    digit.Piece_4.Visibility = Visibility.Visible;
                    digit.Piece_5.Visibility = Visibility.Collapsed;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Collapsed;
                    break;
                case "z":
                    digit.Piece_1.Visibility = Visibility.Collapsed;
                    digit.Piece_2.Visibility = Visibility.Visible;
                    digit.Piece_3.Visibility = Visibility.Visible;
                    digit.Piece_4.Visibility = Visibility.Collapsed;
                    digit.Piece_5.Visibility = Visibility.Visible;
                    digit.Piece_6.Visibility = Visibility.Visible;
                    digit.Piece_7.Visibility = Visibility.Visible;
                    break;

            }

        }

    }
}
