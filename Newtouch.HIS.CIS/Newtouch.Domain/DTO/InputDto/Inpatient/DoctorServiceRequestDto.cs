using System;
using System.Collections.Generic;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.DTO.InputDto.Inpatient
{
    /// <summary>
    /// Khi lưu y lệnh,viewĐối tượng truyền tới backend
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
        public int zbbzzzzzzz { get; set; }//Cờ tự chuẩn bị
        public int? iszzffffff { get; set; }//Cờ chuyển sang tự chi trả
        public int? ispscsfffff { get; set; }//Cờ kiểm tra thử da
        public int? isjffffff { get; set; }//Có tính phí hay không
        public string yztag { get; set; }//Y lệnh thuốc hướng thần/gây mê

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
        public string zxsj { get; set; }//24Thời gian thực hiện theo giờ
        /// <summary>
        /// Hệ số chuyển đổi liều lượng
        /// </summary>
        public decimal? jlzhxs { get; set; }
        /// <summary>
        /// Hệ số chuyển đổi nội trú
        /// </summary>
        public decimal? zyzhxs { get; set; }
        public string bw { get; set; }
        public string zxksdm { get; set; }
        public string zydw { get; set; }

        //Y lệnh chế độ ăn
        public string nlmd { get; set; }//Mật độ năng lượng
        public string nlmddm { get; set; }//Mật độ năng lượng
        public string ssyzcfId { get; set; }

        public string yslb { get; set; }//Loại chế độ ăn
        public string yslbdm { get; set; }//Loại chế độ ănval

        public string yszs { get; set; }//Chỉ thị chế độ ăn
        public string yszsval { get; set; }//Chế độ ănIds

        public string ssxhdm { get; set; }//Mẫu chế độ ăn
        public string ssxhval { get; set; }//Mẫu chế độ ăn
        public string ssxh { get; set; }//Mẫu chế độ ăn
        public string ztmbId { get; set; }//Mẫu chứa góiid
        public string ztId { get; set; }//Góiid
        public string ztmc { get; set; }//Tên gói

        /// <summary>
        /// Lý do dùng kháng sinh
        /// </summary>
        public string kssReason { get; set; }

        public string sqlx { get; set; }

        public string bwff { get; set; }

        public string sqdh { get; set; }

        /// <summary>
        /// Loại y lệnh  Tạm thời-Y lệnh tạm thời;Dài hạn-Y lệnh dài hạn
        /// </summary>
        public string yzlb { get; set; }

        /// <summary>
        /// Số y lệnh,Trong cùng một số y lệnh chứa nhiềuyzId
        /// </summary>
        public string yzh { get; set; }

        /// <summary>
        /// Tốc độ truyền  giọt/Phút
        /// </summary>
        public int? ds { get; set; }
        /// <summary>
        /// Ấn tượng lâm sàng
        /// </summary>
        public string lcyx { get; set; }
        /// <summary>
        /// Ghi chú yêu cầu
        /// </summary>
        public string sqbz { get; set; }

        /// <summary>
        /// Cờ sắc hộ
        /// </summary>
        public int? djbzzzzzzz { get; set; }
        /// <summary>
        /// Cờ thuốc mang về khi xuất viện
        /// </summary>
        public int? cydybzzzzzzz { get; set; }
        /// <summary>
        /// Chẩn đoán y lệnhicd10
        /// </summary>
        public string yzzdid { get; set; }
        /// <summary>
        /// Tên chẩn đoán y lệnh
        /// </summary>
        public string yzzdmc { get; set; }
        /// <summary>
        /// Thời gian chẩn đoán y lệnh
        /// </summary>
        public string yzzdsj { get; set; }
        /// <summary>
        /// Có phải gói hạng mục hay không
        /// </summary>
        public string sfxmzt { get; set; }
        /// <summary>
        /// Số lượng gói
        /// </summary>
        public int? ztsl { get; set; }
        /// <summary>
        /// Gói tương ứng với cách dùng thuốc
        /// </summary>
        public string yfztbm { get; set; }
        public string yfztbs { get; set; }
        public string dcztbs { get; set; }
        /// <summary>
        /// Nguồn gốc thuốc 1:Tồn kho khoa 2Nhà thuốc
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
    /// Quản lý y lệnh nội trú,Khi chuẩn bị dữ liệu thuốc
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
    /// Danh sách tổng quan bệnh nhân nội trú Điều kiện lọc lớp nổi của bệnh nhân đã ra khỏi khu
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
    /// Kiểm tra y lệnh có trùng lặp hay không
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
