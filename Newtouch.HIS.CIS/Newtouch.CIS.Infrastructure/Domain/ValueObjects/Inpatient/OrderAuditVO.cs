using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class OrderAuditVO
    {
        /// <summary>
        /// Y lệnh id
        /// </summary>
        public string yzid { get; set; }
        /// <summary>
        /// Y lệnh Tính chất
        /// </summary>
        public int? yzxz { get; set; }
        /// <summary>
        /// Y lệnh Tính chất Mô tả
        /// </summary>
        public string yzxzsm { get; set; }
        /// <summary>
        /// Thời gian bắt đầu
        /// </summary>
        public DateTime? kssj { get; set; }
        /// <summary>
        /// Mục Loại
        /// </summary>
        public string xmmc { get; set; }
        /// <summary>
        /// Thuốc Liều lượng
        /// </summary>
        public Decimal? ypjl { get; set; }
        /// <summary>
        /// Nội dung y lệnh
        /// </summary>
        public string yzmc { get; set; }
        public string yzjl { get; set; }
        public string yfmc { get; set; }
        public string yzpcmc { get; set; }
        /// <summary>
        /// Dừng/Hủy bỏ Thời gian
        /// </summary>
        public DateTime? tzsj { get; set; }
        /// <summary>
        /// Dừng/Hủy bỏ Bác sĩ Mã nhân viên
        /// </summary>
        public string tzysgh { get; set; }
        /// <summary>
        /// Dừng/Hủy bỏ Thao tác viên
        /// </summary>
        public string tzr { get; set; }

        public string Creator { get; set; }
        /// <summary>
        /// Y lệnh Số nhóm
        /// </summary>
        public int? zh { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 0 chờ duyệt 1Đã duyệt 2đã Thực hiện 3DC 4Dừng
        /// </summary>
        public int yzzt { get; set; }
        /// <summary>
        /// Kiểu y lệnh
        /// </summary>
        public int? yzlx { get; set; }
        public string zyh { get; set; }
        public string hzxm { get; set; }
        public string yzlxmc { get; set; }
        public string yztag { get; set; }
        public string yztagName { get; set; }
        public int? isjf { get; set; }
        public string zh1 { get; set; }
        public string ispscs { get; set; }
        public string yzh { get; set; }
        public string psjg { get; set; }
        public string psjgms { get; set; }
        public string iszt { get; set; }
        public string yfztbs { get; set; }
        /// <summary>
        /// Thuốc mở lập Tồn kho đến nguồn 1:Khoa chuẩn bị thuốc Tồn kho 2:Nhà thuốc Tồn kho
        /// </summary>
        public int? yply { get; set; }
        public decimal? dj { get; set; }
        public decimal? je { get; set; }
        public int? sl { get; set; }
        public int? Px { get; set; }
        public string slstr { get; set; }
    }

    public class yzinfoVo
    {
        public string Id { get; set; }
    }
}
