/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description: Thực thể lưu quyết toán đơn thuốc thu phí ngoại trú 
// Author:HLF
// CreateDate: 2016/12/30 10:17:05 
//**********************************************************/
namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// gridDanh sáchDTO
    /// </summary>
    public class OutpatChargeDto: OutpatInfoDto
    {
    
        /// <summary>
        /// Cờ khám ngoại trú/cấp cứu
        /// </summary>
        public string mjzbz { get; set; }

        public string ybdm { get; set; }

        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string ys { get; set; }

        public string ysmc { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// Mã khoa
        /// </summary>
        public int ksbh { get; set; }

        public string ksmc { get; set; }

        //Loại lớn
        public string dl { get; set; }
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Loại hạng mục thu phí 1Hạng mục  2Thuốc
        /// </summary>
        public string xmtype { get; set; }
        /// <summary>
        /// Mã nhà thuốc
        /// </summary>
        public string yfdm { get; set; }
        /// <summary>
        /// Mã hạng mục thu phí
        /// </summary>
        public string sfxm { get; set; }
        public string ypbzdm { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }
        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public string zje { get; set; }
        public int pt_nm { get; set; }
        public int pt_zdmxnm { get; set; }
        public byte pt_zdmxxz { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public string czh { get; set; }

        //Liên quan chi tiết đơn thuốc sfxm dl
        /// <summary>
        /// Số hạng mục int
        /// </summary>
        public int xmbh { get; set; }
        public string xmmc { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; }
        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal je { get; set; }
        /// <summary>
        /// Tỷ lệ thanh toán
        /// </summary>
        public decimal zfbl { get; set; }
        public string zfxz { get; set; }
        public string dw { get; set; }
        /// <summary>
        /// Phí dịch vụ
        /// </summary>
        public decimal fwfdj { get; set; }

        //Dữ liệu liên quan quyết toán
        //dl dj sl je zfbl zfxz  sfxm
        //Tên hạng mục outJmbl outJmje mxnm mxbm 
        public string nbdl { get; set; }


        ///// <summary>
        ///// Phương thức thanh toán tiền mặt
        ///// </summary>
        //public string xjzffs { get; set; }

        ///// <summary>
        ///// Số tiền thanh toán
        ///// </summary>
        //public string zfje { get; set; }

        ///// <summary>
        ///// Khoản thu trước == Số tiền thực thu
        ///// </summary>
        //public string ysk { get; set; }

        ///// <summary>
        ///// Tiền thối lại
        ///// </summary>
        //public string zl { get; set; }

    }
}
