using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Domain.Entity.CIS
{
    [Tenant(DBEnum.CisDb)]
    [SugarTable("xt_cf", "PrescriptionEntity")]
    public class PrescriptionEntity : IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string cfId { get; set; }

        /// <summary>
        /// bảng khám bệnh liên kết
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// billingtrạng thái thu phí của hệ thống
        /// </summary>
        public bool sfbz { get; set; }

        /// <summary>
        /// enum 1 đơn thuốc Tây y 2 đơn thuốc Đông y 3 đơn phục hồi chức năng 4 đơn xét nghiệm 5 đơn kiểm tra 6 đơn truyền dịch
        /// </summary>
        public int cflx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// liên kếtbasesố hiệu nhân viên của cơ sở dữ liệu
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// khoa
        /// </summary>
        public string ks { get; set; }

    

        /// <summary>
        /// số thang
        /// </summary>
        public int? tieshu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfyf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? djbz { get; set; }

        /// <summary>
        /// cờ đồng bộ，đã đồng bộ tới hay chưaHIS（sau đó phán đoán đã đồng bộ hay chưa đổi thành phụ thuộc vào bảng hồ sơ thao tác）
        /// </summary>
        public bool? tbbz { get; set; }

        /// <summary>
        /// nguyên nhân lâm sàng
        /// </summary>
        public string lcyx { get; set; }
        /// <summary>
        /// ghi chú đăng ký
        /// </summary>
        public string sqbz { get; set; }
        /// <summary>
        /// cờ trả lại
        /// </summary>
        public bool? tbz { get; set; }

        /// <summary>
        /// số phiếu đăng ký（đăng ký y tế từ xa cầnguid）
        /// </summary>
        public string sqdh { get; set; }
        /// <summary>
        /// nhãn đơn thuốc JI tinhI JII tinhII MZ gây mê
        /// </summary>
        public string cftag { get; set; }
        /// <summary>
        /// phương thức sắc hộ
        /// </summary>
        public string djfs { get; set; }
        // <summary>
        /// số lần sắc hộ
        /// </summary>
        public string djts { get; set; }
        // <summary>
        /// dặn dò
        /// </summary>
        public string cfzt { get; set; }


    }
}
