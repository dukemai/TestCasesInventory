using System.Web;

namespace TestCasesInventory.Presenter.Business
{
    public interface IFileControlPresenter 
    {
        void UploadFile(HttpPostedFileBase file, string url);
        void UploadTestCaseAttachment(HttpPostedFileBase file, int testCaseId);        
        string GetTestCaseFileUrl(int testCaseId);  
        bool IsAttachmentExisted(int testCaseId);
        string[] GetFileUrlList(int testCaseId);
        bool DeleteTestCaseFiles(string item);
        string[] GetFileNameList(string[] fileUrlList);
    }
}
