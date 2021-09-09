using System;

namespace Haushaltsplan.Business.Enums
{
    public enum Monate
    {
        Januar = 1,
        Februar = 2,
        Maerz = 3,
        April = 4,
        Mai = 5,
        Juni = 6,
        Juli = 7,
        August = 8,
        September = 9,
        Oktober = 10,
        November = 11,
        Dezember = 12
    }

    public static class MonatExtensions
    {
        public static Monate GetMonat(this DateTime dateTime)
        {
            return (Monate)dateTime.Month;
        }
    }
}
