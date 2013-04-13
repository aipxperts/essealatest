using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using eeSea.Models.ViewModels;
 

namespace eeSea.Models
{
    public class EeSeaContext : DbContext
    {
        public DbSet<Region> Regions {get ; set;}
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Continent>  Continents { get; set; }
        public DbSet<State> States { get; set; }
        //public DbSet<Location> Locations { get; set; }
        public DbSet<VesselDynamicAttributesViewModel> VesselDynamicAttributesViewModels { get; set; }
       
          
    }
     
}