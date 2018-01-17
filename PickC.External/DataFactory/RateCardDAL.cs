using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PickC.External.Contracts;

namespace PickC.External.DataFactory
{
    public class RateCardDAL
    {
        private Database db;

        public RateCardDAL()
        {
            db = DatabaseFactory.CreateDatabase("PickC");
        }
        public List<RateCard> getRateCardList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTRATECARD, MapBuilder<RateCard>.BuildAllProperties()).ToList();
        }
    }
}