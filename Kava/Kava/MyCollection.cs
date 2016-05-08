using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Kava
{
    public class MyCollection : INotifyPropertyChanged
    {

        protected ObservableCollection<Customer> _collection;
        protected int _count;

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
        public MyCollection()
        {
            this._collection = new ObservableCollection<Customer>();

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
            foreach (Customer c in Collection)
            {
                if (c == customer)
                {
                    Collection.Remove(customer);
                }
                else
                {
                    throw new Exception();
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
        static public void GoOut(MyCollection collection, Customer customer)//null obj customer in table
        {
            
            var newCustomer = new Customer(false)
                {
                    Name = "Empty Table",
                    Progress = 0,
                    Time = new TimeSpan(0, 0, 0),
                    TimeQueue = new TimeSpan(0, 0, 0),
                    TimeTable = new TimeSpan(0, 0, 0)

                };
            for (int i = 0; i < 5; i++)
            {
                if (customer.Equals(collection[i]))

                    collection[i] = newCustomer;

            }
        }


    }

}

