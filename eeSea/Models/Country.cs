using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace eeSea.Models
{
    public class Country
    {
        [Key]
        [Column("Country_ID", TypeName = "bigint")]
        public long Id { get; set; }

        [Column("Country_Code", TypeName = "nvarchar")]
        public string Code { get; set; }

        [Column("Country_Name", TypeName = "nvarchar")]
        public string Name { get; set; }

        public virtual IEnumerable<City> City { get; set; }
    }
}