using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.DTO.OutputDto
{
    [NotMapped]
    public class TreatEntityObj : TreatmentEntity
    {
        /// <summary>
        /// Tỉnh
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// Thành phố
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// Huyện
        /// </summary>
        public string county { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string address { get; set; }
        public int? queno { get; set; }
        /// <summary>
        /// Dấu hiệu chuyển viện:Có phải bệnh nhân chuyển viện hay không
        /// </summary>
        public string zzbs { get; set; }
    }

    public class OutPatientRegistrationInfoDTO
    {
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string ysxm { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        public string brxm { get; set; }
        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public string maritalStatus { get; set; }
        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public string maritalStatusValue { get; set; }
        /// <summary>
        /// Loại giấy tờ
        /// </summary>
        public string zjlx { get; set; }
        /// <summary>
        /// Loại giấy tờ
        /// </summary>
        public string zjlxValue { get; set; }
        /// <summary>
        /// Số giấy tờ
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// Số CMND
        /// </summary>
        public string idCardNo { get; set; }
        /// <summary>
        /// Số liên hệ
        /// </summary>
        public string contactNum { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string sexValue { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public string birth { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string statusValue { get; set; }
        /// <summary>
        /// Tỉnh
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// Thành phố
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// Huyện
        /// </summary>
        public string county { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// Thời gian đăng ký khám
        /// </summary>
        public string ghsj { get; set; }
        /// <summary>
        /// Người liên hệ khẩn cấp
        /// </summary>
        public string contPerName { get; set; }
        /// <summary>
        /// Số điện thoại người liên hệ khẩn cấp
        /// </summary>
        public string contPerPhoneNum { get; set; }
        /// <summary>
        /// Quan hệ người liên hệ khẩn cấp
        /// </summary>
        public string contPerRel { get; set; }
        /// <summary>
        /// Quan hệ người liên hệ khẩn cấp
        /// </summary>
        public string contPerRelValue { get; set; }
        /// <summary>
        /// Thời gian cập nhật lần cuối
        /// </summary>
        public string updateTime { get; set; }

        /// <summary>
        /// Cờ khám ngoại trú/cấp cứu
        /// </summary>
        public string mjzbz { get; set; }

        /// <summary>
        /// Cờ khám ngoại trú/cấp cứu
        /// </summary>
        public string mjzbzValue { get; set; }

        /// <summary>
        /// Cờ khám bệnh
        /// </summary>
        public string jiuzhenbz { get; set; }

        /// <summary>
        /// Cờ khám bệnh
        /// </summary>
        public string jiuzhenbzValue { get; set; }

        /// <summary>
        /// Thời gian thao tác đăng ký khám
        /// </summary>
        public string operatingTime { get; set; }

        /// <summary>
        /// Số quyết toán BHYT(tiền xử lý)
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// Cờ tái khám 1Tái khám
        /// </summary>
        public string fzbz { get; set; }

        /// <summary>
        /// Số bảo hiểm xã hội
        /// </summary>
        public string sbbh { get; set; }

        /// <summary>
        /// Mã nơi tham gia bảo hiểm
        /// </summary>
        public string cbdbm { get; set; }

        /// <summary>
        /// Pinyin(Chữ cái đầu pinyin họ tên bệnh nhân)
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }

        public string nlshow { get; set; }
        /// <summary>
        /// Nhãn nguồn đăng ký khám 2020-3-4 Thêm mới
        /// </summary>
        public string ghlybz { get; set; }

        public string grbh { get; set; }
        public short? queno { get; set; }
    }

    public class PatientRegInfoDTO
    {
        public string Mdtrt_ID { get; set; }
        public string PSN_NO { get; set; }
        public string PSN_NAME { get; set; }
        public string CERTNO { get; set; }
        public string PSN_CERT_TYPE { get; set; }
        public string MED_TYPE { get; set; }
        public string MZHM { get; set; }
        public string AGE { get; set; }
        public string BRXB { get; set; }
        public string SBXH { get; set; }
        public string BRID { get; set; }
        public string INSUTYPE { get; set; }
    }

    public class jzdjbrxx
    {
        public string xm { get; set; }
        public string zjh { get; set; }
        public string xb { get; set; }
        public string csny { get; set; }
        public string nlshowdw { get; set; }
        public string phone { get; set; }
        public string gj { get; set; }
        public string mz { get; set; }
        public string zy { get; set; }
        public string hy { get; set; }
        public string brxz { get; set; }
        public string py { get; set; }
        public string mzlx { get; set; }
        public string kscode { get; set; }
        public string ksname { get; set; }
        public string rygh { get; set; }
        public string ryname { get; set; }
        public string orgid { get; set; }
        public string ecToken { get; set; }
        public string grbh { get; set; }
        public string cbdbm { get; set; }
        public string jzid { get; set; }
        public string sbbh { get; set; }
        public string xzlx { get; set; }
        public string med_type { get; set; }
    }

    public class RefSuccess
    {
        public string code { get; set; }
        public string message { get; set; }
        public string mzh { get; set; }
        public string blh { get; set; }
    }
}
