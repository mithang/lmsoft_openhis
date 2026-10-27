using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// quamẫnthông tin
    /// </summary>
    [SugarTable("xt_gmxx")]
    public class AllergyEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chứcid
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// quamẫnMụcCode
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// quamẫnTên mục
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// kếtnếu
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierName { get; set; }

        /// <summary>
        /// Dấu hiệu ngoại trú/nội trú
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// Y lệnhid
        /// </summary>
        public string yzid { get; set; }

        /// <summary>
        /// Đơn thuốcchi tiếtid
        /// </summary>
        public string cfmxid { get; set; }

        /// <summary>
        /// Thực hiệnTrạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// quamẫnLoại
        /// </summary>
        public string gmlx { get; set; }

        /// <summary>
        /// quamẫnThuốcCode
        /// </summary>
        public string ypCode { get; set; }
    }
}
