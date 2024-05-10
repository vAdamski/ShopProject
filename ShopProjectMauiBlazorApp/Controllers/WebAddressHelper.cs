namespace ShopProjectMauiBlazorApp.Controllers;

public static class WebAddressHelper
{
    public static string Port => "6001";
    
    public static string Address => DeviceInfo.Platform == DevicePlatform.Android ? $"https://10.0.2.2:{Port}" : $"https://localhost:{Port}";
}