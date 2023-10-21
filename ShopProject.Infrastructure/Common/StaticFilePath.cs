using Microsoft.Extensions.Configuration;
using ShopProject.Application.Common.Interfaces;

namespace ShopProject.Infrastructure.Common;

public class StaticFilePath : IStaticFilePath
{
    public string StaticFolderFilePath { get; } 
    
    public StaticFilePath(IConfiguration configuration)
    {
        var pathFromEnvironment = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        if (!string.IsNullOrEmpty(pathFromEnvironment))
        {
            StaticFolderFilePath = pathFromEnvironment;
            return;
        }
        
        var pathFromConfiguration = configuration["StaticFolderFilePath"];

        if (!string.IsNullOrEmpty(pathFromConfiguration))
        {
            StaticFolderFilePath = pathFromConfiguration;
            return;
        }

        throw new Exception("Static folder file path is not set");
    }
}