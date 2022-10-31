using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMusica.Entidades
{
    public class Cancion
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")] 
        [StringLength(maximumLength: 10, ErrorMessage = "El campo {0} solo puede tener hasta 15 caracteres")]
        public string Nombre { get; set; }

        public string Genero { get; set; }

        public string Duracion { get; set; }

        public int ArtistaId { get; set; }

        [Url]
        [NotMapped]
        public string Url { get; set; }

        public Artista Artista { get; set; }
    }
}
