namespace Haushaltsplan.Business.Models
{
    using System;
    using System.Collections.Generic;

    public class Plan
    {
        public Dictionary<Int32, Jahr> Jahre { get; set; }
    }
}
