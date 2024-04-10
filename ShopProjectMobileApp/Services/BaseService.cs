using System.Text.Json;

namespace ShopProjectMobileApp.Services;

public class BaseService
{
    protected JsonSerializerOptions _jsonSerializerOptions;

    protected string _baseUrl;

    public BaseService()
    {
        _baseUrl = DeviceInfo.Current.Platform == DevicePlatform.Android
            ? "https://10.0.2.2:6001"
            : "https://localhost:6001";
        
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }
}