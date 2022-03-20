using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.Helper
{
   public static class UplaodFile
    {
        public static string uploadfile(string localpath, IFormFile File)
        {
            try
            {
                string FolderPath =Directory.GetCurrentDirectory()+localpath;
                string fileName = Guid.NewGuid() + Path.GetFileName(File.FileName);
                string finalPath = Path.Combine(FolderPath,fileName);
                using (var stream = new FileStream(finalPath, FileMode.Create))
                {
                    File.CopyTo(stream);
                }
                return fileName;
            }catch(Exception ex)
            {
                return ex.Message;
            }
           
        }
        public static string Removefile(string localpath, string FileName)
        {
            try
            {
                // System.IO ==>   1-Directory(GetCurrentDirectory,SetCurrentDirectory,files,delete,replace)
                //                 2-File(create,readLines,exists,delete,copy,replace) 
                string DeletedPath = System.IO.Directory.GetCurrentDirectory() + localpath + FileName;
                if (System.IO.File.Exists(DeletedPath))
                {
                 
                    System.IO.File.Delete(DeletedPath);
                }
                return "Deleted";
            }catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
