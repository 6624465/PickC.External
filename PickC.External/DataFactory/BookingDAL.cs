using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

using PickC.External.Contracts;

namespace PickC.External.DataFactory
{
    public class BookingDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        public BookingDAL()
        {
            db = DatabaseFactory.CreateDatabase("PickC");
        }

        public Booking GetByBookingNo(string BookingNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.BOOKINGBYBOOKINGNO, MapBuilder<Booking>.BuildAllProperties(), BookingNo).FirstOrDefault();
        }
    }
}