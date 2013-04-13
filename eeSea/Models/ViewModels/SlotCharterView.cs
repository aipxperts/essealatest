using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eeSea.Models.ViewModels
{
    public class SlotCharterView
    {
        [Key]
        public long Id { get; set; }
         public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public SlotCharterInfo SlotCharterInfo { get; set; }

    }
}