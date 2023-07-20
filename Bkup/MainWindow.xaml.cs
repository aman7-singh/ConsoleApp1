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
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace Flicker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
        private const string ApiKey = "134bcd8dd47ffab42046d708f8e84fc5";
        private const string ApiEndpoint = "https://api.flickr.com/services/rest/";
        const string UserId = "1994amansingh";

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();
            //string queryString = $"https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={ApiKey}&user_id={UserId}&format=json&nojsoncallback=1";

             var queryString = $"method=flickr.photos.search&api_key={ApiKey}&tags=kitten&format=json&nojsoncallback=1&per_page=100";
            var response = await httpClient.GetAsync($"{ApiEndpoint}?{queryString}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FlickrPhotosSearchResponse>(responseContent);

            if (result?.Photos?.Photo != null && result.Photos.Photo.Any())
            {
                //var photo = result.Photos.Photo.First();

                foreach (var photo in result.Photos.Photo)
                {
                    var imageUrl = $"https://farm{photo.Farm}.staticflickr.com/{photo.Server}/{photo.Id}_{photo.Secret}.jpg";
                    var imageResponse = await httpClient.GetAsync(imageUrl);
                    var imageContent = await imageResponse.Content.ReadAsByteArrayAsync();
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(imageContent);
                    bitmapImage.EndInit();
                    ResultImage.Source = bitmapImage;
                    Thread.Sleep(5000);
                }
            }
        }
    }
    public class ImageModel
    {
        public string Url { get; set; }
    }

    public class MainViewModel
    {
        public MainViewModel()
        {
            Images = new ObservableCollection<ImageModel>();
            GetImagesFromFlickr();
        }
        private ObservableCollection<ImageModel> _images;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ImageModel> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                OnPropertyChanged(nameof(Images));
            }
        }

        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private async Task GetImagesFromFlickr()
        {
            const string apiKey = "134bcd8dd47ffab42046d708f8e84fc5";
            const string userId = "1994amansingh";

            using (HttpClient client = new HttpClient())
            {
                var apiUrl = $"https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={apiKey}&tags=.&format=json&nojsoncallback=1&per_page=100";
                //string apiUrl = $"https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={apiKey}&user_id={userId}&format=json&nojsoncallback=1";
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);

                    foreach (var photo in data.photos.photo)
                    {
                        string farmId = photo.farm.ToString();
                        string serverId = photo.server.ToString();
                        string id = photo.id.ToString();
                        string secret = photo.secret.ToString();

                        string imageUrl = $"https://farm{farmId}.staticflickr.com/{serverId}/{id}_{secret}.jpg";

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Images.Add(new ImageModel { Url = imageUrl });
                        });
                    }
                }
            }
        }
    }

    public class FlickrPhotosSearchResponse
    {
        [JsonProperty("photos")]
        public FlickrPhotos Photos { get; set; }
    }

    public class FlickrPhotos
    {
        [JsonProperty("photo")]
        public List<FlickrPhoto> Photo { get; set; }
    }

    public class FlickrPhoto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("server")]
        public string Server { get; set; }

        [JsonProperty("farm")]
        public int Farm { get; set; }
    }
}
