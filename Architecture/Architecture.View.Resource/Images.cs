using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.View.Resource
{
    public static class Images
    {
        public enum ImageSize
        {
            Small = 16,
            Medium = 24,
            Large = 32
        }

        /// <summary>
        /// GetImage
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static System.Drawing.Image GetImage(string name,ImageSize size = ImageSize.Medium)
        {
           return (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(name+"_" + (short)size);
        }
        public static System.Drawing.Image GetButtonImageAlways(string name, ImageSize size = ImageSize.Medium)
        {
            var image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(name + "_" + (short)size);
            return image == null ? (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("btn_Refresh" + "_" + (short)size) : image;
        }
        public static System.Drawing.Image GetMenuImageAlways(string name, ImageSize size = ImageSize.Medium)
        {
            var image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(name + "_" + (short)size);
            return image == null ? (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("mnu_Default" + "_" + (short)size) : image;
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        /// 
        public static System.Drawing.Image Access
        {
            get
            {
                return GetImage("btn_Access");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Add
        {
            get
            {
                return GetImage("btn_Add");
            }
        }        

        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Approve
        {
            get
            {
                return GetImage("btn_Approve");
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Attachment
        {
            get
            {
                return GetImage("btn_Attachment");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Back
        {
            get
            {
                return GetImage("btn_Back");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Cancel
        {
            get
            {
                return GetImage("btn_Cancel");
            }
        }
       
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Clear
        {
            get
            {
                return GetImage("btn_Clear");
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Close
        {
            get
            {
                return GetImage("btn_Close");
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Collection
        {
            get
            {
                return GetImage("btn_Collection");
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Copy
        {
            get
            {
                return GetImage("btn_Copy");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Delete
        {
            get
            {
                return GetImage("btn_Delete");
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Divit
        {
            get
            {
                return GetImage("btn_Divit");
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image EditFindReplace
        {
            get
            {
                return GetImage("btn_EditFindReplace");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Email
        {
            get
            {
                return GetImage("btn_Email");
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Error
        {
            get
            {
                return GetImage("btn_Error");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Examine
        {
            get
            {
                return GetImage("btn_Examine");
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Exit
        {
            get
            {
                return GetImage("btn_Exit");
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Export
        {
            get
            {
                return GetImage("btn_Export");
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image File
        {
            get
            {
                return GetImage("btn_File");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Forward
        {
            get
            {
                return GetImage("btn_Forward");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image GetInfo
        {
            get
            {
                return GetImage("btn_GetInfo");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Info
        {
            get
            {
                return GetImage("btn_Info");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image New
        {
            get
            {
                return GetImage("btn_New");;
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Ok
        {
            get
            {
                return GetImage("btn_Ok");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Ok2
        {
            get
            {
                return GetImage("btn_Ok2");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Open
        {
            get
            {
                return GetImage("btn_Open");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Pause
        {
            get
            {
                return GetImage("btn_Pause");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Play
        {
            get
            {
                return GetImage("btn_Play");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Print
        {
            get
            {
                return GetImage("btn_Print");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Refection
        {
            get
            {
                return GetImage("btn_Refection");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Refresh
        {
            get
            {
                return GetImage("btn_Refresh");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Remove
        {
            get
            {
                return GetImage("btn_Remove");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Report
        {
            get
            {
                return GetImage("btn_Report");;
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Save
        {
            get
            {
                return GetImage("btn_Save");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Search
        {
            get
            {
                return GetImage("btn_Search");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Stop
        {
            get
            {
                return GetImage("btn_Stop");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Subtraction
        {
            get
            {
                return GetImage("btn_Subtraction");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Undo
        {
            get
            {
                return GetImage("btn_Undo");;
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Update
        {
            get
            {
                return GetImage("btn_Update");;
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Warning
        {
            get
            {
                return GetImage("btn_Warning");;
            }
        }        

        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Menu_Access
        {
            get
            {
                return GetImage("mnu_Access");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Menu_Default
        {
            get
            {
                return GetImage("mnu_Default");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image Menu_ListMenu
        {
            get
            {
                return GetImage("mnu_listMenu");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image mnu_ResourceManager
        {
            get
            {
                return GetImage("mnu_ResourceManager");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image mnu_Settings
        {
            get
            {
                return GetImage("mnu_Settings");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image mnu_SystemManager
        {
            get
            {
                return GetImage("mnu_SystemManager");;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Image.
        /// </summary>
        public static System.Drawing.Image mnu_Users
        {
            get
            {
                return GetImage("mnu_Users");;
            }
        }
    }
}
