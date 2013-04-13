using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using eeSea.com.eesea.ws;

namespace eeSea.Models
{
    public class Schedule
    {
        [Key]
         public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}")]       
        public decimal LocationFromLatitude { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}")]
        public decimal LocationFromLongitude { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}")]
        public decimal LocationToLatitude { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}")]
        public decimal LocationToLongitude { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}")]
        public LinkDefinition[] WaterLocations { get; set; }
        public long LocationA { get; set; }
        public long LocationB { get; set; }
        public long WaterRoute { get; set; }
        public long Priority  { get; set; }
        
 
    }
}