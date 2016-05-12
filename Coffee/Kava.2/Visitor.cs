using System;
using System.Linq;
using System.Threading;

namespace Kava._2
{
    public class Visitor
    {
        private readonly Caffe caffe;

        private readonly Thread workerThread;

        public Visitor(Caffe caffe)
        {
            this.caffe = caffe;
            workerThread = new Thread(WorkerProc);
            Name = string.Format("{0}, {1}", App.GetRandomName(), App.RandomizerNext(18, 65));
        }

        public string Name { get; }

        public void EnterTheCaffe()
        {
            workerThread.Start();
        }

        private void WorkerProc()
        {
            lock (caffe.VisitorsQueue)
            {
                caffe.VisitorsQueue.Enqueue(this);
            }

            while (true)
            {
                Thread.Sleep(1000); //TODO: fix this
                lock (caffe.VisitorsQueue)
                {
                    var isFirstInQueue = caffe.VisitorsQueue.Peek() == this;
                    if (isFirstInQueue)
                    {
                        break;
                    }
                }
            }

            var tableHandles = caffe.Tables.Select(t => t.TableHandle).ToArray();
            var index = WaitHandle.WaitAny(tableHandles);
            lock (caffe.VisitorsQueue)
            {
                caffe.VisitorsQueue.Dequeue();
            }

            caffe.Tables[index].CurrentVisitor = this;

            var interval = App.RandomizerNext(Constants.minCoffeeConsumptionTime, Constants.maxCoffeeConsumptionTime);
            Thread.Sleep(TimeSpan.FromSeconds(interval));

            caffe.Tables[index].CurrentVisitor = null;
            caffe.Tables[index].ReleaseTable();
        }
    }
}