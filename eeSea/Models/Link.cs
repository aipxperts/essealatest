using eeSea.com.eesea.ws;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eeSea.Models
{
    public class Link
    {
        [Key]
        [Column("Id", TypeName = "bigint")]
        public long Id { get; set; }
        public List<LinkDefinition> Definition { get; set; }
        public Location LocationA { get; set; }
        public Location LocationB { get; set; }
        public Dictionary<long, List<Coordinate>> MapCoordinates { get; set; }

        public Dictionary<long, System.Drawing.KnownColor> RandomColor { get; set; }

        public Dictionary<long, string> MapJs { get; set; }
        public string GMapJs { get; set; }

    }

}
