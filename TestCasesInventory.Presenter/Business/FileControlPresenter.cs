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
            var FolderPath = Path.Combine(TestCaseConfigurations.TestCasesFolderPath, id);
            return FolderPath;
        }
        public string GetFileUrl(int id)
        {
            var attachmentFolder = GetFileFolder(id.ToString());
            var filePath = Directory.GetFiles(HttpContext.Current.Server.MapPath(attachmentFolder));
            var fileName = Path.GetFileName(filePath[0]);
            return Path.Combine(attachmentFolder, fileName);
        }
    }
}
