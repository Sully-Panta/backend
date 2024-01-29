using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Respuesta
    {
        public bool Status { get; set; }
        public object? Data { get; set; }

        public Respuesta(bool Status, object Data)
        {
            this.Status = Status;
            this.Data = Data;
        }
    }
}
