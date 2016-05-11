using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media;

namespace Kava
{
   public class Customer:INotifyPropertyChanged
    {   
        private string _name;
        private static int _count;
        private int _progress;
        private TimeSpan _time;
        public TimeSpan _timeQueue;
        private TimeSpan _timeTable;
        private Brush _color;

        public Brush Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                Notify("Color");
            }
        }
        public string Name
        {
            get{return _name;}
            set{
                _name=value;
                Notify("Name");
                }
        }
        /*public int _count
        {
            get { return _count; }
            set { _count = value; }
        }*/
        public int Progress
        {
            get { return _progress; }
            set { _progress = value;
            Notify("Progress");
            }
        }
        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value;
            Notify("Time");
            }
        }
        public TimeSpan TimeQueue
        {
            get { return _timeQueue; }
            set { _timeQueue = value;
                 Notify("TimeQueue");
            }
        }
        public TimeSpan TimeTable
        {
            get { return _timeTable; }
            set { _timeTable = value;
            Notify("TimeTable");
            }
        }
        
        public Customer()
        {
            this.Name= string.Format("Customer №{0}", ++_count);
            this.Time = new TimeSpan(0, 0, 0);
            this.TimeQueue = new TimeSpan(0, 0, (new Random(DateTime.Now.Millisecond+1)).Next(1, 7));
            this.TimeTable = new TimeSpan(0, 0, (new Random(DateTime.Now.Millisecond + 2)).Next(3, 10));
            this.Progress = 0;
            this.Color = new SolidColorBrush(Colors.Brown);
        }
        public Customer(bool isNull)
        {
            if (!isNull)       
            {
                this.Name = string.Format("Customer №{0}", ++_count);
                this.Time = new TimeSpan(0, 0, 0);
                this.TimeQueue = new TimeSpan(0, 0, (new Random(DateTime.Now.Millisecond + 1)).Next(1, 7));
                this.TimeTable = new TimeSpan(0, 0, (new Random(DateTime.Now.Millisecond + 2)).Next(3, 10));
                this.Progress = 0;
                this.Color = new SolidColorBrush(Colors.Brown);
            }
            else
            {
                Name = "Empty Table";
                Progress = 0;
                Time = new TimeSpan(0, 0, 0);
                TimeQueue = new TimeSpan(0, 0, 0);
                TimeTable = new TimeSpan(0, 0, 0);
                this.Color = new SolidColorBrush(Colors.Green);
            }
        }
        public override string ToString()
        {
            return _name;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(property));
            }
        }
    }
}
