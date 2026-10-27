using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.DTO.OutputDto
{
    [NotMapped]
    public class TreatEntityObj : TreatmentEntity
    {
        /// <summary>
        /// tỉnh
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// thành phố
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// huyện
        /// </summary>
        public string county { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string address { get; set; }
        public int? queno { get; set; }
        /// <summary>
        /// chuyểnkhámnhận dạng：làKhônglàchuyểnkhámBệnh nhân
        /// </summary>
        public string zzbs { get; set; }
    }

    public class OutPatientRegistrationInfoDTO
    {
        /// <summary>
        /// Số khám ngoại trú
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
        /// bệnh nhânTính chất
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// bệnh nhânTính chất
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// bệnh nhânHọ tên
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
        /// chứngmụcLoại
        /// </summary>
        public string zjlx { get; set; }
        /// <summary>
        /// chứngmụcLoại
        /// </summary>
        public string zjlxValue { get; set; }
        /// <summary>
        /// Số giấy tờ
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// CMND/CCCDsốmã
        /// </summary>
        public string idCardNo { get; set; }
        /// <summary>
        /// liênhệsốmã
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
        /// tỉnh
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// thành phố
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// huyện
        /// </summary>
        public string county { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// đăng ký khámThời gian
        /// </summary>
        public string ghsj { get; set; }
        /// <summary>
        /// gấpgấpNgười liên hệ
        /// </summary>
        public string contPerName { get; set; }
        /// <summary>
        /// gấpgấpNgười liên hệĐiện thoại
        /// </summary>
        public string contPerPhoneNum { get; set; }
        /// <summary>
        /// gấpgấpNgười liên hệquan hệ
        /// </summary>
        public string contPerRel { get; set; }
        /// <summary>
        /// gấpgấpNgười liên hệquan hệ
        /// </summary>
        public string contPerRelValue { get; set; }
        /// <summary>
        /// cuối cùngThời gian cập nhật
        /// </summary>
        public string updateTime { get; set; }

        /// <summary>
        /// cửagấpkhámCờ
        /// </summary>
        public string mjzbz { get; set; }

        /// <summary>
        /// cửagấpkhámCờ
        /// </summary>
        public string mjzbzValue { get; set; }

        /// <summary>
        /// Khám bệnhCờ
        /// </summary>
        public string jiuzhenbz { get; set; }

        /// <summary>
        /// Khám bệnhCờ
        /// </summary>
        public string jiuzhenbzValue { get; set; }

        /// <summary>
        /// đăng ký khámThao tácThời gian
        /// </summary>
        public string operatingTime { get; set; }

        /// <summary>
        /// Bảo hiểm y tếquyết toánsố（trướcđặt）
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// lạikhámCờ 1lạikhám
        /// </summary>
        public string fzbz { get; set; }

        /// <summary>
        /// xãbảoSố hiệu
        /// </summary>
        public string sbbh { get; set; }

        /// <summary>
        /// tham sốbảođịaMã
        /// </summary>
        public string cbdbm { get; set; }

        /// <summary>
        /// Pinyin（Tên bệnh nhânMã đầu chữ）
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }

        public string nlshow { get; set; }
        /// <summary>
        /// đăng ký khámđếnnguồnnhãn 2020-3-4 Thêm mới
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
