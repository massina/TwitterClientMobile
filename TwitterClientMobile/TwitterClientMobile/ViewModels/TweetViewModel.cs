using LinqToTwitter;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TwitterClientMobile.Annotations;
using TwitterClientMobile.JsonFormatterTypes;
using TwitterClientMobile.Models;

namespace TwitterClientMobile.ViewModels
{
    public class TweetViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Tweet> _tweets;
        public List<Tweet> Tweets
        {
            get { return _tweets; }
            set
            {
                if (_tweets == value)
                    return;
                _tweets = value;
                OnPropertyChanged(nameof(Tweets));
            }
        }

        private GoogleMapPlace _googleMapPlace;
        public GoogleMapPlace GoogleMapPlace
        {
            get { return _googleMapPlace; }
            set
            {
                if (_googleMapPlace == value)
                    return;
                _googleMapPlace = value;
                OnPropertyChanged(nameof(GoogleMapPlace));
            }
        }

        private GeoCode _geoCodeJSONClass;
        public GeoCode GeoCodeJSONClass
        {
            get { return _geoCodeJSONClass; }
            set
            {
                if (_geoCodeJSONClass == value)
                    return;
                _geoCodeJSONClass = value;
                OnPropertyChanged(nameof(GeoCodeJSONClass));
            }
        }

        public async Task InitTweets(double lat = 0, double lng = 0, int radious = 5, string searchTerm = "@")
        {
            var auth = new SingleUserAuthorizer()
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = "dkYmFF7c4dLXaUXpalKKzItcG",
                    ConsumerSecret = "XPPPJpSNEIbUErbe6Aaw4VMENsrH2NtOLl2J9Wus0zXy3dklBI",
                    OAuthToken = "192372509-7mYW4GXDwZaTyzc7LFGFw8NfKC1T9OrGDUyqhrka",
                    OAuthTokenSecret = "MK1Tj4xKcnvd86MdvBLqh53H5qQXlEfsPQeTJtQ2WE1gv",
                    UserID = 192372509
                }
            };

            string geoCode = $"{lat},{lng},{radious}km";

            await auth.AuthorizeAsync();

            var ctx = new TwitterContext(auth);

            var searchResponse = await (from search in ctx.Search
                                        where search.Type == SearchType.Search &&
                                              search.Query == searchTerm &&
                                              search.GeoCode == geoCode &&
                                              //search.IncludeEntities == true &&
                                              search.Count == 200
                                        select search).SingleOrDefaultAsync();


            Tweets = (from tweet in searchResponse.Statuses
                      where tweet?.Entities?.MediaEntities?.FirstOrDefault()?.MediaUrl != null
                      select new Tweet
                      {
                          StatusId = tweet.StatusID,
                          ScreenName = tweet.User.ScreenNameResponse,
                          Text = tweet.Text,
                          ImageUrl = tweet.User.ProfileImageUrl,
                          MediaUrl = tweet.Entities.MediaEntities.FirstOrDefault().MediaUrl
                      }).ToList();

        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
