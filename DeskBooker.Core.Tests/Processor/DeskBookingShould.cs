using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooker.Core.Tests.Processor
{
    public class DeskBookingShould
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void return_desk_booking_result_with_request_values()
        {
            // Given
            var request = new DeskBookingRequest
            {
                FirstName = "Luis",
                LastName = "Borges",
                Email = "lborges@aidacanarias.com",
                Date = new DateTime(2024, 5, 10)
            };

            var processor = new DeskBookingProcessor();

            // When
            var result = processor.BookDesk(request);

            // Then
            Assert.IsNotNull(request);
            Assert.AreEqual(request.FirstName, result.FirstName);
            Assert.AreEqual(request.LastName, result.LastName);
            Assert.AreEqual(request.Email, result.Email);
            Assert.AreEqual(request.Date, result.Date);
        }
    }
}
