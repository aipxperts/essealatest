using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eeSea.Models.ViewModels
{
    [Table("VVesselDynamicAttributes", Schema = "dbo")]
    public class VesselDynamicAttributesViewModel
    {
        [Key]
        public long VesselDynamicAttributId { get; set; }

        [Column("VesselName_Vessel", TypeName = "bigint")]
        public long VesselNameId { get; set; }

        [Column("VesselName_Name", TypeName = "nvarchar")]
        public string VesselName { get; set; }

        [Column("VesselName_Date", TypeName = "datetime")]
        public DateTime VesselNameDate { get; set; }

        [Column("OwnerName", TypeName = "nvarchar")]
        public string OwnerName { get; set; }

        [Column("vesselOwner_Date", TypeName = "datetime")]
        public DateTime VesselOwnerDate { get; set; }


        [Column("OperatorName", TypeName = "nvarchar")]
        public string OperatorName { get; set; }

        [Column("VesselOperator_Date", TypeName = "datetime")]
        public DateTime VesselOperatorDate { get; set; }




    }
}