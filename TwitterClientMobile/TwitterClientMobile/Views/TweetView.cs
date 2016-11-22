using Newtonsoft.Json;
using System;
using System.Diagnostics;
using TwitterClientMobile.JsonFormatterTypes;
using TwitterClientMobile.Utilities;
using TwitterClientMobile.ViewModels;
using Xamarin.Forms;

namespace TwitterClientMobile.Views
{
    public class TweetView : ContentPage
    {
        private TweetViewModel _viewModel = new TweetViewModel();
        private const string StrAutoCompleteGoogleApi = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=";
        private const string StrGoogleApiKey = "AIzaSyBnAoO3VNVUco4MXf3enYCVBg6ZnpY49NE"; // Just for demo's purpose, I put it here.
        private const string StrGeoCodingUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=";


        /// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
        /// <remarks>To be added.</remarks>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Title = "Twitter Client Mobile";
            BackgroundColor = Color.White;
            BindingContext = _viewModel;

            var searchBarSearchTerm = new SearchBar
            {
                Placeholder = "Enter a location to get tweets",
                FontSize = 18,
                PlaceholderColor = Color.Gray,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            searchBarSearchTerm.SearchButtonPressed += SearchBarSearchTerm_SearchButtonPressed;

            var dataTemplateTweets = new DataTemplate(() =>
            {
                var lblScreenName = new Label
                {
                    TextColor = Color.FromHex("#2196F3"),
                    FontSize = 22
                };
                var lblText = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 18
                };
                var image = new Image
                {
                    WidthRequest = 60,
                    HeightRequest = 60,
                    VerticalOptions = LayoutOptions.Start
                };

                var mediaImage = new Image();
                mediaImage.HorizontalOptions = LayoutOptions.FillAndExpand;

                lblScreenName.SetBinding(Label.TextProperty, new Binding("ScreenName"));
                lblText.SetBinding(Label.TextProperty, new Binding("Text"));
                image.SetBinding(Image.SourceProperty, new Binding("ImageUrl"));
                mediaImage.SetBinding(Image.SourceProperty, new Binding("MediaUrl"));

                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Padding = new Thickness(0, 5),
                        Children =
                        {
                            image,
                            new StackLayout
                            {
                                Orientation = StackOrientation.Vertical,
                                Children =
                                {
                                    lblScreenName,
                                    lblText,
                                    mediaImage
                                }
                            }
                        }
                    }
                };
            });


            var listViewTweets = new ListView
            {
                HasUnevenRows = true
            };
            listViewTweets.SetBinding(ListView.ItemsSourceProperty, "Tweets");
            listViewTweets.ItemTemplate = dataTemplateTweets;

            Content = new StackLayout
            {
                Padding = new Thickness(5, 10),
                Children =
                {
                    searchBarSearchTerm,
                    listViewTweets
                }
            };
        }

        /// <summary>
        /// Invoked when user click on search button, and this event handler will consume Google Places Api,
        /// and then populate view model
        /// </summary>
        /// <param name="sender">The object sender</param>
        /// <param name="e">The events args</param>
        private async void SearchBarSearchTerm_SearchButtonPressed(object sender, EventArgs e)
        {
            var searchBar = sender as SearchBar;

            if (!string.IsNullOrWhiteSpace(searchBar?.Text))
            {
                var result = await DownloadHelper.DownloadStringAsync(StrAutoCompleteGoogleApi + searchBar.Text + "&key=" + StrGoogleApiKey);
                _viewModel.GoogleMapPlace = JsonConvert.DeserializeObject<GoogleMapPlace>(result);

                if (_viewModel.GoogleMapPlace.Status != "OK")
                {
                    await DisplayAlert("Invalid Location", "Something wrong with your search term.\nPlease try to enter a valid location!!!", "Ok");
                    return;
                }

                // Because I am stucked in building an autocomplete control, I decided to get the first address.
                var addressUrl = StrGeoCodingUrl + _viewModel.GoogleMapPlace.Predictions[0].Description;
                var addressGeo = await DownloadHelper.DownloadStringAsync(addressUrl);
                _viewModel.GeoCode = JsonConvert.DeserializeObject<GeoCode>(addressGeo);

                var lat = _viewModel.GeoCode.Results[0].Geometry.Location.Lat;
                var lng = _viewModel.GeoCode.Results[0].Geometry.Location.Lng;

                try
                {
                    await _viewModel.InitTweetsByGeoLocation(lat, lng);

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Something goes wrong while populating the view model!!!", "Ok");
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}