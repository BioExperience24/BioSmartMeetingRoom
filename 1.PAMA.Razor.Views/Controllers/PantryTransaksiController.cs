using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PantryTransaksiController(IPantryTransaksiService service)
    : BaseController<PantryTransaksiViewModel>(service)
{
}