using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Application.Files.Queries.GetImage;

namespace ShopProject.Api.Controllers;

[Route("api/files")]
public class FileController : BaseController
{
    private readonly ILogger<FileController> _logger;

    public FileController(ILogger<FileController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    [Route("{fileId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFile(string fileId)
    {
        try
        {
            var file = await Mediator.Send(new GetImageQuery(fileId));
        
            return File(file.Data, file.ContentType, file.FileName);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting file");
        }

        return NotFound();
    }
}