using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using eeSea.com.eesea;
namespace eeSea.Models.ViewModels
{
    public class VesselAttributes
    {
        [Key]
        public long Id { get; set; }
         public string Name { get; set; }        
        public string NominalCapacity { get; set; }
        public string EffectiveCapacity { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Priority { get; set; }
        public eeSea.com.eesea.ws.Vessel VesselInfos { get; set; }
    }
}