using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PickC.External.Contracts;
using PickC.External.DataFactory;

namespace PickC.External.BusinessFactory
{
    public class BookingBO
    {
        BookingDAL bookingDAL;
        public BookingBO()
        {
            bookingDAL = new BookingDAL();
        }

        public Booking GetByBookingNo(string bookingNo)
        {
            return bookingDAL.GetByBookingNo(bookingNo);
        }
    }
}