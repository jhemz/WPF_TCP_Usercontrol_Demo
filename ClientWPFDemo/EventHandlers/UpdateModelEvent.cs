using System;
using System.Collections;

namespace ClientWPFDemo.EventHandlers
{
    public delegate void UpdateModelHandler(object sender, UpdateModelEvent e);

    public class UpdateModelEvent : EventArgs
    {
        public UpdateModelEvent(Queue updateQueue, int frameRate)
        {
            UpdateQueue = updateQueue;
            FrameRate = frameRate;
        }
        public Queue UpdateQueue;
        public int FrameRate;
    }
   
}
