using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claudia.Domain.Configuration
{
    public partial class AppConfig
    {
        public static Dictionary<string, ConfigModel> ImageData = new Dictionary<string, ConfigModel>(){
            {"gallery", new ConfigModel{
                FilePathRelative = "~/images/gallery/",
                FileFormatType = ".jpg",
                MaxActiveItems = 100,
                ImageVersions = new Dictionary<string,string>()
                    {
                        {"_gltmb", "maxwidth=60&maxheight=40&format=jpg"},
                        {"_glimg", "maxwidth=640&maxheight=480&format=jpg"}
                    }
            }},
            {"carousel", new ConfigModel{
                FilePathRelative = "~/images/gallery/",
                FileFormatType = ".jpg",
                MaxActiveItems = 1,
                ImageVersions = new Dictionary<string,string>()
                    {
                        {"_jttmb", "maxwidth=80&maxheight=80&format=jpg"},
                        {"_jtimg", "width=1200&height=400&format=jpg"}
                    }
            }},
            {"recipe", new ConfigModel{
                FilePathRelative = "~/images/recipes/",
                FileFormatType = ".jpg",
                MaxActiveItems = 100,
                ImageVersions = new Dictionary<string,string>()
                    {
                        {"_rctmb", "maxwidth=80&maxheight=80&format=jpg"},
                        {"_rcimg", "maxwidth=640&maxheight=480&format=jpg"}
                    }
            }}
        };

        public struct ConfigModel
        {
            public string FilePathRelative { get; set; }

            public string FileFormatType { get; set; }

            public int MaxActiveItems { get; set; }

            public Dictionary<string, string> ImageVersions { get; set; }

            public string GetImageUrl(string filename)
            {
                return FilePathRelative.Replace("~", "") + filename + ImageVersions.FirstOrDefault(s => s.Key.EndsWith("img")).Key + FileFormatType;
            }
            public string GetThumnailUrl(string filename)
            {
                return FilePathRelative.Replace("~", "") + filename + ImageVersions.FirstOrDefault(s => s.Key.EndsWith("tmb")).Key + FileFormatType;
            }
        }


        [Obsolete("use image data collection")]
        public class GalleryImages
        {
            public static string FilePathRelative = "~/images/gallery/";

            public static string FileFormatType = ".jpg";

            public static int MaxActiveItems = 100;

            public static Dictionary<string, string> Versions = new Dictionary<string, string>()
            {
                {"_gltmb", "maxwidth=60&maxheight=40&format=jpg"},
                {"_glimg", "maxwidth=640&maxheight=480&format=jpg"}
            };

            public static string GetImageUrl(string filename)
            {
                return FilePathRelative.Replace("~", "") + filename + "_glimg" + FileFormatType;
            }

            public static string GetThumbUrl(string filename)
            {
                return FilePathRelative.Replace("~", "") + filename + "_gltmb" + FileFormatType;
            }
        }

        [Obsolete("use image data collection")]
        public class CarouselImages
        {
            public static string FilePathRelative = "~/images/carousel/";

            public static string FileFormatType = ".jpg";

            public static int MaxActiveItems = 1;

            public static Dictionary<string, string> Versions = new Dictionary<string, string>()
            {
                {"_jtimg", "width=1200&height=400&format=jpg"}
            };

            public static string GetImageUrl(string filename)
            {
                return FilePathRelative.Replace("~", "") + filename + "_jtimg" + FileFormatType;
            }
        }

        [Obsolete("use image data collection")]
        public class RecipeImages
        {
            public static string FilePathRelative = "~/images/recipes/";

            public static string FileFormatType = ".jpg";

            public static int MaxActiveItems = 100;

            public static Dictionary<string, string> Versions = new Dictionary<string, string>()
        {
                {"_rctmb", "maxwidth=80&maxheight=80&format=jpg"},
                {"_rcimg", "maxwidth=640&maxheight=480&format=jpg"}
            };
        }
    }
}
