using System;
using System.Collections.Generic;
using KMISApp.Model;
using SQLite;
using Xamarin.Forms;

namespace KMISApp
{
    public partial class MyDBPage : ContentPage
    {
        public MyDBPage()
        {
            InitializeComponent();
        }

        void save_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                Post post = new Post()
                {
                    Email = emailEntry.Text,
                    Password = passwordEntry.Text,
                    Service = servicePicker.SelectedItem.ToString()
                };

                bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
                bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

                if (isEmailEmpty || isPasswordEmpty)
                {
                    DisplayAlert("ERROR", "Por favor llena todos los campos", "OK");
                }
                else
                {
                    using (SQLiteConnection connection = new SQLiteConnection(App.DataBaseLocation))
                    {
                        connection.CreateTable<Post>();
                        int rows = connection.Insert(post);

                        if (rows > 0)
                        {
                            DisplayAlert("CORRECTO!",
                                "Se agrego la informacion de tu adquisicion, solo para ti!",
                                "OK");
                        }
                        else
                        {
                            DisplayAlert("ERROR",
                                "No se pudo agregar la informacion de tu adquisicion",
                                "OK");
                        }
                    }
                    emailEntry.Text = string.Empty;
                    passwordEntry.Text = string.Empty;
                }
            }
            catch (NullReferenceException exe)
            {
                DisplayAlert("ERROR", "Por favor llena todos los campos", "OK");
                emailEntry.Text = string.Empty;
                passwordEntry.Text = string.Empty;
            }
            catch (Exception ex)
            {
                DisplayAlert("ERROR", ex.ToString(), "OK");
                emailEntry.Text = string.Empty;
                passwordEntry.Text = string.Empty;
            }
        }
    }
}
