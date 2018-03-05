using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PickC.External.Contracts;
using PickC.External.DataFactory;

namespace PickC.External.BusinessFactory
{
    public class RateCardBO
    {
        private RateCardDAL rateCardDAL;
        public RateCardBO()
        {
            rateCardDAL = new RateCardDAL();
        }
        public List<RateCard> getRateCardList()
        {
            return rateCardDAL.getRateCardList();
        }
    }
}