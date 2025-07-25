﻿using _2.BusinessLogic.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _5.Helpers.Consumer.Policy;


namespace _1.API.Controllers.Controllers;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="service"></param>
/// <param name="placemarkService"></param>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
[Route("api/[controller]/[action]")]
[ApiController]
public class AttachmentListController(IAttachmentListService service) : ControllerBase
{

    //harusnya diprotect karena upload file agak rawan
    //[Authorize]
    [HttpPost]
    public async Task<ActionResult> PostFile(IFormFile uploadedFile)
    {
        var result = await service.ProcessFileUploadToBlob(uploadedFile);
        if (result.Status != "Success")
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    //[AllowAnonymous]
    //[HttpGet]
    //public async Task<IActionResult> DownloadFile(Guid id)
    //{
    //    var result = await service.DownloadFile(id);

    //    return File(result.FileStream, "application/octet-stream", result.FileName); // returns a FileStreamResult
    //}

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> NoImageBase64()
    {
        var result = await service.NoImageBase64();

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ViewNoImage()
    {
        var result = await service.ViewNoImage();

        Response.Headers.Append("Content-Disposition", "inline; filename=" + result.FileName);
        return File(result.FileStream, "image/PNG"); // Ganti "image/jpeg" dengan tipe MIME yang sesuai
    }
}