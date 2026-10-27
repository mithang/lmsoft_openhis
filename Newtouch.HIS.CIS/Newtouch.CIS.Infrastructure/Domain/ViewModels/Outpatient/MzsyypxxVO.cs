using System;

namespace Newtouch.Domain.ViewModels.Outpatient
{
    /// <summary>
    /// truyền dịchthông tin
    /// </summary>
    public class MzsyypxxVO
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        public long Id { get; set; }
        public string syzxId { get; set; }
        public string cfId { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Bệnh nhânKhám bệnhSố thẻ
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// Hóa đơnsố
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Đơn thuốcchi tiếtID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// Tính toánchi tiếttrongmã
        /// </summary>
        public int jsmxnm { get; set; }

        /// <summary>
        /// Số giường/ghếsố
        /// </summary>
        public string seatNum { get; set; }

        /// <summary>
        /// Thu phíThời gian  quyết toánThời gian
        /// </summary>
        public DateTime sfsj { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// dùnglượng+dùnglượngĐơn vị
        /// </summary>
        public string ylStr { get; set; }

        /// <summary>
        /// Số lượng+Số lượngĐơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// Liều lượng+Đơn vị liều
        /// </summary>
        public string jlStr { get; set; }
        public string yfcode { get; set; }
        public string yfmc { get; set; }
        /// <summary>
        /// Số nhóm
        /// </summary>
        public string groupNo { get; set; }

        /// <summary>
        /// phốiDược sĩMã nhân viên
        /// </summary>
        public string dispenser { get; set; }

        /// <summary>
        /// phốiDược sĩTên
        /// </summary>
        public string dispenserName { get; set; }

        /// <summary>
        /// Thực hiệnngườiMã nhân viên
        /// </summary>
        public string executor { get; set; }

        /// <summary>
        /// Thực hiệnngườiTên
        /// </summary>
        public string executorName { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// truyền dịchThời gian bắt đầu
        /// </summary>
        public DateTime? sykssj { get; set; }

        /// <summary>
        /// truyền dịchThời gian kết thúc
        /// </summary>
        public DateTime? syjssj { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// tạongười
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửangười
        /// </summary>
        public string LastModifierCode { get; set; }
        public int zcs { get; set; }
        public int yzxcs { get; set; }
        /// <summary>
        /// dathửkếtnếu
        /// </summary>
        public string psjg { get; set; }
        /// <summary>
        /// dathửCờ 0:Không 1：là
        /// </summary>
        public string ispsbz { get; set; }
        /// <summary>
        /// giữxemCờ
        /// </summary>

        public string islgbz { get; set; }
    }
}
