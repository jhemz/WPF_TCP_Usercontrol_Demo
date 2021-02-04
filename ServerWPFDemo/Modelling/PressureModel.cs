using ServerWPFDemo.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerWPFDemo.Modelling
{
    public class PressureModel : IModeller
    {
        //will return a queue of items that have changed in the model based on the current state of the model
        public Queue Process(modelMain model)
        {
            Queue queue = new Queue();
            queue.Enqueue(new KeyValuePair<string, double>("Pressure", model.Temperature / 1.5));
            return queue;
        }
    }
}
