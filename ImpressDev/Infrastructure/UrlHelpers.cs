using System.IO;
using System.Web.Mvc;

namespace ImpressDev.Infrastructure
{
    public static class UrlHelpers
    {
        public static string BookPhotoSourcePath(this UrlHelper helper, string PhotoSource)
        {
            var bookPhotoSourceFolder = AppConfig.BookPhotoSourceFolder;
            var path = Path.Combine(bookPhotoSourceFolder, PhotoSource);
            var absolutePath = helper.Content(path);
            return absolutePath;
        }
    }
}