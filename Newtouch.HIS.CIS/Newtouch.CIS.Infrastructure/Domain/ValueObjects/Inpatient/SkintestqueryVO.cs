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
        /// Y lệnhTính chất
        /// </summary>
        public int? yzxz { get; set; }
        /// <summary>
        /// Y lệnhTính chấtMô tả
        /// </summary>
        public string yzxzsm { get; set; }
        /// <summary>
        /// Thời gian bắt đầu
        /// </summary>
        public DateTime? kssj { get; set; }
        /// <summary>
        /// MụcLoại
        /// </summary>
        public string xmmc { get; set; }
        public string xmdm { get; set; }
        /// <summary>
        /// ThuốcLiều lượng
        /// </summary>
        public Decimal? ypjl { get; set; }
        /// <summary>
        /// Nội dung y lệnh
        /// </summary>
        public string yznr { get; set; }
        /// <summary>
        /// Dừng/Hủy bỏThời gian
        /// </summary>
        public DateTime? tzsj { get; set; }
        /// <summary>
        /// Dừng/Hủy bỏBác sĩMã nhân viên
        /// </summary>
        public string tzysgh { get; set; }
        /// <summary>
        /// Dừng/Hủy bỏThao tácviên
        /// </summary>
        public string tzr { get; set; }

        public string Creator { get; set; }
        /// <summary>
        /// Y lệnhSố nhóm
        /// </summary>
        public int? zh { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 0 chờduyệt 1Đã duyệt 2đãThực hiện 3DC 4Dừng
        /// </summary>
        public int yzzt { get; set; }
        /// <summary>
        /// Kiểu y lệnh
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
