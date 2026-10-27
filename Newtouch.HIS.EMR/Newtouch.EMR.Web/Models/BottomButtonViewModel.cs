namespace Newtouch.EMR.Web.Models
{
    /// <summary>
    /// Bottom function-key button bar model, ported from Newtouch.HIS.Web.Core
    /// (whose newer GetIndex* helpers have no source in this repo). The helpers
    /// are reconstructed from the only usages in this app:
    /// _BottomButtonsView renders one button per ShowKeyList entry, hides entries
    /// with no text, and appends an optional per-index inline style (never set by
    /// any call site here, so it returns null).
    /// NOTE: default F-texts below are the vendor originals; the translation pass
    /// renders them in Vietnamese.
    /// </summary>
    public class BottomButtonViewModel
    {
        public BottomButtonViewModel()
        {
            F4Text = "Xóa";
            F5Text = "Làm mới";
            F8Text = "Lưu";
            F10Text = "Thoát";
        }

        public int[] ShowKeyList { get; set; }
        public string WapperInlineStyle { get; set; }
        public string F2Text { get; set; }
        public string F3Text { get; set; }
        public string F4Text { get; set; }
        public string F5Text { get; set; }
        public string F6Text { get; set; }
        public string F7Text { get; set; }
        public string F8Text { get; set; }
        public string F9Text { get; set; }
        public string F10Text { get; set; }

        public string GetIndexText(int index)
        {
            return index switch
            {
                2 => F2Text,
                3 => F3Text,
                4 => F4Text,
                5 => F5Text,
                6 => F6Text,
                7 => F7Text,
                8 => F8Text,
                9 => F9Text,
                10 => F10Text,
                _ => null
            };
        }

        public bool GetIndexHidden(int index)
        {
            return string.IsNullOrWhiteSpace(GetIndexText(index));
        }

        public string GetIndexExtInlineStyle(int index)
        {
            return null;
        }
    }
}
