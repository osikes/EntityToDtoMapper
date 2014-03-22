using EntityToDtoMapper.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;

namespace EntityToDtoMapper
{
    public class HumidorContext:DbContext {


        public HumidorContext()
        
        {
        }
        public DbSet<Humidor> Humidors { get; set; }
        public DbSet<Cigar> Cigars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
       
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
