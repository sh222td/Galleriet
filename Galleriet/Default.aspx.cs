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
        /* Hämtar Gallery klassen om den redan finns, annars skapas en ny */ 
        public Gallery gallery 
        {
            get { return _gallery ?? (Gallery)(_gallery = new Gallery()); }  
        }

        /* Kollar om sessionen är null eller om den redan finns */
        private Boolean checkSession
        {
            get
            {
                var result = Session["sucessMessage"] as bool? ?? false;
                if (result)
                {
                    Session.Remove("sucessMessage");
                }

                return result;

                //if (Session["sucessMessage"] == null) 
                //{ 
                //    return false; 
                //}
                
                //if ((bool)Session["sucessMessage"]) 
                //{ 
                //    return true; 
                //}
                //return false; 
            }
            set 
            { 
                Session["sucessMessage"] = value; 
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /* Hämtar ut den aktuella url:en, hämtar sedan ut filnamnet */
            string imgPath = Request.Url.ToString();
            string imgName = Path.GetFileName(imgPath);

            if (imgName != "" && imgName != "Default.aspx") 
            {
                BigImage.ImageUrl = "~/Content/Images/" + imgName;
                
                PlaceholderImage.Visible = true;
                if (checkSession)
                {
                    //checkSession = false;
                    PlaceholderMSG.Visible = true;
                }
            }
        }

        protected void SendButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                /* Gör en variabel av den uppladdade bilden och skickar vidare den till Gallery för bildhantering */
                string file = gallery.SaveImage(FileUploadButton.PostedFile.InputStream, FileUploadButton.PostedFile.FileName);
                checkSession = true;
                Response.Redirect("?name=/" + file);
            }
        }

        public IEnumerable<string> ImageRepeater_GetData()
        {
            /* Kallar på metoden i Gallery klassen som hämtar ut alla existerande bilder*/
            return gallery.GetImageNames();
        }
    }
}