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
        #region Fields

        HttpServerUtilityBase server;

        #endregion

        #region Constructors

        public FileControlPresenter(HttpServerUtilityBase serverParam)
        {
            server = serverParam;
        }

        #endregion
        #region Methods
        public void UploadFile(HttpPostedFileBase file, string filePath)
        {          
            PathHelper.EnsureDirectories(filePath);
            file.SaveAs(filePath);
        }
        public string GetFileFolder(string id)
        {
            var folderPath = Path.Combine(TestCaseConfigurations.TestCasesFolderPath, id);
            return folderPath;
        }
        public string GetFileUrl(int id)
        {
            var attachmentFolder = GetFileFolder(id.ToString());
            var filePath = Directory.GetFiles(server.MapPath(attachmentFolder));
            var fileName = Path.GetFileName(filePath[0]);
            return Path.Combine(attachmentFolder, fileName);

        }
        public string[] GetFileUrlList(int id)
        {
            var attachmentFolder = GetFileFolder(id.ToString());
            var filePath = Directory.GetFiles(server.MapPath(attachmentFolder));
            string[] fileUrlList = new string[filePath.Length];
            for (int i = 0; i < filePath.Length; i++)
            {
                fileUrlList[i] = Path.GetFileName(filePath[i]);
                fileUrlList[i] = Path.Combine(attachmentFolder, fileUrlList[i]);
            }
            return fileUrlList;
        }

        public void DeleteFile(string item)
        {
            try
            {
                var path = server.MapPath(item);
                File.Delete(path);
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
        }

        public bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
        public bool IsAttachmentExisted(int id)
        {
            var attachmentFolder = server.MapPath(GetFileFolder(id.ToString()));
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
            var fileNameList = new string[fileUrlList.Length];
            for (int i = 0; i < fileUrlList.Length; i++)
            {
                fileNameList[i] = Path.GetFileName(fileUrlList[i]);
            }
            return fileNameList;
        }

        public void UploadTestCaseAttachment(HttpPostedFileBase file, string id)
        {
            var filePath = GetFileFolder(id);
            var serverPath = Path.Combine(server.MapPath(filePath), Path.GetFileName(file.FileName));
            UploadFile(file, serverPath);
        }
        #endregion;
    }
}
