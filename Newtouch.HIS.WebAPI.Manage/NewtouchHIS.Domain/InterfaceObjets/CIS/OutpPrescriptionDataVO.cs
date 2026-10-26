using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.InterfaceObjets.CIS
{
    public class OutpPrescriptionDataVO
    {
        #region 处方主表
        /// <summary>
        /// 
        /// </summary>
        public string cfId { get; set; }

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
        ///
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// số thang
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
        /// nhãn đơn thuốc JI tinhI JII tinhII MZ gây mê
        /// </summary>
        public string? cftag { get; set; }
        /// <summary>
        /// phương thức sắc hộ
        /// </summary>
        public string? djfs { get; set; }
        // <summary>
        /// số lần sắc hộ
        /// </summary>
        public string? djts { get; set; }
        // <summary>
        /// dặn dò
        /// </summary>
        public string? cfzt { get; set; }

        #endregion
        
        public List<OutpPrescriptionDetailVO>? cfmx { get; set; }

    }
    /// <summary>
    /// chi tiết đơn thuốc
    /// </summary>
    public class OutpPrescriptionDetailVO
    {
        #region 处方明细

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
        /// liều điều trị mỗi lần  đối với hạng mục phục hồi chức năng
        /// </summary>
        public int? mczll { get; set; }

        /// <summary>
        /// liều lượng một từ
        /// </summary>
        public decimal? mcjl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? mcjldw { get; set; }

        /// <summary>
        /// cách dùng
        /// </summary>
        public string? yfCode { get; set; }

        /// <summary>
        /// tần suất
        /// </summary>
        public string? pcCode { get; set; }

        /// <summary>
        /// số lượng trong thuốc=tổng lượng   tổng lượng đang phục hồi chức năng（không phải là số lượng）=tổng lượng
        /// </summary>
        public int? zl { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// đơn không phải phục hồi chức năng tạm thời sử dụng
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
        /// số nhóm（số nhóm của thuốc）
        /// </summary>
        public string? zh { get; set; }

        /// <summary>
        /// vị trí
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
        /// khoa thực hiện（khoa dược của thuốc、khoa thực hiện kiểm tra xét nghiệm）（khoa thực hiện đơn phục hồi chức năng）
        /// </summary>
        public string? zxks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? zxsj { get; set; }
        /// <summary>
        /// mã duy nhất bảo hiểm y tế
        /// </summary>
        public string? ybwym { get; set; }
        /// <summary>
        /// cờ hạn chế sử dụng
        /// </summary>
        public string? xzsybz { get; set; }
        /// <summary>
        /// số ngày
        /// </summary>
        public decimal? ts { get; set; }
        public string? ztId { get; set; }
        public string? ztmc { get; set; }

        /// <summary>
        /// nguyên nhân kháng sinh
        /// </summary>
        public string? kssReason { get; set; }
        /// <summary>
        /// phương pháp vị trí
        /// </summary>
        public string? bwff { get; set; }

        public string? sqlx { get; set; }
        /// <summary>
        /// sắp xếp 
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// tốc độ truyền 
        /// </summary>
        public int? ds { get; set; }
        /// <summary>
        /// cờ chuyển sang tự trả
        /// </summary>
        public int? zzfbz { get; set; }

        /// <summary>
        /// nhóm cách dùng
        /// </summary>
        public string? yfztbm { get; set; }

        public string? yfztmc { get; set; }
        /// <summary>
        /// trạng thái thu phí
        /// </summary>
        public string? sfzt { get; set; }
        /// <summary>
        /// mô tả đơn thuốc Đông y
        /// </summary>
        public string? zysm { get; set; }

        #endregion
    }
}
