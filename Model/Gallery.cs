using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
            PhysicalUploadImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Content\Images\");
            ApprovedExtensions = new Regex(@"^.*\.(gif|jpg|png)$", RegexOptions.IgnoreCase);
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

        private bool IsValidImage(Image image)
        {
            if (image.RawFormat.Guid == ImageFormat.Gif.Guid ||
                image.RawFormat.Guid == ImageFormat.Jpeg.Guid ||
                image.RawFormat.Guid == ImageFormat.Png.Guid) 
            {
                return true;
            }
            return false;
        }

        public string SaveImage(Stream stream, string fileName)
        {
            var image = System.Drawing.Image.FromStream(stream); // stream -> ström med bild
            var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);

            if (IsValidImage(image)) 
            {
                /* Tar bort otillåtna tecken med hjälp utav Regex variabeln SantizePath */
                fileName = SantizePath.Replace(fileName, "");

                /* Ger uppladdade bilder som redan finns ett nytt namn med hjälp utav en räknare */
                int i = 2;
                string tempFilename = fileName;
                while (ImageExists(tempFilename)) 
                {
                    string name = Path.GetFileNameWithoutExtension(fileName);
                    string ext = Path.GetExtension(fileName);
                    tempFilename = fileName;
                    tempFilename = name + "(" + i + ")" + ext;
                    i++;
                }
                fileName = tempFilename;

                /* Sparar både image och thumbnail image i sin rätta mapp */
                image.Save(PhysicalUploadImagesPath + fileName);
                thumbnail.Save(Path.Combine(PhysicalUploadImagesPath, "ThumbImages/" + fileName)); // path -> fullständig fysisk filnamn inklusive sökväg

                return fileName;
            }
            else
            {
                throw new ArgumentException();
            }
        }

    }
}