using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaP2.Models.Entities
{
    public class Puntos
    {
        public int PuntosId { get; set; }   
        public string uid { get; set; } = null!;
        public int valor { get; set; }
        public string factura { get; set; } = null!;
        public string uid_cms { get; set; } = null!;
        public int id_sucursal { get; set; }
        public int id_comercio { get; set; }
        public int id_transaccion { get; set; }

    }
}
