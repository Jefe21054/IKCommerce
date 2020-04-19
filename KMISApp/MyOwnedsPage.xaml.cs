using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using KMISApp.Model;
using Xamarin.Forms;

namespace KMISApp
{
    public partial class MyOwnedsPage : ContentPage
    {
        public MyOwnedsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection connection = new SQLiteConnection(App.DataBaseLocation))
            {
                connection.CreateTable<Post>();
                var posts = connection.Table<Post>().ToList();
                postListView.ItemsSource = posts;
            }    
        }

        void postListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedPost = postListView.SelectedItem as Post;

            if(selectedPost != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
        }
    }
}
