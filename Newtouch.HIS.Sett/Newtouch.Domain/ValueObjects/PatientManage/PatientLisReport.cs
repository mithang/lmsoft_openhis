using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class PatientReportIndex  
    {
        /// <summary>
        /// Số phiếu đăng ký
        /// </summary>
        public string sqdh { get; set; }
        /// <summary>
        /// Tên hạng mục
        /// </summary>
        public string xmmc { get; set; }
        /// <summary>
        /// Số hạng mục
        /// </summary>
        public string xmdm { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string ysmc { get; set; }
        /// <summary>
        /// Trạng thái phiếu đăng ký
        /// </summary>
        public string sqdzt { get; set; }
        /// <summary>
        /// Loại nghiệp vụ phiếu đăng ký 1 Khám ngoại trú 2 Nhập viện
        /// </summary>
        public string ywlx { get; set; }
        /// <summary>
        /// Thời gian đăng ký
        /// </summary>
        public DateTime? sqsj { get; set; }
    }

    public class PatLisReportDetail {
        /// <summary>
        /// Thông tin bệnh nhân
        /// </summary>
        public LisPatInfo patInfo { get; set; }
        /// <summary>
        /// Chi tiết xét nghiệm
        /// </summary>
        public List<LisReportBase> sqdInfo { get; set; }
    }

    public class LisPatInfo
    {
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// Tuổi
        /// </summary>
        public string nlshow { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string ysmc { get; set; }
        /// <summary>
        /// Nhập viện-Khu bệnh
        /// </summary>
        public string bq { get; set; }
        /// <summary>
        /// Nhập viện-Giường bệnh
        /// </summary>
        public string cwmc { get; set; }
        public string brxz { get; set; }
        public string mzh { get; set; }
        public string zyh { get; set; }
        public string zdmc { get; set; }

    }

    public class LisReportBase
    {
        /// <summary>
        /// Số phiếu đăng ký
        /// </summary>
        public string sqdh { get; set; }
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int xh { get; set; }
        /// <summary>
        /// Tên hạng mục
        /// </summary>
        public string xmzwmc { get; set; }
        /// <summary>
        /// Tên viết tắt tiếng Anh
        /// </summary>
        public string xmywmc { get; set; }
        /// <summary>
        /// Kết quả xét nghiệm
        /// </summary>
        public string jyjg { get; set; }
        /// <summary>
        /// Nhắc kết quả
        /// </summary>
        public string gdbj { get; set; }
        /// <summary>
        /// Đơn vị
        /// </summary>
        public string xmdw { get; set; }
        /// <summary>
        /// Giá trị thấp nhất phạm vi tham khảo
        /// </summary>
        public string ckzdx { get; set; }
        /// <summary>
        /// Giá trị cao nhất phạm vi tham khảo
        /// </summary>
        public string ckzgx { get; set; }
        /// <summary>
        /// Giá trị tham khảo
        /// </summary>
        public string ckz { get; set; }
        /// <summary>
        /// Giá trị thấp nhất nguy kịch
        /// </summary>
        public string wjzdx { get; set; }
        /// <summary>
        /// Giá trị cao nhất nguy kịch
        /// </summary>
        public string wjzgx { get; set; }
        /// <summary>
        /// Loại nghiệp vụ 1 Khám ngoại trú 2 Nhập viện
        /// </summary>
        public string ywlx { get; set; }
        /// <summary>
        /// Người nhập
        /// </summary>
        public string lrrxm { get; set; }
        /// <summary>
        /// Người kiểm tra
        /// </summary>
        public string jyrxm { get; set; }
        /// <summary>
        /// Người kiểm tra
        /// </summary>
        public string shrxm { get; set; }
        /// <summary>
        /// Ngày xét nghiệm
        /// </summary>
        public DateTime? jyrq { get; set; }
        /// <summary>
        /// Ngày kiểm tra
        /// </summary>
        public DateTime? shrq { get; set; }
        /// <summary>
        /// Tên hạng mục
        /// </summary>
        public string xmmc { get; set; }
        /// <summary>
        /// Mã hạng mục
        /// </summary>
        public string xmdm { get; set; }

    }
}
