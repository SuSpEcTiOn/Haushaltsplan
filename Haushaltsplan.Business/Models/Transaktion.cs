namespace Haushaltsplan.Business.Models
{
    using System;

    public class Transaktion
    {
        public DateTime DateTime { get; set; }
        public String Verwendungszweck { get; set; }
        public Decimal Value { get; set; }
        public Boolean Fix { get; set; }
    }
}
