using DCSoft.Writer;
using DCSoft.Writer.Controls;
using System.Drawing;

namespace Newtouch.EMR.DCWriter
{
    public class WebWriterContorl
    {
        private WebWriterControlEngine webControl;
        public WebWriterContorl() { }

        public WebWriterControlEngine WebWriter
        {
            get { return webControl; }
            set { webControl = value; }
        }

        public WebWriterControlEngine Init()
        {
            WebWriter = new WebWriterControlEngine();

            WebWriter.Options.ControlName = "myWriterControl";
            WebWriter.DocumentOptions.ViewOptions.ShowInputFieldStateTag = false;
            //Màu nền khi kiểm tra dữ liệu không đạt
            WebWriter.DocumentOptions.ViewOptions.FieldInvalidateValueBackColor = Color.Yellow;
            WebWriter.DocumentOptions.BehaviorOptions.AcceptDataFormats = WriterDataFormats.Text;

            WebWriter.DocumentOptions.ViewOptions.PreserveBackgroundTextWhenPrint = true;
            //Dùng phím Tab để chuyển giữa các vùng nhập
            WebWriter.DocumentOptions.BehaviorOptions.MoveFocusHotKey = MoveFocusHotKeys.Tab;
            //Dấu hiệu hiển thị ký hiệu đoạn văn trong tài liệu
            WebWriter.DocumentOptions.ViewOptions.ShowParagraphFlag = false;
            //Có hiển thị nhãn trạng thái của vùng nhập hay không
            WebWriter.DocumentOptions.ViewOptions.ShowInputFieldStateTag = true;
            //Có tắt hiển thị mã hóa hay không
            WebWriter.DocumentOptions.ViewOptions.EnableEncryptView = true;
            //Có hiển thị nhãn trạng thái của vùng nhập hay không
            //WebWriter.Options.BackgroundTextOutputMode = DCBackgroundTextOutputMode.Underline;
            //Cách trình bày nội dung
            WebWriter.Options.ContentRenderMode = WebWriterControlRenderMode.NormalHtmlEditable;

            //DCControl.Options.ReferencePath = "dcwriterreference";
            WebWriter.Options.WorkspaceBackColorString = "#B1CAEB";
            WebWriter.Options.WorkspaceBackColor = ColorTranslator.FromHtml("#f2f2f2");//Màu nền     
            WebWriter.BorderStyle = "1px solid black";//Kiểu đường viền
            WebWriter.Options.ServicePageURL = "/MedicalRecord/HandleDCWriterServicePage";
            WebWriter.Options.IndentHtmlCode = false;
            WebWriter.DocumentOptions.BehaviorOptions.ParagraphFlagFollowTableOrSection = true;// Hủy hiển thị dòng trống sau khi chèn bảng
            WebWriter.Options.ImageDataEmbedInHtml = true;// In hiển thị biểu thức y học
            WebWriter.Options.ClientContextMenuType = WebClientContextMenuType.SystemDefault;// Hiển thị menu chuột phải mặc định của hệ thống
            WebWriter.DocumentOptions.BehaviorOptions.OutputFieldBorderTextToContentText = false;
            //Cài đặt liên quan đến lưu vết
            WebWriter.DocumentOptions.SecurityOptions.ShowLogicDeletedContent = true;
            WebWriter.DocumentOptions.SecurityOptions.ShowPermissionMark = true;
            WebWriter.DocumentOptions.SecurityOptions.ShowPermissionTip = true;

            WebWriter.Options.UseDCWriterMiniJS = DCBooleanValueHasDefault.Default;//Có bật tệp DCWriterMini.js đã nén hay không
            WebWriter.Options.CompressSessionData = true;//Có nén dữ liệu phiên phía máy chủ hay không
            WebWriter.Options.CurrentLoadOptions = WebWriterControlLoadDocumentOptions.CompressSessionData;//Nén dữ liệu phiên phía máy chủ    

            return WebWriter;
        }

    }
}
