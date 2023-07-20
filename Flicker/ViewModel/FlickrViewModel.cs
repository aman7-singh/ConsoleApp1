using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Flicker.Model;
using Newtonsoft.Json;

namespace Flicker.ViewModel
{
    public class FlickrViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int currentPage = 1;
        private const int itemsPerQuery = 150;
        private const int itemsPerPage = 14;
        private const string _apiKey = "134bcd8dd47ffab42046d708f8e84fc5";
        private const string _apiEndpoint = "https://api.flickr.com/services/rest/";
        private const string _userId = "1994amansingh";
        private ICommand _searchCommand;
        private ICommand _previousPageCommand;
        private ICommand _nextPageCommand;
        private string _searchTextBox;
        private List<FlickrPhoto> searchResults;
        private ObservableCollection<FlickrPhoto> _flickrPhotos;
        private bool _isEnableNextPageButton = false;
        private bool _isEnablePreviousPageButton;
        private string _paginationTextBlock;

        public FlickrViewModel()
        {
            SearchCommand = new RelayCommand(async o => await Task.Run(() => OnSearchButton_Click()), o => true);
            PreviousPageCommand = new RelayCommand(async o => await Task.Run(() => OnPreviousButton_Click()), o => true);
            NextPageCommand = new RelayCommand(async o => await Task.Run(() => OnNextButton_Click()), o => true);
        }

        #region public Properties
        public string PaginationTextBlock
        {
            get { return _paginationTextBlock; }
            set
            {
                _paginationTextBlock = value;
                OnPropertyChanged(nameof(PaginationTextBlock));
            }
        }
        public bool IsEnablePreviousPageButton
        {
            get { return _isEnablePreviousPageButton; }
            set
            {
                _isEnablePreviousPageButton = value;
                OnPropertyChanged(nameof(IsEnablePreviousPageButton));
            }
        }
        public bool IsEnableNextPageButton
        {
            get { return _isEnableNextPageButton; }
            set
            {
                _isEnableNextPageButton = value;
                OnPropertyChanged(nameof(IsEnableNextPageButton));
            }
        }
        public ObservableCollection<FlickrPhoto> FlickrPhotos
        {
            get { return _flickrPhotos; }
            set
            {
                _flickrPhotos = value;
                OnPropertyChanged(nameof(FlickrPhotos));

            }
        }
        public string SearchTextBox
        {
            get { return _searchTextBox; }
            set
            {
                _searchTextBox = value;
                OnPropertyChanged(nameof(SearchTextBox));
            }
        }
        public ICommand NextPageCommand
        {
            get { return _nextPageCommand; }
            set { _nextPageCommand = value; }
        }
        public ICommand PreviousPageCommand
        {
            get { return _previousPageCommand; }
            set { _previousPageCommand = value; }
        }
        public ICommand SearchCommand
        {
            get { return _searchCommand; }
            set { _searchCommand = value; }
        }
        private Dictionary<int, List<FlickrPhoto>> imagesPerPage= new Dictionary<int, List<FlickrPhoto>>();
        #endregion

        #region Private Method
        private async void OnSearchButton_Click()
        {
            currentPage = 1;
            await PerformSearch();
            FlickrPhotos = new ObservableCollection<FlickrPhoto>(imagesPerPage[currentPage]);
            IsEnableNextPageButton = currentPage < Math.Ceiling((double)imagesPerPage.Count);
            IsEnablePreviousPageButton = currentPage > 1;
            PaginationTextBlock = $"Page {currentPage} of {Math.Ceiling((double)searchResults.Count / itemsPerPage)}";
        }
        private void OnPreviousButton_Click()
        {
            if (currentPage > 1)
            {
                FlickrPhotos = new ObservableCollection<FlickrPhoto>(imagesPerPage[--currentPage]);
                IsEnableNextPageButton = currentPage < Math.Ceiling((double)imagesPerPage.Count);
                IsEnablePreviousPageButton = currentPage > 1;
                PaginationTextBlock = $"Page {currentPage} of {Math.Ceiling((double)searchResults.Count / itemsPerPage)}";
            }
        }
        private void OnNextButton_Click()
        {
            FlickrPhotos = new ObservableCollection<FlickrPhoto>(imagesPerPage[++currentPage]);
            IsEnableNextPageButton = currentPage < Math.Ceiling((double)imagesPerPage.Count);
            IsEnablePreviousPageButton = currentPage > 1;
            PaginationTextBlock = $"Page {currentPage} of {Math.Ceiling((double)searchResults.Count / itemsPerPage)}";
        }
        private async Task PerformSearch()
        {
            var searchQuery = SearchTextBox;
            var httpClient = new HttpClient();
            var queryString = $"method=flickr.photos.search&api_key={_apiKey}&text={searchQuery}&format=json&nojsoncallback=1&per_page={itemsPerQuery}&page={currentPage}";
            var response = await httpClient.GetAsync($"{_apiEndpoint}?{queryString}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FlickrPhotosSearchResponse>(responseContent);

            if (result?.Photos?.Photo != null && result.Photos.Photo.Any())
            {
                searchResults = result.Photos.Photo;
                var items = searchResults.Select(photo => new FlickrPhoto
                {
                    Title = photo.Title,
                    Description = photo.Description ?? "no description found",
                    ImageUrl = $"https://farm{photo.Farm}.staticflickr.com/{photo.Server}/{photo.Id}_{photo.Secret}.jpg"
                }).ToList();
                FlickrPhotos =new ObservableCollection<FlickrPhoto>(items);
                await Task.Run(()=>ArrangePages(items));
            }
            else
            {
                FlickrPhotos = new ObservableCollection<FlickrPhoto>(){ new FlickrPhoto() { Title = "No Image found" } };
            }
        }
        private void ArrangePages(List<FlickrPhoto> images)
        {
            var numberOfPages = Math.Ceiling((double)searchResults.Count / itemsPerPage);
            for(int i=0; i<= numberOfPages; i++)
            {
                imagesPerPage[i+1] = images.Skip(i*itemsPerPage).Take(itemsPerPage).ToList();
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
