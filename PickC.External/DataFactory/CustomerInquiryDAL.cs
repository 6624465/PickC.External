using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

using PickC.External.Contracts;

namespace PickC.External.DataFactory
{
    public class CustomerInquiryDAL
    {
        private Database db;

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerInquiryDAL()
        {
            db = DatabaseFactory.CreateDatabase("PickC");
        }

        public bool Save(CustomerInquiry customerInquiry)
        {
            var result = 0;            

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVECUSTOMERINQUIRY);

                db.AddInParameter(savecommand, "InquiryID", System.Data.DbType.Int64, customerInquiry.InquiryID);
                db.AddInParameter(savecommand, "InquiryType", System.Data.DbType.Int16, customerInquiry.InquiryType);
                db.AddInParameter(savecommand, "InquiryDate", System.Data.DbType.DateTime, customerInquiry.InquiryDate);
                db.AddInParameter(savecommand, "MobileNo", System.Data.DbType.String, customerInquiry.MobileNo);
                db.AddInParameter(savecommand, "EmailID", System.Data.DbType.String, customerInquiry.EmailID);
                db.AddInParameter(savecommand, "CustomerName", System.Data.DbType.String, customerInquiry.CustomerName);

                result = db.ExecuteNonQuery(savecommand, transaction);

                transaction.Commit();

            }
            catch (Exception ex)
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

        public List<FareChart> GetApproximateFareWEB(decimal distance, decimal duration)
        {
            return db.ExecuteSprocAccessor(DBRoutine.GETAPPROXIMATEFAREWEB, 
                MapBuilder<FareChart>.BuildAllProperties(), distance, duration).ToList();
        }
        public bool SaveContactUs(CustomerInquiry customerInquiry)
        {
            var result = 0;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.CUSTOMERSAVE);

                
                db.AddInParameter(savecommand, "InquiryType", System.Data.DbType.Int16, customerInquiry.InquiryType);
                db.AddInParameter(savecommand, "InquiryDate", System.Data.DbType.DateTime, customerInquiry.InquiryDate);
                db.AddInParameter(savecommand, "MobileNo", System.Data.DbType.String, customerInquiry.MobileNo);
                db.AddInParameter(savecommand, "EmailID", System.Data.DbType.String, customerInquiry.EmailID);
                db.AddInParameter(savecommand, "CustomerName", System.Data.DbType.String, customerInquiry.CustomerName);
                db.AddInParameter(savecommand, "Subject", System.Data.DbType.String, customerInquiry.Subject);
                db.AddInParameter(savecommand, "Description", System.Data.DbType.String, customerInquiry.Description);

                result = db.ExecuteNonQuery(savecommand, transaction);

                transaction.Commit();

            }
            catch (Exception ex)
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
        public List<LookUp> GetCustomerSelectList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.CUSTOMERSELECTLIST, MapBuilder<LookUp>.BuildAllProperties()).ToList();
        }
    }
}