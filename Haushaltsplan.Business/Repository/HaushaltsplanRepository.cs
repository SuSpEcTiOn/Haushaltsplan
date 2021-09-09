namespace Haushaltsplan.Business.Repository
{
    using Haushaltsplan.Business.Enums;
    using Haushaltsplan.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class HaushaltsplanRepository
    {
        private const String cConnectionString = @"Server=DESKTOP-A3L0KBD\SUSPECTIONDB;Database=Haushaltsplan;Trusted_Connection=True;";

        private Transaktion MapTransaktion(SqlDataReader reader)
        {
            return new Transaktion
            {
                DateTime = DateTime.Parse(reader["DateTime"].ToString()),
                Verwendungszweck = reader["Zweck"].ToString(),
                Value = Convert.ToDecimal(reader["Value"])
            };
        }

        private IEnumerable<Transaktion> LoadTransaktionen(Monate monat, Int32 jahr, Boolean valuePositive)
        {
            var list = new List<Transaktion>();

            using (var connection = new SqlConnection(cConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(
                    $"SELECT * " +
                    $"FROM Transaktionen " +
                    $"WHERE FORMAT(DateTime, 'yyyy') = '{jahr}' " +
                    $"  AND FORMAT(DateTime, 'MM') = '{((Int32)monat).ToString().PadLeft(2, '0')}' " +
                    $"  AND Value {(valuePositive ? ">" : "<")} 0"
                    , connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapTransaktion(reader));
                    }
                }
            }

            return list;
        }

        public IEnumerable<Transaktion> LoadEinnahmen(Monate monat, Int32 jahr)
        {
            return LoadTransaktionen(monat, jahr, true);
        }

        public IEnumerable<Transaktion> LoadAusgaben(Monate monat, Int32 jahr)
        {
            return LoadTransaktionen(monat, jahr, false);
        }

        public IEnumerable<Int32> GetAllJahre()
        {
            var list = new List<Int32>();

            using (var connection = new SqlConnection(cConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(
                    $"SELECT DISTINCT(FORMAT(DateTime, 'yyyy')) as 'Jahr' " +
                    $"FROM Transaktionen"
                    , connection);

                using (var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(Convert.ToInt32(reader["Jahr"]));
                    }
                }

                connection.Close();
            }

            return list;
        }
    }
}
