using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaP2.Models.Providers.Puntosleal
{
    public class CargarPuntosleal
    {
        public int code { get; set; }
        public int id_transaccion { get; set; }
        public int puntos_activos { get; set; }
        public int puntos { get; set; }
        public int descuentoTotal { get; set; }
        public string no_factura { get; set; } = null!;
    }
}
