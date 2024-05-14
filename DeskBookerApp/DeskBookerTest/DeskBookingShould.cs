using DeskBookerApp.Domain.DeskBooking;
using DeskBookerApp.Exceptions;
using DeskBookerApp.Interfaces;
using DeskBookerApp.Utils;
using FluentAssertions;
using Moq;

namespace DeskBookerTest;

public class DeskBookingShould
{
    private DeskBookingRequest _request;
    private Mock<IDeskBookingRepository> _deskBookingRepositoryMock;
    private DeskBookingService _service;

    [SetUp]
    public void Setup()
    {
        _request =
            DeskBookingRequest.Create("Luis", "Borges", "lborges@aidacanarias.com", new DateTime(2024, 5, 10));
        
        _deskBookingRepositoryMock = new Mock<IDeskBookingRepository>();

        _service = new DeskBookingService(_deskBookingRepositoryMock.Object);
    }

    [Test]
    public void return_desk_booking_result_with_request_values()
    {
        var result = _service.BookDesk(_request);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_request);
    }

    [Test]
    public void throw_invalid_email_exception_when_email_is_not_valid()
    {
        var action = () => DeskBookingRequest.Create("Luis", "Borges", "pepee.pep.com", new DateTime(2024, 5, 10));

        action.Should().Throw<InvalidEmailException>().WithMessage(Constants.InvalidEmailMessage);
    }

    [Test]
    public void throw_exception_if_request_is_null()
    {
        var action = () => _service.BookDesk(null);

        action.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void save_desk_booking()
    {
        DeskBooking savedDeskBooking = null;
        _deskBookingRepositoryMock.Setup(db => db.Save(It.IsAny<DeskBooking>())).Callback<DeskBooking>(
            deskBooking =>
            {
                savedDeskBooking = deskBooking;
            });

        _service.BookDesk(_request);
        _deskBookingRepositoryMock.Verify(db => db.Save(It.IsAny<DeskBooking>()), Times.Once);

        savedDeskBooking.Should().NotBeNull();
        savedDeskBooking.Should().BeEquivalentTo(_request);
    }
}