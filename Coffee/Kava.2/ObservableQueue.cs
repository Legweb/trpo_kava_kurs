using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Threading;

namespace Kava._2
{
    public class ObservableQueue<T> : INotifyCollectionChanged, IEnumerable
    {
        private readonly Queue<T> queue;

        public ObservableQueue()
        {
            queue = new Queue<T>();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        public void Enqueue(T item)
        {
            queue.Enqueue(item);
            NotifyCollectionChanged(NotifyCollectionChangedAction.Add, item, queue.Count - 1);
        }

        public T Dequeue()
        {
            var item = queue.Dequeue();
            NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, item, 0);
            return item;
        }

        public T Peek()
        {
            return queue.Peek();
        }

        private void NotifyCollectionChanged(NotifyCollectionChangedAction action, T item, int index)
        {
            var args = new NotifyCollectionChangedEventArgs(action, new List<T> {item}, index);
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                CollectionChanged?.Invoke(this, args);
            }));
        }
    }
}