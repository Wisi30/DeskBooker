using DeskBookerApp.Domain.DeskBooking;
using DeskBookerApp.Interfaces;
using DeskBookerWeb.Pages;
using Moq;

namespace DeskBookerTest
{
    public class BookDeskModelShould
    {
        [SetUp]

        [Test]
        public void call_book_desk_method_from_service()
        {
            var serviceMock = new Mock<IDeskBookingService>();
            var bookDeskModel = new BookDeskModel(serviceMock.Object)
            {
                DeskBookingRequest = new DeskBookingRequest()
            };

            bookDeskModel.OnPost();

            serviceMock.Verify(sm => sm.BookDesk(bookDeskModel.DeskBookingRequest), Times.Once());
        }
    }
}
