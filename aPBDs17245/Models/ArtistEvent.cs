using System;
using System.Collections.Generic;

namespace aPBDs17245.Models
{
    public partial class ArtistEvent
    {
        public int IdEvent { get; set; }
        public int IdArtist { get; set; }
        public DateTime PerformanceDate { get; set; }

        public virtual Event IdArtistNavigation { get; set; }
        public virtual Artist IdEventNavigation { get; set; }
    }
}
