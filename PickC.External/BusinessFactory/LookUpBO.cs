using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PickC.External.Contracts;
using PickC.External.DataFactory;

namespace PickC.External.BusinessFactory
{
    public class LookUpBO
    {
        private LookUpDAL lookupDAL;
        public LookUpBO()
        {
            lookupDAL = new LookUpDAL();
        }

        //public List<LookUp> GetList()
        //{
        //    return lookupDAL.GetList();
        //}
        public List<LookUp> GetVehicleTypeList()
        {
            return lookupDAL.GetVehicleTypeList();
        }
        public List<LookUp> GetVehicleGroupList()
        {
            return lookupDAL.GetVehicleGroupList();
        }


        //public bool SaveLookUp(LookUp newItem)
        //{
        //    return lookupDAL.Save(newItem);
        //}

        //public bool DeleteLookUp(LookUp item)
        //{
        //    return lookupDAL.Delete(item);
        //}

        //public LookUp GetLookUp(LookUp item)
        //{
        //    return (LookUp)lookupDAL.GetItem<LookUp>(item);
        //}



        //public List<CargoTypeList> GetCargoTypeList()
        //{
        //    return lookupDAL.GetCargoTypeList();
        //}

        //public List<LookUp> GetLoadingUnLoadingList()
        //{
        //    return lookupDAL.GetLoadingUnLoadingList();
        //}
    }
}