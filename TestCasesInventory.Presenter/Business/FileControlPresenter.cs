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
        protected string GetTestCaseFileFolder(int id)
        {
            var folderPath = Path.Combine(TestCaseConfigurations.TestCasesFolderPath, id.ToString());
            return folderPath;
        }    
        public string[] GetFileUrlList(int testCaseId)
        {
            var attachmentFolder = GetTestCaseFileFolder(id);
            var filePath = Directory.GetFiles(server.MapPath(attachmentFolder));
            string[] fileUrlList = new string[filePath.Length];
            for (int i = 0; i < filePath.Length; i++)
            {
                fileUrlList[i] = Path.GetFileName(filePath[i]);
                fileUrlList[i] = Path.Combine(attachmentFolder, fileUrlList[i]);
            }
            return fileUrlList;
        } 
        public bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
        public bool IsAttachmentExisted(int id)
        {
            var attachmentFolder = server.MapPath(GetTestCaseFileFolder(id));
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
        public void UploadTestCaseAttachment(HttpPostedFileBase file, int testCaseId)
        {
            var filePath = GetTestCaseFileFolder(testCaseId);
            var serverPath = Path.Combine(server.MapPath(filePath), Path.GetFileName(file.FileName));
            UploadFile(file, serverPath);
        }

        public string GetTestCaseFileUrl(int testCaseId)
        {
            var attachmentFolder = GetTestCaseFileFolder(testCaseId);
            var filePath = Directory.GetFiles(server.MapPath(attachmentFolder));
            var fileName = Path.GetFileName(filePath[0]);
            return Path.Combine(attachmentFolder, fileName);
        }

        public bool DeleteTestCaseFiles(string item)
        {
            try
            {
                var path = server.MapPath(item);
                File.Delete(path);
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e);
                return false;
            }
        }
        #endregion;
    }
}
