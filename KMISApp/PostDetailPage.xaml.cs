using System;
using System.Collections.Generic;
using KMISApp.Model;
using SQLite;
using Xamarin.Forms;

namespace KMISApp
{
    public partial class PostDetailPage : ContentPage
    {
        Post selectedPost;

        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();

            this.selectedPost = selectedPost;

            emailEntry.Text = selectedPost.Email;
            passwordEntry.Text = selectedPost.Password;
            servicePicker.SelectedItem = selectedPost.Service;
        }

        void updateButton_Clicked(System.Object sender, System.EventArgs e)
        {
            selectedPost.Email = emailEntry.Text;
            selectedPost.Password = passwordEntry.Text;
            selectedPost.Service = servicePicker.SelectedItem.ToString();

            using (SQLiteConnection connection = new SQLiteConnection(App.DataBaseLocation))
            {
                connection.CreateTable<Post>();
                int rows = connection.Update(selectedPost);

                if (rows > 0)
                {
                    DisplayAlert("CORRECTO!",
                        "Se actualizo la informacion de tu adquisicion",
                        "OK");
                }
                else
                {
                    DisplayAlert("ERROR",
                        "No se pudo actualizar la informacion de tu adquisicion",
                        "OK");
                }
            }
            emailEntry.Text = string.Empty;
            passwordEntry.Text = string.Empty;
        }

        async void eraseButton_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DataBaseLocation))
            {
                connection.CreateTable<Post>();
                bool answer = await DisplayAlert("CAUTION", "Are you sure you want to erase your adquisition?", "YES", "NO");
                if (answer)
                {
                    int rows = connection.Delete(selectedPost);

                    if (rows > 0)
                    {
                        await DisplayAlert("CORRECTO!",
                            "Se borro la informacion de tu adquisicion",
                            "OK");
                    }
                    else
                    {
                        await DisplayAlert("ERROR",
                            "No se pudo borrar la informacion de tu adquisicion",
                            "OK");
                    }
                }
            }
            emailEntry.Text = string.Empty;
            passwordEntry.Text = string.Empty;
        }
    }
}
