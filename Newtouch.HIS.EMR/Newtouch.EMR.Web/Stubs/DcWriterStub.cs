// DCSoft.Writer stub — explicit replacement for the vendor document editor.
//
// DCSoft.Writer (DCSoft.Writer.ForASPNET + DCWriterWeb.cab ActiveX) has NO .NET Core
// build and cannot run on Linux. This stub keeps every record/template action compiling
// and every editor route reachable, while making the limitation VISIBLE instead of
// faking an editor:
//  - Pages that used to embed the editor render an explicit notice area
//    (GetAllContentHtml) instead of a blank control.
//  - The writer service endpoints (HandleDCWriterServicePage / HandelDCWriterServicePage
//    / MoreHandleDCWriterServicePage) return an explicit error JSON, never a 404.
//  - LoadDocumentFromRequestFormData() always returns false, so the old editor-save
//    branches are skipped instead of silently dropping a document.
// A replacement web editor is the one remaining EMR item (see task report).

using System;

namespace Newtouch.EMR.Web
{
    public enum WebWriterControlRenderMode
    {
        NormalHtmlEditable,
        NormalHtml,
        ReadonlyHtml,
        PagePreviewHtml
    }

    public class WriterOptions
    {
        public string ControlName { get; set; }
        public string ClientID { get; set; }
        public WebWriterControlRenderMode ContentRenderMode { get; set; } = WebWriterControlRenderMode.NormalHtmlEditable;
        public string ServicePageURL { get; set; }
        public string WorkspaceBackColorString { get; set; }
        public object WorkspaceBackColor { get; set; }
        public string WorkspaceBackgroundImage { get; set; }
        public bool IndentHtmlCode { get; set; }
        public bool ImageDataEmbedInHtml { get; set; }
        public object ClientContextMenuType { get; set; }
        public object UseDCWriterMiniJS { get; set; }
        public bool CompressSessionData { get; set; }
        public string ExcludeKeywords { get; set; }
        public object BackgroundTextOutputMode { get; set; }
        public object CurrentLoadOptions { get; set; }
        public bool LogUserEditTrack { get; set; }
        public string CurrentUserID { get; set; }
        public string CurrentUserName { get; set; }
        public object CurrentUserPermissionLevel { get; set; }
        public string ClientMachineName { get; set; }
        public bool ExcludeLogicDeleted { get; set; }
        public bool OutputOriginUserTrackInfos { get; set; }
    }

    public class WriterViewOptions
    {
        public object FieldBackColor { get; set; }
        public object FieldFocusedBackColor { get; set; }
        public object FieldInvalidateValueBackColor { get; set; }
        public object FieldBorderColor { get; set; }
        public bool ShowInputFieldStateTag { get; set; }
        public bool ShowParagraphFlag { get; set; }
        public bool EnableEncryptView { get; set; }
        public bool ShowCellNoneBorder { get; set; }
        public bool ShowGridLine { get; set; }
        public bool PreserveBackgroundTextWhenPrint { get; set; }
    }

    public class WriterBehaviorOptions
    {
        public object MoveFocusHotKey { get; set; }
        public object AcceptDataFormats { get; set; }
        public bool ParagraphFlagFollowTableOrSection { get; set; }
        public bool EnableDataBinding { get; set; }
        public bool OutputFieldBorderTextToContentText { get; set; }
    }

    public class WriterTrackVisibleLevel
    {
        public int DeleteLineNum { get; set; }
        public string DeleteLineColorString { get; set; }
        public string UnderLineColorString { get; set; }
        public int UnderLineColorNum { get; set; }
        public string BackgroundColorString { get; set; }
    }

    public class WriterSecurityOptions
    {
        public bool ShowLogicDeletedContent { get; set; }
        public bool ShowPermissionMark { get; set; }
        public bool ShowPermissionTip { get; set; }
        public bool ShowFlagForOnlySoftwareSign { get; set; }
        public bool EnablePermission { get; set; }
        public bool EnableLogicDelete { get; set; }
        public WriterTrackVisibleLevel TrackVisibleLevel0 { get; } = new WriterTrackVisibleLevel();
    }

    public class WriterDocumentOptions
    {
        public WriterViewOptions ViewOptions { get; } = new WriterViewOptions();
        public WriterBehaviorOptions BehaviorOptions { get; } = new WriterBehaviorOptions();
        public WriterSecurityOptions SecurityOptions { get; } = new WriterSecurityOptions();
    }

    public class WriterControlOptions
    {
        public object FormView { get; set; }
    }

    // Minimal stand-ins for DCSoft.Writer.Dom document element types. The stub
    // document is always empty: render flows that set a field value skip their
    // work (Count == 0), and data-extraction helpers return empty results, so no
    // document content is ever fabricated. Extra properties (Name, Text,
    // DisplayName, BackgroundText, TextValue, InnerValue) exist only so the
    // ported extraction code compiles; they are never populated.
    public class XTextElement
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string InnerText { get; set; }
        public string FormulaValue { get; set; }
        public string DisplayName { get; set; }
        public string BackgroundText { get; set; }
        public string TextValue { get; set; }
        public string InnerValue { get; set; }

        public string Text
        {
            get { return InnerText; }
            set { InnerText = value; }
        }

        // Child elements (table rows contain cell elements). Always empty in
        // the stub document, so extraction loops over them never execute.
        public System.Collections.Generic.List<XTextElement> Elements { get; } =
            new System.Collections.Generic.List<XTextElement>();
    }

    public class XTextInputFieldElement : XTextElement
    {
    }

    public class XTextElementList : System.Collections.Generic.List<XTextElement>
    {
    }

    public class WriterTable
    {
        public string ID { get; set; }
        public System.Collections.Generic.List<XTextElement> Elements { get; } =
            new System.Collections.Generic.List<XTextElement>();
    }

    public class WriterDocument
    {
        public XTextElementList GetElementsByName(string name)
        {
            return new XTextElementList();
        }

        public XTextElementList GetAllElements()
        {
            return new XTextElementList();
        }

        // The stub document has no elements, so lookups return a blank element
        // (callers read .InnerText, which is then "") instead of null.
        public XTextElement GetElementById(string id)
        {
            return new XTextElement { ID = id, InnerText = "" };
        }

        public System.Collections.Generic.List<WriterTable> Tables
        {
            get { return new System.Collections.Generic.List<WriterTable>(); }
        }

        public XTextElementList Fields
        {
            get { return new XTextElementList(); }
        }

        public string InnerXML
        {
            get { return ""; }
        }

        public string InnerText
        {
            get { return ""; }
        }

        // Well-formed but empty: XMLDataBind parses it into zero tables and
        // falls through to the notice HTML.
        public string GetDataSourceBindingDescriptionsXML()
        {
            return "<root/>";
        }
    }

    public class WebWriterControlEngine : IDisposable
    {
        public const string EditorUnavailableNotice =
            "<div style=\"border:1px solid #e0a800;background:#fff8e1;color:#5d4037;" +
            "padding:24px;margin:12px;font-size:15px;line-height:1.8\">" +
            "<b>Trình soạn thảo văn bản DCSoft chưa khả dụng trên .NET Core.</b><br/>" +
            "DCSoft.Writer không có bản build .NET Core và không chạy được trên Linux, " +
            "nên khu vực soạn thảo bệnh án hiển thị thông báo này thay cho trình soạn thảo. " +
            "Danh sách, tra cứu và các chức năng không phụ thuộc trình soạn thảo vẫn hoạt động bình thường. " +
            "Cần thay bằng một trình soạn thảo web khác để soạn thảo trực tuyến." +
            "</div>";

        public string BorderStyle { get; set; }
        public WriterOptions Options { get; } = new WriterOptions();
        public WriterDocumentOptions DocumentOptions { get; } = new WriterDocumentOptions();
        public WriterControlOptions ControlOptions { get; } = new WriterControlOptions();
        public WriterDocument Document { get; } = new WriterDocument();

        public static void StartLinuxMode()
        {
            // No-op: the real engine cannot start on .NET Core/Linux.
        }

        public void SetRegisterCode(string code)
        {
        }

        public void LoadDocument(string fileName, object format)
        {
            // No-op: there is no editor to load the document into.
        }

        // Always false: the old editor-save branches are skipped, never silently dropped.
        public bool LoadDocumentFromRequestFormData()
        {
            return false;
        }

        public void SaveDocument(string fileName, object format)
        {
            throw new NotSupportedException(
                "Trình soạn thảo DCSoft chưa khả dụng trên .NET Core — không thể lưu tài liệu qua trình soạn thảo.");
        }

        public string GetAllContentHtml()
        {
            return EditorUnavailableNotice;
        }

        public void Dispose()
        {
        }
    }
}
