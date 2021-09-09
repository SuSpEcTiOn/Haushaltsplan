namespace Haushaltsplan.Business.Models
{
    using Haushaltsplan.Business.Enums;
    using System;
    using System.Collections.Generic;

    public class Jahr
    {
        public Jahr(Int32 alsZahl)
        {
            AlsZahl = alsZahl;
        }

        public Int32 AlsZahl { get; }
        public Dictionary<Monate, Monat> Monate { get; set; }
    }
}
