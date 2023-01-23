using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaP2.Helpers.LoggerManager
{
    public interface ILog
    {
        void LogError(Exception ex);
    }
}
