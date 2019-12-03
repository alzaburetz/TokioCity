using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TokioCity.Models
{
    [Serializable]
    public class Restraunt: INotifyPropertyChanged
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public List<string> stations { get; set; }
        public string photo { get; set; }
        public List<string> photos { get; set; }
        public string phone { get; set; }
        public string worktime { get; set; }
        public string city { get; set; }

        [NonSerialized]
        private double distance;
        public double Distance
        { 
            get
            {
                return distance;
            }
            set
            {
                distance = value;
                OnPropertyChanged("Distance");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
