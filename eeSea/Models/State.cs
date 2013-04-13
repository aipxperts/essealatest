using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using eeSea.Models;

namespace eeSea.Models
{
    public class State
    {
        [Key]
        [Column("State_ID", TypeName = "bigint")]
        public long Id { get; set; }

        [Column("State_Code", TypeName = "nvarchar")]
        public string Code { get; set; }

        [Column("State_Name", TypeName = "nvarchar")]
        public string Name { get; set; }

        public virtual IEnumerable<City> City { get; set; }
    }
}