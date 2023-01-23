using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaP2.DataAccess.Repository
{
    public class DataRepository<TEntity> : IDataRepository<TEntity> where TEntity : class, new()
    {
        #region Fields
        public DbContext _context { get; set; }
        public DbSet<TEntity> _entity { get; set; }
        #endregion

        #region Builder

        public DataRepository(DbContext context)
        {
            _context = context;
            _entity = context.Set<TEntity>();
        }
        #endregion

        public async Task<bool> AddRange(List<TEntity> Entitie)
        {
            await _entity.AddRangeAsync(Entitie);
            int changes = await _context.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<List<TEntity>> List(Expression<Func<TEntity, bool>>? expresion = null)
        {
            await _context.Database.EnsureCreatedAsync();
            List<TEntity> result;
            if (expresion == null)
            {
                result = await _entity.ToListAsync();
            }
            else
            {
                result = await _entity.Where(expresion).ToListAsync();
            }

            return result;
        }
    }
}
