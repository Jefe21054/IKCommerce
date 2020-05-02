namespace KMISApp.Model
{
    public class Usuario
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Clave { get; set; }

        public string Username { get; set; }

        public static async void Insert(Usuario user)
        {
            await App.MobileService.GetTable<Usuario>().InsertAsync(user);
        }
    }
}
