﻿using TwitterClientMobile.Views;
using Xamarin.Forms;

namespace TwitterClientMobile
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            //var content = new ContentPage
            //{
            //    Title = "TwitterClientMobile",
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Label {
            //                HorizontalTextAlignment = TextAlignment.Center,
            //                Text = "Welcome to Xamarin Forms!"
            //            }
            //        }
            //    }
            //};

            MainPage = new NavigationPage(new TweetView());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}