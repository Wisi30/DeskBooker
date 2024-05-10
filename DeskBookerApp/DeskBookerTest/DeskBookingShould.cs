using DeskBookerApp.DeskBooking;
using DeskBookerApp.Exceptions;
using DeskBookerApp.Utils;
using FluentAssertions;

namespace DeskBookerTest;

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
        var request =
            DeskBookingRequest.Create("Luis", "Borges", "lborges@aidacanarias.com", new DateTime(2024, 5, 10));

        var processor = new DeskBookingService();

        // When
        var result = processor.BookDesk(request);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(request);
    }

    [Test]
    public void throw_invalid_email_exception_when_email_is_not_valid()
    {
        // When
        var action = () => DeskBookingRequest.Create("Luis", "Borges", "pepee.pep.com", new DateTime(2024, 5, 10));

        // Then
        action.Should().Throw<InvalidEmailException>().WithMessage(Constants.InvalidEmailMessage);
    }
}