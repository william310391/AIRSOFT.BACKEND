using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airsoft.Domain.Entities
{
    public class Persona
    {
        public int PersonaID { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
    }
}
