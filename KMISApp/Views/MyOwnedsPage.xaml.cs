using KMISApp.Models;
using SQLite;
using System.Collections.Generic;
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
                List<Post> posts = connection.Table<Post>().ToList();
                postListView.ItemsSource = posts;
            }
        }

        private void postListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Post selectedPost = postListView.SelectedItem as Post;

            if (selectedPost != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
        }
    }
}
