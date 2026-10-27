using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects
{
    public class BlmbListVO
    {
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
        public string parentId { get; set; }
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
        /// Có tải lên bảo hiểm y tế hay không
        /// </summary>
        public string IsYB { get; set; }
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
        /// Loại lớn bệnh án trực thuộc
        /// </summary>
        public string bldl { get; set; }
        /// <summary>
        /// Tên loại lớn bệnh án trực thuộc
        /// </summary>
        public string bldlmc { get; set; }
        public string Ybbm { get; set; }
        public string mzbz { get; set; }
        /// <summary>
        /// Cách tải mẫu
        /// </summary>
        public int? LoadWay { get; set; }
        public string Turl { get; set; }
    }
}
