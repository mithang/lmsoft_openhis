using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Dùng chungmodel Kiểm thử
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
    /// Thông tin chi tiết bệnh nhân khu bệnh phát thuốc
    /// </summary>
    public class DispenseBQRYXXIndexVO
    {
        public int Id { get; set; }//Khóa chính
        public string zxId { get; set; }//Thực hiện y lệnhID
        public string zyh { get; set; }//Số nội trú
        public string xm { get; set; }//Họ tên
        public string cw { get; set; }//Giường bệnh
        public string ypCode { get; set; }//Thuốccode
        public string ypmc { get; set; }//Tên thuốc
        public string ycmc { get; set; }//Tên hãng thuốc
        public string ypgg { get; set; }//Quy cách thuốc
        public decimal sl { get; set; }//Số lượng
        public string dw { get; set; }//Đơn vị
        public string yldw { get; set; }//Đơn vị liều dùng
        public decimal yl { get; set; }//Số lượng liều dùng /Lượng thuốc
        public short cls { get; set; }//Số lượng chia
        public string zlff { get; set; }//Phương pháp điều trị
        public string yzxz { get; set; }//Trạng thái y lệnh
        public string ypfz { get; set; }//Nhóm thuốc
        public DateTime tdrq { get; set; }//Ngày lấy hồ sơ
        public string fyrq { get; set; }//Ngày phát thuốc
        public string rymc { get; set; }//Tên nhân sự
        public string Pcmc { get; set; }// PC Tên
        public string fybz { get; set; }//Trạng thái phát thuốc 2 là đã phát，1là chưa phát
        public string OrganizeId { get; set; }//Tổ chức
        public string yppc { get; set; }//Lô
        public string ypph { get; set; }//Số lô
    }


    /// <summary>
    /// Lấy đơn giá cần tính phát thuốc，Số tiền，
    /// </summary>
    public class ModelBQBRYZZXJEInfoVO
    {
        public Decimal dj { get; set; } //Đơn giá
        public Decimal je { get; set; } //Số tiền
        public Decimal zfbl { get; set; } //Tỷ lệ tự trả
        public string zfxz { get; set; } //Tính chất tự trả
    }

    /// <summary>
    /// Trả thuốc nội trú，Y lệnh trạm bác sĩID
    /// </summary>
    public class ModelZyTyYszyzIdInfoVO
    {
        public int yszyzId { get; set; } //Y lệnhID  yszyzid Trạm bác sĩ Y lệnhID
        public int zxId { get; set; } //Thực hiện y lệnhID 
        public string yzdm { get; set; }// Mã y lệnh
    }

    /// <summary>
    /// Lưu bảng chi tiết trả thuốc，Lô
    /// </summary>
    public class ModelBQBRYZZXTYInfoVO
    {
        public int Id { get; set; }//Khóa chính
        public string zxId { get; set; } //Thực hiện y lệnhID
        public DateTime? tdrq { get; set; } //Ngày trả hồ sơ
        public string ypCode { get; set; } //ThuốcCode
        public decimal tysl { get; set; }//Số lượng trả thuốc
        public int tymxId { get; set; }//Chi tiết trả thuốcID
        public string tybz { get; set; }// Ghi chú trả thuốc
        public DateTime? tyjlrq { get; set; }//Ngày ghi trả thuốc
        public string tyxh { get; set; }//Số thứ tự trả thuốc 
        public string tyry { get; set; }//Nhân viên trả thuốc
        public DateTime? tyrq { get; set; }//Ngày trả thuốc
        public string yppc { get; set; }//Bảng số lô thực hiện y lệnh Lô
        public string ypph { get; set; }//Bảng số lô thực hiện y lệnh Số lô
        public decimal sl { get; set; }//Số lượng
        public string OrganizeId { get; set; }//Tổ chức
        public DateTime zxrq { get; set; }//Ngày thực hiện
        public string zyh { get; set; }//Số nội trú
    }

    /// <summary>
    /// Phát thuốc nội trú
    /// </summary>
    public class DispensingDrugsParam
    {
        /// <summary>
        /// Y lệnhID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// Thực hiện y lệnhID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// Có chia lẻ không
        /// 1： Có
        /// 2： Không
        /// </summary>
        public int? sfcl { get; set; }

    }
}
