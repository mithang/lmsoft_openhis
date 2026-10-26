using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Đồng bộ hồ sơ điều trị
    /// </summary>
    [Table("TB_Sync_TreatmentServiceRecord")]
    public class SyncTreatmentServiceRecordEntity : IEntity<SyncTreatmentServiceRecordEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string outerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string serviceCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string serviceDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string collectionMethod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? units { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? durationPerUnit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? minutes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string siteId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string siteName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string admsNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string therapistId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string therapistName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string disciplineTrack { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        /// Chính là nhật ký điều trị(Ngày dịch vụ),Sẽ không thay đổi
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Thời gian đồng bộ
        /// </summary>
        public DateTime DownloadTime { get; set; }

        /// <summary>
        /// 1 Chưa xác nhận 2 Đã xác nhận 3 Hủy
        /// </summary>
        public int? clzt { get; set; }

        /// <summary>
        /// Có tự động xác nhận hay không
        /// </summary>
        public bool? zdqrbz { get; set; }

        /// <summary>
        /// Cờ ghi vấn đề.Ví dụserviceCodeTrong dữ liệu cơ sở của chúng ta không có
        /// </summary>
        public bool? wtjlbz { get; set; }

        /// <summary>
        /// Ghi chú.Lý do dữ liệu đồng bộ bị gác lại,Lý do không thể xác nhận
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Người xử lý(UserCode)
        /// </summary>
        public string clr { get; set; }

        /// <summary>
        /// Thời gian xử lý
        /// </summary>
        public DateTime? clsj { get; set; }

        /// <summary>
        /// Cờ xử lý lại
        /// </summary>
        public bool? cxclbz { get; set; }

        /// <summary>
        /// Người xử lý lần cuối(UserCode)
        /// </summary>
        public string zhclr { get; set; }

        /// <summary>
        /// Thời gian xử lý lần cuối
        /// </summary>
        public DateTime? zhclsj { get; set; }

        /// <summary>
        /// Bảng tính phíId
        /// </summary>
        public int? jfbId { get; set; }

        public string zt { get; set; }

        public DateTime? serviceDate { get; set; }

    }
}
