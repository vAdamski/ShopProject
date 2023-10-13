namespace ShopProject.Application.Common.Interfaces;

public interface ICurrentUserService
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    string Email { get; set; }
    string Name { get; set; }
    bool IsAuthenticated { get; set; }
}