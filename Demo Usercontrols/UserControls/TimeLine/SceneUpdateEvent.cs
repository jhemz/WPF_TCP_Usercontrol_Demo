using System;

namespace Demo_Usercontrols.UserControls.TimeLine
{
    public enum SceneEvent {Started, Ended }
    public delegate void SceneUpdateHandler(object sender, SceneUpdateEvent e);

    public class SceneUpdateEvent : EventArgs
    {
        public SceneUpdateEvent(SceneEvent sceneEvent, vmTimelineScene scene)
        {
            Scene = scene;
            SceneEvent = sceneEvent;
        }
        public vmTimelineScene Scene;
        public SceneEvent SceneEvent;
    }
   
}
