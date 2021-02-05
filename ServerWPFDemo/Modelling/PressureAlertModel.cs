using ServerWPFDemo.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerWPFDemo.Modelling
{
    public class PressureAlertModel : IModeller
    {
        //will return a queue of items that have changed in the model based on the current state of the model
        public Queue Process(modelMain model)
        {
            Queue queue = new Queue();

            if (model.Pressure > model.DangerThresholdPressure)
            {
                model.DangerPressureAlert = true;
            }
            else
            {
                model.DangerPressureAlert = false;
            }
            queue.Enqueue(new KeyValuePair<string, object>("DangerPressureAlert", model.DangerPressureAlert));


            return queue;
        }
    }
}
