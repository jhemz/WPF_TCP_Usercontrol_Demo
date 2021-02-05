using ServerWPFDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServerWPFDemo.ViewModels
{
    public class vmMain : vmBase
    {
        private modelMain model = new modelMain();
        public modelMain Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
                OnPropertyChanged("CommandDisplay");
                OnPropertyChanged("Temperature");
                OnPropertyChanged("Connected");
                OnPropertyChanged("IncreaseTemp");
                OnPropertyChanged("DecreaseTemp");
                OnPropertyChanged("DangerTemperatureAlert");
                OnPropertyChanged("Pressure");
                OnPropertyChanged("DangerPressureAlert");
                OnPropertyChanged("Button3");
                OnPropertyChanged("Button4");
                OnPropertyChanged("Button5");
                OnPropertyChanged("Button6");
            }
        }

        public string CommandDisplay
        {
            get
            {
                return Model.CommandDisplay;
            }
            set
            {
                Model.CommandDisplay = value;
                OnPropertyChanged();
            }
        }

        public double Temperature
        {
            get
            {
                return Model.Temperature;
            }
            set
            {
                Model.Temperature = value;
                OnPropertyChanged();
            }
        }

        public double MaxTemperature
        {
            get
            {
                return Model.MaxTemperature;
            }
            set
            {
                Model.MaxTemperature = value;
                OnPropertyChanged();
            }
        }

        public double MinTemperature
        {
            get
            {
                return Model.MinTemperature;
            }
            set
            {
                Model.MinTemperature = value;
                OnPropertyChanged();
            }
        }

        public double DangerThresholdTemperature
        {
            get
            {
                return Model.DangerThresholdTemperature;
            }
            set
            {
                Model.DangerThresholdTemperature = value;
                OnPropertyChanged();
            }
        }

        public bool DangerTemperatureAlert
        {
            get
            {
                return Model.DangerTemperatureAlert;
            }
            set
            {
                Model.DangerTemperatureAlert = value;
                OnPropertyChanged();
            }
        }

        public double Pressure
        {
            get
            {
                return Model.Pressure;
            }
            set
            {
                Model.Pressure = value;
                OnPropertyChanged();
            }
        }

        public double MaxPressure
        {
            get
            {
                return Model.MaxPressure;
            }
            set
            {
                Model.MaxPressure = value;
                OnPropertyChanged();
            }
        }

        public double MinPressure
        {
            get
            {
                return Model.MinPressure;
            }
            set
            {
                Model.MinPressure = value;
                OnPropertyChanged();
            }
        }

        public double DangerThresholdPressure
        {
            get
            {
                return Model.DangerThresholdPressure;
            }
            set
            {
                Model.DangerThresholdPressure = value;
                OnPropertyChanged();
            }
        }

        public bool DangerPressureAlert
        {
            get
            {
                return Model.DangerPressureAlert;
            }
            set
            {
                Model.DangerPressureAlert = value;
                OnPropertyChanged();
            }
        }

        public bool Connected
        {
            get
            {
                return Model.Connected;
            }
            set
            {
                Model.Connected = value;
                OnPropertyChanged();
            }
        }

        public bool IncreaseTemp
        {
            get
            {
                return Model.IncreaseTemp;
            }
            set
            {
                Model.IncreaseTemp = value;
                OnPropertyChanged();
            }
        }

        public bool DecreaseTemp
        {
            get
            {
                return Model.DecreaseTemp;
            }
            set
            {
                Model.DecreaseTemp = value;
                OnPropertyChanged();
            }
        }

        public bool Button3
        {
            get
            {
                return Model.Button3;
            }
            set
            {
                Model.Button3 = value;
                OnPropertyChanged();
            }
        }

        public bool Button4
        {
            get
            {
                return Model.Button4;
            }
            set
            {
                Model.Button4 = value;
                OnPropertyChanged();
            }
        }

        public static explicit operator vmMain(Window v)
        {
            throw new NotImplementedException();
        }

        public bool Button5
        {
            get
            {
                return Model.Button5;
            }
            set
            {
                Model.Button5 = value;
                OnPropertyChanged();
            }
        }

        public bool Button6
        {
            get
            {
                return Model.Button6;
            }
            set
            {
                Model.Button6 = value;
                OnPropertyChanged();
            }
        }
    }
}
