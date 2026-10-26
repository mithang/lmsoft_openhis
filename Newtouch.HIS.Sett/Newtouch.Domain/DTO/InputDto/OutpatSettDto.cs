/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description: Thực thể chi tiết hạng mục quyết toán
// Author:hlf
// CreateDate: 2017/1/4 14:13:43 
//**********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// Thực thể dữ liệu quyết toán
    /// </summary>
   public class OutpatSettDto
    {
        /// <summary>
        /// Loại lớn
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// Hạng mục thu phí
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Số tiền = (Đơn giá phí dịch vụ+Đơn giá)*Số lượng
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// Tỷ lệ tự chi trả
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// Tính chất tự chi trả
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// Tỷ lệ giảm miễn
        /// </summary>
        public decimal jmbl { get; set; }

        /// <summary>
        /// Số tiền giảm miễn
        /// </summary>
        public decimal jmje { get; set; }

        /// <summary>
        /// Mã nội bộ chi tiết
        /// </summary>
        public int mxnm { get; set; }

        /// <summary>
        /// Mã chi tiết
        /// </summary>
        //public string mxbm { get; set; }

        /// <summary>
        /// Mã nội bộ chi tiết đơn thuốc
        /// </summary>
        public int cf_mxnm { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public string czh { get; set; }

        //----------Trường dùng cho Bảo hiểm nông nghiệp------------------//
        /// <summary>
        /// HisTên(Tên thuốc hoặc hạng mục khám chữa bệnh)
        /// </summary>
        public string Hismc { get; set; }

        /// <summary>
        /// Mã hạng mục trung tâm dùng thuốc(Loại lớn Bảo hiểm nông nghiệp)
        /// </summary>
        public string Xmbm { get; set; }

        /// <summary>
        /// Đơn vị thuốc
        /// </summary>
        public string Dw { get; set; }

        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Dạng bào chế thuốc
        /// </summary>
        public string Jx { get; set; }

        /// <summary>
        /// Tên nhân viên thao tác
        /// </summary>
        public string Czymc { get; set; }

        ///// <summary>
        ///// Liều dùng mỗi lần
        ///// </summary>
        //public string Jl { get; set; }

        ///// <summary>
        ///// Đơn vị liều dùng
        ///// </summary>
        //public string Jldw { get; set; }

        ///// <summary>
        ///// Cách dùng thuốc
        ///// </summary>
        //public string Ypyf { get; set; }

        ///// <summary>
        ///// Tần suất
        ///// </summary>
        //public string Pc { get; set; }

        ///// <summary>
        ///// Số ngày
        ///// </summary>
        //public string Ts { get; set; }

        /// <summary>
        /// Bác sĩ kê đơn
        /// </summary>
        public string Kdys { get; set; }

        /// <summary>
        /// Khoa kê đơn
        /// </summary>
        public string Kdks { get; set; }

        /// <summary>
        /// Chẩn đoán ngoại trú
        /// </summary>
        public string mzzd { get; set; }

        /// <summary>
        /// Ngày khám
        /// </summary>
        public DateTime jzrq { get; set; }


        //----------Trường dùng cho Bảo hiểm nông nghiệp------------------//

        /// <summary>
        /// Đơn giá phí dịch vụ
        /// </summary>
        public decimal fwfdj { get; set; }

        /// <summary>
        /// Số tiền giao dịch BHYT(Dữ liệu thu được sau khi tính toán)
        /// </summary>
        public decimal jyje { get; set; }

        /// <summary>
        /// Số tiền trong phạm vi giao dịch BHYT(Dữ liệu thu được sau khi tính toán)
        /// </summary>
        public decimal jyfwje { get; set; }

        /// <summary>
        /// Số tiền được thanh toán(Dữ liệu thu được sau khi tính toán)
        /// </summary>
        public decimal kbje { get; set; }

        /// <summary>
        /// Tự chi trả theo phân loại(Dữ liệu thu được sau khi tính toán)
        /// </summary>
        public decimal flzf { get; set; }

        /// <summary>
        /// Chi phí tự lo(Dữ liệu thu được sau khi tính toán)
        /// </summary>
        public decimal zlfy { get; set; }

        //----------Trường trạng thái đơn thuốc dùng cho trạm bác sĩ ngoại trú------------------//
        /// <summary>
        /// Mã nội bộ đơn thuốc tại trạm bác sĩ
        /// </summary>
        public int pt_cfnm { get; set; }
        //----------Trường trạng thái đơn thuốc dùng cho trạm bác sĩ ngoại trú------------------//
    }
}
