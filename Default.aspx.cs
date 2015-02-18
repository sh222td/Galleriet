using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Galleriet.Model;
using System.IO;

namespace Galleriet
{
    public partial class Default : System.Web.UI.Page
    {
        private Gallery _gallery;
        public Gallery gallery 
        {
            get { return _gallery ?? (Gallery)(_gallery = new Gallery()); }  
        }

        private Boolean checkSession
        {
            get { if ((bool)Session["sucessMessage"]) 
            { 
                return true; 
            } return false; }
            set { Session["sucessMessage"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string imgPath = Request.Url.ToString();
            string imgName = Path.GetFileName(imgPath);

            if (imgName != "" && imgName != "Default.aspx") 
            {
                BigImage.ImageUrl = "~/Content/Images/" + imgName;
                
                PlaceholderImage.Visible = true;

                if (checkSession == true)
                {
                    checkSession = false;
                }
            }
        }

        protected void SendButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            { 
                string file = gallery.SaveImage(FileUploadButton.PostedFile.InputStream, FileUploadButton.PostedFile.FileName);
                PlaceholderMSG.Visible = true;
                checkSession = true;
                Response.Redirect("Default.aspx/" + file);
            }
        }

        public IEnumerable<string> ImageRepeater_GetData()
        {
            /* Kallar på metoden i Gallery klassen som hämtar ut alla existerande bilder*/
            return gallery.GetImageNames();
        }
    }
}