using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

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
        
        public Customer()//use when we create obj in queue
        {
            this.Name= string.Format("Customer №{0}", ++_count);
            this.Time = new TimeSpan(0, 0, 0);
            this.TimeQueue = new TimeSpan(0, 0, (new Random(1)).Next(1, 7));
            this.TimeTable = new TimeSpan(0, 0, (new Random(2)).Next(3, 10));
            this.Progress = 0;
        }
        public Customer(bool bo)//use when we null some table
        {
            if (!bo)
            {
                
                this.Progress = 0;
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
