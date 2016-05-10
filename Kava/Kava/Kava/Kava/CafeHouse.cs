using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Threading;
namespace Kava
{
    public class CafeHouse : INotifyPropertyChanged
    {
        private Customer newCustumer;
        private Queue _queue;
        private Tables _tables;
        private ListBox _listBoxTable;
        private ListBox _listBoxQueue;
        public DispatcherTimer _dispatcher;
        private TimeSpan QTCompere = new TimeSpan(0, 0, 0);
        public Queue Queue
        {
            get { return _queue; }
            set
            {
                _queue = value;
                Notify("Queue");
            }
        }
        public Tables Tables
        {
            get { return _tables; }
            set
            {
                _tables = value;
                Notify("Tables");
            }
        }
        /*public DispatcherTimer Dispatcher
        {
            get { return _dispatcher;}
            set
            {
                _dispatcher = value;
                Notify("Dispatcher");
            }
        }
         * */
        public CafeHouse(ListBox queue, ListBox tables)
        {
            this._queue = new Queue();
            this._tables = new Tables();
            this._dispatcher = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            this._dispatcher.Tick += DispatherTick;
            _listBoxQueue = queue;
            _listBoxTable = tables;
            _listBoxQueue.ItemsSource = this._queue.Collection;
            _listBoxTable.ItemsSource = this._tables.Collection;


        }
        private void DispatherTick(object sender, EventArgs e)
        {
                bool isGoingToTable = false;
                CreateCuctumer();
                GoOutTable(_dispatcher);
                if (Tables.HaveEmptyTable())
                {
                    _dispatcher.Stop();
                    GoToTable();
                    isGoingToTable = true;
                }
                _listBoxTable.ItemsSource = _tables.Collection;
                _listBoxQueue.ItemsSource = _queue.Collection;

            if (isGoingToTable)
                    _dispatcher.Start();
        }
        public void CreateTables(int countTables)
        {
            for (int i = 0; i < countTables; i++) //paralel.for
            {
                Tables.Add(new Customer());
            }
        }
        public void CreateCuctumer()
        {
            if (newCustumer == null)
            {
                newCustumer = new Customer();
                QTCompere = new TimeSpan(0, 0, 0);
            }
            else
            {
                QTCompere += TimeSpan.FromSeconds(1);
                if (QTCompere == newCustumer._timeQueue)
                {
                    Queue.Add(newCustumer);
                    newCustumer = null;
                }
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        private void GoOutTable(DispatcherTimer timer)
        {
            foreach (Customer customer in Tables.Collection.ToList<Customer>())
            {
                customer.Time += TimeSpan.FromSeconds(1);
                if (customer.Time == customer.TimeTable) //переробити в нестатичну таблес
                {
                    timer.Stop();
                    Tables.GoOut(Tables, customer);
                    timer.Start();
                    return;
                }

            }


        }
        private void GoToTable()
        {
            MyCollection.GoToTable(Tables, Queue);
        }
    }
}

