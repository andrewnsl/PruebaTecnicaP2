using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaP2.Models.PayLoads
{
    public class PuntosLoad
    {
        public string Uid { get; set; } = null!;
        public string Uid_cms { get; set; } = null!;
        public string Factura { get; set; } = null!;
        public int Id_comercio { get; set; }
        public int Id_sucursal { get; set; }        
    }
}
