using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PruebaTecnicaP2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PruebaTecnicaP2.DataAccess
{
    public partial class ProyectoContext : DbContext
    {
        private readonly string _connectionStrings;

        public ProyectoContext(IConfiguration appConfiguration)
        {
            _connectionStrings = appConfiguration.GetConnectionString("Bd")!;
        }

        #region DbSet
        public virtual DbSet<Puntos> Puntos { get; set; }
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseSqlite(_connectionStrings, p =>
            {
                p.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Puntos>(entity =>
            {
                entity.ToTable("Puntos");
                entity.HasKey(e => e.PuntosId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
