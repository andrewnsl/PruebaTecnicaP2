using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaP2.DataAccess.Repository
{
    public interface IDataRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Agrega una lista de una entidad
        /// </summary>
        /// <param name="Entitie"></param>
        /// <returns></returns>
        Task<bool> AddRange(List<TEntity> Entitie);

        /// <summary>
        /// Obtiene una lista de registros por una expresion
        /// </summary>
        /// <param name="expresion"></param>
        /// <returns></returns>
        Task<List<TEntity>> List(Expression<Func<TEntity, bool>>? expresion = null);        
    }
}
