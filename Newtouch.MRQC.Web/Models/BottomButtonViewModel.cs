namespace Newtouch.MRQC.Web.Models
{
    /// <summary>
    /// Minimal replacement for Newtouch.HIS.Web.Core.Models.BottomButtonViewModel
    /// (Framework-only binary), covering only what _BottomButtonsView uses.
    /// </summary>
    public class BottomButtonViewModel
    {
        public int[] ShowKeyList { get; set; }
        public string WapperInlineStyle { get; set; }
        public string F1Text { get; set; }
        public string F2Text { get; set; }
        public string F3Text { get; set; }
        public string F4Text { get; set; }
        public string F5Text { get; set; }
        public string F6Text { get; set; }
        public string F7Text { get; set; }
        public string F8Text { get; set; }
        public string F9Text { get; set; }
        public string F10Text { get; set; }
        public string F11Text { get; set; }
        public string F12Text { get; set; }

        public string GetIndexText(int i)
        {
            switch (i)
            {
                case 1: return F1Text;
                case 2: return F2Text;
                case 3: return F3Text;
                case 4: return F4Text;
                case 5: return F5Text;
                case 6: return F6Text;
                case 7: return F7Text;
                case 8: return F8Text;
                case 9: return F9Text;
                case 10: return F10Text;
                case 11: return F11Text;
                case 12: return F12Text;
                default: return null;
            }
        }

        public bool GetIndexHidden(int i)
        {
            return false;
        }

        public string GetIndexExtInlineStyle(int i)
        {
            return null;
        }
    }
}
