/**** Định nghĩa và  Nhất quán với giao diện phòng khám ******/

using Mapster;
using Mapster.Utils;
using Microsoft.Data.SqlClient.Server;
using System.Runtime.Serialization;

namespace NewtouchHIS.WebAPI.Manage.Areas.ExtClinic
{
    /// <summary>
    /// Xác thực danh tính phòng khámToken
    /// </summary>
    public class ClinicTokenDTO
    {
        public string token { get; set; }
    }

    /// <summary>
    /// Đăng ký khám chữa bệnhThông báo kết quả
    /// </summary>
    public class TreatedApplyResultDTO
    {
        /// <summary>
        /// Từ xaĐăng ký khám chữa bệnhID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Trạng thái khám(0:Chưa nộp, 1:Chờ xác nhận,2:Đang khám,3:Đã kết thúc,4:Đã phát thuốc,5:Đã trả lại)
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Cuộc họpSố
        /// </summary>
        public string? conferenceId { get; set; }
        /// <summary>
        /// Đăng kýKết quả(0:Đạt:1:Không đạt)
        /// </summary>
        public string applicationResults { get; set; }
    }
    /// <summary>
    /// Phòng khámBệnh nhânBệnh án
    /// </summary>
    public class ClinicPatMedicalRecordDTO
    {
        public ClinicDicDTO? doctor { get; set; }
        public string? name { get; set; }
        /// <summary>
        /// Phòng khámĐăng kýId
        /// </summary>
        public string? diagnosisId { get; set; }
        public string? department { get; set; }
        public string? createBy { get; set; }
        public DateTime? createDate { get; set; }
        /// <summary>
        /// Lý do khám chính
        /// </summary>
        public string? patientTell { get; set; }
        /// <summary>
        /// Bệnh sử hiện tại
        /// </summary>
        public string? nowHistory { get; set; }
        /// <summary>
        /// Tiền sử
        /// </summary>
        public string? beforeHistory { get; set; }
        /// <summary>
        /// Tiền sử cá nhân
        /// </summary>
        public string? personalHistory { get; set; }
        /// <summary>
        /// Tiền sử dị ứng
        /// </summary>
        public string? allergyHistory { get; set; }
        /// <summary>
        /// Tiền sử bệnh
        /// </summary>
        public string? diseaseHistory { get; set; }
        /// <summary>
        /// Tiền sử hôn nhân và mang thai
        /// </summary>
        public string? pregnancyHistory { get; set; }
        /// <summary>
        /// Tiền sử bệnh truyền nhiễm
        /// </summary>
        public string? infectiousHistory { get; set; }
        /// <summary>
        /// Tiền sử kinh nguyệt
        /// </summary>
        public string? lunariaHistory { get; set; }
        /// <summary>
        /// Tiền sử gia đình
        /// </summary>
        public string? familyHistory { get; set; }
        /// <summary>
        /// Tiền sử phẫu thuật
        /// </summary>
        public string? surgeryHistory { get; set; }
        /// <summary>
        /// Tiền sử truyền máu
        /// </summary>
        public string? transfusionHistory { get; set; }
        /// <summary>
        /// Khám thể chất
        /// </summary>
        public string? physiqueCheck { get; set; }
        /// <summary>
        /// Kiểm tra hỗ trợ
        /// </summary>
        public string? assistCheck { get; set; }
        /// <summary>
        /// Chẩn đoán cấp cứu
        /// </summary>
        public string? emergencyDiagnose { get; set; }
        /// <summary>
        /// Hiệu quả cấp cứu
        /// </summary>
        public string? emergencyEffect { get; set; }
        /// <summary>
        /// Kiểm tra khác
        /// </summary>
        public string? otherCheck { get; set; }
        /// <summary>
        /// Tình hình xử lý
        /// </summary>
        public string? handlingSituation { get; set; }
        /// <summary>
        /// Giáo dục sức khỏe cá thể hóa
        /// </summary>
        public string? healthEducation { get; set; }
        /// <summary>
        /// Tiền sử dịch tễ học
        /// </summary>
        public string? epidemicDisease { get; set; }
        /// <summary>
        /// Hạng mục y lệnh
        /// </summary>
        public string? doctorAdvice { get; set; }
        /// <summary>
        /// Chẩn đoán tây y
        /// </summary>
        public string? westernDiagnose { get; set; }
        /// <summary>
        /// Chẩn đoán y học cổ truyền
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
    /// Thông tin đơn thuốc phòng khám
    /// </summary>
    public class ClinicRecipelDTO
    {
        /// <summary>
        /// Tên đơn thuốc
        /// </summary>
        public string? name { get; set; }
        /// <summary>
        /// Loại đơn thuốc
        /// value:recipelType_0:Đơn thuốc tây  recipelType_1:Đơn thuốc đông y
        /// </summary>
        public ClinicDicDTO? recipelType { get; set; }
        /// <summary>
        /// Liều dùng thuốc đông y 
        /// Ví dụ:1thang(Không kèm đơn vị)
        /// </summary>
        public string? dosage { get; set; }
        /// <summary>
        /// Cách dùng thuốc đông y
        /// value:
        /// chineseMedicineRecipelUse_0  Sắc uống
        /// chineseMedicineRecipelUse_1  Nấu uống
        /// chineseMedicineRecipelUse_2  Pha nước uống
        /// chineseMedicineRecipelUse_3  Ngâm uống
        /// chineseMedicineRecipelUse_4  Pha uống
        /// chineseMedicineRecipelUse_5  Tiêm huyệt vị
        /// chineseMedicineRecipelUse_6  Sắc nước uống
        /// </summary>
        public ClinicDicDTO? recipelUse { get; set; }
        /// <summary>
        /// Liều lượng theo tần suất
        /// value:
        /// chineseMedicineRecipelFrequency_0  Một ngày một thang
        /// chineseMedicineRecipelFrequency_1  Một ngày hai thang
        /// chineseMedicineRecipelFrequency_2  Một ngày ba thang
        /// </summary>
        public ClinicDicDTO? frequency { get; set; }
        /// <summary>
        /// Tần suất dùng
        /// value:
        /// Ví dụ:Một ngày một lần
        /// </summary>
        public ClinicDicDTO? takeFrequency { get; set; }
        /// <summary>
        /// Liều dùng một lần
        /// Ví dụ:100ml(Không kèm đơn vị)
        /// </summary>
        public int? singleDosage { get; set; }
        /// <summary>
        /// Tổng giá đơn thuốc
        /// </summary>
        public decimal? fee { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? remarks { get; set; }
        /// <summary>
        /// Y lệnh
        /// </summary>
        public string? entrust { get; set; }
        /// <summary>
        /// Hạng mục ghi chú thuốc đông y
        /// </summary>
        public string? chinessNotes { get; set; }
    }
    /// <summary>
    /// Chi tiết đơn thuốc
    /// </summary>
    public class ClinicRecipelDetailDTO
    {
        /// <summary>
        /// Loại thuốc
        /// 1 Thuốc đông y  2 Thuốc tây)
        /// </summary>
        public int? stuffType { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal? unitPrice { get; set; }
        /// <summary>
        /// Tổng giá
        /// </summary>
        public decimal? allFee { get; set; }

        /// <summary>
        /// Cách dùng thuốc tây
        /// value:
        /// westernMedicineUse_0  Uống
        /// westernMedicineUse_1  Tiêm tĩnh mạch
        /// westernMedicineUse_2  Trước bữa ăn
        /// westernMedicineUse_3  Sau bữa ăn
        /// westernMedicineUse_4  Tiêm dưới da
        /// westernMedicineUse_5  Ngậm tan
        /// westernMedicineUse_6  Đắp ngoài
        /// westernMedicineUse_7  Truyền tĩnh mạch
        /// </summary>
        public ClinicDicDTO? westernMedicineUse { get; set; }
        /// <summary>
        /// Cách dùng thuốc đông y
        /// value:
        /// chineseMedicineRecipelUse_0  Sắc uống
        /// chineseMedicineRecipelUse_1  Nấu uống
        /// chineseMedicineRecipelUse_2  Pha nước uống
        /// chineseMedicineRecipelUse_3  Ngâm uống
        /// chineseMedicineRecipelUse_4  Pha uống
        /// chineseMedicineRecipelUse_5  Tiêm huyệt vị
        /// chineseMedicineRecipelUse_6  Sắc nước uống
        /// </summary>
        public ClinicDicDTO? chineseMedicineUse { get; set; }
        /// <summary>
        /// Liều lượng theo tần suất thuốc tây
        /// chineseMedicineRecipelTakeFrequency_0.5   Mỗi ngày bốn lần
        /// chineseMedicineRecipelTakeFrequency_1.5   Hai ngày một lần
        /// chineseMedicineRecipelTakeFrequency_1      Mỗi ngày một lần
        /// chineseMedicineRecipelTakeFrequency_2      Mỗi ngày hai lần
        /// chineseMedicineRecipelTakeFrequency_3      Mỗi ngày ba lần
        /// </summary>
        public ClinicDicDTO? frequency { get; set; }
        /// <summary>
        /// Số ngày dùng thuốc tây
        /// name 
        /// </summary>
        public ClinicDicDTO? days { get; set; }
        /// <summary>
        /// Tổng lượng đơn vị nhỏ nhất
        /// </summary>
        public decimal? minTotal { get; set; }

        /// <summary>
        /// Liều dùng một lần
        /// </summary>
        public decimal? singleDosage { get; set; }
        /// <summary>
        /// Tổng lượng thuốc dùng
        /// Tổng lượng đơn vị nhỏ nhất khi dùng, ví dụ,Như:một lần1g,một ngày ba lần,uống ba ngày,thì lượng thuốc này là9g
        /// </summary>
        public decimal? total { get; set; }
        public ClinicDrugInfoDTO? drugStuffId { get; set; }
    }

    public class ClinicDrugInfoDTO
    {
        /// <summary>
        /// Thuốcid
        /// </summary>
        public string? drugStuffId { get; set; }
        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string? name { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal? price { get; set; }
        /// <summary>
        /// Giá bán lẻ sau khi tách
        /// </summary>
        public decimal? retailPrice { get; set; }
        /// <summary>
        /// Đơn vị liều lượng
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
        /// Đơn vị chế phẩm
        /// medicalPreparationUnit_0   phim 
        /// medicalPreparationUnit_1   Viên 
        /// medicalPreparationUnit_2  Túi 
        /// medicalPreparationUnit_3  g 
        /// medicalPreparationUnit_4  Cái 
        /// medicalPreparationUnit_5  Ống 
        /// medicalPreparationUnit_6  ml 
        /// medicalPreparationUnit_7  mm 
        /// medicalPreparationUnit_8  mg 
        /// medicalPreparationUnit_9  Lọ 
        /// medicalPreparationUnit_10 Quyển 
        /// medicalPreparationUnit_11 ug 
        /// medicalPreparationUnit_12 Hoàn
        /// </summary>
        public ClinicDicDTO? preparationUnit { get; set; }
        /// <summary>
        /// Đơn vị đóng gói
        /// medicalPackUnit_0   Gói
        /// medicalPackUnit_1 Túi
        ///medicalPackUnit_2 Hộp
        ///medicalPackUnit_3 Thùng
        ///medicalPackUnit_5 Vỉ
        ///medicalPackUnit_6 Lon
        ///medicalPackUnit_7 Ống
        ///medicalPackUnit_8 Cái
        ///medicalPackUnit_9 Lọ
        ///medicalPackUnit_10 g
        ///medicalPackUnit_11 Quyển
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
