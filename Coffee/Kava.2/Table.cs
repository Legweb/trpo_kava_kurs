using System.ComponentModel;
using System.Threading;

namespace Kava._2
{
    public class Table : INotifyPropertyChanged
    {
        private Visitor currentVisitor;

        private readonly Mutex tableReleasEvent;

        public Table(string name)
        {
            Name = name;
            tableReleasEvent = new Mutex();
        }

        public WaitHandle TableHandle => tableReleasEvent;

        public string Name { get; }

        public Visitor CurrentVisitor
        {
            get { return currentVisitor; }
            set
            {
                if (currentVisitor != value)
                {
                    currentVisitor = value;
                    OnPropertyChanged(nameof(CurrentVisitor));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ReleaseTable()
        {
            tableReleasEvent.ReleaseMutex();
        }
    }
}