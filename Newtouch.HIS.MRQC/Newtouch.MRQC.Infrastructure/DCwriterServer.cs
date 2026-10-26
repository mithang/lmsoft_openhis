using DCSoft.Writer.Controls;
using Newtouch.Core.Common.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Infrastructure
{
    public class DCwriterServer
    {
        private WebWriterControlEngine dcWControl;
        public static string EditorRegCode = ConfigurationHelper.GetAppConfigValue("EditorRegCode");
        public WebWriterControlEngine DCWControl
        {
            get { return dcWControl; }
            set { dcWControl = value; }
        }

        public DCwriterServer()
        {
        }

        public WebWriterControlEngine Init()
        {
            DCWControl = new WebWriterControlEngine();
            DCWControl.SetRegisterCode(EditorRegCode);
            DCWControl.Options.ControlName = "myWriterControl";//khi tạo nhãn giao diệnID
            DCWControl.Options.ContentRenderMode = WebWriterControlRenderMode.NormalHtmlEditable;
            DCWControl.Options.ServicePageURL = "/MedicalCenter/MoreHandleDCWriterServicePage";
            DCWControl.Options.ImageDataEmbedInHtml = true;//hiển thị hình ảnh và biểu thức    
            DCWControl.BorderStyle = "1px solid black";//kiểu viền
            DCWControl.Options.WorkspaceBackColor = ColorTranslator.FromHtml("#7cb9e8");//màu nền  
            DCWControl.DocumentOptions.ViewOptions.FieldBackColor = Color.Transparent;
            DCWControl.DocumentOptions.ViewOptions.FieldFocusedBackColor = Color.White;
            return DCWControl;
        }
    }
}
