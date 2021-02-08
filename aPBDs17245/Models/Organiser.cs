using System;
using System.Collections.Generic;

namespace aPBDs17245.Models
{
    public partial class Organiser
    {
        public Organiser()
        {
            EventOrganiser = new HashSet<EventOrganiser>();
        }

        public int IdOrganiser { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EventOrganiser> EventOrganiser { get; set; }
    }
}
