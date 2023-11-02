using Microsoft.AspNetCore.Http;

namespace ShopProject.Shared.Dtos;

public class ProductImageDto
{
    public Guid Id { get; set; }
    public bool IsMain { get; set; }
    public string ImagePath { get; set; }
}