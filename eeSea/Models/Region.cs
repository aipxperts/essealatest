using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eeSea.Models
{
    [Table("TRegion", Schema = "dbo")]
    public class Region
    {
        [Key]
        [Column("Region_ID", TypeName = "bigint")]
        public  long Id { get; set; }

        [Column("Region_Code", TypeName = "nvarchar")]
        public string RegionCode { get; set; }

        [Column("Region_Name", TypeName = "nvarchar")]
        public string  Name { get; set; }

        public virtual IEnumerable<Location> Locations { get; set; }
    }
}