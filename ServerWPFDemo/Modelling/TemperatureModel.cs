using ServerWPFDemo.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerWPFDemo.Modelling
{
    public class TemperatureModel : IModeller
    {
        //will return a queue of items that have changed in the model based on the current state of the model
        public Queue Process(modelMain model)
        {
            Queue queue = new Queue();
            if (model.IncreaseTemp)
            {
                if (model.Temperature < model.MaxTemperature)
                {
                    model.Temperature += 1;
                }
                queue.Enqueue(new KeyValuePair<string, object>("Temperature", model.Temperature));
            }
            else if (model.DecreaseTemp)
            {
                if (model.Temperature > model.MinTemperature)
                {
                    model.Temperature -= 1;
                }
                queue.Enqueue(new KeyValuePair<string, object>("Temperature", model.Temperature));
            }
            return queue;
        }
    }
}
