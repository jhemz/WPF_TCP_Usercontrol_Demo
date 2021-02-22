using System.Collections.Generic;
using System.Windows.Media;

namespace Demo_Usercontrols.UserControls.TimeLine
{
    public class vmTimelineScene : vmTimelineBase
    {
        public vmTimelineScene()
        {
            Panels = new List<vmTimelinePanel>();
            vmTimelinePanel newPanel = new vmTimelinePanel();
            newPanel.Header = "Panel";
            newPanel.Property1 = false;
            newPanel.Property2 = false;
            newPanel.Property3 = false;
            Panels.Add(newPanel);

            Start = 0;
            End = 5;
            Color = Colors.Blue;
        }

        private Color color;
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                OnPropertyChanged();
            }
        }

        private double start;
        public double Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
                OnPropertyChanged();
            }
        }

        private double end;
        public double End
        {
            get
            {
                return end;
            }
            set
            {
                end = value;
                OnPropertyChanged();
            }
        }

        private List<vmTimelinePanel> panels;
        public List<vmTimelinePanel> Panels
        {
            get
            {
                return panels;
            }
            set
            {
                panels = value;
                OnPropertyChanged();
            }
        }

    }
}
