using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eeSea.Models.ViewModels
{
    public class ScheduleLocation
    {
        [Key]
        public long Id { get; set; }
        public string StringCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Arival { get; set; }
        public string ArrivalDoW { get; set; }
        public int ArivalOffset { get; set; }
        public int Departure { get; set; }       
        public string DepartureDoW { get; set; }
        public int DepartureOffset { get; set; }
        public int Utilization { get; set; }
        public int Priority { get; set; }
        public string TimeInSea { get; set; }
         [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal DistanceToNextPort { get; set; }
        public decimal Distance { get; set; }
         [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Speed { get; set; }
         
    }
}