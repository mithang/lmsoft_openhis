using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.BaseData
{
    [Table("Gaxnh_S30")]
    public class BaseGaXnhS30Entity : IEntity<BaseGaXnhS30Entity>
    {
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tên tiếng Trung của bệnh
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Mã bệnh
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// Loại hạng mục
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// Quy cách
        /// </summary>
        public string specification { get; set; }
        /// <summary>
        /// Dạng bào chế
        /// </summary>
        public string dosageForm { get; set; }
        /// <summary>
        /// Đơn vị
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// Cấp sử dụng
        /// </summary>
        public string rank { get; set; }
        /// <summary>
        /// Tỷ lệ bồi thường
        /// </summary>
        public string scale { get; set; }
        /// <summary>
        /// Phạm vi BHYT1Trong bảo hiểm 0Ngoài bảo hiểm
        /// </summary>
        public string scope { get; set; }
        /// <summary>
        /// Có phải là thuốc cơ bản hay không
        /// </summary>
        public string isBase { get; set; }
        /// <summary>
        /// Mã pinyin
        /// </summary>
        public string pcode { get; set; }
        /// <summary>
        /// Giá(Tỉnh)
        /// </summary>
        public string price5 { get; set; }
        /// <summary>
        /// Giá(Thành phố)
        /// </summary>
        public string price4 { get; set; }
        /// <summary>
        /// Giá(Huyện)
        /// </summary>
        public string price3 { get; set; }
        /// <summary>
        /// Giá(Xã)
        /// </summary>
        public string price2 { get; set; }
        /// <summary>
        /// Giá(Thôn)
        /// </summary>
        public string price1 { get; set; }
        /// <summary>
        /// Giá giới hạn(Tỉnh)
        /// </summary>
        public string limitPrice5 { get; set; }
        /// <summary>
        /// Giá giới hạn(Thành phố)
        /// </summary>
        public string limitPrice4 { get; set; }
        /// <summary>
        /// Giá giới hạn(Huyện)
        /// </summary>
        public string limitPrice3 { get; set; }
        /// <summary>
        /// Giá giới hạn(Xã)
        /// </summary>
        public string limitPrice2 { get; set; }
        /// <summary>
        /// Giá giới hạn(Thôn)
        /// </summary>
        public string limitPrice1 { get; set; }
        /// <summary>
        /// Cấp danh mục
        /// </summary>
        public string grade { get; set; }
        /// <summary>
        /// Trạng thái0Chưa kiểm tra1Đã kiểm tra2Chưa thông qua
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Dấu thời gian
        /// </summary>
        public string datetime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

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
        public string zt { get; set; }
    }
}
