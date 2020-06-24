using System.Configuration;

namespace ImpressDev.Infrastructure
{
    public class AppConfig
    {
        private static string bookPhotoSourceFolder = ConfigurationManager.AppSettings["BookPhotoSourceFolder"];
        public static string BookPhotoSourceFolder
        {
            get
            {
                return bookPhotoSourceFolder;
            }
        }
    }
}