using ImageResizer;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace WhiteRaven.Core.Helper
{
    public class FileHelper
    {

        public static string SaveFile(HttpPostedFileBase file, string folder, string format, string fileName = null, bool cleanFolder = false)
        {
            byte[] data;
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }
            return SaveFile(data, folder, format, fileName);
        }

        public static string SaveFileResizer(byte[] file, string folder, string format, int height, int width, string fileName = null, bool cleanFolder = false)
        {
            try
            {
                if (file == null)
                {
                    return "";
                }

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(folder)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folder));
                }
                else
                {
                    if (cleanFolder)
                    {
                        Directory.GetFiles(HttpContext.Current.Server.MapPath(folder), "*.jpg").ToList().ForEach(f =>
                        {
                            File.Delete(f);
                        });
                    }
                }

                if (string.IsNullOrEmpty(fileName))
                    fileName = DateTime.UtcNow.ToString("yyyyMMddHHmmss") + format;

                string filePath = HttpContext.Current.Server.MapPath(folder + fileName);

                MemoryStream stream = new MemoryStream();
                MemoryStream dest = new MemoryStream();
                stream.Write(file, 0, file.Length);

                stream.Seek(0, SeekOrigin.Begin);

                if (height == 0) {
                    height = 700;
                }
                if (width == 0)
                {
                    width = 800;
                }

                var settings = new ResizeSettings()
                {
                    Height = height,
                    Width = width
                };

                settings.Mode = FitMode.Stretch;

                ImageBuilder.Current.Build(stream, filePath, settings);


                return fileName;
            }
            catch (Exception)
            {

            }

            return "";
        }

        public static string SaveFile(byte[] file, string folder, string format, string fileName = null, bool cleanFolder = false)
        {

            MemoryStream stream = new MemoryStream();

            MemoryStream dest = new MemoryStream();
            stream.Write(file, 0, file.Length);
            stream.Seek(0, SeekOrigin.Begin);
            ImageBuilder.Current.Build(stream, dest, new ResizeSettings());

            if (file == null)
            {
                return "";
            }

            if (!Directory.Exists(HttpContext.Current.Server.MapPath(folder)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folder));
            }
            else
            {
                if (cleanFolder)
                {
                    Directory.GetFiles(HttpContext.Current.Server.MapPath(folder), "*.jpg").ToList().ForEach(f =>
                    {
                        File.Delete(f);
                    });
                }
            }

            if (string.IsNullOrEmpty(fileName))
                fileName = DateTime.UtcNow.ToString("yyyyMMddHHmmss") + format;

            BinaryWriter Writer = null;

            string filePath = HttpContext.Current.Server.MapPath(folder + fileName);
            Writer = new BinaryWriter(File.OpenWrite(filePath));
            Writer.Write(file);
            Writer.Flush();
            Writer.Close();
            return fileName;
        }

    }
}
