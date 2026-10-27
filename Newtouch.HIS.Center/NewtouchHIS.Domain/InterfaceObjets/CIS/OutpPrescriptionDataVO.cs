using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.InterfaceObjets.CIS
{
    public class OutpPrescriptionDataVO
    {
        #region Bảng chính đơn thuốc
        /// <summary>
        /// 
        /// </summary>
        public string cfId { get; set; }

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
        ///
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// Số thang
        /// </summary>
        public int? tieshu { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string? cfyf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? djbz { get; set; }
        /// <summary>
        /// Nhãn đơn thuốc JI TinhI JII TinhII MZ Gây mê
        /// </summary>
        public string? cftag { get; set; }
        /// <summary>
        /// Cách sắc thay
        /// </summary>
        public string? djfs { get; set; }
        // <summary>
        /// Số thang sắc thay
        /// </summary>
        public string? djts { get; set; }
        // <summary>
        /// Dặn dò
        /// </summary>
        public string? cfzt { get; set; }

        #endregion
        
        public List<OutpPrescriptionDetailVO>? cfmx { get; set; }

    }
    /// <summary>
    /// Chi tiết đơn thuốc
    /// </summary>
    public class OutpPrescriptionDetailVO
    {
        #region Chi tiết đơn thuốc

        /// <summary>
        /// 
        /// </summary>
        public string? xmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? xmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? ypCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? ypmc { get; set; }

        /// <summary>
        /// Lượng điều trị mỗi lần  Đối với hạng mục phục hồi chức năng
        /// </summary>
        public int? mczll { get; set; }

        /// <summary>
        /// Liều lượng một từ
        /// </summary>
        public decimal? mcjl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? mcjldw { get; set; }

        /// <summary>
        /// Cách dùng
        /// </summary>
        public string? yfCode { get; set; }

        /// <summary>
        /// Tần suất
        /// </summary>
        public string? pcCode { get; set; }

        /// <summary>
        /// Số lượng trong thuốc=Tổng lượng   Tổng lượng trong phục hồi chức năng(không phải số lượng)=Tổng lượng
        /// </summary>
        public int? zl { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Đơn thuốc không phục hồi chức năng Tạm thời dùng
        /// </summary>
        public string? dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? je { get; set; }

        /// <summary>
        /// Số nhóm(Số nhóm của thuốc)
        /// </summary>
        public string? zh { get; set; }

        /// <summary>
        /// Vị trí
        /// </summary>
        public string? bw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? urgent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? purpose { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// Khoa thực hiện(Bộ phận nhà thuốc của thuốc,Khoa thực hiện xét nghiệm/kiểm tra)(Khoa thực hiện đơn phục hồi chức năng)
        /// </summary>
        public string? zxks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? zxsj { get; set; }
        /// <summary>
        /// Mã duy nhất BHYT
        /// </summary>
        public string? ybwym { get; set; }
        /// <summary>
        /// Cờ giới hạn sử dụng
        /// </summary>
        public string? xzsybz { get; set; }
        /// <summary>
        /// Số ngày
        /// </summary>
        public decimal? ts { get; set; }
        public string? ztId { get; set; }
        public string? ztmc { get; set; }

        /// <summary>
        /// Nguyên nhân dùng kháng sinh
        /// </summary>
        public string? kssReason { get; set; }
        /// <summary>
        /// Phương pháp theo vị trí
        /// </summary>
        public string? bwff { get; set; }

        public string? sqlx { get; set; }
        /// <summary>
        /// Sắp xếp 
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// Tốc độ truyền 
        /// </summary>
        public int? ds { get; set; }
        /// <summary>
        /// Cờ chuyển sang tự chi trả
        /// </summary>
        public int? zzfbz { get; set; }

        /// <summary>
        /// Gói cách dùng
        /// </summary>
        public string? yfztbm { get; set; }

        public string? yfztmc { get; set; }
        /// <summary>
        /// Trạng thái thu phí
        /// </summary>
        public string? sfzt { get; set; }
        /// <summary>
        /// Giải thích đơn thuốc đông y
        /// </summary>
        public string? zysm { get; set; }

        #endregion
    }
}
