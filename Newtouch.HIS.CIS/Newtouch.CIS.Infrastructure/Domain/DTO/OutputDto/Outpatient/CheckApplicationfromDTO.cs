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
        //Cơ sởMã  Y
        public string MedNo { get; set; }
        //Tên cơ sở  Y
        public string MedName { get; set; }

    }
    public class Patient
    {
        //cư dânHọ tên Y
        public string Name { get; set; }
        //Giới tính  Nam/Nữ Y
        public string Gender { get; set; }
        //Ngày sinh Y 
        public DateTime? Birthday { get; set; }
        //CMND/CCCDsố Y
        public string IdentityID { get; set; }
        // Khu bệnhsố
        public string WardNo { get; set; }
        //giường ký tựsố
        public string BedNo { get; set; }
        //bệnh nhânSố thẻ  Y
        public string CardID { get; set; }

        public Request Request;
    }
    public class Request
    {
        //Số phiếu  Y
        public string Reqno { get; set; }
        //Ngày đăng ký  Y
        public DateTime? Indate { get; set; }
        //Bác sĩ đăng kýMã nhân viên
        public string DoctorID { get; set; }
        // Bác sĩ đăng kýHọ tên
        public string DoctorName { get; set; }
        // Bác sĩ đăng kýCMND/CCCD
        public string DoctorIdCard { get; set; }
        // Khám bệnhghi chépMã  Y Ngoại trú、Nhập viện、thểkiểm trasố
        public string RecordID { get; set; }
        //đếnnguồn   Y Ngoại trú MagQ700001   Nhập viện MagQ700002  thểkiểm tra MagQ700003
        public string Source { get; set; }
        //Hình ảnhbáo cáoLoại
        public string Pic { get; set; }
        //báo cáochi tiếtLoại  Y
        public string PicDetail { get; set; }
        //PhiếuTên
        public string ApplyName { get; set; }
        //Vị trí:Phương pháp
        public string BodyPart { get; set; }
        //thứ tựsố
        public int? WhichNo { get; set; }
        // Cờ hiệu lực ký tự  N/Y，N biểu thịHủy
        public string IsVaild { get; set; }
        //làKhôngđãquaThu phí
        public string IsCharge { get; set; }
        //Thu phíNgày
        public DateTime? ChargeDate { get; set; }
        //nghiệp vụphiếusố phiếu
        public string InvoiceID { get; set; }
        //HủyNgày
        public DateTime? CancelDate { get; set; }
        //Ghi chú
        public string Comment { get; set; }
        //Khoa đăng kýMã Y
        public string ApplyDepartmentCode { get; set; }
        //Khoa đăng kýTên Y
        public string ApplyDepartmentName { get; set; }
        // Tên chẩn đoán lâm sàng
        public string DiagnoseName { get; set; }

        public List<Order> Order;
    }
    public class Order
    {
        //Phiếuchi tiếtsố  Y
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
        //thứ tựsố
        public string SN { get; set; }
    }

    public class CheckfromDTO
    {
        // Khám bệnhghi chépMã  Y Ngoại trú、Nhập viện、thểkiểm trasố
        public string RecordID { get; set; }
        //PhiếuTên
        public string ApplyName { get; set; }
        //Số phiếu  Y
        public string Reqno { get; set; }
        //Ngày đăng ký  Y
        public DateTime? Indate { get; set; }
        //Bác sĩ đăng kýMã nhân viên
        public string DoctorID { get; set; }
        // Bác sĩ đăng kýHọ tên
        public string DoctorName { get; set; }
        //Khoa đăng kýMã Y
        public string ApplyDepartmentCode { get; set; }
        //Khoa đăng kýTên Y
        public string ApplyDepartmentName { get; set; }
        // Khu bệnhsố
        public string WardNo { get; set; }
        //giường ký tựsố
        public string BedNo { get; set; }
        //bệnh nhânSố thẻ  Y
        public string CardID { get; set; }
        //Phiếuchi tiếtsố  Y
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
