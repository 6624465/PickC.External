using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickC.External.Contracts
{
    public class LookUp : IContract
    {
        public LookUp() { }


        public Int16 LookupID { get; set; }


        public string LookupCode { get; set; }


        public string LookupDescription { get; set; }


        public string LookupCategory { get; set; }


    }
}