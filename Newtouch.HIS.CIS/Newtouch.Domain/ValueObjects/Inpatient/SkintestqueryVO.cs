using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class SkintestqueryVO
    {
        /// <summary>
        /// Y lệnhid
        /// </summary>
        public string yzid { get; set; }
        /// <summary>
        /// Tính chất y lệnh
        /// </summary>
        public int? yzxz { get; set; }
        /// <summary>
        /// Giải thích tính chất y lệnh
        /// </summary>
        public string yzxzsm { get; set; }
        /// <summary>
        /// Thời gian bắt đầu
        /// </summary>
        public DateTime? kssj { get; set; }
        /// <summary>
        /// Loại hạng mục
        /// </summary>
        public string xmmc { get; set; }
        public string xmdm { get; set; }
        /// <summary>
        /// Liều lượng thuốc
        /// </summary>
        public Decimal? ypjl { get; set; }
        /// <summary>
        /// Nội dung y lệnh
        /// </summary>
        public string yznr { get; set; }
        /// <summary>
        /// Dừng/Thời gian hủy bỏ
        /// </summary>
        public DateTime? tzsj { get; set; }
        /// <summary>
        /// Dừng/Mã nhân viên bác sĩ hủy bỏ
        /// </summary>
        public string tzysgh { get; set; }
        /// <summary>
        /// Dừng/Nhân viên hủy bỏ
        /// </summary>
        public string tzr { get; set; }

        public string Creator { get; set; }
        /// <summary>
        /// Số nhóm y lệnh
        /// </summary>
        public int? zh { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 0 Chờ duyệt 1Đã duyệt 2Đã thực hiện 3DC 4Dừng
        /// </summary>
        public int yzzt { get; set; }
        /// <summary>
        /// Loại y lệnh
        /// </summary>
        //public int? yzlx { get; set; }
        public string lrjg { get; set; }

        public string zyh { get; set; }
        public string blh { get; set; }
        public string hzxm { get; set; }
        //public string yzlxmc { get; set; }
        public string yztag { get; set; }
        public string yztagName { get; set; }
        public int? isjf { get; set; }
        public string zh1 { get; set; }
        public string ispscs { get; set; }
        public string sex { get; set; }
        public string psbz { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastModifierName { get; set; }
        public string CreatorName { get; set; }
        public DateTime? LastModifyTime { get; set; }
    }
}
