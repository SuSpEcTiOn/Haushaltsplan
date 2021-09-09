namespace Haushaltsplan.Business.Models
{
    using System.Collections.Generic;

    public class Monat
    {
        public IEnumerable<Transaktion> Einnahmen { get; set; }
        public IEnumerable<Transaktion> Ausgaben { get; set; }
    }
}
