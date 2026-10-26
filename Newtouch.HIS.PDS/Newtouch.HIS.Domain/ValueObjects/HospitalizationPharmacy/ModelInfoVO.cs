using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// dùng chung model 测试
    /// </summary>
    public class ModelInfoVO
    {
        public string model1 { get; set; }
        public string model2 { get; set; }
        public string model3 { get; set; }
        public string model4 { get; set; }
        public string model5 { get; set; }
        public string model6 { get; set; }
        public string model7 { get; set; }
        public string model8 { get; set; }
        public string model9 { get; set; }
        public string model10 { get; set; }
        public string model11 { get; set; }
        public string model12 { get; set; }
    }

    public class ModelBRXXInfoVO
    {
        public int model1 { get; set; }
        public int model2 { get; set; }
        public string model3 { get; set; }
        public string model4 { get; set; }
        public string model5 { get; set; }
        public string model6 { get; set; }
        public string model7 { get; set; }
        public DateTime model8 { get; set; }
        public string model9 { get; set; }
        public short model10 { get; set; }
        public string model11 { get; set; }
        public string model12 { get; set; }

    }

    /// <summary>
    /// phát thuốc khu bệnh bệnh nhân thông tin chi tiết
    /// </summary>
    public class DispenseBQRYXXIndexVO
    {
        public int Id { get; set; }//khóa chính
        public string zxId { get; set; }//thực hiện y lệnh ID
        public string zyh { get; set; }//số nội trú
        public string xm { get; set; }//họ tên
        public string cw { get; set; }//giường
        public string ypCode { get; set; }//thuốc code
        public string ypmc { get; set; }//tên thuốc
        public string ycmc { get; set; }//tên nhà máy dược
        public string ypgg { get; set; }//quy cách thuốc
        public decimal sl { get; set; }//số lượng
        public string dw { get; set; }//đơn vị
        public string yldw { get; set; }//đơn vị liều lượng
        public decimal yl { get; set; }//liều lượng số lượng /thuốc lượng
        public short cls { get; set; }//số lượng tách
        public string zlff { get; set; }//điều trị phương thức
        public string yzxz { get; set; }//y lệnh trạng thái
        public string ypfz { get; set; }//nhóm thuốc
        public DateTime tdrq { get; set; }//ngày rút hồ sơ
        public string fyrq { get; set; }//ngày phát thuốc
        public string rymc { get; set; }//nhân viên tên
        public string Pcmc { get; set; }// PC tên
        public string fybz { get; set; }//trạng thái phát thuốc 2 là đã phát, 1 là chưa phát
        public string OrganizeId { get; set; }//tổ chức
        public string yppc { get; set; }//lô
        public string ypph { get; set; }//số lô
    }


    /// <summary>
    /// lấy tính cần phát thuốc của của đơn giá, số tiền, 
    /// </summary>
    public class ModelBQBRYZZXJEInfoVO
    {
        public Decimal dj { get; set; } //đơn giá
        public Decimal je { get; set; } //số tiền
        public Decimal zfbl { get; set; } //tỷ lệ tự chi trả
        public string zfxz { get; set; } //tính chất tự chi trả
    }

    /// <summary>
    /// trả thuốc nội trú, 医生站医嘱ID
    /// </summary>
    public class ModelZyTyYszyzIdInfoVO
    {
        public int yszyzId { get; set; } //y lệnh ID  yszyzid 医生站 y lệnh ID
        public int zxId { get; set; } //thực hiện y lệnh ID 
        public string yzdm { get; set; }// y lệnh mã
    }

    /// <summary>
    /// 存储退药明细表, lô
    /// </summary>
    public class ModelBQBRYZZXTYInfoVO
    {
        public int Id { get; set; }//khóa chính
        public string zxId { get; set; } //thực hiện y lệnh ID
        public DateTime? tdrq { get; set; } //退档日期
        public string ypCode { get; set; } //thuốc Code
        public decimal tysl { get; set; }//số lượng trả thuốc
        public int tymxId { get; set; }//trả thuốc chi tiết ID
        public string tybz { get; set; }// trả thuốc ghi chú
        public DateTime? tyjlrq { get; set; }//trả thuốc bản ghi ngày
        public string tyxh { get; set; }//trả thuốc số thứ tự 
        public string tyry { get; set; }//trả thuốc nhân viên
        public DateTime? tyrq { get; set; }//trả thuốc ngày
        public string yppc { get; set; }//thực hiện y lệnh số lô bảng lô
        public string ypph { get; set; }//thực hiện y lệnh số lô bảng số lô
        public decimal sl { get; set; }//số lượng
        public string OrganizeId { get; set; }//tổ chức
        public DateTime zxrq { get; set; }//thực hiện ngày
        public string zyh { get; set; }//số nội trú
    }

    /// <summary>
    /// phát thuốc nội trú
    /// </summary>
    public class DispensingDrugsParam
    {
        /// <summary>
        /// y lệnh ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// thực hiện y lệnh ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// có tách lẻ không
        /// 1: có
        /// 2: không
        /// </summary>
        public int? sfcl { get; set; }

    }
}
