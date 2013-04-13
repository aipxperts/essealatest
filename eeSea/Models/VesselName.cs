using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eeSea.Models
{
     
    public class VesselName
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }
    }
}