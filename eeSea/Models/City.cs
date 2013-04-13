using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using eeSea.Models;

namespace eeSea.Models
{
    public class City
    {
        [Key]
        [Column("MLocation_ID", TypeName = "bigint")]
        public long Id { get; set; }

        [Column("MLocation_Code", TypeName = "nvarchar")]
        public string Code { get; set; }

        [Column("MLocation_Name", TypeName = "nvarchar")]
        public string Name { get; set; }

        public virtual State State { get; set; }
        public virtual Country Country { get; set; }
        public virtual Continent Continent { get; set; }

        public virtual IEnumerable<Location> Locations { get; set; }
    }
}