using System.Windows;
using System.Windows.Controls;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Demo_Usercontrols.UserControls.Thermometer
{
    public partial class UCThermometer : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private double temperatureStep = 1;
        public double TemperatureHeight
        {
            get => bulb != null ? ((Temperature - minTemp) * temperatureStep) + (bulb.ActualHeight / 2) : ((Temperature - minTemp) * temperatureStep);
        }

        private double minTemp = -30.0;
        public double MinTemperature
        {
            get => minTemp;
            set
            {
                minTemp = value;
                temperatureStep = (temperatureTube.ActualHeight - (bulb.ActualHeight / 2)) / (maxTemp - minTemp);
                NotifyPropertyChanged(nameof(TemperatureHeight));
                NotifyPropertyChanged(nameof(MinTemperatureStr));
            }
        }

        public string MinTemperatureStr
        {
            get => $"{(int)minTemp}°" + (isCelsius ? "C" : "F");
        }

        public double Temperature
        {
            get { return (double)GetValue(TemperatureProperty); }
            set { SetValue(TemperatureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Temperature.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemperatureProperty =
            DependencyProperty.Register("Temperature", typeof(double), typeof(UCThermometer), new PropertyMetadata(37.0));



        public static readonly DependencyProperty ThermometerProperty =
           DependencyProperty.Register("Thermometer", typeof(double), typeof(UCThermometer), new PropertyMetadata(37.0));
        public UCThermometer()
        {
            this.DataContext = this;
            InitializeComponent();
        }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            switch (e.Property.Name)
            {
                case "ActualHeight":
                    {
                        temperatureStep = (temperatureTube.ActualHeight - (bulb.ActualHeight / 2)) / (maxTemp - minTemp);
                        NotifyPropertyChanged(nameof(TemperatureHeight));
                    }
                    break;
                case "Temperature":
                    NotifyPropertyChanged(nameof(TemperatureHeight));
                    NotifyPropertyChanged(nameof(TemperatureText));
                    break;
            }
        }

        public string TemperatureText
        {
            get => $"{(int)Temperature}°" + (isCelsius ? "C" : "F");
        }

        private double maxTemp = 50.0;
        public double MaxTemperature
        {
            get => maxTemp;
            set
            {
                maxTemp = value;
                temperatureStep = (temperatureTube.ActualHeight - (bulb.ActualHeight / 2)) / (maxTemp - minTemp);
                NotifyPropertyChanged(nameof(TemperatureHeight));
                NotifyPropertyChanged(nameof(MaxTemperatureStr));
            }
        }
        public string MaxTemperatureStr
        {
            get => $"{(int)maxTemp}°" + (isCelsius ? "C" : "F");
        }
        private bool isCelsius = true;
       

        public bool IsCelsius
        {
            get => isCelsius;
            set
            {
                isCelsius = value;
                NotifyPropertyChanged(nameof(MinTemperatureStr));
                NotifyPropertyChanged(nameof(MaxTemperatureStr));
                NotifyPropertyChanged(nameof(TemperatureText));
            } 
        }
    }
}
