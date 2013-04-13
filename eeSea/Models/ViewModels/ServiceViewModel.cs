using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eeSea.Models.ViewModels
{
    public class ServiceViewModel
    {
        [Key]
        public long Id { get; set; }
        public string CarrierName { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public string Frequency { get; set; }
        public string AvgCapacity { get; set; }
        public string MarketingName { get; set; }
        public int NoVSA { get; set; }
        public int NoVess { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; } 

    }
}