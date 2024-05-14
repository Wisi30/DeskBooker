using System.ComponentModel.DataAnnotations;
using DeskBookerApp.Domain.Desk;
using DeskBookerApp.Domain.DeskBooking;
using DeskBookerApp.Exceptions;
using DeskBookerApp.Interfaces;
using DeskBookerApp.Utils;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DeskBookerTest;

public class DeskBookingShould
{
    private DeskBookingRequest _request;
    private Mock<IDeskBookingRepository> _deskBookingRepositoryMock;
    private Mock<IDeskRepository> _deskRepositoryMock;
    private IDeskBookingRepository _repository;
    private DeskBookingService _service;
    private List<Desk> _availableDesks;

    [SetUp]
    public void Setup()
    {
        _request =
            DeskBookingRequest.Create("Luis", "Borges", "lborges@aidacanarias.com", new DateTime(2024, 5, 10));

        _availableDesks = new List<Desk> { new Desk() };

        _deskBookingRepositoryMock = new Mock<IDeskBookingRepository>();
        _deskRepositoryMock = new Mock<IDeskRepository>();
        _deskRepositoryMock.Setup(dr => dr.GetAvailableDesks(_request.Date)).Returns(_availableDesks);

        _repository = Substitute.For<IDeskBookingRepository>();

        _service = new DeskBookingService(_deskBookingRepositoryMock.Object, _deskRepositoryMock.Object);
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
        _deskBookingRepositoryMock.Setup(dbr => dbr.Save(It.IsAny<DeskBooking>())).Callback<DeskBooking>(
            deskBooking => { savedDeskBooking = deskBooking; });

        _service.BookDesk(_request);
        _deskBookingRepositoryMock.Verify(dbr => dbr.Save(It.IsAny<DeskBooking>()), Times.Once);

        savedDeskBooking.Should().NotBeNull();
        savedDeskBooking.Should().BeEquivalentTo(_request);
    }

    [Test]
    public void not_save_desk_booking_if_no_desk_available()
    {
        _availableDesks.Clear();

        _service.BookDesk(_request);

        _deskBookingRepositoryMock.Verify(dbr => dbr.Save(It.IsAny<DeskBooking>()), Times.Never);
    }
}