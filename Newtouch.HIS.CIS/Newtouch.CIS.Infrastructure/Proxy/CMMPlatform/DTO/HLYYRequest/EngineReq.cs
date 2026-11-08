using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.HLYYRequest
{
    [XmlRoot("root")]
    public class EngineReq
    {
        public bool ShouldSerializeMyProperty() => true;
        public string Type { get; set; } // Đơn thuốc Loại
        public PatientRecord Patient { get; set; } // Thông tin bệnh nhân
        public Operation Operation { get; set; } // Phẫu thuật thông tin
        public List<Prescription> Prescriptions { get; set; } // Thuốc Đơn thuốc danh sách

    }
     public class PatientRecord
    {
        [XmlElement("departID")]
        public string DepartID { get; set; } // Số hiệu khoa
        
        [XmlElement("department")]
        public string Department { get; set; } // Tên khoa
        
        [XmlElement("bedNo")]
        public string BedNo { get; set; } // Nhập viện bệnh nhân Số giường
        
        [XmlElement("presType")]
        public string PresType { get; set; } // Đơn thuốc Loại
        
        [XmlElement("presSource")]
        public string PresSource { get; set; } // đến nguồn
        
        [XmlElement("presDateTime")]
        public string PresDatetime { get; set; } // Đơn thuốc/Y lệnh mở cụ thể Thời gian
        
        [XmlElement("payType")]
        public string PayType { get; set; } // phí loại
        
        [XmlElement("patientNo")]
        public string PatientNo { get; set; } // Khám bệnh Số thẻ
        
        [XmlElement("presNo")]
        public string PresNo { get; set; } // Đơn thuốc/Y lệnh số
        
        [XmlElement("name")]
        public string Name { get; set; } // bệnh nhân Họ tên
        
        [XmlElement("diagnoseid")]
        public string DiagnoseID { get; set; } // Chẩn đoánid
        
        [XmlElement("diagnose")]
        public string Diagnose { get; set; } // Chẩn đoán
        
        [XmlElement("IDCard")]
        public string IDCard { get; set; } // CMND/CCCD số mã
        
        [XmlElement("address")]
        public string Address { get; set; } // bệnh nhân Địa chỉ
        
        [XmlElement("phoneNo")]
        public string PhoneNo { get; set; } // Điện thoại liên hệ
        
        [XmlElement("age")]
        public string Age { get; set; } // Tuổi
        
        [XmlElement("sex")]
        public string Sex { get; set; } // Giới tính
        
        [XmlElement("height")]
        public string Height { get; set; } // thân cao
        
        [XmlElement("weight")]
        public string Weight { get; set; } // thể trọng
        
        [XmlElement("birthWeight")]
        public string BirthWeight { get; set; } // ra sinh khi thể trọng
        
        [XmlElement("previousHistory")]
        public string PreviousHistory { get; set; } // Tiền sử
        
        [XmlElement("nowMedicalHistory")]
        public string NowMedicalHistory { get; set; } // hiện bệnh sử
        
        [XmlElement("ccr")]
        public string Ccr { get; set; } // trongsinh Creatininxóatỷ lệ
        
        [XmlElement("anaphylactogen")]
        public string Anaphylactogen { get; set; } // qua Tiền sử dị ứng
        
        [XmlElement("allergicHistory")]
        public string AllergicHistory { get; set; } // qua mẫn nguồn
        
        [XmlElement("pregnancy")]
        public string Pregnancy { get; set; } // là Không mang thai
        
        [XmlElement("timeOfPreg")]
        public string TimeOfPreg { get; set; } // Thời kỳ mang thai
        
        [XmlElement("disease")]
        public string Disease { get; set; } // là Không Bệnh mạn tính
        
        [XmlElement("breastFeeding")]
        public string BreastFeeding { get; set; } // Có cho con bú hay không
        
        [XmlElement("dialysis")]
        public string Dialysis { get; set; } // là Không Thẩm tách
        
        [XmlElement("proxName")]
        public string ProxName { get; set; } // thay làm người Họ tên
        
        [XmlElement("proxIDCard")]
        public string ProxIDCard { get; set; } // thay làm người CMND/CCCD số
        
        [XmlElement("docID")]
        public string DocID { get; set; } // Bác sĩ Mã nhân viên
        
        [XmlElement("docName")]
        public string DocName { get; set; } // Bác sĩ Họ tên
        
        [XmlElement("totalAmount")]
        public string TotalAmount { get; set; } // Đơn thuốc Số tiền
        // Add any additional fields as necessary
    }

    public class Operation
    {
        [XmlElement("operationCode")]
        public string OperationCode { get; set; } // Mã phẫu thuật
        
        [XmlElement("operationName")]
        public string OperationName { get; set; } // Tên phẫu thuật
        
        [XmlElement("operationStartTime")]
        public string OperationStartTime { get; set; } // Thời gian bắt đầu phẫu thuật
        
        [XmlElement("operationEndTime")]
        public string OperationEndTime { get; set; } // Thời gian kết thúc phẫu thuật
        
        [XmlElement("incisionType")]
        public string IncisionType { get; set; } // cắt miệng Loại
        
        [XmlElement("incisionStatus")]
        public string IncisionStatus { get; set; } // Loại liền vết thương
        
        [XmlElement("inplant")]
        public string Inplant { get; set; } // Có vật cấy ghép hay không
    }

    [XmlRoot("Prescription")]
    public class Prescription
    {
        [XmlElement("drug")]
        public string Drug { get; set; } // Thuốc ID
        
        [XmlElement("drugName")]
        public string DrugName { get; set; } // Tên thuốc
        
        [XmlElement("regName")]
        public string RegName { get; set; } // Tên thương mại
        
        [XmlElement("specification")]
        public string Specification { get; set; } // chứa lượng Quy cách
        
        [XmlElement("package")]
        public string Package { get; set; } // gói lắp Quy cách
        
        [XmlElement("quantity")]
        public float Quantity { get; set; } // Số lượng
        
        [XmlElement("packUnit")]
        public string PackUnit { get; set; } // Đơn vị đóng gói
        
        [XmlElement("unitPrice")]
        public string UnitPrice { get; set; } // Đơn giá
        
        [XmlElement("amount")]
        public string Amount { get; set; } // tổng giá
        
        [XmlElement("groupNo")]
        public string GroupNo { get; set; } // Số nhóm
        
        [XmlElement("firstUse")]
        public string FirstUse { get; set; } // đầu liều Sử dụng
        
        [XmlElement("prepForm")]
        public string PrepForm { get; set; } // Dạng bào chế
        
        [XmlElement("adminRoute")]
        public string AdminRoute { get; set; } // cho thuốc Đường
        
        [XmlElement("adminArea")]
        public string AdminArea { get; set; } // cho thuốc Vị trí
        
        [XmlElement("adminFrequency")]
        public string AdminFrequency { get; set; } // cho thuốc tần suất tỷ lệ
        
        [XmlElement("adminDose")]
        public string AdminDose { get; set; } // cho thuốc Liều lượng
        
        [XmlElement("adminMethod")]
        public string AdminMethod { get; set; } // cho thuốc khi máy
        
        [XmlElement("type")]
        public string Type { get; set; } // Loại
        
        [XmlElement("adminGoal")]
        public string AdminGoal { get; set; } // cho thuốc mắt của
        
        [XmlElement("docID")]
        public string DocID { get; set; } // Bác sĩ Mã nhân viên
        
        [XmlElement("docName")]
        public string DocName { get; set; } // Bác sĩ Họ tên
        
        [XmlElement("docTitle")]
        public string DocTitle { get; set; } // Bác sĩ Chức danh
        
        [XmlElement("departID")]
        public string DepartID { get; set; } // KhoaID
        
        [XmlElement("department")]
        public string Department { get; set; } // Tên khoa
        
        [XmlElement("nurseName")]
        public string NurseName { get; set; } // Điều dưỡng Tên
        
        [XmlElement("startTime")]
        public string StartTime { get; set; } // Thời gian bắt đầu
        
        [XmlElement("endTime")]
        public string EndTime { get; set; } // Thời gian kết thúc
        
        [XmlElement("specialPromote")]
        public string SpecialPromote { get; set; } // Phương pháp sắc thuốc
        
        [XmlElement("continueDays")]
        public string ContinueDays { get; set; } // liên tục Thời gian
    }
}
