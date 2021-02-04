using ServerWPFDemo.Models;
using ServerWPFDemo.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ServerWPFDemo.Modelling
{
    public class ShipModel
    {
        IList<IModeller> modelClasses;

        public ShipModel()
        {
            modelClasses = new List<IModeller>();
            modelClasses.Add(new TemperatureModel());
            modelClasses.Add(new TemperatureAlertModel());
            modelClasses.Add(new PressureModel());
            modelClasses.Add(new PressureAlertModel());
        }

        public async Task<Queue> Process(Queue queue)
        {
            //initilaize the return object
            Queue returnQueue = new Queue();

            //dispatch as we need ui items and we are in a thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                App currentApp = Application.Current as App;

                //update model based on queue
                modelMain model = ((vmMain)currentApp.MainWindow.DataContext).Model;

                while (queue.Count > 0)
                {
                    KeyValuePair<string, object> queueValue = (KeyValuePair<string, object>)queue.Dequeue();
                    model.GetType().GetProperty(queueValue.Key).SetValue(model, queueValue.Value);
                }

                //*****
                //this may get slow, and is technically running on the ui thread, so not great to do here
                //*****

                //calculations to modify model
                foreach (IModeller modeller in modelClasses)
                {
                    Queue temperatureQueue = modeller.Process(model);
                    foreach (var item in temperatureQueue)
                    {
                        returnQueue.Enqueue(temperatureQueue);
                    }
                }

                //update ui
                 ((vmMain)currentApp.MainWindow.DataContext).Model = model;

            });

            //return any changes back to tcp service to return to client
            return returnQueue;
        }
    }
}
