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
    public class CafeHouse:INotifyPropertyChanged
    {
        private Customer newCustumer;
        private Queue _queue;
        private Tables _tables;
        private ListBox _listBoxTable;
        private ListBox _listBoxQueue;
        public DispatcherTimer _dispatcherAddQueue;
        public DispatcherTimer _dispatcherRemoveTable;
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
            get { return _tables;}
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
            this._dispatcherAddQueue = new DispatcherTimer(){Interval = TimeSpan.FromSeconds(1)};
            this._dispatcherAddQueue.Tick += DispatherAddQueueTick;

            this._dispatcherRemoveTable = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            this._dispatcherRemoveTable.Tick += DispatherRemoveTableTick;

            _listBoxQueue = queue;
            _listBoxTable = tables;        
            _listBoxQueue.ItemsSource = this._queue.Collection;
            _listBoxTable.ItemsSource = this._tables.Collection;

            
        }
        private void DispatherAddQueueTick(object sender,EventArgs e) 
        {
            CreateCuctumer();
            
           
        }
        private void DispatherRemoveTableTick(object sender, EventArgs e)
        {
           
            GoOutTable();

        }
        public void CreateTables(int countTables)
        {
            for(int i = 0; i < countTables; i++) //paralel.for
            {
                Tables.Add(new Customer());
            }
        }
        public void CreateCuctumer()//create custumer in queue(check when we must create new custimer)
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
        public event PropertyChangedEventHandler  PropertyChanged;

        private void Notify(string property)
        {
            if (PropertyChanged != null)
            {
                 PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        private void GoOutTable()//add 1 second to time customer and chek time for siting on tables
        {
            for (int i=0; i < 5;i++)
            {
                var customer = Tables[i];
                customer.Time += TimeSpan.FromSeconds(1);
                if (customer.Time == customer.TimeTable) //переробити в нестатичну таблес
                {
                    Tables.GoOut(Tables, customer);
                    customer.Time = new TimeSpan(0, 0, 0);
                }

            }
            
            
        }
    }
}

