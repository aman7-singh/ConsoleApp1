using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Flicker.View;
using Newtonsoft.Json;

namespace Flicker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Get the selected image's source
            var selectedImageSource = ((Image)sender).Source;

            // Create an instance of the ImagePopupWindow
            var popupWindow = new ImagePopupWindow();

            // Set the DataContext of the popup window to the selected image source
            popupWindow.DataContext = selectedImageSource;

            // Show the popup window as a dialog
            popupWindow.ShowDialog();
        }
    }
}
