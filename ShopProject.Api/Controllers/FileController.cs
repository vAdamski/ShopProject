using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;

namespace ShopProject.Api.Controllers;

[Route("api/files")]
public class FileController : BaseController
{
    private readonly IProductFileManagementService _productFileManagementService;
    private readonly IAppDbContext _ctx;

    public FileController(IProductFileManagementService productFileManagementService, IAppDbContext ctx)
    {
        _productFileManagementService = productFileManagementService;
        _ctx = ctx;
    }
    
    [HttpGet]
    [Route("{fileId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFile(string fileId)
    {
        var fileInfo = await _ctx.ProductImages.FirstOrDefaultAsync(x => x.Id == Guid.Parse(fileId));
        
        if (fileInfo == null)
        {
            return NotFound();
        }
        
        var file = await _productFileManagementService.GetFile(fileInfo.ImagePath);
        
        if (file == null)
        {
            return NotFound();
        }
        
        return File(file.Data, file.ContentType, file.FileName);
    }
}