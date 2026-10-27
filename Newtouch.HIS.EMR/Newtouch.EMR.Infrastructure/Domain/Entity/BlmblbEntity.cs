using System;
using SqlSugar;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2018-09-05 11:34
    /// Mô tả：Danh sách mẫu bệnh án
    /// </summary>
    [SugarTable("bl_mblb")]
    public class BlmblbEntity 
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        /// <summary>
        /// Tổ chức ID
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 0 Chung 1 Cá nhân 2 Khoa
        /// </summary>
        /// <returns></returns>
        public int? mbqx { get; set; }
        /// <summary>
        /// Mã mẫu
        /// </summary>
        public string mbbm { get; set; }
        /// <summary>
        /// Tên mẫu
        /// </summary>
        /// <returns></returns>
        public string mbmc { get; set; }
        /// <summary>
        /// Loại bệnh án ID
        /// </summary>
        /// <returns></returns>
        public string bllxId { get; set; }
        /// <summary>
        /// Loại bệnh án
        /// </summary>
        /// <returns></returns>
        public string bllxmc { get; set; }
        /// <summary>
        /// Mã khoa
        /// </summary>
        /// <returns></returns>
        public string ksbm { get; set; }
        /// <summary>
        /// Mã bác sĩ
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }
        /// <summary>
        /// Đường dẫn mẫu
        /// </summary>
        /// <returns></returns>
        public string mblj { get; set; }
        /// <summary>
        /// Mã Pinyin
        /// </summary>
        /// <returns></returns>
        public string py { get; set; }
        /// <summary>
        /// Có phải mẫu trống hay không0 Không 1 Có
        /// </summary>
        /// <returns></returns>
        public int? Isempty { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        /// <returns></returns>
        public string Memo { get; set; }
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

        public string bllx { get; set; }
        /// <summary>
        /// Có tải lên bảo hiểm y tế hay không
        /// </summary>
        public string IsYB { get; set; }
        /// <summary>
        /// Phương pháp liên kết bảo hiểm y tế
        /// </summary>
        public string Ybbm { get; set; }
        /// <summary>
        /// Dấu hiệu mẫu ngoại trú 1 Ngoại trú 
        /// </summary>
        public string mzbz { get; set; }
        /// <summary>
        /// Có bật tải nguồn dữ liệu hay không
        /// </summary>
        public string EnableDataLoad { get; set; }
        /// <summary>
        /// Gắn nguồn dữ liệu
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// Cách tải mẫu
        /// </summary>
        public int? LoadWay { get; set; }
    }



}
