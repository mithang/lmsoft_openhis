using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCSoft.Writer;
using DCSoft.Writer.Controls;
using System.Drawing;

namespace DCControl
{
    public class DCWebControl
    {
        private WebWriterControlEngine dcWControl;
        public WebWriterControlEngine DCWControl 
        {
            get { return dcWControl; }
            set { dcWControl = value; }
        }

        public DCWebControl()
        { 

        }

         public WebWriterControlEngine Init(string _regcode)
        {            
            DCWControl = new WebWriterControlEngine();
            DCWControl.SetRegisterCode (_regcode);
            DCWControl.Options.ControlName = "myWriterControl";          
            //DCWControl.DocumentOptions.ViewOptions.ShowInputFieldStateTag = false;
            //Màu nền khi kiểm tra dữ liệu không đạt
            //DCWControl.DocumentOptions.ViewOptions.FieldInvalidateValueBackColor = Color.Yellow;
            //DCWControl.DocumentOptions.BehaviorOptions.AcceptDataFormats = WriterDataFormats.Text;
            
            //DCWControl.DocumentOptions.ViewOptions.PreserveBackgroundTextWhenPrint = true;
            //Dùng phím Tab để chuyển giữa các vùng nhập
            //DCWControl.DocumentOptions.BehaviorOptions.MoveFocusHotKey = MoveFocusHotKeys.Tab;           
            //Dấu hiệu hiển thị ký hiệu đoạn văn trong tài liệu
            //DCWControl.DocumentOptions.ViewOptions.ShowParagraphFlag = false;            
            //Có hiển thị nhãn trạng thái của vùng nhập hay không
            //DCWControl.DocumentOptions.ViewOptions.ShowInputFieldStateTag = true;
            //Có tắt hiển thị mã hóa hay không
            //DCWControl.DocumentOptions.ViewOptions.EnableEncryptView = true;           
            //Có hiển thị nhãn trạng thái của vùng nhập hay không
            //DCWControl.Options.BackgroundTextOutputMode = DCBackgroundTextOutputMode.Underline;
            //Cách trình bày nội dung
            DCWControl.Options.ContentRenderMode = WebWriterControlRenderMode.NormalHtmlEditable;

            //DCWControl.Options.ReferencePathForDebug = "dcwriterreference";
            //DCControl.Options.ReferencePath = "dcwriterreference";
            //DCWControl.Options.WorkspaceBackColorString = "#B1CAEB";
            //DCWControl.Options.WorkspaceBackColor = ColorTranslator.FromHtml("#f2f2f2");//Màu nền     
            DCWControl.BorderStyle = "1px solid black";//Kiểu đường viền
            DCWControl.DocumentOptions.ViewOptions.FieldBackColor = Color.Transparent;
            //DCWControl.DocumentOptions.ViewOptions.FieldBackColorValue = "Transparent";
            DCWControl.DocumentOptions.ViewOptions.FieldFocusedBackColor = Color.White;
            //DCWControl.DocumentOptions.ViewOptions.FieldFocusedBackColorValue = "White";
            //DCWControl.Options.ServicePageURL = "/MedicalRecordManage/MedicalRecord/HandleDCWriterServicePage";
            DCWControl.Options.IndentHtmlCode = false;
            DCWControl.DocumentOptions.BehaviorOptions.ParagraphFlagFollowTableOrSection = true;// Hủy hiển thị dòng trống sau khi chèn bảng
            DCWControl.Options.ImageDataEmbedInHtml = true;// In hiển thị biểu thức y học
            DCWControl.Options.ClientContextMenuType = WebClientContextMenuType.SystemDefault;// Hiển thị menu chuột phải mặc định của hệ thống
            //DCWControl.DocumentOptions.BehaviorOptions.OutputFieldBorderTextToContentText = false;           
            //Cài đặt liên quan đến lưu vết
            DCWControl.DocumentOptions.SecurityOptions.ShowLogicDeletedContent = true;
            DCWControl.DocumentOptions.SecurityOptions.ShowPermissionMark = true;
            DCWControl.DocumentOptions.SecurityOptions.ShowPermissionTip = true;

            DCWControl.Options.UseDCWriterMiniJS = DCBooleanValueHasDefault.Default;//Có bật tệp DCWriterMini.js đã nén hay không
            DCWControl.Options.CompressSessionData = true;//Có nén dữ liệu phiên phía máy chủ hay không
            DCWControl.Options.CurrentLoadOptions = WebWriterControlLoadDocumentOptions.CompressSessionData;//Nén dữ liệu phiên phía máy chủ    
            
            return DCWControl;
        }
    }
}
