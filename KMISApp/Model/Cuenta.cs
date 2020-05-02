namespace KMISApp.Model
{
    public class Cuenta
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Clave { get; set; }

        public string Servicio { get; set; }

        public string UsuarioEmail { get; set; }

        public static async void Insert(Cuenta cuenta)
        {
            await App.MobileService.GetTable<Cuenta>().InsertAsync(cuenta);
        }
    }
}
