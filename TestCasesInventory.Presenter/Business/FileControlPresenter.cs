using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Config;

namespace TestCasesInventory.Presenter.Business
{
    public class FileControlPresenter : PresenterBase, IFileControlPresenter
    {
        public void UploadFile(HttpPostedFileBase file, string id, HttpContextBase context)
        {
            var filePath = GetFileFolder(id);
            var serverPath = Path.Combine(context.Server.MapPath(filePath), Path.GetFileName(file.FileName));
            PathHelper.EnsureDirectories(serverPath);
            file.SaveAs(serverPath);
        }

        public string GetFileFolder(string id)
        {
            var folderPath = Path.Combine(TestCaseConfigurations.TestCasesFolderPath, id);
            return folderPath;
        }
        
        public string[] GetFileUrlList(int id, HttpContextBase context)
        {
            var attachmentFolder = GetFileFolder(id.ToString());
            var filePath = Directory.GetFiles(context.Server.MapPath(attachmentFolder));
            string[] fileUrlList= new string[filePath.Length];
            for (int i =0; i < filePath.Length; i++)
            {
                fileUrlList[i] = Path.GetFileName(filePath[i]);
                fileUrlList[i] = Path.Combine(attachmentFolder, fileUrlList[i]);
            }
            return fileUrlList;
        }
       
        public void DeleteFile(string item, HttpContextBase context)
        {
            try
            {
                var path = context.Server.MapPath(item);
                File.Delete(path);
            }
            catch(Exception e)
            {
                logger.Error(e);
            }
        }
        
        public bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
        public bool IsAttachmentExisted(int id, HttpContextBase context)
        {
            var attachmentFolder = context.Server.MapPath(GetFileFolder(id.ToString()));
            if (!Directory.Exists(attachmentFolder))
            {
                return false;
            }
            else
            {
                var isAttachmentUrlExisted = !IsDirectoryEmpty(attachmentFolder);
                if (isAttachmentUrlExisted)
                {
                    return true;
                }
                else
                    return false;
            }
        }
        public string[] GetFileNameList(string[] fileUrlList)
        {
            var fileNameList = new string[fileUrlList.Length] ;
            for (int i = 0; i < fileUrlList.Length; i++)
            {
                fileNameList[i] = Path.GetFileName(fileUrlList[i]);
            }
            return fileNameList;
        }

    }
}
