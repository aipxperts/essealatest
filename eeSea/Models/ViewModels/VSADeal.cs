using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eeSea.Models.ViewModels
{
    public class VSADeal
    {
        [Key]
        public long Id { get; set; }
        public string StringCode { get; set; }
        public string StringName { get; set; }
        public string Carrier { get; set; }
        public decimal Percentage { get; set; }
        public decimal EffectiveCapacity { get; set; }
        public decimal AVGShare { get; set; }
    }
}