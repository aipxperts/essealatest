using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eeSea.Models
{
    public class Vessel
    {
        public long Id { get; set; }

        public string HullCode { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BuiltDate { get; set; }

        public int NominalCapacity { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Length { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Breadth { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Beam { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Draft { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal MaxSpeed { get; set; }

        public virtual  VesselName VesselName { get; set; }
        public virtual VesselOwner VesselOwner { get; set; }
        public virtual VesselOperator VesselOperator { get; set; }
    }
}