using System;
using System.Collections;

namespace ClientWPFDemo.EventHandlers
{
    public delegate void UpdateModelHandler(object sender, UpdateModelEvent e);

    public class UpdateModelEvent : EventArgs
    {
        public UpdateModelEvent(Queue updateQueue)
        {
            UpdateQueue = updateQueue;
        }
        public Queue UpdateQueue;
    }
   
}
