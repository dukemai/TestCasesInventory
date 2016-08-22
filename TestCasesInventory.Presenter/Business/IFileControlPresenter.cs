using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TestCasesInventory.Presenter.Business
{
    public interface IFileControlPresenter 

    {
        void UploadFile(HttpPostedFileBase file, string id, HttpContextBase context);
        string GetFileFolder(string id);
        bool IsDirectoryEmpty(string path);
        bool IsAttachmentExisted(int id, HttpContextBase context);
        string[] GetFileUrlList(int id, HttpContextBase context);
        void DeleteFile(string item, HttpContextBase context);
        string[] GetFileNameList(string[] fileUrlList);
    }
}
