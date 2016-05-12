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

        public string Name { get; private set; }

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


            var intervalSeconds = App.RandomizerNext(Constants.minCoffeeConsumptionTime, Constants.maxCoffeeConsumptionTime);
            var interval = TimeSpan.FromSeconds(intervalSeconds);
            caffe.Tables[index].EnterTable(this, interval);

            Thread.Sleep(interval);

            caffe.Tables[index].ExitTable();
        }
    }
}