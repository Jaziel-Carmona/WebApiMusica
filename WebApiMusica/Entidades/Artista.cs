using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMusica.Entidades
{
    public class Artista
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")] 
        [StringLength(maximumLength:10, ErrorMessage = "El campo {0} solo puede tener hasta 8 caracteres")] 
        public string Nombre { get; set; }

        [Range(18,100, ErrorMessage = "El campo no se encuentra dentro del rango")]
        [NotMapped]
        public int Edad { get; set; }

        [CreditCard]
        [NotMapped]
        public string Tarjeta { get; set; }

        [Url]
        [NotMapped]
        public string Url { get; set; }

        public List<Cancion> canciones { get; set; }
    }
}
