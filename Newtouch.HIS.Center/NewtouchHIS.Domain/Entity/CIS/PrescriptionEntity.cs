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
        /// Bảng khám liên kết
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// billingTrạng thái thu phí của hệ thống billing
        /// </summary>
        public bool sfbz { get; set; }

        /// <summary>
        /// Enum 1 Đơn thuốc tây 2 Đơn thuốc đông y 3 Đơn thuốc phục hồi chức năng 4 Đơn xét nghiệm 5 Đơn kiểm tra 6 Đơn truyền dịch
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
        /// Liên kếtbaseMã nhân viên của cơ sở dữ liệu
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }

    

        /// <summary>
        /// Số thang
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
        /// Cờ đồng bộ,Đã đồng bộ tới hay chưaHIS(sau xét có đồng bộ hay chưa đổi thành dựa vào bảng hồ sơ thao tác)
        /// </summary>
        public bool? tbbz { get; set; }

        /// <summary>
        /// Nguyên nhân lâm sàng
        /// </summary>
        public string lcyx { get; set; }
        /// <summary>
        /// Đăng kýGhi chú
        /// </summary>
        public string sqbz { get; set; }
        /// <summary>
        /// Cờ trả
        /// </summary>
        public bool? tbz { get; set; }

        /// <summary>
        /// Đăng kýSố phiếu(Y tế từ xaĐăng kýcầnguid)
        /// </summary>
        public string sqdh { get; set; }
        /// <summary>
        /// Nhãn đơn thuốc JI TinhI JII TinhII MZ Gây mê
        /// </summary>
        public string cftag { get; set; }
        /// <summary>
        /// Cách sắc thay
        /// </summary>
        public string djfs { get; set; }
        // <summary>
        /// Số thang sắc thay
        /// </summary>
        public string djts { get; set; }
        // <summary>
        /// Dặn dò
        /// </summary>
        public string cfzt { get; set; }


    }
}
