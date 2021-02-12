using ClientWPFDemo.Models;
using ClientWPFDemo.Services;
using System;
using System.Collections;
using System.Collections.Generic;
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

    public int roationalSwitchValue = 0;
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


    public bool temperatureGaugeCracked = false;
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

    public bool pressureGaugeCracked = false;
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

