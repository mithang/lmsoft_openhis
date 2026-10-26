using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
    public class CheckApplicationfromDTO
    {
       public RequestHost RequestHost;
       public Patient Patient;
    }

    public class RequestHost
    {
        //Mã cơ sở  Y
        public string MedNo { get; set; }
        //Tên cơ sở  Y
        public string MedName { get; set; }

    }
    public class Patient
    {
        //Tên cư dân Y
        public string Name { get; set; }
        //Giới tính  Nam/Nữ Y
        public string Gender { get; set; }
        //Ngày sinh Y 
        public DateTime? Birthday { get; set; }
        //Số CMND/CCCD Y
        public string IdentityID { get; set; }
        // Số khu bệnh
        public string WardNo { get; set; }
        //Số giường bệnh
        public string BedNo { get; set; }
        //Số thẻ bệnh nhân  Y
        public string CardID { get; set; }

        public Request Request;
    }
    public class Request
    {
        //Số phiếu đăng ký  Y
        public string Reqno { get; set; }
        //Ngày đăng ký  Y
        public DateTime? Indate { get; set; }
        //Mã nhân viên bác sĩ đăng ký
        public string DoctorID { get; set; }
        // Tên bác sĩ đăng ký
        public string DoctorName { get; set; }
        // CMND bác sĩ đăng ký
        public string DoctorIdCard { get; set; }
        // Mã hồ sơ khám bệnh  Y Khám ngoại trú,Nhập viện,Số khám sức khỏe
        public string RecordID { get; set; }
        //Nguồn   Y Khám ngoại trú MagQ700001   Nhập viện MagQ700002  Khám sức khỏe MagQ700003
        public string Source { get; set; }
        //Loại báo cáo hình ảnh
        public string Pic { get; set; }
        //Loại chi tiết báo cáo  Y
        public string PicDetail { get; set; }
        //Tên phiếu đăng ký
        public string ApplyName { get; set; }
        //Vị trí:Phương pháp
        public string BodyPart { get; set; }
        //Số thứ tự
        public int? WhichNo { get; set; }
        // Bit cờ hiệu lực  N/Y,N Thể hiện hủy
        public string IsVaild { get; set; }
        //Đã thu phí hay chưa
        public string IsCharge { get; set; }
        //Ngày thu phí
        public DateTime? ChargeDate { get; set; }
        //Số chứng từ nghiệp vụ
        public string InvoiceID { get; set; }
        //Ngày hủy
        public DateTime? CancelDate { get; set; }
        //Ghi chú
        public string Comment { get; set; }
        //Mã khoa đăng ký Y
        public string ApplyDepartmentCode { get; set; }
        //Tên khoa đăng ký Y
        public string ApplyDepartmentName { get; set; }
        //Tên chẩn đoán lâm sàng
        public string DiagnoseName { get; set; }

        public List<Order> Order;
    }
    public class Order
    {
        //Số chi tiết phiếu đăng ký  Y
        public string Seqno { get; set; }
        //Số phiếu đăng ký  Y
        public string Reqno { get; set; }
        //Mã hạng mục  Y
        public string ItemID { get; set; }
        //Tên hạng mục  Y
        public string ItemName { get; set; }
        //Vị trí 1,Vị trí 2
        public string BodyPart { get; set; }
        //Số lượng
        public string Qty { get; set; }
        //Đơn giá
        public string Price { get; set; }
        //Số thứ tự
        public string SN { get; set; }
    }

    public class CheckfromDTO
    {
        // Mã hồ sơ khám bệnh  Y Khám ngoại trú,Nhập viện,Số khám sức khỏe
        public string RecordID { get; set; }
        //Tên phiếu đăng ký
        public string ApplyName { get; set; }
        //Số phiếu đăng ký  Y
        public string Reqno { get; set; }
        //Ngày đăng ký  Y
        public DateTime? Indate { get; set; }
        //Mã nhân viên bác sĩ đăng ký
        public string DoctorID { get; set; }
        // Tên bác sĩ đăng ký
        public string DoctorName { get; set; }
        //Mã khoa đăng ký Y
        public string ApplyDepartmentCode { get; set; }
        //Tên khoa đăng ký Y
        public string ApplyDepartmentName { get; set; }
        // Số khu bệnh
        public string WardNo { get; set; }
        //Số giường bệnh
        public string BedNo { get; set; }
        //Số thẻ bệnh nhân  Y
        public string CardID { get; set; }
        //Số chi tiết phiếu đăng ký  Y
        public string Seqno { get; set; }

        //Mã hạng mục  Y
        public string ItemID { get; set; }
        //Tên hạng mục  Y
        public string ItemName { get; set; }
        //Số lượng
        public string Qty { get; set; }
        //Đơn giá
        public string Price { get; set; }

		public string PicDetail { get; set; }
		public string xm { get; set; }
        public string xb { get; set; }
        public DateTime? csny { get; set; }
        public string zjh { get; set; }

    }
    public class RefJson
    {
        public string Reqno { get; set; }
        public string status { get; set; }
        public string errorCode { get; set; }

    }

}
