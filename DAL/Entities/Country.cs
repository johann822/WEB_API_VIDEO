using System.ComponentModel.DataAnnotations;

namespace WebAPITest.DAL.Entities
{
    public class Country: AuditBase
    {
        [Display(Name = "Pais")] // Identificar Nombre 
        [MaxLength(50, ErrorMessage = "El campo {0} dbe tener maximo {1} caracteres")] //Longitud MAxima
        [Required(ErrorMessage = "El campo {0} es olbigatorio")] // Obligatorio
        public String Name { get; set; }

        [Display(Name = "Estados/Departamentos")]
        public ICollection<State>? States { get; set; }
    }
}
