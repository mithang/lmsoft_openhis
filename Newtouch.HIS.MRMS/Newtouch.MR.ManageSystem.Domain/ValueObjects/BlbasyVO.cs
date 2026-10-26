using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
    public class BlbasyVO
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// Bệnh án điện tửID
        /// </summary>
        /// <returns></returns>
        public decimal? dzbl_id { get; set; }
        /// <summary>
        /// Đăng ký nhập việnID
        /// </summary>
        /// <returns></returns>
        public decimal? zydj_id { get; set; }
        /// <summary>
        /// Bảo hiểm y tếĐăng kýID
        /// </summary>
        /// <returns></returns>
        public decimal? ybdj_id { get; set; }
        /// <summary>
        /// yebh
        /// </summary>
        /// <returns></returns>
        public int? yebh { get; set; }
        /// <summary>
        /// Số thứ tự đăng ký
        /// </summary>
        /// <returns></returns>
        public int? djxh { get; set; }
        /// <summary>
        /// Số chứng từ bệnh án
        /// </summary>
        /// <returns></returns>
        public decimal pzhm_bl { get; set; }
        /// <summary>
        /// Mã số mẫu
        /// </summary>
        /// <returns></returns>
        public string mbbh { get; set; }
        /// <summary>
        /// Thư mục lưu trữ hệ thống bệnh án
        /// </summary>
        /// <returns></returns>
        public string blxtml { get; set; }
        /// <summary>
        /// Số hiệu tên hệ thống bệnh án_Bản gốc
        /// </summary>
        /// <returns></returns>
        public string blxtmc_yj { get; set; }
        /// <summary>
        /// Số hiệu tên hệ thống bệnh án_Dấu vết
        /// </summary>
        /// <returns></returns>
        public string blxtmc_hj { get; set; }
        /// <summary>
        /// Số hiệu tên hệ thống bệnh án_xml
        /// </summary>
        /// <returns></returns>
        public string blxtmc_xml { get; set; }
        /// <summary>
        /// ksmc
        /// </summary>
        /// <returns></returns>
        public string ksmc { get; set; }
        /// <summary>
        /// Mã khoa
        /// </summary>
        /// <returns></returns>
        public string ksdm { get; set; }
        /// <summary>
        /// zgysmc
        /// </summary>
        /// <returns></returns>
        public string zgysmc { get; set; }
        /// <summary>
        /// Bác sĩ quản lý chính
        /// </summary>
        /// <returns></returns>
        public string czydm_zgys { get; set; }
        /// <summary>
        /// zzysmc
        /// </summary>
        /// <returns></returns>
        public string zzysmc { get; set; }
        /// <summary>
        /// Bác sĩ điều trị(Nhậpngười)
        /// </summary>
        /// <returns></returns>
        public string czydm_zzys { get; set; }
        /// <summary>
        /// Ngày bệnh án
        /// </summary>
        /// <returns></returns>
        public DateTime? blrq { get; set; }
        /// <summary>
        /// Hệ thống bệnh ánNgày
        /// </summary>
        /// <returns></returns>
        public DateTime? blxtrq { get; set; }
        /// <summary>
        /// shrmc
        /// </summary>
        /// <returns></returns>
        public string shrmc { get; set; }
        /// <summary>
        /// Người kiểm duyệt
        /// </summary>
        /// <returns></returns>
        public string czydm_shr { get; set; }
        /// <summary>
        /// Ngày kiểm duyệt
        /// </summary>
        /// <returns></returns>
        public DateTime? shrq { get; set; }
        /// <summary>
        /// kzrmc
        /// </summary>
        /// <returns></returns>
        public string kzrmc { get; set; }
        /// <summary>
        /// Kiểm duyệt của trưởng khoa
        /// </summary>
        /// <returns></returns>
        public string czydm_kzr { get; set; }
        /// <summary>
        /// Ngày kiểm duyệt của trưởng khoa
        /// </summary>
        /// <returns></returns>
        public DateTime? shrq_kzr { get; set; }
        /// <summary>
        /// Ngày hủy bỏ
        /// </summary>
        /// <returns></returns>
        public DateTime? zfrq { get; set; }
        /// <summary>
        /// zfrmc
        /// </summary>
        /// <returns></returns>
        public string zfrmc { get; set; }
        /// <summary>
        /// Người hủy bỏ
        /// </summary>
        /// <returns></returns>
        public string czydm_zfr { get; set; }
        /// <summary>
        /// czrmc
        /// </summary>
        /// <returns></returns>
        public string czrmc { get; set; }
        /// <summary>
        /// Bản ghi hiện tạiThao tácngười(Chỉ người có đượcThao táccủa bản ghi nàyngười,mới được phép lưu、Sửa đổiThao tác,nếu không thì chỉ có quyền đọc)
        /// </summary>
        /// <returns></returns>
        public string czydm_czr { get; set; }
        /// <summary>
        /// sxysmc
        /// </summary>
        /// <returns></returns>
        public string sxysmc { get; set; }
        /// <summary>
        /// Bác sĩ thực tập(DoBác sĩ thực tập、bác sĩ tu nghiệp đều có thầy hướng dẫn)
        /// </summary>
        /// <returns></returns>
        public string czydm_sxys { get; set; }
        /// <summary>
        /// sxysxm
        /// </summary>
        /// <returns></returns>
        public string sxysxm { get; set; }
        /// <summary>
        /// Bác sĩ thực tập(Khi in thì dùng tên này)
        /// </summary>
        /// <returns></returns>
        public string czyxm_sxys { get; set; }
        /// <summary>
        /// blbxzt
        /// </summary>
        /// <returns></returns>
        public int? blbxzt { get; set; }
        /// <summary>
        /// blbczt
        /// </summary>
        /// <returns></returns>
        public bool? blbczt { get; set; }
        /// <summary>
        /// qmrq
        /// </summary>
        /// <returns></returns>
        public DateTime? qmrq { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        public Nullable<int> IsLock { get; set; }
    }
}
