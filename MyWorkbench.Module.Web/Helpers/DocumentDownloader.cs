using DevExpress.ExpressApp.Web;
using System.IO;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.FileAttachments.Web;

namespace MyWorkbench.Module.Web.Helpers
{
    public class DocumentDownloader : FileDataDownloader
    {
        public override void DownloadCore(DevExpress.Persistent.Base.IFileData fileData)
        {
            string content = System.Convert.ToBase64String(((FileData)fileData).Content);
            if (Path.GetExtension(fileData.FileName).ToLower() == ".pdf")
            {
                string script = "window.open('data:application/pdf;base64," + content + "');";
                WebWindow.CurrentRequestWindow.RegisterClientScript("showPDF1", script, true);
            }
            if (Path.GetExtension(fileData.FileName).ToLower() == ".jpg" | Path.GetExtension(fileData.FileName).ToLower() == ".jpeg" | Path.GetExtension(fileData.FileName).ToLower() == ".jpe")
            {
                string script = "window.open('data:image/jpeg;base64, " + content + "');";
                WebWindow.CurrentRequestWindow.RegisterClientScript("showJPEG", script, true);
            }
            if (Path.GetExtension(fileData.FileName).ToLower() == ".png")
            {
                string script = "window.open('data:image/png;base64, " + content + "');";
                WebWindow.CurrentRequestWindow.RegisterClientScript("showPNG", script, true);
            }
            if (Path.GetExtension(fileData.FileName).ToLower() == ".bmp")
            {
                string script = "window.open('data:image/bmp;base64, " + content + "');";
                WebWindow.CurrentRequestWindow.RegisterClientScript("showBMP", script, true);
            }
        }
    }
}