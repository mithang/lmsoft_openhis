using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Bản ghi yêu cầu bệnh án
    /// </summary>
    [SugarTable("Mr_BlApplyRecord")]
    public class MrBlApplyRecordEntity 
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string xm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ks { get; set; }
        /// <summary>
        /// Mẫu bệnh án
        /// </summary>
        /// <returns></returns>
        public string bllx { get; set; }
        /// <summary>
        /// Tên bệnh án
        /// </summary>
        /// <returns></returns>
        public string blmc { get; set; }
        /// <summary>
        /// Thời gian yêu cầu
        /// </summary>
        /// <returns></returns>
        public DateTime? sqsj { get; set; }
        /// <summary>
        /// Thời gian hoàn thành
        /// </summary>
        /// <returns></returns>
        public DateTime? wcsj { get; set; }
        /// <summary>
        /// Thời gian hoàn thành kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public DateTime? zkwcsj { get; set; }

        /// <summary>
        /// Trạng thái phê duyệt 0:Chờ phê duyệt 1：Phê duyệt thông qua  2：Trả lại
        /// </summary>
        public int applyStatus { get; set; }
        /// <summary>
        /// Số trả về khi yêu cầu thành công
        /// </summary>
        public string applyReturnNo { get; set; }
        /// <summary>
        /// Bộ phận phê duyệt、Khoa
        /// </summary>
        public string ApproveDept { get; set; }
        /// <summary>
        /// Người phê duyệt
        /// </summary>
        public string ApprovePerson { get; set; }
        /// <summary>
        /// Ngày phê duyệt
        /// </summary>
        public DateTime? ApproveDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? Px { get; set; }
        /// <summary>
        /// brxm
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
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
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
    }
}
