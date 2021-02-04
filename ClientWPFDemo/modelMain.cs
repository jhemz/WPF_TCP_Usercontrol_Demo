using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPFDemo.Models
{
    public class modelMain
    {
        public double Temperature { get; set; }
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
        public double DangerThresholdTemperature { get; set; }
        public bool DangerTemperatureAlert { get; set; }

        public double Pressure { get; set; }
        public double MaxPressure { get; set; }
        public double MinPressure { get; set; }
        public double DangerThresholdPressure { get; set; }
        public bool DangerPressureAlert { get; set; }

        public bool Connected { get; set; }
        public bool IncreaseTemp { get; set; }
        public bool DecreaseTemp { get; set; }
        public bool Button3 { get; set; }
        public bool Button4 { get; set; }
        public bool Button5 { get; set; }
        public bool Button6 { get; set; }
    }
}
