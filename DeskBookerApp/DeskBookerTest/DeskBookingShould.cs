using System.ComponentModel.DataAnnotations;
using DeskBookerApp.Domain.Desk;
using DeskBookerApp.Domain.DeskBooking;
using DeskBookerApp.Exceptions;
using DeskBookerApp.Interfaces;
using DeskBookerApp.Services;
using DeskBookerApp.Utils;
using FluentAssertions;
using NSubstitute;

namespace DeskBookerTest;

public class DeskBookingShould
{
    private DeskBookingRequest _request;
    private IDeskRepository _deskRepository;
    private IDeskBookingRepository _deskBookingRepository;
    private DeskBookingService _service;
    private List<Desk> _availableDesks;

    [SetUp]
    public void Setup()
    {
        _request =
            DeskBookingRequest.Create("Luis", "Borges", "lborges@aidacanarias.com", new DateTime(2024, 5, 10));
        _availableDesks = new List<Desk> { new Desk{ Id = 7 } };

        _deskBookingRepository = Substitute.For<IDeskBookingRepository>();
        _deskRepository = Substitute.For<IDeskRepository>();
        _deskRepository.GetAvailableDesks(_request.Date).Returns(_availableDesks);

        _service = new DeskBookingService(_deskBookingRepository, _deskRepository);
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
        _deskBookingRepository.Save(Arg.Do<DeskBooking>(deskBooking => { savedDeskBooking = deskBooking; }));

        _service.BookDesk(_request);
        _deskBookingRepository.Received(1).Save(Arg.Is<DeskBooking>(savedDeskBooking));

        savedDeskBooking.Should().NotBeNull();
        savedDeskBooking.Should().BeEquivalentTo(_request, options => options.Excluding(dbr => dbr.DeskId));
        savedDeskBooking.DeskId.Should().Be(_availableDesks.First().Id);
    }

    [Test]
    public void not_save_desk_booking_if_no_desk_available()
    {
        _availableDesks.Clear();

        _service.BookDesk(_request);

        _deskBookingRepository.Received(0).Save(Arg.Any<DeskBooking>());
    }

    [Test]
    public void return_no_desk_available_code_when_no_desk_available()
    {
        _availableDesks.Clear();

        var result = _service.BookDesk(_request);

        result.Code.Should().Be(DeskBookingResultCode.NoDeskAvailable);
    }

    [Test]
    public void return_success_code_when_desk_is_available()
    {
        var result = _service.BookDesk(_request);

        result.Code.Should().Be(DeskBookingResultCode.Success);
    }
    
    [Test]
    public void return_expected_desk_booking_id()
    {
        _deskBookingRepository.Save(Arg.Do<DeskBooking>(deskBooking => { deskBooking.Id = 5; }));

        var result = _service.BookDesk(_request);

        result.DeskBookingId.Should().Be(5);
    }

    [Test]
    public void return_null_desk_booking_when_no_desks_available()
    {
        _availableDesks.Clear();

        var result = _service.BookDesk(_request);

        result.DeskBookingId.Should().BeNull();
    }
}