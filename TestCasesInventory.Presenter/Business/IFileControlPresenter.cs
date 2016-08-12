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
        void UploadFile(HttpPostedFileBase file, string id);
        string GetFileFolder(string id);
        string GetFileUrl(int id);
    }
}
