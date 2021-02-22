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

namespace Demo_Usercontrols.UserControls.Manometer
{
    /// <summary>
    /// Interaction logic for Manometer.xaml
    /// </summary>
    public partial class Manometer : UserControl
    {
        public Manometer()
        {
            InitializeComponent();

            (this.Content as FrameworkElement).DataContext = this;
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(Manometer),
                new PropertyMetadata(0.0, new PropertyChangedCallback(ValueChanged)));

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Manometer m = d as Manometer;
            //0 = 500
            //6 = 110
            double newValue = (double)e.NewValue;

            if (newValue <= 6)
            {
                int heightLeft = (int)((newValue / 6) * 390) + 110;
                int heightRight = (500 - heightLeft) + 110;

                m.RightLevel.Margin = new Thickness(0, heightRight, 58, 168);
                m.LeftLevel.Margin = new Thickness(57, heightLeft, 0, 168);
            }

        }
    }
}
