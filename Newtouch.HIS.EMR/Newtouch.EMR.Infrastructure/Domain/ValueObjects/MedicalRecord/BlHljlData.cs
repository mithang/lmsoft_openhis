using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MedicalRecord
{
    public class BlHljlData
    {
        public string Id { get; set; }
        public string blId { get; set; }
        /// <summary>
        /// Ngày ghi chép
        /// </summary>
        public string jlrq { get; set; }
        /// <summary>
        /// Nhiệt độ
        /// </summary>
        public string tw { get; set; }
        /// <summary>
        /// Mạch
        /// </summary>
        public string mb { get; set; }
        /// <summary>
        /// Hô hấp
        /// </summary>
        public string hx { get; set; }
        /// <summary>
        /// Huyết áp
        /// </summary>
        public string xy { get; set; }
        /// <summary>
        /// Độ bão hòa oxy 
        /// </summary>
        public string ybhd { get; set; }
        /// <summary>
        /// Theo dõi điện tim liên tục
        /// </summary>
        public string cxxdjc { get; set; }
        /// <summary>
        /// Thở oxy        L/phút
        /// </summary>
        public string xroyx { get; set; }
        /// <summary>
        /// Cấp độ điều dưỡng cơ bản
        /// </summary>
        public string hljb { get; set; }
        /// <summary>
        /// Hỗ trợ ăn uống
        /// </summary>
        public string xzjs { get; set; }
        /// <summary>
        /// Vỗ lưng và ho khạc hiệu quả
        /// </summary>
        public string pbjyxkt { get; set; }
        /// <summary>
        /// Phòng ngừa loét do tì đè
        /// </summary>
        public string ycyf { get; set; }
        /// <summary>
        /// Phòng ngừa ngã
        /// </summary>
        public string ddyf { get; set; }
        /// <summary>
        /// Điều dưỡng cơ bản khác
        /// </summary>
        public string qtjh { get; set; }
        /// <summary>
        /// Điều dưỡng chuyên khoa
        /// </summary>
        public string zkhl { get; set; }
        /// <summary>
        /// Loại ống thông/Biện pháp
        /// </summary>
        public string dglb { get; set; }
        /// <summary>
        /// Hướng dẫn điều dưỡng
        /// </summary>
        public string hlzd { get; set; }
        /// <summary>
        /// Lượng nước tiểu     ml
        /// </summary>
        public string nl { get; set; }
        /// <summary>
        /// Dịch dạ dày   ml
        /// </summary>
        public string wy { get; set; }
        /// <summary>
        /// Theo dõi tình trạng/Hiệu quả biện pháp chăm sóc
        /// </summary>
        public string bqhlcontent { get; set; }
        /// <summary>
        /// Chữ ký
        /// </summary>
        public string hsqm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
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
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }
        public string OrganizeId { get; set; }
    }
}
