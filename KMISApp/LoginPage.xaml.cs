using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace KMISApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            logoImage.Source = ImageSource.FromResource("KMISApp.Assets.Images.web_hi_res_512.png", assembly);
        }

        public async void loginButton_Clicked(System.Object sender, System.EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(email.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(password.Text);

            if(isEmailEmpty || isPasswordEmpty)
            {
                await DisplayAlert("ERROR", "Por favor llena todos los campos!", "OK");
            }
            else
            {
                var user = email.Text;
                var pass = password.Text;

                /*try
                {
                    var postData = new List<KeyValuePair<string, string>>();
                    postData.Add(new KeyValuePair<string, string>("email", user));
                    postData.Add(new KeyValuePair<string, string>("clave", pass));

                    var content = new FormUrlEncodedContent(postData);

                    HttpClient client = new HttpClient();

                    client.BaseAddress = new Uri("http://localhost:8012"); 

                    var response = await client.PostAsync("http://localhost:8012/api/testing.php", content);
                    IAsyncResult result = response.Content.ReadAsStringAsync();*/
                    _ = Navigation.PushAsync(new MainPage());/*

                }*/
                /*catch(Exception ex)
                {
                    await DisplayAlert("ERROR", "No se pudo encontrar el usuario", "OK");
                    //return;
                }*/

                email.Text = string.Empty;
                password.Text = string.Empty;
            }
        }

        void forgetPass_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ForgetPasswordPage());
            email.Text = string.Empty;
            password.Text = string.Empty;
        }

        void signUp_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
            email.Text = string.Empty;
            password.Text = string.Empty;
        }
    }
}
