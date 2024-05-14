using DeskBookerApp.Domain.DeskBooking;
using DeskBookerApp.Exceptions;
using DeskBookerApp.Utils;
using FluentAssertions;

namespace DeskBookerTest;

public class DeskBookingShould
{
    private DeskBookingService _service;

    [SetUp]
    public void Setup()
    {
        _service = new DeskBookingService();
    }

    [Test]
    public void return_desk_booking_result_with_request_values()
    {
        var request =
            DeskBookingRequest.Create("Luis", "Borges", "lborges@aidacanarias.com", new DateTime(2024, 5, 10));

        var result = _service.BookDesk(request);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(request);
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
}