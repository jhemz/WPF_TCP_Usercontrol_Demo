using ServerWPFDemo.Models;
using ServerWPFDemo.ViewModels;
using System;
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

        public Queue Process(Queue queue)
        {
            //initilaize the return object
            Queue returnQueue;

            //initilaize the updateQueue object
            Queue updateQueue;

            App currentApp = Application.Current as App;
            modelMain model = ((vmMain)currentApp.MainWindow.DataContext).Model;

            //UPDATE LOCAL MODEL  - based on whats in the queue
            UpdateModel(queue, model, out model, out queue);


            //CALCULATE CHANGES TO MODEL
            CalculateChanges((List<IModeller>)modelClasses, model, out updateQueue);


            //RE-UPDATE LOCAL MODEL
            UpdateModel(updateQueue, model, out model, out returnQueue);


            //check for any commands issued by server to send to client, and add them to return queue
            returnQueue.Enqueue(new KeyValuePair<string, object>("CommandDisplay", DateTime.Now.ToString("HH:mm:ss")));

            //UPDATE LOCAL UI - pass the refreshed model to the viewmodel for the view
            ((vmMain)currentApp.MainWindow.DataContext).Model = model;


            //return any changes back to tcp service to return to client
            return returnQueue;
        }

        private void UpdateModel(Queue queue, modelMain model, out modelMain returnModel, out Queue returnQueue)
        {
            returnQueue = new Queue();
            returnModel = model;
            while (queue.Count > 0)
            {
                KeyValuePair<string, object> queueValue = (KeyValuePair<string, object>)queue.Dequeue();
                returnModel.GetType().GetProperty(queueValue.Key).SetValue(returnModel, queueValue.Value);
                returnQueue.Enqueue(queueValue);
            }
        }

        private void CalculateChanges(List<IModeller> modelClasses, modelMain model, out Queue returnQueue)
        {
            returnQueue = new Queue();

            foreach (IModeller modeller in modelClasses)
            {
                Queue processedQueue = modeller.Process(model);
                while (processedQueue.Count > 0)
                {
                    var queueValue = processedQueue.Dequeue();
                    returnQueue.Enqueue(queueValue);
                }
            }
        }
    }
}
