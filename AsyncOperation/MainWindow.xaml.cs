using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncOperation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("############### sync breakfast is started. ######################");
            SyncBreakFast.PrepareBreakFast();
            Console.WriteLine("############### sync breakfast is done. ######################");
        }

        private async void AsyncButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("############### async breakfast is started. ######################");
            await AsyncBreakfast.PrepareBreakFast();
            Console.WriteLine("############### async breakfast is done. ######################");
        }
        private async void AsyncFastButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("############### async fast breakfast is started. ######################");
            await AsyncBreakfastFaster.PrepareBreakfast();
            Console.WriteLine("############### async fast breakfast is done. ######################");
        }
    }
}
