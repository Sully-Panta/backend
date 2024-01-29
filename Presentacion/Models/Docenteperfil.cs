using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Docenteperfil { 
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string? Nombre { get; set; }

    [Required]
    [StringLength(200)]
    public string? Edad { get; set; }

    [Required]
    [StringLength(100)]
        public string? Telefono { get; set; }
        [Required]
        [StringLength(100)]
        public string? Correo { get; set; }
        [Required]
        [StringLength(100)]
        public string? Github { get; set; }
        [Required]
        [StringLength(100)]
        public string? FechaNacimiento { get; set; }

        [StringLength(100)]
        public string? opdocente { get; set; }


    }
}
