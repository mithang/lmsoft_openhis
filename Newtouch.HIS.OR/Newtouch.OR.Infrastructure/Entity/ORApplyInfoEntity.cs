using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// Tạo bởi:chl
    /// Ngày:2019-10-31 10:04
    /// Mô tả:Hồ sơ đơn đăng ký phẫu thuật
    /// </summary>
    [SugarTable("OR_ApplyInfo")]
    public class ORApplyInfoEntity : IEntityBase
    {
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Mã đơn đăng ký
        /// </summary>
        /// <returns></returns>
        public string Applyno { get; set; }

        /// <summary>
        /// Số nhập viện
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        /// <returns></returns>
        public string xm { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        /// <returns></returns>
        public string xb { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        /// <returns></returns>
        public string csrq { get; set; }

        /// <summary>
        /// Số CMND
        /// </summary>
        /// <returns></returns>
        public string sfz { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        /// <returns></returns>
        public string ks { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        /// <returns></returns>
        public string bq { get; set; }

        /// <summary>
        /// Số giường
        /// </summary>
        /// <returns></returns>
        public string ch { get; set; }

        /// <summary>
        /// Ngày nhập viện
        /// </summary>
        /// <returns></returns>
        public DateTime? ryrq { get; set; }

        /// <summary>
        /// Chẩn đoán
        /// </summary>
        /// <returns></returns>
        public string zd { get; set; }

        /// <summary>
        /// Trạng thái đơn đăng ký
        /// </summary>
        /// <returns></returns>
        public string sqzt { get; set; }

        /// <summary>
        /// Tên phẫu thuật dự kiến
        /// </summary>
        /// <returns></returns>
        public string ssmcn { get; set; }

        /// <summary>
        /// Mã phẫu thuật đăng ký
        /// </summary>
        /// <returns></returns>
        public string ssdm { get; set; }

        /// <summary>
        /// Thời gian phẫu thuật
        /// </summary>
        /// <returns></returns>
        public DateTime? sssj { get; set; }

        /// <summary>
        /// Mã bác sĩ mổ chính
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }

        /// <summary>
        /// Tên bác sĩ mổ chính
        /// </summary>
        /// <returns></returns>
        public string ysxm { get; set; }
		/// <summary>
		/// Bác sĩ gây mê
		/// </summary>
		public string mzys { get; set; }
        /// <summary>
        /// Phương pháp gây mê
        /// </summary>
        /// <returns></returns>
        public string AnesCode { get; set; }
        /// <summary>
        /// Vị trí phẫu thuật
        /// </summary>
        public string ssbw { get; set; }
        /// <summary>
        /// Có cách ly không
        /// </summary>
        public string isgl { get; set; }
        /// <summary>
        /// Trạng thái     1:Có hiệu lực  0:Không hiệu lực
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }   
        
        /// <summary>
        /// Tuổi
        /// </summary>
        /// <returns></returns>
        //public string nl { get; set; }
    }
}