using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Flicker.View
{
    /// <summary>
    /// Interaction logic for ImagePopupWindow.xaml
    /// </summary>
    public partial class ImagePopupWindow : Window
    {
        public ImagePopupWindow()
        {
            InitializeComponent();
            ImagePopUp.Focus();
            ImagePopUp.RenderTransform = new ScaleTransform(1.0, 1.0);
        }
        
        private void Image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image image)
            {
                // Reset the zoom scale of the selected image
                image.RenderTransform = new ScaleTransform(1.0, 1.0);
            }
        }

        private void Image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is Image image)
            {
                // Get the current zoom scale of the image
                ScaleTransform transform = image.RenderTransform as ScaleTransform;
                if (transform == null) return;
                double zoomScale = transform.ScaleX;

                // Calculate the new zoom scale based on the mouse wheel delta
                double zoomDelta = e.Delta > 0 ? 0.1 : -0.1;
                double newZoomScale = zoomScale + zoomDelta;

                // Apply the new zoom scale within the desired limits
                if (newZoomScale >= 0.1 && newZoomScale <= 5.0)
                {
                    transform.ScaleX = newZoomScale;
                    transform.ScaleY = newZoomScale;
                }
            }
        }
    }
}
