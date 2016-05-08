using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace Kava
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        public CafeHouse CafeHouse { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = CafeHouse;
            
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CafeHouse = new CafeHouse(ListBoxQueue, ListBoxTables);
            CafeHouse.CreateTables(5);
            CafeHouse._dispatcherAddQueue.Start();
            CafeHouse._dispatcherRemoveTable.Start();
            
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CafeHouse._dispatcherAddQueue.Stop();
            CafeHouse._dispatcherRemoveTable.Stop();
        }
    }
}
