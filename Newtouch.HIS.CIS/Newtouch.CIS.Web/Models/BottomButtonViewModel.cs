using System.Collections.Generic;

namespace Newtouch.HIS.Web.Core.Models
{
    /// <summary>
    /// Reconstruction of the binary Web.Core BottomButtonViewModel. The
    /// Web.Core SOURCE in the Sett tree only has ShowKeyList + F-texts, but
    /// the CIS partials (_BottomButtonsView/_MiddleButtonsView) also use
    /// WapperInlineStyle, GetIndexHidden, GetIndexExtInlineStyle and
    /// GetIndexText (source drifted from the shipped DLL, same as the
    /// VerificationActionFIlterAttribute case). Semantics reconstructed
    /// from the partials + callers: GetIndexText maps key -&gt; F-text,
    /// GetIndexHidden hides buttons with no label, extra styles come from
    /// an optional per-key map (no caller sets one today).
    /// </summary>
    public class BottomButtonViewModel
    {
        public BottomButtonViewModel()
        {
            F4Text = "Xóa sạch";
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
        public Dictionary<int, string> ExtInlineStyleMap { get; set; }
        public bool F2Hidden { get; set; }
        public bool F3Hidden { get; set; }
        public bool F4Hidden { get; set; }
        public bool F5Hidden { get; set; }
        public bool F6Hidden { get; set; }
        public bool F7Hidden { get; set; }
        public bool F8Hidden { get; set; }
        public bool F9Hidden { get; set; }
        public bool F10Hidden { get; set; }

        public string GetIndexText(int i)
        {
            switch (i)
            {
                case 2: return F2Text;
                case 3: return F3Text;
                case 4: return F4Text;
                case 5: return F5Text;
                case 6: return F6Text;
                case 7: return F7Text;
                case 8: return F8Text;
                case 9: return F9Text;
                case 10: return F10Text;
                default: return null;
            }
        }

        public bool GetIndexHidden(int i)
        {
            switch (i)
            {
                case 2: return F2Hidden;
                case 3: return F3Hidden;
                case 4: return F4Hidden;
                case 5: return F5Hidden;
                case 6: return F6Hidden;
                case 7: return F7Hidden;
                case 8: return F8Hidden;
                case 9: return F9Hidden;
                case 10: return F10Hidden;
                default: return false;
            }
        }

        public string GetIndexExtInlineStyle(int i)
        {
            if (ExtInlineStyleMap != null && ExtInlineStyleMap.TryGetValue(i, out var v)) return v;
            return null;
        }
    }
}
