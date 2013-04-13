using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eeSea.Models
{
    [Table("TLocation", Schema = "dbo")]
    public class Location
    {
        [Key]
        [Column("Location_ID", TypeName = "bigint")]
        public long Id { get; set; }

        [Column("Location_Code", TypeName = "nvarchar")]
        public string Code { get; set; }

        [Column("Location_Name", TypeName = "nvarchar")]
        public string Name { get; set; }

         
        [DisplayFormat(DataFormatString = "{0:0.000000}")]
        [Column("Location_Latitude", TypeName = "numeric")]
        public decimal  Latitude { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.000000}")]
        [Column("Location_Longitude", TypeName = "numeric")]
        public decimal  Longitude { get; set; }

        /* 
        [Column("Location_Region", TypeName = "bigint")]
        public long LocationRegionID { get; set; }
        */
        
        public virtual Region Region { get; set; }
        public virtual City City { get; set; }
       
    }
}