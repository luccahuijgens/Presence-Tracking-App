﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using praticeApp.Controller;
using praticeApp.Domain;
using praticeApp.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praticeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Feed : ContentPage
    {
        public ObservableCollection<FeedItem> FeedList { get; set; }

        public Feed()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            showFeed();
        }

        void showFeed()
        {
            FeedList = FeedController.updateFeed();
            if (FeedList.Any())
            {
                MyListView.ItemsSource = FeedList;
                emptyLabel.IsVisible = false;
            }
        }

        private void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Page displayPage = FeedController.ItemTapped(sender, e);
            Navigation.PushAsync(displayPage);
        }
        private void UpdateFeedButton(object sender, EventArgs args)
        {
            showFeed();
        }
    }
}
