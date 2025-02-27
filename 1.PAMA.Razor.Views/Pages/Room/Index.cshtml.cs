using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Room;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IRoomService _service;
    public IndexModel(IRoomService service)
    {
        _service = service;
    }

    // Message property to display in the view
    public string Message { get; set; } = "Room";  // Default message

    public RoomDetailsViewModel RoomDetails { get; set; }

    public async Task OnGetAsync()
    {
        // Call the service to get room details
        RoomDetails = await _service.GetRoomDetailsAsync();
   
    }

}
