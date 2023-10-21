namespace ShopProject.Infrastructure.Common;

public static class PathSlashHelper
{
    public static string ChangePathSlash(string path)
    {
        return path.Replace('\\', '/');
    }
}