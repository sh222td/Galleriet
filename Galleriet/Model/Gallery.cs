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
            SantizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
            PhysicalUploadImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Content", "Images");
            ApprovedExtensions = new Regex(@"^.*\.(gif|jpg|png)$", RegexOptions.IgnoreCase);
        }

        /* Returnerar en lista på uppladdade bilder i en sorterad ordning */
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
            /* Undersöker om bildfilen är av en godkänd MIME-typ */
            return image.RawFormat.Guid == ImageFormat.Gif.Guid ||
                image.RawFormat.Guid == ImageFormat.Jpeg.Guid ||
                image.RawFormat.Guid == ImageFormat.Png.Guid;
        }

        public string SaveImage(Stream stream, string fileName)
        {
            /* Skapar och sparar en tumnagel av bilden som används av den abstrakta basklassen */
            var image = System.Drawing.Image.FromStream(stream);

            if (IsValidImage(image)) 
            {
                /* Tar bort otillåtna tecken med hjälp utav Regex variabeln SantizePath */
                fileName = SantizePath.Replace(fileName, "");

                /* Ger uppladdade bilder som redan finns ett nytt namn med hjälp utav en räknare, 
                 * använder sig utav ett temporärt filnamn för att "nollställa" namnet om den redan innehåller en räknare */
                int i = 2;
                string tempFilename = fileName;
                while (ImageExists(tempFilename)) 
                {
                    tempFilename = Path.GetFileNameWithoutExtension(fileName) + "(" + i++ + ")" + Path.GetExtension(fileName);
                }
                fileName = tempFilename;

                /* Sparar både image och thumbnail image i sin angivna mapp */
                image.Save(Path.Combine(PhysicalUploadImagesPath, fileName));

                var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
                thumbnail.Save(Path.Combine(PhysicalUploadImagesPath, "ThumbImages", fileName));

                return fileName;
            }
            else
            {
                throw new ArgumentException();
            }
        }

    }
}