using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string? Horario { get; set; }

        [Required]
        [StringLength(200)]
        public string? Dias { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nivel { get; set; }


        [StringLength(100)]
        public string? estudiantes { get; set; }

    }
}
