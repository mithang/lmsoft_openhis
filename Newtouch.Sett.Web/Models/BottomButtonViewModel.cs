namespace Newtouch.Sett.Web.Models
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

        public bool F1Hidden { get; set; }
        public string F1ExtInlineStyle { get; set; }
        public bool F2Hidden { get; set; }
        public string F2ExtInlineStyle { get; set; }
        public bool F3Hidden { get; set; }
        public string F3ExtInlineStyle { get; set; }
        public bool F4Hidden { get; set; }
        public string F4ExtInlineStyle { get; set; }
        public bool F5Hidden { get; set; }
        public string F5ExtInlineStyle { get; set; }
        public bool F6Hidden { get; set; }
        public string F6ExtInlineStyle { get; set; }
        public bool F7Hidden { get; set; }
        public string F7ExtInlineStyle { get; set; }
        public bool F8Hidden { get; set; }
        public string F8ExtInlineStyle { get; set; }
        public bool F9Hidden { get; set; }
        public string F9ExtInlineStyle { get; set; }
        public bool F10Hidden { get; set; }
        public string F10ExtInlineStyle { get; set; }
        public bool F11Hidden { get; set; }
        public string F11ExtInlineStyle { get; set; }
        public bool F12Hidden { get; set; }
        public string F12ExtInlineStyle { get; set; }

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
            switch (i)
            {
                case 1: return F1Hidden;
                case 2: return F2Hidden;
                case 3: return F3Hidden;
                case 4: return F4Hidden;
                case 5: return F5Hidden;
                case 6: return F6Hidden;
                case 7: return F7Hidden;
                case 8: return F8Hidden;
                case 9: return F9Hidden;
                case 10: return F10Hidden;
                case 11: return F11Hidden;
                case 12: return F12Hidden;
                default: return false;
            }
        }

        public string GetIndexExtInlineStyle(int i)
        {
            switch (i)
            {
                case 1: return F1ExtInlineStyle;
                case 2: return F2ExtInlineStyle;
                case 3: return F3ExtInlineStyle;
                case 4: return F4ExtInlineStyle;
                case 5: return F5ExtInlineStyle;
                case 6: return F6ExtInlineStyle;
                case 7: return F7ExtInlineStyle;
                case 8: return F8ExtInlineStyle;
                case 9: return F9ExtInlineStyle;
                case 10: return F10ExtInlineStyle;
                case 11: return F11ExtInlineStyle;
                case 12: return F12ExtInlineStyle;
                default: return null;
            }
        }
    }
}
