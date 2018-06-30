using SOSApp.Core.Helper;
using ImageResizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SOSApp.Core.Media
{
    public partial class ImageManager
    {
        private static string[] ValidImageFileTypes = { "jpg", "jpeg", "png" };

        public static string LoadResizedImage(string origin, string destination, string filename, int width, int height)
        {
            try
            {
                var thumb = string.Format("{0}_{1}_{2}{3}", Path.GetFileNameWithoutExtension(filename), width, height, Path.GetExtension(filename));

                if (File.Exists(Path.Combine(destination, thumb)))
                    return thumb;

                var instructions = new Instructions
                {
                    Width = width,
                    Height = height,
                    Mode = FitMode.Crop,
                    JpegQuality = AppHelper.LoadAppSettingInteger("SOSApp.WebApp.Media.Image.Quality", 90)
                };

                if (File.Exists(Path.Combine(origin, filename)))
                {
                    if (!Directory.Exists(destination))
                        Directory.CreateDirectory(destination);

                    ImageBuilder.Current.Build(Path.Combine(origin, filename), Path.Combine(destination, thumb), instructions);
                }

                return thumb;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string LoadImageURL(Type type, int id, string name, int width, int height)
        {
            if (name.NotEmpty())
            {
                var pathByType = LoadPathByType(type);

                var path = Path.Combine(PhysicalPath, pathByType, id.ToString());

                var thumbPath = Path.Combine(PhysicalThumbPath, pathByType, id.ToString());

                var thumb = LoadResizedImage(
                        path,
                        thumbPath,
                        name,
                        width,
                        height);

                if (File.Exists(Path.Combine(thumbPath, thumb)))
                    return string.Format("{0}/{1}/{2}/{3}", WebThumbPath, pathByType, id, thumb);
            }

            return AppDefaultImage(width, height);
        }

        public static string AppDefaultImage(int width, int height)
        {
            var thumb = LoadResizedImage(PhysicalPath, PhysicalThumbPath, AppHelper.LoadAppSetting("SOSApp.WebApp.Media.Image.Default"), width, height);

            return string.Format("{0}/{1}", WebThumbPath, thumb);
        }

        public static string LoadPathByType(Type type)
        {
            var path = string.Empty;

            switch (type.Name.ToLower())
            {
                case "hotel":
                    path = "hotels";
                    break;

                case "service":
                    path = "services";
                    break;

                case "receptivetour":
                    path = "receptivetours";
                    break;

                case "restaurant":
                    path = "restaurants";
                    break;

                case "Ciudad":
                    path = "cities";
                    break;

                case "benchmark":
                    path = "benchmarks";
                    break;

                default:
                    path = string.Empty;
                    break;
            }

            return path;
        }

        public static void DeleteImage(string path, string filename)
        {
            var curr = Path.Combine(PhysicalPath, path, filename);

            if (File.Exists(curr))
                File.Delete(curr);
        }

        public static bool IsValidImageFileType(string filename)
        {
            string ext = Path.GetExtension(filename);

            ext = ext.ToLower();

            return ValidImageFileTypes.Any(t => ext == "." + t);
        }

        public static string GetImageExtension(string filename)
        {
            return Path.GetExtension(filename);
        }

        public static string PhysicalPath
        {
            get
            {
                return AppHelper.LoadAppSetting("SOSApp.WebApp.Media.PhysicalPath");
            }
        }

        public static string PhysicalThumbPath
        {
            get
            {
                return AppHelper.LoadAppSetting("SOSApp.WebApp.Media.PhysicalThumbPath");
            }
        }

        public static string WebThumbPath
        {
            get
            {
                var url = AppHelper.LoadAppSetting("SOSApp.WebApp.Media.WebThumbPath");

                if (!url.NotEmpty())
                    url = string.Format("{0}/avatars/thumbs", AppHelper.CDNLocation());

                return url;
            }
        }
    }
}
