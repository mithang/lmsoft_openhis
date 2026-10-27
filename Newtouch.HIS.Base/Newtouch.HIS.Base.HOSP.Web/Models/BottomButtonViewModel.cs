using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Newtouch.HIS.Web.Core.Models
{
    /// <summary>
    /// 
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

        /// <summary>
        /// Danh sách phím tắt hiển thị
        /// </summary>
        public int[] ShowKeyList { get; set; }

        /// <summary>
        /// Custom inline style for the button wrapper div; when empty the view falls
        /// back to its default style. (HOSP-era member missing from the Sett copy.)
        /// </summary>
        public string WapperInlineStyle { get; set; }

        /// <summary>
        /// Whether the F-key button with this index is hidden. No call site sets a
        /// hidden list, so nothing is hidden beyond ShowKeyList filtering.
        /// </summary>
        public bool GetIndexHidden(int index)
        {
            return false;
        }

        /// <summary>
        /// Extra inline style for the F-key button with this index (none by default).
        /// </summary>
        public string GetIndexExtInlineStyle(int index)
        {
            return null;
        }

        /// <summary>
        /// Display text for the F-key button with this index (F2..F10).
        /// </summary>
        public string GetIndexText(int index)
        {
            switch (index)
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

        /// <summary>
        /// Văn bản hiển thị
        /// </summary>
        public string F2Text { get; set; }
        /// <summary>
        /// Văn bản hiển thị
        /// </summary>
        public string F3Text { get; set; }
        /// <summary>
        /// Văn bản hiển thị
        /// </summary>
        public string F4Text { get; set; }
        /// <summary>
        /// Văn bản hiển thị
        /// </summary>
        public string F5Text { get; set; }
        /// <summary>
        /// Văn bản hiển thị
        /// </summary>
        public string F6Text { get; set; }
        /// <summary>
        /// Văn bản hiển thị
        /// </summary>
        public string F7Text { get; set; }
        /// <summary>
        /// Văn bản hiển thị
        /// </summary>
        public string F8Text { get; set; }
        /// <summary>
        /// Văn bản hiển thị
        /// </summary>
        public string F9Text { get; set; }
        /// <summary>
        /// Văn bản hiển thị
        /// </summary>
        public string F10Text { get; set; }
    }


}
