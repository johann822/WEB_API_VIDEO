using System.ComponentModel.DataAnnotations;

namespace WebAPITest.DAL.Entities
{
    public class State:AuditBase
    {
        [Display(Name = "Estado/Departamento")] // Identificar Nombre 
        [MaxLength(50, ErrorMessage = "El campo {0} dbe tener maximo {1} caracteres")] //Longitud MAxima
        [Required(ErrorMessage = "El campo {0} es olbigatorio")] // Obligatorio
        public String Name{ get; set; }

        //relacion dos tablas
        [Display(Name="Pais")]
        public Country? Country{ get; set; }
        //FK
        [Display(Name="Id Pais")]
        public Guid CountryId{ get; set; }

    }
}
