using ClientWPFDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPFDemo.ViewModels
{
    public class vmMain : vmBase
    {
        private modelMain model = new modelMain();
        public modelMain Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
                OnPropertyChanged();
            }
        }

       
        public bool Connected
        {
            get
            {
                return Model.Connected;
            }
            set
            {
                Model.Connected = value;
                OnPropertyChanged();
            }
        }

        
        public bool Button1
        {
            get
            {
                return Model.Button1;
            }
            set
            {
                Model.Button1 = value;
                OnPropertyChanged();
            }
        }

       
        public bool Button2
        {
            get
            {
                return Model.Button2;
            }
            set
            {
                Model.Button2 = value;
                OnPropertyChanged();
            }
        }

       
        public bool Button3
        {
            get
            {
                return Model.Button3;
            }
            set
            {
                Model.Button3 = value;
                OnPropertyChanged();
            }
        }

    
        public bool Button4
        {
            get
            {
                return Model.Button4;
            }
            set
            {
                Model.Button4 = value;
                OnPropertyChanged();
            }
        }

       
        public bool Button5
        {
            get
            {
                return Model.Button5;
            }
            set
            {
                Model.Button5 = value;
                OnPropertyChanged();
            }
        }

       
        public bool Button6
        {
            get
            {
                return Model.Button6;
            }
            set
            {
                Model.Button6 = value;
                OnPropertyChanged();
            }
        }
    }
}

