using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaP2.Models.Providers.Puntosleal
{
    public class LoginDataPuntosleal
    {
        public string token { get; set; } = null!;
        public string refresh_token { get; set; } = null!;
        public int id_rol { get; set; }
        public string plataforma { get; set; } = null!;
    }
}
