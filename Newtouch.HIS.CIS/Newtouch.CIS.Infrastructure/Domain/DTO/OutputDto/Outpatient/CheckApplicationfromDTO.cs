using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.OutputDto.Outpatient
{
    public class CheckApplicationfromDTO
    {
        public RequestHost RequestHost;
        public Patient Patient;
    }
    public class RequestHost
    {
        //Cơ sở Mã  Y
        public string MedNo { get; set; }
        //Tên cơ sở  Y
        public string MedName { get; set; }

    }
    public class Patient
    {
        //cư dân Họ tên Y
        public string Name { get; set; }
        //Giới tính  Nam/Nữ Y
        public string Gender { get; set; }
        //Ngày sinh Y 
        public DateTime? Birthday { get; set; }
        //CMND/CCCD số Y
        public string IdentityID { get; set; }
        // Khu bệnh số
        public string WardNo { get; set; }
        //giường ký tự số
        public string BedNo { get; set; }
        //bệnh nhân Số thẻ  Y
        public string CardID { get; set; }

        public Request Request;
    }
    public class Request
    {
        //Số phiếu  Y
        public string Reqno { get; set; }
        //Ngày đăng ký  Y
        public DateTime? Indate { get; set; }
        //Bác sĩ đăng ký Mã nhân viên
        public string DoctorID { get; set; }
        // Bác sĩ đăng ký Họ tên
        public string DoctorName { get; set; }
        // Bác sĩ đăng ký CMND/CCCD
        public string DoctorIdCard { get; set; }
        // Khám bệnh ghi chép Mã  Y Ngoại trú,Nhập viện,thể kiểm tra số
        public string RecordID { get; set; }
        //đến nguồn   Y Ngoại trú MagQ700001   Nhập viện MagQ700002  thể kiểm tra MagQ700003
        public string Source { get; set; }
        //Hình ảnh báo cáo Loại
        public string Pic { get; set; }
        //báo cáo chi tiết Loại  Y
        public string PicDetail { get; set; }
        //Phiếu Tên
        public string ApplyName { get; set; }
        //Vị trí:Phương pháp
        public string BodyPart { get; set; }
        //thứ tự số
        public int? WhichNo { get; set; }
        // Cờ hiệu lực ký tự  N/Y,N biểu thị Hủy
        public string IsVaild { get; set; }
        //là Không đã qua Thu phí
        public string IsCharge { get; set; }
        //Thu phí Ngày
        public DateTime? ChargeDate { get; set; }
        //nghiệp vụ phiếu số phiếu
        public string InvoiceID { get; set; }
        //Hủy Ngày
        public DateTime? CancelDate { get; set; }
        //Ghi chú
        public string Comment { get; set; }
        //Khoa đăng ký Mã Y
        public string ApplyDepartmentCode { get; set; }
        //Khoa đăng ký Tên Y
        public string ApplyDepartmentName { get; set; }
        // Tên chẩn đoán lâm sàng
        public string DiagnoseName { get; set; }

        public List<Order> Order;
    }
    public class Order
    {
        //Phiếu chi tiết số  Y
        public string Seqno { get; set; }
        //Số phiếu  Y
        public string Reqno { get; set; }
        //Mã hạng mục  Y
        public string ItemID { get; set; }
        //Tên mục  Y
        public string ItemName { get; set; }
        //Vị trí 1,Vị trí 2
        public string BodyPart { get; set; }
        //Số lượng
        public string Qty { get; set; }
        //Đơn giá
        public string Price { get; set; }
        //thứ tự số
        public string SN { get; set; }
    }

    public class CheckfromDTO
    {
        // Khám bệnh ghi chép Mã  Y Ngoại trú,Nhập viện,thể kiểm tra số
        public string RecordID { get; set; }
        //Phiếu Tên
        public string ApplyName { get; set; }
        //Số phiếu  Y
        public string Reqno { get; set; }
        //Ngày đăng ký  Y
        public DateTime? Indate { get; set; }
        //Bác sĩ đăng ký Mã nhân viên
        public string DoctorID { get; set; }
        // Bác sĩ đăng ký Họ tên
        public string DoctorName { get; set; }
        //Khoa đăng ký Mã Y
        public string ApplyDepartmentCode { get; set; }
        //Khoa đăng ký Tên Y
        public string ApplyDepartmentName { get; set; }
        // Khu bệnh số
        public string WardNo { get; set; }
        //giường ký tự số
        public string BedNo { get; set; }
        //bệnh nhân Số thẻ  Y
        public string CardID { get; set; }
        //Phiếu chi tiết số  Y
        public string Seqno { get; set; }

        //Mã hạng mục  Y
        public string ItemID { get; set; }
        //Tên mục  Y
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
    public class retinfo {
        public INFO INFO { get; set; }
    }
    public class INFO
    {
        public MESSAGE MESSAGE { get; set; }
        public DATA DATA { get; set; }
    }
    public class MESSAGE
    {
        public string VERSION { get; set; }
        public string FORMAT { get; set; }
    }
    public class DATA
    {
        public BEAN BEAN { get; set; }
    }
    public class BEAN
    {
        public string BILL_ID { get; set; }
        public string HIS_BILL_ID { get; set; }
        public string IS_ERROR { get; set; }
    }
}
