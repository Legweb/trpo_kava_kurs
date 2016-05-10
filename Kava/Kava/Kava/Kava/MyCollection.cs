using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

namespace Kava
{
    public class MyCollection : INotifyPropertyChanged
    {
        public MyCollection()
                {
                    this._collection = new ObservableCollection<Customer>();

                }
        protected ObservableCollection<Customer> _collection;
        protected int _count;
        static bool tab;
        public ObservableCollection<Customer> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                Notify("Collection");
            }
        }
        public int Cont
        {
            get { return _count; }
            set { _count = value; }
        }
        
        public void Add(Customer custumer)
        {
            Collection.Add(custumer);
        }
        public Customer this[int i]
        {
            set
            {
                _collection[i] = value;
            }
            get
            {
                return _collection[i];
            }
        }
        public void Remove(Customer customer)
        {
            //foreach (Customer c in Collection)
            //{
            //    if (c == customer)
            //    {
                    Collection.Remove(customer);
                //}
                //else
                //{
                //    throw new Exception();
                //}
            //}

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        
        static public void GoToTable(MyCollection tables, MyCollection queue)
        {
            for (int i = 0; i < tables._collection.Count; i++)
            {
                //if(tab)
                if (tables[i].Name == "Empty Table")
                {
                    if (queue.Collection.Count != 0)
                    {
                        Thread.Sleep(1000);
                        tables[i] = queue[0];
                        queue.Remove(tables[i]);
                    }
                    return;
                }
            }
        }
        
        //static private Customer GetAndDelFirst(ObservableCollection<Customer> list)
        //{
        //    Customer firstCustumer;
        //    Queue<Customer> queue = new Queue<Customer>(list);
        //    firstCustumer = queue.Peek();
        //    list = new ObservableCollection<Customer>(queue);
        //    return firstCustumer;
        //}

        //////////////
        static public void GoOut(MyCollection collection, Customer customer)
        {
            var newCustomer = new Customer(true);
            for (int i = 0; i < collection._collection.Count; i++)
            {
                if (customer == collection[i])
                {
                    collection[i] = newCustomer;
                    tab = true;
                }
            }
        }

    }
    static class Blocker
    {
        static public object block = new object();
    }
}

