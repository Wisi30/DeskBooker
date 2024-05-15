using DeskBookerApp.Domain.DeskBooking;
using DeskBookerApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeskBookerWeb.Pages
{
    public class BookDeskModel(IDeskBookingService serviceMockObject) : PageModel
    {
        [BindProperty]
        public DeskBookingRequest DeskBookingRequest { get; set; }
        public void OnPost()
        {
        }
    }
}
