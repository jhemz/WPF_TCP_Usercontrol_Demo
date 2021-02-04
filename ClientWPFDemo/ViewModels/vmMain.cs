using ClientWPFDemo.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Threading;

namespace ClientWPFDemo.ViewModels
{
    public class vmMain : vmBase
    {

        private DispatcherTimer timer;

        public vmMain()
        {
            IncreaseTemp = true;
            MinTemperature = 0;
            MaxTemperature = 300;
            DangerThresholdTemperature = 200;

            MinPressure = 0;
            MaxPressure = 300;
            DangerThresholdPressure = 130;

            //timer = new DispatcherTimer();
            //timer.Tick += new EventHandler(UpdateTime);
            //timer.Interval = new TimeSpan(0, 0, 1);
            //timer.Start();
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            Time = DateTime.Now.ToString("HH:mm:ss");
        }

        public Queue UpdateQueue = new Queue();

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
                OnPropertyChanged();
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

        public string time = "";
        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
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
                UpdateQueue.Enqueue(new KeyValuePair<string, double>("Temperature", value));
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
                UpdateQueue.Enqueue(new KeyValuePair<string, double>("MaxTemperature", value));
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
                UpdateQueue.Enqueue(new KeyValuePair<string, double>("MinTemperature", value));
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
                UpdateQueue.Enqueue(new KeyValuePair<string, double>("DangerThresholdTemperature", value));
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
                UpdateQueue.Enqueue(new KeyValuePair<string, double>("Pressure", value));
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
                UpdateQueue.Enqueue(new KeyValuePair<string, double>("MaxPressure", value));
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
                UpdateQueue.Enqueue(new KeyValuePair<string, double>("MinPressure", value));
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
                UpdateQueue.Enqueue(new KeyValuePair<string, double>("DangerThresholdPressure", value));
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
                UpdateQueue.Enqueue(new KeyValuePair<string, bool>("Connected", value));
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
                UpdateQueue.Enqueue(new KeyValuePair<string, bool>("IncreaseTemp", value));
                if (DecreaseTemp == value)
                {
                    DecreaseTemp = !value;
                }
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
                UpdateQueue.Enqueue(new KeyValuePair<string, bool>("DecreaseTemp", value));
                if (IncreaseTemp == value)
                {
                    IncreaseTemp = !value;
                }
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
                UpdateQueue.Enqueue(new KeyValuePair<string, bool>("Button3", value));
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
                UpdateQueue.Enqueue(new KeyValuePair<string, bool>("Button4", value));
                OnPropertyChanged();
            }
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
                UpdateQueue.Enqueue(new KeyValuePair<string, bool>("Button5", value));
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
                UpdateQueue.Enqueue(new KeyValuePair<string, bool>("Button6", value));
                OnPropertyChanged();
            }
        }
    }
}

