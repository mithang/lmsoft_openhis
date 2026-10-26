using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class OrderExecutionVO
     {
        /// <summary>
        /// Y lệnhid
        /// </summary>
        public string yzid { get; set; }
        /// <summary>
        /// Số thứ tự lĩnh thuốc
        /// </summary>
        public int lyxh { get; set; }
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
        /// <summary>
        /// Liều lượng thuốc
        /// </summary>
        public Decimal? ypjl { get; set; }
        /// <summary>
        /// /Loại y lệnh
        /// </summary>
        public int yzlx { get; set; }
        /// <summary>
        /// Nội dung y lệnh
        /// </summary>
        public string yzmc { get; set; }
        public string yzjl { get; set; }
        public string yfmc { get; set; }
        public string yzpcmc { get; set; }
        /// <summary>
        /// Dừng/Thời gian hủy bỏ
        /// </summary>
        public DateTime? tzsj { get; set; }
        /// <summary>
        /// Thời gian thực hiện
        /// </summary>
        public DateTime? zxsj { get; set; }
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
        /// Họ tên bệnh nhân
        /// </summary>
        public string hzxm { get; set; }
        /// <summary>
        /// Số nội trú
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Người duyệt
        /// </summary>
        public string shr { get; set; }
        /// <summary>
        /// Đơn giá hạng mục thu phí
        /// </summary>
        public decimal? dj { get; set; }
        /// <summary>
        /// Số nhóm
        /// </summary>
         public int? zh { get; set; }
        /// <summary>
        /// Người thực hiện
        /// </summary>
        public string zxr { get; set; }
        /// <summary>
        /// Có tính phí hay không
        /// </summary>
        public int? isjf { get; set; }
        public string yztag { get; set; }
        public string yztagName { get; set; }
        public string zh1 { get; set; }
        public string yfztbs { get; set; }
        /// <summary>
        /// Nguồn tồn kho kê thuốc 1:Tồn kho thuốc dự trữ khoa tại trạm bác sĩ 2:Tồn kho nhà thuốc
        /// </summary>
        public int? yply { get; set; }
        public decimal? je { get; set; }
        public int? sl { get; set; }
        public string yzh { get; set; }
        public string isfsyz { get; set; }
        public string slstr { get; set; }
        public string deptCode { get; set; }
        public string xmdm { get; set; }
    }

    public class ApiResponseVO
    {
        
        /// <summary>
        /// Số nội trú
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Y lệnhid
        /// </summary>
        public string yzid { get; set; }
        /// <summary>
        /// Tính chất y lệnh
        /// </summary>
        public int yzxz { get; set; }
        /// <summary>
        /// Loại y lệnh,ThuốcOrHạng mục
        /// </summary>
        public int yzlx { get; set; }
        /// <summary>
        /// Số thứ tự lĩnh thuốc
        /// </summary>
        public int lyxh { get; set; }
        /// <summary>
        /// Đơn giá hạng mục thu phí
        /// </summary>
        public Decimal dj { get; set; }
        /// <summary>
        /// Thuốc/Tên hạng mục
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// TableThời gian thực hiện
        /// </summary>
         public DateTime? zxsj { get; set; }
        /// <summary>
        /// Người thực hiện
        /// </summary>
        public string zxr { get; set; }
        /// <summary>
        /// Số nhóm
        /// </summary>
        public int ? zh { get; set; }
        /// <summary>
        /// Có tính phí hay không 1:Có 0:Không
        /// </summary>
        public string isjf { get; set; }
        public string zh1 { get; set; }
        public string yfztbs { get; set; }
        /// <summary>
        /// Nguồn gốc thuốc 1:Tồn kho thuốc dự trữ khoa tại trạm bác sĩ  2:Tồn kho nhà thuốc
        /// </summary>
        public string yply { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        public string deptCode { get; set; }
        public string xmdm { get; set; }
        public int sl { get; set; }
    }

    public class HospItemFeeVO {
        public string ks { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public int kykc { get; set; }
    }
}
