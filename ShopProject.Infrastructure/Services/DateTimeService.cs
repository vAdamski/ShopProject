using ShopProject.Application.Common.Interfaces;

namespace ShopProject.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}