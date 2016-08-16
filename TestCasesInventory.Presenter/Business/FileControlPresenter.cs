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
    public class FileControlPresenter : IFileControlPresenter
    {
        public void UploadFile(HttpPostedFileBase file, string id)
        {
            var filePath = GetFileFolder(id);
            var serverPath = Path.Combine(HttpContext.Current.Server.MapPath(filePath), Path.GetFileName(file.FileName));
            PathHelper.EnsureDirectories(serverPath);
            file.SaveAs(serverPath);
        }

        public string GetFileFolder(string id)
        {
            var folderPath = Path.Combine(TestCaseConfigurations.TestCasesFolderPath, id);
            return folderPath;
        }
        public string GetFileUrl(int id)
        {
            var attachmentFolder = GetFileFolder(id.ToString());
            var filePath = Directory.GetFiles(HttpContext.Current.Server.MapPath(attachmentFolder));
            var fileName = Path.GetFileName(filePath[0]);
            return Path.Combine(attachmentFolder, fileName);

        }
        public void DeleteFile(int id)
        {
            var attachmentFolder = GetFileFolder(id.ToString());
            var filePath = Directory.GetFiles(HttpContext.Current.Server.MapPath(attachmentFolder));
            File.Delete(filePath[0]);
        }
        public bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
        public bool IsAttachmentExisted(int id)
        {
            var attachmentFolder = HttpContext.Current.Server.MapPath(GetFileFolder(id.ToString()));
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
    }
}
