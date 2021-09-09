namespace Haushaltsplan.Business.Manager
{
    using Haushaltsplan.Business.Enums;
    using Haushaltsplan.Business.Models;
    using Haushaltsplan.Business.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HaushaltsplanManager
    {
        public HaushaltsplanManager(HaushaltsplanRepository repository)
        {
            Repository = repository;
        }

        private HaushaltsplanRepository Repository { get; set; }

        public IEnumerable<Int32> GetAlleJahre()
        {
            return Repository.GetAllJahre();
        }

        public Plan LadePlan()
        {
            return LadePlan(Repository.GetAllJahre().ToArray());
        }

        public Plan LadePlan(params Int32[] jahre)
        {
            var plan = new Plan();

            var jahrList = new Dictionary<Int32, Jahr>(); 

            foreach(var jahr in jahre)
            {
                var jahrModel = new Jahr(jahr);

                var monatList = new Dictionary<Monate, Monat>();

                foreach(var monat in (Monate[])Enum.GetValues(typeof(Monate)))
                {
                    var monatModel = new Monat();

                    monatModel.Einnahmen = Repository.LoadEinnahmen(monat, jahr);
                    monatModel.Ausgaben = Repository.LoadAusgaben(monat, jahr);

                    monatList.Add(monat, monatModel);
                }

                jahrModel.Monate = monatList;

                jahrList.Add(jahr, jahrModel);
            }

            plan.Jahre = jahrList;
  
            return plan;
        }
    }
}
