namespace WebApiMusica.Entidades
{
    public class Artista
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public List<Cancion> canciones { get; set; }
    }
}
