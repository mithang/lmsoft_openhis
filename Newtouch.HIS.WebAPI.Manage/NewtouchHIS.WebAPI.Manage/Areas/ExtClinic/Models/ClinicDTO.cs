/**** định nghĩa và giao diện phòng khám nhất quán ******/

using Mapster;
using Mapster.Utils;
using Microsoft.Data.SqlClient.Server;
using System.Runtime.Serialization;

namespace NewtouchHIS.WebAPI.Manage.Areas.ExtClinic
{
    /// <summary>
    /// xác thực danh tính phòng khámToken
    /// </summary>
    public class ClinicTokenDTO
    {
        public string token { get; set; }
    }

    /// <summary>
    /// thông báo kết quả đăng ký khám chữa bệnh
    /// </summary>
    public class TreatedApplyResultDTO
    {
        /// <summary>
        /// đăng ký khám chữa bệnh từ xaID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// trạng thái khám bệnh（0:chưa nộp, 1:chờ xác nhận、2:đang khám、3:đã kết thúc、4:đã phát thuốc、5:đã trả lại）
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// số phòng họp
        /// </summary>
        public string? conferenceId { get; set; }
        /// <summary>
        /// kết quả đăng ký（0：thông qua:1：không đạt）
        /// </summary>
        public string applicationResults { get; set; }
    }
    /// <summary>
    /// bệnh án bệnh nhân phòng khám
    /// </summary>
    public class ClinicPatMedicalRecordDTO
    {
        public ClinicDicDTO? doctor { get; set; }
        public string? name { get; set; }
        /// <summary>
        /// đăng ký phòng khámId
        /// </summary>
        public string? diagnosisId { get; set; }
        public string? department { get; set; }
        public string? createBy { get; set; }
        public DateTime? createDate { get; set; }
        /// <summary>
        /// than phiền chính
        /// </summary>
        public string? patientTell { get; set; }
        /// <summary>
        /// bệnh sử hiện tại
        /// </summary>
        public string? nowHistory { get; set; }
        /// <summary>
        /// tiền sử
        /// </summary>
        public string? beforeHistory { get; set; }
        /// <summary>
        /// tiền sử cá nhân
        /// </summary>
        public string? personalHistory { get; set; }
        /// <summary>
        /// tiền sử dị ứng
        /// </summary>
        public string? allergyHistory { get; set; }
        /// <summary>
        /// tiền sử bệnh
        /// </summary>
        public string? diseaseHistory { get; set; }
        /// <summary>
        /// tiền sử hôn nhân – mang thai
        /// </summary>
        public string? pregnancyHistory { get; set; }
        /// <summary>
        /// tiền sử bệnh truyền nhiễm
        /// </summary>
        public string? infectiousHistory { get; set; }
        /// <summary>
        /// tiền sử kinh nguyệt
        /// </summary>
        public string? lunariaHistory { get; set; }
        /// <summary>
        /// tiền sử gia đình
        /// </summary>
        public string? familyHistory { get; set; }
        /// <summary>
        /// tiền sử phẫu thuật
        /// </summary>
        public string? surgeryHistory { get; set; }
        /// <summary>
        /// tiền sử truyền máu
        /// </summary>
        public string? transfusionHistory { get; set; }
        /// <summary>
        /// khám thực thể
        /// </summary>
        public string? physiqueCheck { get; set; }
        /// <summary>
        /// kiểm tra hỗ trợ
        /// </summary>
        public string? assistCheck { get; set; }
        /// <summary>
        /// chẩn đoán cấp cứu
        /// </summary>
        public string? emergencyDiagnose { get; set; }
        /// <summary>
        /// hiệu quả cấp cứu
        /// </summary>
        public string? emergencyEffect { get; set; }
        /// <summary>
        /// kiểm tra khác
        /// </summary>
        public string? otherCheck { get; set; }
        /// <summary>
        /// tình hình xử lý
        /// </summary>
        public string? handlingSituation { get; set; }
        /// <summary>
        /// giáo dục sức khỏe cá nhân hóa
        /// </summary>
        public string? healthEducation { get; set; }
        /// <summary>
        /// tiền sử dịch tễ học
        /// </summary>
        public string? epidemicDisease { get; set; }
        /// <summary>
        /// hạng mục y lệnh
        /// </summary>
        public string? doctorAdvice { get; set; }
        /// <summary>
        /// chẩn đoán Tây y
        /// </summary>
        public string? westernDiagnose { get; set; }
        /// <summary>
        /// chẩn đoán y học cổ truyền
        /// </summary>
        public string? chinaDiagnose { get; set; }
    }

    public class ClinicDicDTO
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? value { get; set; }
    }
    /// <summary>
    /// thông tin đơn thuốc phòng khám
    /// </summary>
    public class ClinicRecipelDTO
    {
        /// <summary>
        /// tên đơn thuốc
        /// </summary>
        public string? name { get; set; }
        /// <summary>
        /// loại đơn thuốc
        /// value:recipelType_0：đơn thuốc Tây y  recipelType_1：đơn thuốc Đông y
        /// </summary>
        public ClinicDicDTO? recipelType { get; set; }
        /// <summary>
        /// liều dùng thuốc Đông y 
        /// Ví dụ：1thang（không kèm đơn vị）
        /// </summary>
        public string? dosage { get; set; }
        /// <summary>
        /// cách dùng thuốc Đông y
        /// value:
        /// chineseMedicineRecipelUse_0  sắc rồi uống
        /// chineseMedicineRecipelUse_1  sắc rồi uống
        /// chineseMedicineRecipelUse_2  uống pha với nước
        /// chineseMedicineRecipelUse_3  uống ngâm
        /// chineseMedicineRecipelUse_4  uống pha
        /// chineseMedicineRecipelUse_5  tiêm huyệt vị
        /// chineseMedicineRecipelUse_6  sắc với nước rồi uống
        /// </summary>
        public ClinicDicDTO? recipelUse { get; set; }
        /// <summary>
        /// liều lượng tần suất
        /// value:
        /// chineseMedicineRecipelFrequency_0  một thang một ngày
        /// chineseMedicineRecipelFrequency_1  hai thang một ngày
        /// chineseMedicineRecipelFrequency_2  ba thang một ngày
        /// </summary>
        public ClinicDicDTO? frequency { get; set; }
        /// <summary>
        /// tần suất sử dụng
        /// value:
        /// Ví dụ：một lần một ngày
        /// </summary>
        public ClinicDicDTO? takeFrequency { get; set; }
        /// <summary>
        /// liều dùng một lần
        /// Ví dụ：100ml（không kèm đơn vị）
        /// </summary>
        public int? singleDosage { get; set; }
        /// <summary>
        /// tổng giá đơn thuốc
        /// </summary>
        public decimal? fee { get; set; }
        /// <summary>
        /// ghi chú
        /// </summary>
        public string? remarks { get; set; }
        /// <summary>
        /// y lệnh
        /// </summary>
        public string? entrust { get; set; }
        /// <summary>
        /// hạng mục ghi chú thuốc Đông y
        /// </summary>
        public string? chinessNotes { get; set; }
    }
    /// <summary>
    /// chi tiết đơn thuốc
    /// </summary>
    public class ClinicRecipelDetailDTO
    {
        /// <summary>
        /// loại thuốc
        /// 1 thuốc Đông y  2 thuốc Tây y）
        /// </summary>
        public int? stuffType { get; set; }
        /// <summary>
        /// đơn giá
        /// </summary>
        public decimal? unitPrice { get; set; }
        /// <summary>
        /// tổng giá
        /// </summary>
        public decimal? allFee { get; set; }

        /// <summary>
        /// cách dùng thuốc Tây y
        /// value:
        /// westernMedicineUse_0  uống
        /// westernMedicineUse_1  tiêm tĩnh mạch
        /// westernMedicineUse_2  trước bữa ăn
        /// westernMedicineUse_3  sau bữa ăn
        /// westernMedicineUse_4  tiêm dưới da
        /// westernMedicineUse_5  ngậm
        /// westernMedicineUse_6  đắp ngoài
        /// westernMedicineUse_7  truyền tĩnh mạch
        /// </summary>
        public ClinicDicDTO? westernMedicineUse { get; set; }
        /// <summary>
        /// cách dùng thuốc Đông y
        /// value:
        /// chineseMedicineRecipelUse_0  sắc rồi uống
        /// chineseMedicineRecipelUse_1  sắc rồi uống
        /// chineseMedicineRecipelUse_2  uống pha với nước
        /// chineseMedicineRecipelUse_3  uống ngâm
        /// chineseMedicineRecipelUse_4  uống pha
        /// chineseMedicineRecipelUse_5  tiêm huyệt vị
        /// chineseMedicineRecipelUse_6  sắc với nước rồi uống
        /// </summary>
        public ClinicDicDTO? chineseMedicineUse { get; set; }
        /// <summary>
        /// liều lượng tần suất thuốc Tây y
        /// chineseMedicineRecipelTakeFrequency_0.5   bốn lần mỗi ngày
        /// chineseMedicineRecipelTakeFrequency_1.5   một lần hai ngày
        /// chineseMedicineRecipelTakeFrequency_1      một lần mỗi ngày
        /// chineseMedicineRecipelTakeFrequency_2      hai lần mỗi ngày
        /// chineseMedicineRecipelTakeFrequency_3      ba lần mỗi ngày
        /// </summary>
        public ClinicDicDTO? frequency { get; set; }
        /// <summary>
        /// số ngày sử dụng thuốc Tây y
        /// name 
        /// </summary>
        public ClinicDicDTO? days { get; set; }
        /// <summary>
        /// tổng lượng đơn vị nhỏ nhất
        /// </summary>
        public decimal? minTotal { get; set; }

        /// <summary>
        /// liều dùng một lần
        /// </summary>
        public decimal? singleDosage { get; set; }
        /// <summary>
        /// tổng lượng thuốc dùng
        /// sử dụng tổng lượng đơn vị nhỏ nhất，Ví dụ：một lần1g，ba lần một ngày，uống ba ngày，liều lượng thuốc này là9g
        /// </summary>
        public decimal? total { get; set; }
        public ClinicDrugInfoDTO? drugStuffId { get; set; }
    }

    public class ClinicDrugInfoDTO
    {
        /// <summary>
        /// thuốcid
        /// </summary>
        public string? drugStuffId { get; set; }
        /// <summary>
        /// tên thuốc
        /// </summary>
        public string? name { get; set; }
        /// <summary>
        /// đơn giá
        /// </summary>
        public decimal? price { get; set; }
        /// <summary>
        /// giá bán lẻ sau khi tách
        /// </summary>
        public decimal? retailPrice { get; set; }
        /// <summary>
        /// đơn vị liều lượng
        /// value:
        /// medicalDosisUnit_0  g
        ///medicalDosisUnit_1 ml
        ///medicalDosisUnit_2 l
        ///medicalDosisUnit_3 mg
        ///medicalDosisUnit_4 m
        ///medicalDosisUnit_5  %
        ///medicalDosisUnit_6 ug
        ///medicalDosisUnit_7 phim
        /// </summary>
        public ClinicDicDTO? dosisUnit { get; set; }
        /// <summary>
        /// đơn vị bào chế
        /// medicalPreparationUnit_0   phim 
        /// medicalPreparationUnit_1   viên 
        /// medicalPreparationUnit_2  túi 
        /// medicalPreparationUnit_3  g 
        /// medicalPreparationUnit_4  cái 
        /// medicalPreparationUnit_5  ống 
        /// medicalPreparationUnit_6  ml 
        /// medicalPreparationUnit_7  mm 
        /// medicalPreparationUnit_8  mg 
        /// medicalPreparationUnit_9  chai 
        /// medicalPreparationUnit_10 quyển 
        /// medicalPreparationUnit_11 ug 
        /// medicalPreparationUnit_12 viên hoàn
        /// </summary>
        public ClinicDicDTO? preparationUnit { get; set; }
        /// <summary>
        /// đơn vị đóng gói
        /// medicalPackUnit_0   gói
        /// medicalPackUnit_1 túi
        ///medicalPackUnit_2 hộp
        ///medicalPackUnit_3 thùng
        ///medicalPackUnit_5 vỉ
        ///medicalPackUnit_6 lon
        ///medicalPackUnit_7 ống
        ///medicalPackUnit_8 cái
        ///medicalPackUnit_9 chai
        ///medicalPackUnit_10 g
        ///medicalPackUnit_11 quyển
        /// </summary>
        public ClinicDicDTO? pack { get; set; }
        
    }

    public class ClinicPatRecipelData
    {
        public ClinicRecipelDTO? recipelInfo { get; set; }
        public List<ClinicRecipelDetailDTO>? recipelDetailEvtList { get; set; }
    }

    public class ClinicPatRecipelDataSync
    {
        public List<ClinicPatRecipelData>? recipelInfoEvtList { get; set; }
        public string id { get; set; }
    }
}
