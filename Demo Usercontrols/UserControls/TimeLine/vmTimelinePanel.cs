namespace Demo_Usercontrols.UserControls.TimeLine
{
    public class vmTimelinePanel : vmTimelineBase
    {
        private bool property1;
        [Property(PropertyType.Internal)]
        public bool Property1
        {
            get
            {
                return property1;
            }
            set
            {
                property1 = value;
                OnPropertyChanged();
            }
        }

        private bool property1_DefaultValue;
        [Property(PropertyType.Display, "Property1")]
        public bool Property1_DefaultValue
        {
            get
            {
                return property1_DefaultValue;
            }
            set
            {
                property1_DefaultValue = value;
                OnPropertyChanged();
            }
        }

        private bool property2;
        [Property(PropertyType.Internal)]
        public bool Property2
        {
            get
            {
                return property2;
            }
            set
            {
                property2 = value;
                OnPropertyChanged();
            }
        }
        private bool property2_DefaultValue;
        [Property(PropertyType.Display, "Property2")]
        public bool Property2_DefaultValue
        {
            get
            {
                return property2_DefaultValue;
            }
            set
            {
                property2_DefaultValue = value;
                OnPropertyChanged();
            }
        }

        private bool property3;
        [Property(PropertyType.Internal)]
        public bool Property3
        {
            get
            {
                return property3;
            }
            set
            {
                property3 = value;
                OnPropertyChanged();
            }
        }
        private bool property3_DefaultValue;
        [Property(PropertyType.Display, "Property3")]
        public bool Property3_DefaultValue
        {
            get
            {
                return property3_DefaultValue;
            }
            set
            {
                property3_DefaultValue = value;
                OnPropertyChanged();
            }
        }

        private double property4;
        [Property(PropertyType.Internal)]
        public double Property4
        {
            get
            {
                return property4;
            }
            set
            {
                property4 = value;
                OnPropertyChanged();
            }
        }
        private double property4_DefaultValue;
        [Property(PropertyType.Display, "Property4")]
        public double Property4_DefaultValue
        {
            get
            {
                return property4_DefaultValue;
            }
            set
            {
                property4_DefaultValue = value;
                OnPropertyChanged();
            }
        }
    }
}
