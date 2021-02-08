using System;
using System.Collections.Generic;

namespace aPBDs17245.Models
{
    public partial class EventOrganiser
    {
        public int IdEvent { get; set; }
        public int IdOrganiser { get; set; }

        public virtual Event IdEventNavigation { get; set; }
        public virtual Organiser IdOrganiserNavigation { get; set; }
    }
}
