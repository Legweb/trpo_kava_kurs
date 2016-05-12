using System;
using System.ComponentModel;
using System.Threading;

namespace Kava._2
{
    public class Table : INotifyPropertyChanged
    {
        private Visitor currentVisitor;

        private TimeSpan? timeLeft;

        private readonly Mutex tableReleasMutex;

        public Table(string name)
        {
            Name = name;
            tableReleasMutex = new Mutex();
        }

        public WaitHandle TableHandle
        {
            get { return tableReleasMutex; }
        }

        public string Name { get; }

        public TimeSpan? TimeLeft
        {
            get { return timeLeft; }
            set
            {
                if (timeLeft != value)
                {
                    timeLeft = value;
                    OnPropertyChanged("TimeLeft");
                }
            }
        }

        public Visitor CurrentVisitor
        {
            get { return currentVisitor; }
            private set
            {
                if (currentVisitor != value)
                {
                    currentVisitor = value;
                    OnPropertyChanged("CurrentVisitor");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void EnterTable(Visitor visitor, TimeSpan interval)
        {
            CurrentVisitor = visitor;
            TimeLeft = interval;
        }

        public void ExitTable()
        {
            CurrentVisitor = null;
            TimeLeft = null;
            tableReleasMutex.ReleaseMutex();
        }
    }
}