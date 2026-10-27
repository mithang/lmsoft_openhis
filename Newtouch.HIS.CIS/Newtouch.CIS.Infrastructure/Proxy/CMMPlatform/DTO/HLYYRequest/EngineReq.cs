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
        public string Type { get; set; } // Đơn thuốcLoại
        public PatientRecord Patient { get; set; } // Thông tin bệnh nhân
        public Operation Operation { get; set; } // Phẫu thuậtthông tin
        public List<Prescription> Prescriptions { get; set; } // ThuốcĐơn thuốcdanh sách

    }
     public class PatientRecord
    {
        [XmlElement("departID")]
        public string DepartID { get; set; } // Số hiệu khoa
        
        [XmlElement("department")]
        public string Department { get; set; } // Tên khoa
        
        [XmlElement("bedNo")]
        public string BedNo { get; set; } // Nhập việnbệnh nhânSố giường
        
        [XmlElement("presType")]
        public string PresType { get; set; } // Đơn thuốcLoại
        
        [XmlElement("presSource")]
        public string PresSource { get; set; } // đếnnguồn
        
        [XmlElement("presDateTime")]
        public string PresDatetime { get; set; } // Đơn thuốc/Y lệnhmởcụ thểThời gian
        
        [XmlElement("payType")]
        public string PayType { get; set; } // phíloại
        
        [XmlElement("patientNo")]
        public string PatientNo { get; set; } // Khám bệnhSố thẻ
        
        [XmlElement("presNo")]
        public string PresNo { get; set; } // Đơn thuốc/Y lệnhsố
        
        [XmlElement("name")]
        public string Name { get; set; } // bệnh nhânHọ tên
        
        [XmlElement("diagnoseid")]
        public string DiagnoseID { get; set; } // Chẩn đoánid
        
        [XmlElement("diagnose")]
        public string Diagnose { get; set; } // Chẩn đoán
        
        [XmlElement("IDCard")]
        public string IDCard { get; set; } // CMND/CCCDsốmã
        
        [XmlElement("address")]
        public string Address { get; set; } // bệnh nhânĐịa chỉ
        
        [XmlElement("phoneNo")]
        public string PhoneNo { get; set; } // Điện thoại liên hệ
        
        [XmlElement("age")]
        public string Age { get; set; } // Tuổi
        
        [XmlElement("sex")]
        public string Sex { get; set; } // Giới tính
        
        [XmlElement("height")]
        public string Height { get; set; } // thâncao
        
        [XmlElement("weight")]
        public string Weight { get; set; } // thểtrọng
        
        [XmlElement("birthWeight")]
        public string BirthWeight { get; set; } // rasinhkhithểtrọng
        
        [XmlElement("previousHistory")]
        public string PreviousHistory { get; set; } // Tiền sử
        
        [XmlElement("nowMedicalHistory")]
        public string NowMedicalHistory { get; set; } // hiệnbệnhsử
        
        [XmlElement("ccr")]
        public string Ccr { get; set; } // trongsinh肌酐xóatỷ lệ
        
        [XmlElement("anaphylactogen")]
        public string Anaphylactogen { get; set; } // quaTiền sử dị ứng
        
        [XmlElement("allergicHistory")]
        public string AllergicHistory { get; set; } // quamẫnnguồn
        
        [XmlElement("pregnancy")]
        public string Pregnancy { get; set; } // làKhôngmang thai
        
        [XmlElement("timeOfPreg")]
        public string TimeOfPreg { get; set; } // 孕kỳ
        
        [XmlElement("disease")]
        public string Disease { get; set; } // làKhôngBệnh mạn tính
        
        [XmlElement("breastFeeding")]
        public string BreastFeeding { get; set; } // làKhông哺乳
        
        [XmlElement("dialysis")]
        public string Dialysis { get; set; } // làKhông透析
        
        [XmlElement("proxName")]
        public string ProxName { get; set; } // thaylàm ngườiHọ tên
        
        [XmlElement("proxIDCard")]
        public string ProxIDCard { get; set; } // thaylàm ngườiCMND/CCCDsố
        
        [XmlElement("docID")]
        public string DocID { get; set; } // Bác sĩMã nhân viên
        
        [XmlElement("docName")]
        public string DocName { get; set; } // Bác sĩHọ tên
        
        [XmlElement("totalAmount")]
        public string TotalAmount { get; set; } // Đơn thuốcSố tiền
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
        public string IncisionType { get; set; } // cắtmiệngLoại
        
        [XmlElement("incisionStatus")]
        public string IncisionStatus { get; set; } // 愈hợpLoại
        
        [XmlElement("inplant")]
        public string Inplant { get; set; } // làKhôngcó植vàovật
    }

    [XmlRoot("Prescription")]
    public class Prescription
    {
        [XmlElement("drug")]
        public string Drug { get; set; } // ThuốcID
        
        [XmlElement("drugName")]
        public string DrugName { get; set; } // Tên thuốc
        
        [XmlElement("regName")]
        public string RegName { get; set; } // Tên thương mại
        
        [XmlElement("specification")]
        public string Specification { get; set; } // chứalượngQuy cách
        
        [XmlElement("package")]
        public string Package { get; set; } // góilắpQuy cách
        
        [XmlElement("quantity")]
        public float Quantity { get; set; } // Số lượng
        
        [XmlElement("packUnit")]
        public string PackUnit { get; set; } // Đơn vị đóng gói
        
        [XmlElement("unitPrice")]
        public string UnitPrice { get; set; } // Đơn giá
        
        [XmlElement("amount")]
        public string Amount { get; set; } // tổnggiá
        
        [XmlElement("groupNo")]
        public string GroupNo { get; set; } // Số nhóm
        
        [XmlElement("firstUse")]
        public string FirstUse { get; set; } // đầuliềuSử dụng
        
        [XmlElement("prepForm")]
        public string PrepForm { get; set; } // Dạng bào chế
        
        [XmlElement("adminRoute")]
        public string AdminRoute { get; set; } // chothuốcĐường
        
        [XmlElement("adminArea")]
        public string AdminArea { get; set; } // chothuốcVị trí
        
        [XmlElement("adminFrequency")]
        public string AdminFrequency { get; set; } // chothuốctần suấttỷ lệ
        
        [XmlElement("adminDose")]
        public string AdminDose { get; set; } // chothuốcLiều lượng
        
        [XmlElement("adminMethod")]
        public string AdminMethod { get; set; } // chothuốckhimáy
        
        [XmlElement("type")]
        public string Type { get; set; } // Loại
        
        [XmlElement("adminGoal")]
        public string AdminGoal { get; set; } // chothuốcmắtcủa
        
        [XmlElement("docID")]
        public string DocID { get; set; } // Bác sĩMã nhân viên
        
        [XmlElement("docName")]
        public string DocName { get; set; } // Bác sĩHọ tên
        
        [XmlElement("docTitle")]
        public string DocTitle { get; set; } // Bác sĩChức danh
        
        [XmlElement("departID")]
        public string DepartID { get; set; } // KhoaID
        
        [XmlElement("department")]
        public string Department { get; set; } // Tên khoa
        
        [XmlElement("nurseName")]
        public string NurseName { get; set; } // Điều dưỡngTên
        
        [XmlElement("startTime")]
        public string StartTime { get; set; } // Thời gian bắt đầu
        
        [XmlElement("endTime")]
        public string EndTime { get; set; } // Thời gian kết thúc
        
        [XmlElement("specialPromote")]
        public string SpecialPromote { get; set; } // sắc煮Phương pháp
        
        [XmlElement("continueDays")]
        public string ContinueDays { get; set; } // liên tụcThời gian
    }
}
