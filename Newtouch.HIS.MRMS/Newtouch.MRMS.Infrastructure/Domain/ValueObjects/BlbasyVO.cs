using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;

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
        /// Đăng ký BHYTID
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
        /// Số hiệu mẫu
        /// </summary>
        /// <returns></returns>
        public string mbbh { get; set; }
        /// <summary>
        /// Thư mục lưu trữ hệ thống bệnh án
        /// </summary>
        /// <returns></returns>
        public string blxtml { get; set; }
        /// <summary>
        /// Mã tên hệ thống bệnh án_Bản gốc
        /// </summary>
        /// <returns></returns>
        public string blxtmc_yj { get; set; }
        /// <summary>
        /// Mã tên hệ thống bệnh án_Dấu vết
        /// </summary>
        /// <returns></returns>
        public string blxtmc_hj { get; set; }
        /// <summary>
        /// Mã tên hệ thống bệnh án_xml
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
        /// Bác sĩ điều trị chính(Người nhập)
        /// </summary>
        /// <returns></returns>
        public string czydm_zzys { get; set; }
        /// <summary>
        /// Bệnh ánNgày
        /// </summary>
        /// <returns></returns>
        public DateTime? blrq { get; set; }
        /// <summary>
        /// Ngày hệ thống bệnh án
        /// </summary>
        /// <returns></returns>
        public DateTime? blxtrq { get; set; }
        /// <summary>
        /// shrmc
        /// </summary>
        /// <returns></returns>
        public string shrmc { get; set; }
        /// <summary>
        /// Người duyệt
        /// </summary>
        /// <returns></returns>
        public string czydm_shr { get; set; }
        /// <summary>
        /// Ngày duyệt
        /// </summary>
        /// <returns></returns>
        public DateTime? shrq { get; set; }
        /// <summary>
        /// kzrmc
        /// </summary>
        /// <returns></returns>
        public string kzrmc { get; set; }
        /// <summary>
        /// Trưởng khoa duyệt
        /// </summary>
        /// <returns></returns>
        public string czydm_kzr { get; set; }
        /// <summary>
        /// Ngày trưởng khoa duyệt
        /// </summary>
        /// <returns></returns>
        public DateTime? shrq_kzr { get; set; }
        /// <summary>
        /// Ngày hủy
        /// </summary>
        /// <returns></returns>
        public DateTime? zfrq { get; set; }
        /// <summary>
        /// zfrmc
        /// </summary>
        /// <returns></returns>
        public string zfrmc { get; set; }
        /// <summary>
        /// Người hủy
        /// </summary>
        /// <returns></returns>
        public string czydm_zfr { get; set; }
        /// <summary>
        /// czrmc
        /// </summary>
        /// <returns></returns>
        public string czrmc { get; set; }
        /// <summary>
        /// Người thao tác bản ghi hiện tại(chỉ người được phép thao tác bản ghi này mới được lưu và sửa, ngược lại chỉ có quyền đọc)
        /// </summary>
        /// <returns></returns>
        public string czydm_czr { get; set; }
        /// <summary>
        /// sxysmc
        /// </summary>
        /// <returns></returns>
        public string sxysmc { get; set; }
        /// <summary>
        /// Bác sĩ thực tập(vì bác sĩ thực tập và bác sĩ tu nghiệp đều có giáo viên hướng dẫn)
        /// </summary>
        /// <returns></returns>
        public string czydm_sxys { get; set; }
        /// <summary>
        /// sxysxm
        /// </summary>
        /// <returns></returns>
        public string sxysxm { get; set; }
        /// <summary>
        /// Bác sĩ thực tập(Khi in dùng tên này)
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
