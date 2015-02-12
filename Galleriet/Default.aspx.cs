using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Galleriet.Model;

namespace Galleriet
{
    public partial class Default : System.Web.UI.Page
    {
        private Gallery _gallery;
        public Gallery gallery 
        {
            get { return _gallery ?? (Gallery)(_gallery = new Gallery()); }  
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SendButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                //var imageList = gallery.GetImageNames();
                
                Gallery gallery = new Gallery();
                
                gallery.SaveImage(FileUploadButton.PostedFile.InputStream, FileUploadButton.PostedFile.FileName);
                
            }
        }

        public IEnumerable<string> ImageRepeater_GetData()
        {
            return gallery.GetImageNames();
        }
    }
}