using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Kava._2
{
    public partial class MainWindow
    {
        private readonly Caffe caffe;

        private Thread visitorsGenerateThread;

        private readonly TimeSpan timerInterval;

        public MainWindow()
        {
            InitializeComponent();
            timerInterval = TimeSpan.FromSeconds(1);
            var timer = new DispatcherTimer
            {
                Interval = timerInterval
            };
            
            timer.Tick += Timer_Tick;
            caffe = new Caffe();
            visitorsQueue.ItemsSource = caffe.VisitorsQueue;
            tablesList.ItemsSource = caffe.Tables;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var table in caffe.Tables)
            {
                if (table.TimeLeft.HasValue)
                {
                    table.TimeLeft = table.TimeLeft - timerInterval;
                }
            }
        }

        private void GenerateVisitorsProc()
        {
            while (true)
            {
                var interval = App.RandomizerNext(Constants.minVisitorAppearIntervalSeconds, Constants.maxVisitorAppearIntervalSeconds);
                Thread.Sleep(TimeSpan.FromSeconds(interval));
                var visitor = new Visitor(caffe);
                visitor.EnterTheCaffe();
            }
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;
            visitorsGenerateThread = new Thread(GenerateVisitorsProc);
            visitorsGenerateThread.Start();
        }

        private void StopButton_OnClick(object sender, RoutedEventArgs e)
        {
            StopButton.IsEnabled = false;
            StartButton.IsEnabled = true;
            visitorsGenerateThread.Abort();
        }
    }
}