using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiMusica.Validaciones;

namespace WebApiMusica.Entidades
{
    public class Artista
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")] 
        [StringLength(maximumLength:15, ErrorMessage = "El campo {0} solo puede tener hasta 15 caracteres")] 
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        [Range(18, 100, ErrorMessage = "El campo no se encuentra dentro del rango")]
        [NotMapped]
        public int Edad { get; set; }

        [CreditCard]
        [NotMapped]
        public string Tarjeta { get; set; }

        public List<Cancion> canciones { get; set; }

        [NotMapped]
        public int Menor { get; set; }

        [NotMapped]
        public int Mayor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Para que se ejecuten debe de primero cumplirse con las reglas por Atributo Ejemplo: Range
            //Tomar a consideracion que primero se ejecutaran las validaciones mappeadas en los atributos
            //y posteriormente las declaradas en la entidad
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula",
                        new String[] { nameof(Nombre) });
                }
            }

            if (Menor > Mayor)
            {
                yield return new ValidationResult("Este valor no puede ser mas grande que el campo Mayor",
                    new String[] { nameof(Menor) });
            }
        }
    }
}
