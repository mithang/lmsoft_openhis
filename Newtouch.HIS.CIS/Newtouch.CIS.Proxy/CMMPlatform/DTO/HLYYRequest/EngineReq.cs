using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.HLYYRequest
{
    [XmlRoot("root")]
    public class EngineReq
    {
        public bool ShouldSerializeMyProperty() => true;
        public string Type { get; set; } // Loại đơn thuốc
        public PatientRecord Patient { get; set; } // Thông tin bệnh nhân
        public Operation Operation { get; set; } // Thông tin phẫu thuật
        public List<Prescription> Prescriptions { get; set; } // Danh sách đơn thuốc

    }
     public class PatientRecord
    {
        [XmlElement("departID")]
        public string DepartID { get; set; } // Số khoa
        
        [XmlElement("department")]
        public string Department { get; set; } // Tên khoa
        
        [XmlElement("bedNo")]
        public string BedNo { get; set; } // Số giường bệnh nhân nội trú
        
        [XmlElement("presType")]
        public string PresType { get; set; } // Loại đơn thuốc
        
        [XmlElement("presSource")]
        public string PresSource { get; set; } // Nguồn
        
        [XmlElement("presDateTime")]
        public string PresDatetime { get; set; } // Thời gian kê đơn thuốc/y lệnh
        
        [XmlElement("payType")]
        public string PayType { get; set; } // Loại phí
        
        [XmlElement("patientNo")]
        public string PatientNo { get; set; } // Số thẻ khám bệnh
        
        [XmlElement("presNo")]
        public string PresNo { get; set; } // Số đơn thuốc/y lệnh
        
        [XmlElement("name")]
        public string Name { get; set; } // Tên bệnh nhân
        
        [XmlElement("diagnoseid")]
        public string DiagnoseID { get; set; } // id chẩn đoán
        
        [XmlElement("diagnose")]
        public string Diagnose { get; set; } // Chẩn đoán
        
        [XmlElement("IDCard")]
        public string IDCard { get; set; } // Số chứng minh nhân dân
        
        [XmlElement("address")]
        public string Address { get; set; } // Địa chỉ bệnh nhân
        
        [XmlElement("phoneNo")]
        public string PhoneNo { get; set; } // Điện thoại liên hệ
        
        [XmlElement("age")]
        public string Age { get; set; } // Tuổi
        
        [XmlElement("sex")]
        public string Sex { get; set; } // Giới tính
        
        [XmlElement("height")]
        public string Height { get; set; } // Chiều cao
        
        [XmlElement("weight")]
        public string Weight { get; set; } // Cân nặng
        
        [XmlElement("birthWeight")]
        public string BirthWeight { get; set; } // Cân nặng lúc sinh
        
        [XmlElement("previousHistory")]
        public string PreviousHistory { get; set; } // Tiền sử
        
        [XmlElement("nowMedicalHistory")]
        public string NowMedicalHistory { get; set; } // Bệnh sử hiện tại
        
        [XmlElement("ccr")]
        public string Ccr { get; set; } // Độ thanh thải creatinin nội sinh
        
        [XmlElement("anaphylactogen")]
        public string Anaphylactogen { get; set; } // Tiền sử dị ứng
        
        [XmlElement("allergicHistory")]
        public string AllergicHistory { get; set; } // Dị nguyên
        
        [XmlElement("pregnancy")]
        public string Pregnancy { get; set; } // Có mang thai hay không
        
        [XmlElement("timeOfPreg")]
        public string TimeOfPreg { get; set; } // Thai kỳ
        
        [XmlElement("disease")]
        public string Disease { get; set; } // Có bệnh mạn tính hay không
        
        [XmlElement("breastFeeding")]
        public string BreastFeeding { get; set; } // Có đang cho con bú hay không
        
        [XmlElement("dialysis")]
        public string Dialysis { get; set; } // Có đang lọc máu hay không
        
        [XmlElement("proxName")]
        public string ProxName { get; set; } // Tên người làm thay
        
        [XmlElement("proxIDCard")]
        public string ProxIDCard { get; set; } // Số chứng minh nhân dân của người làm thay
        
        [XmlElement("docID")]
        public string DocID { get; set; } // Mã nhân viên bác sĩ
        
        [XmlElement("docName")]
        public string DocName { get; set; } // Tên bác sĩ
        
        [XmlElement("totalAmount")]
        public string TotalAmount { get; set; } // Số tiền đơn thuốc
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
        public string IncisionType { get; set; } // Loại vết mổ
        
        [XmlElement("incisionStatus")]
        public string IncisionStatus { get; set; } // Loại lành vết thương
        
        [XmlElement("inplant")]
        public string Inplant { get; set; } // Có vật cấy ghép hay không
    }

    [XmlRoot("Prescription")]
    public class Prescription
    {
        [XmlElement("drug")]
        public string Drug { get; set; } // ID thuốc
        
        [XmlElement("drugName")]
        public string DrugName { get; set; } // Tên thuốc
        
        [XmlElement("regName")]
        public string RegName { get; set; } // Tên thương mại
        
        [XmlElement("specification")]
        public string Specification { get; set; } // Quy cách hàm lượng
        
        [XmlElement("package")]
        public string Package { get; set; } // Quy cách đóng gói
        
        [XmlElement("quantity")]
        public float Quantity { get; set; } // Số lượng
        
        [XmlElement("packUnit")]
        public string PackUnit { get; set; } // Đơn vị đóng gói
        
        [XmlElement("unitPrice")]
        public string UnitPrice { get; set; } // Đơn giá
        
        [XmlElement("amount")]
        public string Amount { get; set; } // Tổng giá
        
        [XmlElement("groupNo")]
        public string GroupNo { get; set; } // Số nhóm
        
        [XmlElement("firstUse")]
        public string FirstUse { get; set; } // Sử dụng liều đầu
        
        [XmlElement("prepForm")]
        public string PrepForm { get; set; } // Dạng bào chế
        
        [XmlElement("adminRoute")]
        public string AdminRoute { get; set; } // Đường dùng thuốc
        
        [XmlElement("adminArea")]
        public string AdminArea { get; set; } // Vị trí dùng thuốc
        
        [XmlElement("adminFrequency")]
        public string AdminFrequency { get; set; } // Tần suất dùng thuốc
        
        [XmlElement("adminDose")]
        public string AdminDose { get; set; } // Liều dùng thuốc
        
        [XmlElement("adminMethod")]
        public string AdminMethod { get; set; } // Thời điểm dùng thuốc
        
        [XmlElement("type")]
        public string Type { get; set; } // Loại
        
        [XmlElement("adminGoal")]
        public string AdminGoal { get; set; } // Mục đích dùng thuốc
        
        [XmlElement("docID")]
        public string DocID { get; set; } // Mã nhân viên bác sĩ
        
        [XmlElement("docName")]
        public string DocName { get; set; } // Tên bác sĩ
        
        [XmlElement("docTitle")]
        public string DocTitle { get; set; } // Chức danh bác sĩ
        
        [XmlElement("departID")]
        public string DepartID { get; set; } // ID khoa
        
        [XmlElement("department")]
        public string Department { get; set; } // Tên khoa
        
        [XmlElement("nurseName")]
        public string NurseName { get; set; } // Tên y tá
        
        [XmlElement("startTime")]
        public string StartTime { get; set; } // Thời gian bắt đầu
        
        [XmlElement("endTime")]
        public string EndTime { get; set; } // Thời gian kết thúc
        
        [XmlElement("specialPromote")]
        public string SpecialPromote { get; set; } // Phương pháp sắc thuốc
        
        [XmlElement("continueDays")]
        public string ContinueDays { get; set; } // Thời gian kéo dài
    }
}
