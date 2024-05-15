using DeskBookerApp.Domain.DeskBooking;
using DeskBookerApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeskBookerWeb.Pages
{
    public class BookDeskModel : PageModel
    {
        private IDeskBookingService _deskBookingService;

        public BookDeskModel(IDeskBookingService deskBookingService)
        {
            _deskBookingService = deskBookingService;
        }

        [BindProperty]
        public DeskBookingRequest DeskBookingRequest { get; set; }
        
        public void OnPost()
        {
            _deskBookingService.BookDesk(DeskBookingRequest);
        }
    }
}
