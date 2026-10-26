using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.ValueObjects
{
    public class SysChargeItemTemplateVO
    {
        public int sfxmId { get; set; }

        /// <summary>Mã</summary>
        public string sfxmCode { get; set; }

        /// <summary>Tên khoản thu phí</summary>
        public string sfxmmc { get; set; }

        /// <summary>Nhóm thu phí lớnCode</summary>
        public string sfdlCode { get; set; }
        public string sfdlmc { get; set; }

        /// <summary>Nhóm bệnh án lớnCode</summary>
        public string badlCode { get; set; }

        /// <summary>Nhóm bảo hiểm nông nghiệp lớnCode</summary>
        public string nbdlCode { get; set; }

        /// <summary>Tổ chức</summary>
        public string OrganizeId { get; set; }

        /// <summary>Phiên âm đầu</summary>
        public string py { get; set; }

        /// <summary>Đơn vị</summary>
        public string dw { get; set; }

        /// <summary>Đơn giá</summary>
        public Decimal dj { get; set; }

        /// <summary>Tỷ lệ tự chi trả</summary>
        public Decimal zfbl { get; set; }

        /// <summary>Tính chất tự chi trả</summary>
        public string zfxz { get; set; }

        /// <summary>Cờ ngoại trú/nội trú</summary>
        public string mzzybz { get; set; }

        /// <summary>Có cần thực hiện không 0 Không cần thực hiện，（Ghi sổ trực tiếp）    1 Cần thực hiện，（Chỉ được ghi sổ sau khi thực hiện）</summary>
        public string ssbz { get; set; }

        /// <summary>0 Hạng mục thông thường 1 Hạng mục đặc biệt   Hạng mục đặc biệt chỉ áp dụng cho tính chất bệnh nhân cụ thể。</summary>
        public string tsbz { get; set; }

        /// <summary>Cờ thu phí    0 Hạng mục không thu phí（Ví dụ：Phí đăng ký khám..） 1 Khoản thu phí</summary>
        public string sfbz { get; set; }

        /// <summary>Mã bảo hiểm y tế</summary>
        public string ybdm { get; set; }

        /// <summary>Mã giá</summary>
        public string wjdm { get; set; }

        /// <summary>Thời lượng dịch vụ liên tục（Đơn vị：phút）</summary>
        public int? duration { get; set; }

        /// <summary>Ghi chú</summary>
        public string bz { get; set; }

        /// <summary>Số lượng đơn vị tính</summary>
        public int? dwjls { get; set; }

        /// <summary>Chiến lược tính giá Enum tương ứngEnumKfxmJjcl 1Theo thời lượng 2Theo số lượng 3Theo diện tích</summary>
        public int? jjcl { get; set; }

        /// <summary>Khoa thực hiện（Khoa thực hiện mặc định）</summary>
        public string zxks { get; set; }

        /// <summary>Quy cách</summary>
        public string gg { get; set; }
        public int? sl { get; set; }
    }
}
