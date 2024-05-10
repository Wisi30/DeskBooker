using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooker.Core.Tests.Processor
{
    public class DeskBookingProcessor
    {
        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
            return new DeskBookingResult
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date
            };
        }
    }
}
