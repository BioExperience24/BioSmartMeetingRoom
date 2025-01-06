using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Booking;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IBookingService _service;
    public IndexModel(IBookingService service)
    {
        _service = service;
    }

    // Message property to display in the view
    public string Message { get; set; } = "Booking";  // Default message

    public BookingMenuDetailViewModel BookingDetails { get; set; }

    public async Task OnGetAsync()
    {
        BookingDetails = await _service.GetDataBooking();

    }
}