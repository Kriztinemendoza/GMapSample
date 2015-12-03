using GMapSample.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMapSample.Repository
{
    public class GMapSampleContext : DbContext
    {

        public GMapSampleContext() : base("GMapSampleContext")
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Place> Places { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
