using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eeSea.Models
{
    public class VesselOperator
    {
        public long Id { get; set; }
        public Company Company { get; set; }
        public DateTime Date { get; set; }
    }
}