using Microsoft.Practices.EnterpriseLibrary.Data;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

using PickC.External.Contracts;

namespace PickC.External.DataFactory
{
    public class LookUpDAL
    {
        private Database db;

        /// <summary>
        /// Constructor
        /// </summary>
        public LookUpDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }

        #region IDataFactory Members

        public List<LookUp> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTLOOKUP, MapBuilder<LookUp>.BuildAllProperties()).ToList();
        }

        public List<LookUp> GetVehicleGroupList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.VEHICLEGROUPLIST, MapBuilder<LookUp>.BuildAllProperties()).ToList();
        }        

        public List<LookUp> GetLoadingUnLoadingList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LOADINGUNLOADINGLIST, MapBuilder<LookUp>.BuildAllProperties()).ToList();
        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var lookup = (LookUp)(object)item;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVELOOKUP);

                db.AddInParameter(savecommand, "LookupID", System.Data.DbType.Int16, lookup.LookupID);
                db.AddInParameter(savecommand, "LookupCode", System.Data.DbType.String, lookup.LookupCode);
                db.AddInParameter(savecommand, "LookupDescription", System.Data.DbType.String, lookup.LookupDescription);
                db.AddInParameter(savecommand, "LookupCategory", System.Data.DbType.String, lookup.LookupCategory);


                result = db.ExecuteNonQuery(savecommand, transaction);

                transaction.Commit();

            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
                connection.Close();

            }

            return (result > 0 ? true : false);

        }

        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var lookup = (LookUp)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETELOOKUP);


                db.AddInParameter(deleteCommand, "LookupID", System.Data.DbType.Int16, lookup.LookupID);

                result = Convert.ToBoolean(db.ExecuteNonQuery(deleteCommand, transaction));

                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Dispose();
                connnection.Close();

            }

            return result;
        }

        public IContract GetItem<T>(IContract lookupItem) where T : IContract
        {


            var Item = db.ExecuteSprocAccessor(DBRoutine.SELECTLOOKUP,
                                                    MapBuilder<LookUp>.BuildAllProperties(),
                                                    ((LookUp)lookupItem).LookupID).FirstOrDefault();
            return Item;
        }

        public List<LookUp> GetVehicleTypeList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.VEHICLETYPELIST, MapBuilder<LookUp>.BuildAllProperties()).ToList();
        }
        #endregion

    }
}