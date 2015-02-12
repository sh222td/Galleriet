using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Galleriet.Model
{
    public class Gallery
    {
        private static Regex ApprovedExtensions;
        private static string PhysicalUploadImagesPath;
        private static Regex SantizePath;

        static Gallery()
        {
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            PhysicalUploadImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Content\Images");
            ApprovedExtensions = new Regex(@"\.(jpg|gif|png)$", RegexOptions.IgnoreCase);
            SantizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
        }

        public IEnumerable<string> GetImageNames()
        {
            var di = new DirectoryInfo(PhysicalUploadImagesPath);

            FileInfo[] images = di.GetFiles();

            var imageList = images
                .Select(image => image.Name)
                .Where(filename => ApprovedExtensions.IsMatch(filename))
                .OrderBy(filename => filename)
                .ToList();

            return imageList.AsReadOnly();
        }

        public static bool ImageExists(string name)
        {
            return File.Exists(Path.Combine(PhysicalUploadImagesPath, name));
        }

        public bool IsValidImage(Image image)
        {
            throw new System.NotImplementedException();
        }

        public string SaveImage(Stream stream, string fileName)
        {
            var image = System.Drawing.Image.FromStream(stream); // stream -> ström med bild
            var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
            image.Save("~/Content/Images/" + fileName);
            thumbnail.Save(@"\Content\Images\ThumbImages\" + fileName); // path -> fullständig fysisk filnamn inklusive sökväg

            return fileName;
        }
    }
}