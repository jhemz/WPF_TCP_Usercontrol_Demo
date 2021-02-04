using ServerWPFDemo.Models;
using System.Collections;
using System.Collections.Generic;

namespace ServerWPFDemo.Modelling
{
    public class TemperatureAlertModel : IModeller
    {
        public Queue Process(modelMain model)
        {
            Queue queue = new Queue();

            if (model.Temperature > model.DangerThresholdTemperature)
            {
                model.DangerTemperatureAlert = true;
            }
            else
            {
                model.DangerTemperatureAlert = false;
            }
            queue.Enqueue(new KeyValuePair<string, bool>("DangerTemperatureAlert", model.DangerTemperatureAlert));


            return queue;
        }
    }
}
