namespace WebApiMusica.Entidades
{
    public class Cancion
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Genero { get; set; }

        public string Duracion { get; set; }

        public int ArtistaId { get; set; }

        public Artista Artista { get; set; }
    }
}
