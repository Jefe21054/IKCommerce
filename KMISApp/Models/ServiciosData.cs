using System.Collections.Generic;

namespace KMISApp.Models
{
    public static class ServiciosData
    {
        public static IList<Servicios> Servicios { get; private set; }

        static ServiciosData()
        {
            Servicios = new List<Servicios>();

            Servicios.Add(new Servicios
            {
                ServiceName = "Netflix",
                Id = 1,
            });

            Servicios.Add(new Servicios
            {
                ServiceName = "Spotify",
                Id = 2,
            });

            Servicios.Add(new Servicios
            {
                ServiceName = "Deezer",
                Id = 3,
            });

            Servicios.Add(new Servicios
            {
                ServiceName = "Disney+",
                Id = 4,
            });

            Servicios.Add(new Servicios
            {
                ServiceName = "Hulu",
                Id = 5,
            });

            Servicios.Add(new Servicios
            {
                ServiceName = "Nintendo Online",
                Id = 6,
            });

            Servicios.Add(new Servicios
            {
                ServiceName = "Amazon Prime Video",
                Id = 7,
            });
        }
    }
}
