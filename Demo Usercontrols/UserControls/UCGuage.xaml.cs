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

namespace Demo_Usercontrols
{
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
