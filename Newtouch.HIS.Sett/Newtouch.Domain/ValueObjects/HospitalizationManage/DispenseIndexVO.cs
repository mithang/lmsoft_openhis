using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    public class DispenseIndexVO
    {
    }
    /// <summary>
    /// Khu bệnh lưu phát thuốc
    /// </summary>
    public class DispenseBQIndexVO
    {
        public string bqCode { get; set; }//Mã khu bệnh
        public string bqmc { get; set; }//Tên khu bệnh

    }
    /// <summary>
    /// Bệnh nhân khu bệnh lưu phát thuốc
    /// </summary>
    public class DispenseBQRYIndexVO
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }//Số nhập viện
        public string xm { get; set; }//Họ tên
        public string bq { get; set; }//Khu bệnh
        public string cw { get; set; }//Giường bệnh
    }


    /// <summary>
    /// Chi tiết bệnh nhân khu bệnh đã phát thuốc, cẩn thận
    /// </summary>
    public class DispenseBQRYXXIndexVO
    {
        public int yzId { get; set; }//Y lệnhID
        public string zyh { get; set; }//Số nhập viện
        public string xm { get; set; }//Họ tên
        public string cw { get; set; }//Giường bệnh
        public string ypCode { get; set; }//Thuốccode
        public string ypmc { get; set; }//Tên thuốc
        public string ycmc { get; set; }//Tên nhà máy dược
        public string ypgg { get; set; }//Quy cách thuốc
        public decimal sl { get; set; }//Số lượng
        public string dw { get; set; }//Đơn vị
        public string yldw { get; set; }//Đơn vị liều lượng
        public decimal yl { get; set; }//Số lượng liều dùng /Lượng thuốc
        public short cls { get; set; }//Số lượng đã tách
        public string zlff { get; set; }//Phương pháp điều trị
        public string yzxz { get; set; }//Trạng thái y lệnh
        public string ypfz { get; set; }//Nhóm thuốc
        public DateTime tdrq { get; set; }//Ngày chuyển hồ sơ
        public string fyrq { get; set; }//Ngày phát thuốc
        public string rymc { get; set; }//Tên nhân viên
        public string Pcmc { get; set; }// PC Tên
        public string fybz { get; set; }//Trạng thái phát thuốc 2 Là đã phát,1Là chưa phát

    }

    /// <summary>
    /// Chi tiết bệnh nhân khu bệnh đã phát thuốc, cẩn thận
    /// </summary>
    public class DispenseBQRYCXXXIndexVO
    {
        public string ksrq { get; set; }
        public string yxq { get; set; }
        public string xm { get; set; }
        public string cw { get; set; }
        public string ypCode { get; set; }
        public string ypmc { get; set; }
        public string ycmc { get; set; }
        public string ypgg { get; set; }
        public string sl { get; set; }
        public string sfdw { get; set; }
        public string slydw { get; set; }
        public string tysl { get; set; }
        public string tyry { get; set; }
        public string tyrymc { get; set; }
        public string tyrq { get; set; }
        public string tysqczyh { get; set; }
        public string tysqry { get; set; }
        public string tyjlrq { get; set; }
        public string tybz { get; set; }
        public string yldw { get; set; }
        public string yl { get; set; }
        public string ylydw { get; set; }
        public string dj { get; set; }
        public string je { get; set; }
        public string jxmc { get; set; }
        public string zlff { get; set; }
        public string yzxz { get; set; }
        public string czbh { get; set; }
        public string pc { get; set; }
        public string pyrq { get; set; }
        public string fyrq { get; set; }
        public string rymc { get; set; }
        public string yzId { get; set; }
        public string tdrq { get; set; }
        public string pyry { get; set; }
        public string ks { get; set; }
        public string fybz { get; set; }
        public string zyh { get; set; }
        public string yszyzid { get; set; }
        public string xb { get; set; }
        public string yppc { get; set; }
        public string gmxx { get; set; }
        public string sjap { get; set; }
        public string Cls { get; set; }
    }

    //Thao tác pha thuốc bệnh nhân khu bệnh
    public class DispenseBQBRPYIndexVO
    {
        public int yzId   { get; set; }//Số y lệnh
        public string zyh { get; set; }//Số nhập viện
        public Decimal sl { get; set; }//Số lượng
        public string lyyf { get; set; }//Loại cần nhà thuốc
        public string ypCode { get; set; }//Thuốccode
        public short cls { get; set; }// Số lượng đã tách
        public DateTime jdrq { get; set; }//Ngày chuyển hồ sơ,Ngày thao tác,Ngày lập hồ sơ
    }

    /// <summary>
    /// Nhập viện Thông tin trả thuốc
    /// </summary>
    public class DispenseBQRYTYXXIndexVO
    {
        public string xm { get; set; } // Họ tên
        public string cw { get; set; }// Giường bệnh
        public string ypCode { get; set; }// Thuốccode
        public string ypmc { get; set; }// Tên thuốc
        public string ycmc { get; set; }// Tên nhà máy dược
        public string ypgg { get; set; }// Quy cách thuốc
        public Decimal sl { get; set; }// Số lượng
        public string tysl { get; set; }// Số lượng trả thuốc
        public string dw { get; set; }// Đơn vị
        public string yldw { get; set; }// Đơn vị liều lượng
        public Decimal yl { get; set; }// Liều dùng
        public string zlff { get; set; }// Cách dùng
        public string yzxz { get; set; }//Tính chất y lệnh
        public string czbh { get; set; }// Thuốc Phân tử
        public string pc { get; set; }// Bảng trả thuốc Lô
        public DateTime? pyrq { get; set; }// Ngày pha thuốc
        public DateTime? fyrq { get; set; }// Ngày phát thuốc
        public string rymc { get; set; }//Tên nhân viên
        public string tymxId { get; set; }// Chi tiết trả thuốcID
        public string tdrq { get; set; }// Ngày trả hồ sơ
        public string pyry { get; set; }//Nhân viên pha thuốc
        public string ks { get; set; }//Khoa
        public string tybz { get; set; }//Cờ trả thuốc
        public string zyh { get; set; }//Số nhập viện
        public int yszyzid { get; set; }//Trạm bác sĩ Y lệnhID
        public string xb { get; set; }//Giới tính
        public string yppc { get; set; }//Lô thuốc
        public string bz { get; set; }//Ghi chú(Mặc định để trống)
        public string gmxx { get; set; }//Thông tin dị ứng(Mặc định để trống)
        public string sjap { get; set; }//Sắp xếp thời gian
        public string tyjlrq { get; set; }//Ngày ghi trả thuốc
        public string tyxh { get; set; }// Số thứ tự trả thuốc
        public string ypph { get; set; }//Bảng trả thuốc Số lô thuốc
        public string tyrq { get; set; }// Ngày trả thuốc của bảng trả thuốc
        public string tyry { get; set; }//Nhân viên trả thuốc
    }
}
