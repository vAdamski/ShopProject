using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using ShopProject.Domain.Exceptions;
using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Files.Queries.GetImage;

public class GetImageQueryHandler : IRequestHandler<GetImageQuery, FileData>
{
    private readonly IAppDbContext _ctx;
    private readonly IProductFileManagementService _productFileManagementService;

    public GetImageQueryHandler(IAppDbContext ctx, IProductFileManagementService productFileManagementService)
    {
        _ctx = ctx;
        _productFileManagementService = productFileManagementService;
    }

    public async Task<FileData> Handle(GetImageQuery request, CancellationToken cancellationToken)
    {
        var fileInfo = await _ctx.ProductImages.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (fileInfo == null)
            throw new NotFoundException(nameof(ProductImage), request.Id);

        var file = await _productFileManagementService.GetFile(fileInfo.ImagePath);

        if (file == null)
            throw new NotFoundException(nameof(ProductImage), request.Id);
        
        return file;
    }
}