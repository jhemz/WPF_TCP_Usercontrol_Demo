using ClientWPFDemo.Models;
using ClientWPFDemo.Services;
using Demo_Usercontrols.UserControls.TimeLine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace ClientWPFDemo.ViewModels
{
    public class vmMain : vmBase
    {

        private DispatcherTimer timer;

        private IMessageCore messageCore;

        public vmMain(IMessageCore _messageCore)
        {
            messageCore = _messageCore;
            IncreaseTemp = true;
            MinTemperature = 0;
            MaxTemperature = 300;
            DangerThresholdTemperature = 200;

            MinPressure = 0;
            MaxPressure = 6;
            DangerThresholdPressure = 6;
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            Time = DateTime.Now.ToString("HH:mm:ss");
        }

        public Queue QueueOfItemsToUpdateOnServer = new Queue();

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
                OnPropertyChanged("FrameRate");
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

        private ObservableCollection<vmTimelineScene> scenes = new ObservableCollection<vmTimelineScene>();
        public ObservableCollection<vmTimelineScene> Scenes
        {
            get
            {
                return scenes;
            }
            set
            {
                scenes = value;
                OnPropertyChanged();
            }
        }

        private vmTimelineScene currentScene = new vmTimelineScene();
        public vmTimelineScene CurrentScene
        {
            get
            {
                return currentScene;
            }
            set
            {
                currentScene = value;
                OnPropertyChanged();
                //when a scene changes, set the default values
                Property1 = currentScene.Panels[0].Property1_DefaultValue;
                Property2 = currentScene.Panels[0].Property2_DefaultValue;
                Property3 = currentScene.Panels[0].Property3_DefaultValue;
            }
        }

        private bool property1;
        public bool Property1
        {
            get
            {
                return property1;
            }
            set
            {
                property1 = value;
                CurrentScene.Panels[0].Property1 = value;//this updates the working proeprty that the timeline can see that the user has changed
                OnPropertyChanged();
            }
        }

        private bool property2;
        public bool Property2
        {
            get
            {
                return property2;
            }
            set
            {
                property2 = value;
                CurrentScene.Panels[0].Property2 = value;//this updates the working proeprty that the timeline can see that the user has changed
                OnPropertyChanged();
            }
        }

        private bool property3;
        public bool Property3
        {
            get
            {
                return property3;
            }
            set
            {
                property3 = value;
                CurrentScene.Panels[0].Property3 = value;//this updates the working proeprty that the timeline can see that the user has changed
                OnPropertyChanged();
            }
        }


        private int roationalSwitchValue = 0;
        public int RoationalSwitchValue
        {
            get
            {
                return roationalSwitchValue;
            }
            set
            {
                roationalSwitchValue = value;
                OnPropertyChanged();
            }
        }


        private bool temperatureGaugeCracked = false;
        public bool TemperatureGaugeCracked
        {
            get
            {
                return temperatureGaugeCracked;
            }
            set
            {
                temperatureGaugeCracked = value;
                OnPropertyChanged();
            }
        }

        private bool pressureGaugeCracked = false;
        public bool PressureGaugeCracked
        {
            get
            {
                return pressureGaugeCracked;
            }
            set
            {
                pressureGaugeCracked = value;
                OnPropertyChanged();
            }
        }

        public int FrameRate
        {
            get
            {
                return Model.FrameRate;
            }
            set
            {
                Model.FrameRate = value;
                OnPropertyChanged();
            }
        }
        private string time = "";
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
                messageCore.AddItemToQueueToSendToServer(new KeyValuePair<string, object>("Temperature", value));
                if (value == MaxTemperature)
                {
                    TemperatureGaugeCracked = true;
                }
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
                messageCore.AddItemToQueueToSendToServer(new KeyValuePair<string, object>("MaxTemperature", value));
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
                messageCore.AddItemToQueueToSendToServer(new KeyValuePair<string, object>("MinTemperature", value));
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
                messageCore.AddItemToQueueToSendToServer(new KeyValuePair<string, object>("DangerThresholdTemperature", value));
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
                messageCore.AddItemToQueueToSendToServer(new KeyValuePair<string, object>("Pressure", value));
                if (value == MaxPressure)
                {
                    PressureGaugeCracked = true;
                }
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
                messageCore.AddItemToQueueToSendToServer(new KeyValuePair<string, object>("MaxPressure", value));
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
                messageCore.AddItemToQueueToSendToServer(new KeyValuePair<string, object>("MinPressure", value));
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
                messageCore.AddItemToQueueToSendToServer(new KeyValuePair<string, object>("DangerThresholdPressure", value));
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
                messageCore.AddItemToQueueToSendToServer(new KeyValuePair<string, object>("Connected", value));
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
                messageCore.AddItemToQueueToSendToServer(new KeyValuePair<string, object>("IncreaseTemp", value));
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
                messageCore.AddItemToQueueToSendToServer(new KeyValuePair<string, object>("DecreaseTemp", value));
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
                messageCore.AddItemToQueueToSendToServer(new KeyValuePair<string, object>("Button3", value));
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
                messageCore.AddItemToQueueToSendToServer(new KeyValuePair<string, object>("Button4", value));
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
                messageCore.AddItemToQueueToSendToServer(new KeyValuePair<string, object>("Button5", value));
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
                messageCore.AddItemToQueueToSendToServer(new KeyValuePair<string, object>("Button6", value));
                OnPropertyChanged();
            }
        }
    }
}

