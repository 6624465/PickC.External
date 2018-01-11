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
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

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
        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;
            var booking = (Booking)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEBOOKING);
                db.AddInParameter(savecommand, "BookingNo", System.Data.DbType.String, booking.BookingNo);
                db.AddInParameter(savecommand, "BookingDate", System.Data.DbType.DateTime, booking.BookingDate);
                db.AddInParameter(savecommand, "CustomerID", System.Data.DbType.String, booking.CustomerId);
                db.AddInParameter(savecommand, "RequiredDate", System.Data.DbType.DateTime, booking.RequiredDate);
                db.AddInParameter(savecommand, "LocationFrom", System.Data.DbType.String, booking.LocationFrom);
                db.AddInParameter(savecommand, "LocationTo", System.Data.DbType.String, booking.LocationTo);
                db.AddInParameter(savecommand, "CargoDescription", System.Data.DbType.String, booking.CargoDescription ?? "");
                db.AddInParameter(savecommand, "VehicleType", System.Data.DbType.Int16, booking.VehicleType);
                db.AddInParameter(savecommand, "VehicleGroup", System.Data.DbType.Int16, booking.VehicleGroup);
                db.AddInParameter(savecommand, "Remarks", System.Data.DbType.String, booking.Remarks ?? "");
                /*
                db.AddInParameter(savecommand, "IsConfirm", System.Data.DbType.Boolean, booking.IsConfirm);
                db.AddInParameter(savecommand, "ConfirmDate", System.Data.DbType.DateTime, booking.ConfirmDate);
                db.AddInParameter(savecommand, "DriverID", System.Data.DbType.String, booking.DriverID);
                db.AddInParameter(savecommand, "VehicleNo", System.Data.DbType.String, booking.VehicleNo);
                db.AddInParameter(savecommand, "IsCancel", System.Data.DbType.Boolean, booking.IsCancel);
                db.AddInParameter(savecommand, "CancelTime", System.Data.DbType.DateTime, booking.CancelTime);
                db.AddInParameter(savecommand, "CancelRemarks", System.Data.DbType.String, booking.CancelRemarks);
                db.AddInParameter(savecommand, "IsComplete", System.Data.DbType.Boolean, booking.IsComplete);
                db.AddInParameter(savecommand, "CompleteTime", System.Data.DbType.DateTime, booking.CompleteTime);
                */
                db.AddInParameter(savecommand, "PayLoad", System.Data.DbType.String, booking.PayLoad ?? "");
                db.AddInParameter(savecommand, "CargoType", System.Data.DbType.String, booking.CargoType ?? "");
                db.AddInParameter(savecommand, "Latitude", System.Data.DbType.Decimal, booking.Latitude);
                db.AddInParameter(savecommand, "Longitude", System.Data.DbType.Decimal, booking.Longitude);
                db.AddInParameter(savecommand, "ToLatitude", System.Data.DbType.Decimal, booking.ToLatitude);
                db.AddInParameter(savecommand, "ToLongitude", System.Data.DbType.Decimal, booking.ToLongitude);
                db.AddInParameter(savecommand, "ReceiverMobileNo", System.Data.DbType.String, "9666245400");
                db.AddInParameter(savecommand, "LoadingUnLoading", System.Data.DbType.Int16, booking.LoadingUnLoading);
                db.AddOutParameter(savecommand, "NewBookingNo", System.Data.DbType.String, 50);


                result = db.ExecuteNonQuery(savecommand, transaction);

                if (result > 0)
                {
                    booking.BookingNo = savecommand.Parameters["@NewBookingNo"].Value.ToString();
                }

                if (currentTransaction == null)
                    transaction.Commit();

            }
            catch (Exception ex)
            {
                if (currentTransaction == null)
                    transaction.Rollback();

                throw ex;
            }
            finally
            {
                if (currentTransaction == null)
                {
                    transaction.Dispose();
                    connection.Close();
                }
            }

            return (result > 0 ? true : false);

        }
        public IContract GetItem<T>(IContract lookupItem) where T : IContract
        {
            var item = ((Booking)lookupItem);

            var bookingItem = db.ExecuteSprocAccessor(DBRoutine.SELECTBOOKING,
                                                    MapBuilder<Booking>.BuildAllProperties(),
                                                    item.BookingNo).FirstOrDefault();

            if (bookingItem == null) return null;

            return bookingItem;
        }
        public List<Driver> GetNearTrucksDeviceID(string bookingNo, short minDistance, short vehicleType, short vehicleGroup, decimal latitude, decimal longitude)
        {


            return db.ExecuteSprocAccessor(DBRoutine.NEARTRUCKSDEVICELIST,
                                            MapBuilder<Driver>
                                            .BuildAllProperties(),
                                            bookingNo, minDistance, vehicleType, vehicleGroup, latitude, longitude).ToList();
        }
        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var booking = (Booking)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEBOOKING);
                db.AddInParameter(deleteCommand, "BookingNo", System.Data.DbType.String, booking.BookingNo);


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

    }
}