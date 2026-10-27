using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// Tạo bởi：chl
    /// Ngày：2019-11-06 10:22
    /// Mô tả：Đăng ký phẫu thuật
    /// </summary>
    [SugarTable("OR_Registration")]
    public class ORRegistrationEntity : IEntityBase
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
        /// ID xếp lịch
        /// </summary>
        /// <returns></returns>
        public string ArrangeId { get; set; }

        /// <summary>
        /// Mã đơn đăng ký
        /// </summary>
        /// <returns></returns>
        public string Applyno { get; set; }

        /// <summary>
        /// Số đăng ký phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string ssxh { get; set; }

        /// <summary>
        /// Số nhập viện
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }

        /// <summary>
        /// Trạng thái đơn đăng ký 
        /// </summary>
        /// <returns></returns>
        public string sqzt { get; set; }

        /// <summary>
        /// Chẩn đoán lúc nhập viện
        /// </summary>
        /// <returns></returns>
        public string ryzd { get; set; }

        /// <summary>
        /// Tên chẩn đoán lúc nhập viện
        /// </summary>
        /// <returns></returns>
        public string ryzdmc { get; set; }

        /// <summary>
        /// Chẩn đoán sau phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string sszd { get; set; }

        /// <summary>
        /// Tên chẩn đoán sau phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string sszdmc { get; set; }

        /// <summary>
        /// Tình trạng bệnh
        /// </summary>
        /// <returns></returns>
        public string shbq { get; set; }

        /// <summary>
        /// Tên phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string ssmc { get; set; }

        /// <summary>
        /// Mã phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string ssdm { get; set; }

        /// <summary>
        /// Thời gian xếp lịch phẫu thuật
        /// </summary>
        /// <returns></returns>
        public DateTime? ssapsj { get; set; }

        /// <summary>
        /// Thời gian đăng ký phẫu thuật
        /// </summary>
        /// <returns></returns>
        public DateTime? sssqsj { get; set; }

        /// <summary>
        /// Thời gian bắt đầu phẫu thuật
        /// </summary>
        /// <returns></returns>
        public DateTime? sskssj { get; set; }

        /// <summary>
        /// Thời gian kết thúc phẫu thuật
        /// </summary>
        /// <returns></returns>
        public DateTime? ssjssj { get; set; }

        ///// <summary>
        ///// Mã bác sĩ mổ chính
        ///// </summary>
        ///// <returns></returns>
        //public string ysgh { get; set; }

        ///// <summary>
        ///// Tên bác sĩ mổ chính
        ///// </summary>
        ///// <returns></returns>
        //public string ysxm { get; set; }

        /// <summary>
        /// Phương pháp gây mê
        /// </summary>
        /// <returns></returns>
        public string AnesCode { get; set; }

        /// <summary>
        /// Phòng phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string oproom { get; set; }

        /// <summary>
        /// Lượt mổ
        /// </summary>
        /// <returns></returns>
        public string oporder { get; set; }

        ///// <summary>
        ///// Bác sĩ phụ mổ
        ///// </summary>
        ///// <returns></returns>
        //public string zlys1 { get; set; }

        ///// <summary>
        ///// Tên bác sĩ phụ mổ
        ///// </summary>
        ///// <returns></returns>
        //public string zlysxm1 { get; set; }

        ///// <summary>
        ///// zlys2
        ///// </summary>
        ///// <returns></returns>
        //public string zlys2 { get; set; }

        ///// <summary>
        ///// zlysxm2
        ///// </summary>
        ///// <returns></returns>
        //public string zlysxm2 { get; set; }

        ///// <summary>
        ///// zlys3
        ///// </summary>
        ///// <returns></returns>
        //public string zlys3 { get; set; }

        ///// <summary>
        ///// zlysxm3
        ///// </summary>
        ///// <returns></returns>
        //public string zlysxm3 { get; set; }

        ///// <summary>
        ///// zlys4
        ///// </summary>
        ///// <returns></returns>
        //public string zlys4 { get; set; }

        ///// <summary>
        ///// zlysxm4
        ///// </summary>
        ///// <returns></returns>
        //public string zlysxm4 { get; set; }

        /// <summary>
        /// Cấp vết mổ
        /// </summary>
        /// <returns></returns>
        public string qkdj { get; set; }

        /// <summary>
        /// Có cách ly không 0 Không 1 Có
        /// </summary>
        /// <returns></returns>
        public string isgl { get; set; }

        /// <summary>
        /// Có nhiễm khuẩn không 0 Không 1 Có
        /// </summary>
        /// <returns></returns>
        public string isjun { get; set; }

        /// <summary>
        /// Lượng truyền máu
        /// </summary>
        /// <returns></returns>
        public decimal? shuxl { get; set; }

        /// <summary>
        /// Lượng mất máu
        /// </summary>
        /// <returns></returns>
        public decimal? shixl { get; set; }

        /// <summary>
        /// Tổng lượng vào
        /// </summary>
        /// <returns></returns>
        public decimal? zrxl { get; set; }

        /// <summary>
        /// Tổng lượng ra
        /// </summary>
        /// <returns></returns>
        public decimal? zcxl { get; set; }

        ///// <summary>
        ///// Y tá lưu động
        ///// </summary>
        ///// <returns></returns>
        //public string xhhs { get; set; }

        ///// <summary>
        ///// Tên y tá lưu động
        ///// </summary>
        ///// <returns></returns>
        //public string xhhsxm { get; set; }

        ///// <summary>
        ///// Y tá rửa tay
        ///// </summary>
        ///// <returns></returns>
        //public string xshs { get; set; }

        ///// <summary>
        ///// Tên y tá rửa tay
        ///// </summary>
        ///// <returns></returns>
        //public string xshsxm { get; set; }

        /// <summary>
        /// Vị trí phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string ssbw { get; set; }

        /// <summary>
        /// Trạng thái     1:Có hiệu lực  0：Không hiệu lực
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// memo
        /// </summary>
        /// <returns></returns>
        public string memo { get; set; }

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

    }
}