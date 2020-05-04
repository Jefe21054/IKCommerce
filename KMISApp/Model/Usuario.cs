namespace KMISApp.Model
{
    public class Usuario
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public static async void Insert(Usuario usuario)
        {
            await App.MobileService.GetTable<Usuario>().InsertAsync(usuario);
        }
    }
}
