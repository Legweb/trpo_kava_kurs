using System;
using System.Threading;
using System.Windows;

namespace Kava._2
{
    public partial class MainWindow
    {
        private readonly Caffe caffe;

        private Thread visitorsGenerateThread;

        public MainWindow()
        {
            InitializeComponent();
            caffe = new Caffe();
            visitorsQueue.ItemsSource = caffe.VisitorsQueue;
            tablesList.ItemsSource = caffe.Tables;
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