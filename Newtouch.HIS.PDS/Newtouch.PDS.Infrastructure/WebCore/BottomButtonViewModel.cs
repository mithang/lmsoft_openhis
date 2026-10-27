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
            F4Text = "Xóa trống";
            F5Text = "Làm mới";
            F8Text = "Lưu";
            F10Text = "Thoát";
        }

        /// <summary>
        /// Danh sách phím tắt hiển thị
        /// </summary>
        public int[] ShowKeyList { get; set; }

        public string WapperInlineStyle { get; set; }

        public bool F9Hidden { get; set; }

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

        public bool GetIndexHidden(int i)
        {
            // Only F9 has a dedicated hidden flag in the ported views (HandOutMedicine sets F9Hidden=true);
            // other keys have no hidden flag, so they are always shown.
            return i == 9 && F9Hidden;
        }

        public string GetIndexExtInlineStyle(int i)
        {
            return "";
        }

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
                default: return "";
            }
        }
    }


}
