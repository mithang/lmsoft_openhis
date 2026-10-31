using System;
using System.Collections.Generic;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.DTO.InputDto.Inpatient
{
    /// <summary>
    /// LưuY lệnhkhi，viewtruyềnđếnmáy chủcủađối tượng
    /// </summary>
    public class DoctorServiceRequestDto
    {
        public string Id { get; set; }
        public string zyh { get; set; }

        public int? zh { get; set; }

        public string pcCode { get; set; }
        public string pcmc { get; set; }

        public int? zxcs { get; set; }

        public int? zxzq { get; set; }

        public int? zxzqdw { get; set; }

        public string zdm { get; set; }

        public decimal? idm { get; set; }

        public string xmdm { get; set; }

        public string xmmc { get; set; }

        public string dwwwwwww { get; set; }//Đơn vị
        public int zbbzzzzzzz { get; set; }//tựchuẩn bịCờ
        public int? iszzffffff { get; set; }//Chuyển sang tự chi trảCờ
        public int? ispscsfffff { get; set; }//dathửđothửCờ
        public int? isjffffff { get; set; }//làKhôngtínhphí
        public string yztag { get; set; }//thuốc gây nghiện hướng thầnY lệnh

        public decimal? ypjl { get; set; }

        public string yfmc { get; set; }

        public string ypyfdm { get; set; }

        public int sl { get; set; }

        public int? dwlb { get; set; }

        public string ypgg { get; set; }

        public string ztnr { get; set; }

        public string yznr { get; set; }

        public string hzxm { get; set; }

        public string WardCode { get; set; }

        public string DeptCode { get; set; }

        public string ysgh { get; set; }

        public int yzlx { get; set; }

        public int? ts { get; set; }

        public string kssj { get; set; }
        public string zxsj { get; set; }//24giờThực hiệncủaThời gian
        /// <summary>
        /// Hệ số chuyển đổi liều
        /// </summary>
        public decimal? jlzhxs { get; set; }
        /// <summary>
        /// Nhập việnchuyểnđổihệsố
        /// </summary>
        public decimal? zyzhxs { get; set; }
        public string bw { get; set; }
        public string zxksdm { get; set; }
        public string zydw { get; set; }

        //ăn uốngY lệnh
        public string nlmd { get; set; }//mật độ năng lượng
        public string nlmddm { get; set; }//mật độ năng lượng
        public string ssyzcfId { get; set; }

        public string yslb { get; set; }//ăn uốngLoại
        public string yslbdm { get; set; }//ăn uốngLoạival

        public string yszs { get; set; }//ăn uốngchỉhiển thị
        public string yszsval { get; set; }//ăn uốngIds

        public string ssxhdm { get; set; }//ăn uốngloạisố
        public string ssxhval { get; set; }//ăn uốngloạisố
        public string ssxh { get; set; }//ăn uốngloạisố
        public string ztmbId { get; set; }//Bộ mẫusởởmẫuid
        public string ztId { get; set; }//Bộ mẫuid
        public string ztmc { get; set; }//Tên bộ mẫu

        /// <summary>
        /// Kháng sinhnguyên nhân
        /// </summary>
        public string kssReason { get; set; }

        public string sqlx { get; set; }

        public string bwff { get; set; }

        public string sqdh { get; set; }

        /// <summary>
        /// Loại y lệnh  临-Y lệnh tạm thời；长-Y lệnh dài hạn
        /// </summary>
        public string yzlb { get; set; }

        /// <summary>
        /// Y lệnhsố，cùng mộtY lệnhsốtronggóichứanhiềucáiyzId
        /// </summary>
        public string yzh { get; set; }

        /// <summary>
        /// tốc độ truyền  giọt/Phút
        /// </summary>
        public int? ds { get; set; }
        /// <summary>
        /// Ấn tượng lâm sàng
        /// </summary>
        public string lcyx { get; set; }
        /// <summary>
        /// Đăng kýGhi chú
        /// </summary>
        public string sqbz { get; set; }

        /// <summary>
        /// sắc hộCờ
        /// </summary>
        public int? djbzzzzzzz { get; set; }
        /// <summary>
        /// Xuất việnmang thuốcCờ
        /// </summary>
        public int? cydybzzzzzzz { get; set; }
        /// <summary>
        /// Y lệnhChẩn đoánicd10
        /// </summary>
        public string yzzdid { get; set; }
        /// <summary>
        /// Y lệnhTên chẩn đoán
        /// </summary>
        public string yzzdmc { get; set; }
        /// <summary>
        /// Y lệnhChẩn đoánThời gian
        /// </summary>
        public string yzzdsj { get; set; }
        /// <summary>
        /// làKhôngMụcBộ mẫu
        /// </summary>
        public string sfxmzt { get; set; }
        /// <summary>
        /// Bộ mẫuSố lượng
        /// </summary>
        public int? ztsl { get; set; }
        /// <summary>
        /// Cách dùng thuốctương ứngBộ mẫu
        /// </summary>
        public string yfztbm { get; set; }
        public string yfztbs { get; set; }
        public string dcztbs { get; set; }
        /// <summary>
        /// Thuốcđếnnguồn 1:KhoaTồn kho 2Nhà thuốc
        /// </summary>
        public int? yply { get; set; }
        public int? Px { get; set; }
    }

    public class DoctorServiceUIRequestDto : DoctorServiceRequestDto
    {
        public string action { get; set; }
        public string redundant_jldw { get; set; }
        public string dw { get; set; }
        public string tcmc { get; set; }
        public string yfmcval { get; set; }
        public string jxCode { get; set; }
        public string qzfs { get; set; }
        public string zxksmc { get; set; }
        public int? kcsl { get; set; }
        public string kzbz { get; set; }
        public int? dwjls { get; set; }
        public string yzlb { get; set; }
        public int tcfw { get; set; }
        public decimal? dj { get; set; }
        public string zfxz { get; set; }
        public string sqdh { get; set; }
        public string ispscs { get; set; }
        public int? isjf { get; set; }
        public int? iszzf { get; set; }
        
    }
    public class HistoricalOrdersRequestDto : DoctorServiceRequestDto
    {
        public string action { get; set; }
        public string redundant_jldw { get; set; }
        public string dw { get; set; }
        public string yfmcval { get; set; }
        public string jxCode { get; set; }
        public string qzfs { get; set; }
        public string zxksmc { get; set; }
        public int? kcsl { get; set; }
        public string kzbz { get; set; }
        public int? dwjls { get; set; }
        public string yzlb { get; set; }
        public decimal? dj { get; set; }
        public string zfxz { get; set; }
        public string ispscs { get; set; }
        public int? isjf { get; set; }
        public int? zzfbz { get; set; }
        public string CreateTime { get; set; }
    }
    public class PatientDto
    {
        public string zyh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string ryrq { get; set; }
        public string cyrq { get; set; }
        public string ryzd { get; set; }
        public string cyzd { get; set; }
    }
    /// <summary>
    /// Nhập việnY lệnhQuản lý，Thuốcdựchuẩn bịDữ liệukhi
    /// </summary>
    public class MedicineInfoDto
    {
        public string dw { get; set; }
        public string jx { get; set; }
        public decimal? jlzhxs { get; set; }
        public decimal? zyzhxs { get; set; }
        public string qzfs { get; set; }
    }

    /// <summary>
    /// Bệnh nhân nội trúmộtxem đãra khỏi khubệnh nhâncủalớp nổiĐiều kiện lọc
    /// </summary>
    public class patientInfoDto
    {
        public string Id { get; set; }
        public string zyh { get; set; }
        public string blh { get; set; }
        public string xm { get; set; }
        public string birth { get; set; }
        public string sex { get; set; }
        public int age { get; set; }
        public string ksdm { get; set; }
        public string ksmc { get; set; }
        public string zdmc { get; set; }
        public string ysgh { get; set; }
        public string ysxm { get; set; }
        public DateTime ryrq { get; set; }
        public string brxzmc { get; set; }
        public string bqmc { get; set; }
        public string cwmc { get; set; }
        public DateTime? rqrq { get; set; }
        public DateTime? cqrq { get; set; }
        public int? zybz { get; set; }
    }
    public class DoctorServiceparentRequestDto
    {
        public patientInfoDto patientInfo { get; set; }
        public List<DoctorServiceUIRequestDto> DoctorServiceUIRequestDto { get; set; }
        public string DrugStockInfo { get; set; }
    }

    public class SaveDoctorServiceDto
    {
        public List<InpatientSTATOrderEntity> lsyzList { get; set; }
        public List<InpatientLongTermOrderEntity> cqyzList { get; set; }
        public List<InpatientSTATOrderEntity> EditlsyzList { get; set; }
        public List<InpatientLongTermOrderEntity> EditcqyzList { get; set; }
        public List<InpatientDietDetailSplitEntity> ssyzList { get; set; }
        public List<InpatientDiagnosisEntity> zyyzzdList { get; set; }
        public List<string> DelIds { get; set; }
        public List<string> DelssyzIds { get; set; }
    }

    /// <summary>
    /// kiểm traY lệnhlàKhôngtrùng lặp
    /// </summary>
    public class DSrepeatRequestDto
    {
        public string Id { get; set; }
        //0 Y lệnh tạm thời 1 Y lệnh dài hạn
        public int clbz { get; set; }
        public DateTime kssj { get; set; }
        public string xmdm { get; set; }
    }

    public class SfxmztDto
    {
        public string ztId { get; set; }
        public string ztmc { get; set; }
        public string sfxm { get; set; }
        public string sfxmmc { get; set; }
        public string dw { get; set; }
        public decimal dj { get; set; }
        public int sl { get; set; }
        public string zxks { get; set; }
        public string zxksmc { get; set; }
        public string sfmb { get; set; }
    }
    public class yfsfxmdyDto
    {
        public string sfmb { get; set; }
        public string sfmbmc { get; set; }
    }

    public class ypyfdataDto
    {
        public string yfbmCode { get; set; }
        public string yfbmmc { get; set; }
        public string mzzybz { get; set; }
    }
    public class base_SfxmDto
    {
        public decimal? dj { get; set; }
        public int? dwjls { get; set; }
    }
}
