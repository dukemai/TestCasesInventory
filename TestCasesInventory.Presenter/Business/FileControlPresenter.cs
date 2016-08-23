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
            try
            {
                PathHelper.EnsureDirectories(filePath);
                file.SaveAs(filePath);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

        }

        public void UploadRelativeUrlFile(HttpPostedFileBase file, string relativeUrl)
        {
            UploadFile(file, server.MapPath(relativeUrl));
        }


        public bool DeleteFile(string url)
        {
            try
            {
                File.Delete(url);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        public bool DeleteRelativeUrlFile(string relativeUrl)
        {
            return DeleteFile(server.MapPath(relativeUrl));
        }
        
        #endregion;


    }
}
