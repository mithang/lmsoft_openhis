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
        /// lĩnhthuốcthứ tựsố
        /// </summary>
        public int lyxh { get; set; }
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
        /// /Kiểu y lệnh
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
        /// Dừng/Hủy bỏ Thời gian
        /// </summary>
        public DateTime? tzsj { get; set; }
        /// <summary>
        /// Thời gian thực hiện
        /// </summary>
        public DateTime? zxsj { get; set; }
        /// <summary>
        /// Dừng/Hủy bỏ Bác sĩ Mã nhân viên
        /// </summary>
        public string tzysgh { get; set; }
        /// <summary>
        /// Dừng/Hủy bỏ Thao tácviên
        /// </summary>
        public string tzr { get; set; }

        public string Creator { get; set; }
        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string hzxm { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Phê duyệt người
        /// </summary>
        public string shr { get; set; }
        /// <summary>
        /// Mục thu phí Đơn giá
        /// </summary>
        public decimal? dj { get; set; }
        /// <summary>
        /// Số nhóm
        /// </summary>
         public int? zh { get; set; }
        /// <summary>
        /// Thực hiện người
        /// </summary>
        public string zxr { get; set; }
        /// <summary>
        /// là Khôngtínhphí
        /// </summary>
        public int? isjf { get; set; }
        public string yztag { get; set; }
        public string yztagName { get; set; }
        public string zh1 { get; set; }
        public string yfztbs { get; set; }
        /// <summary>
        /// Thuốcmởlập Tồn khođếnnguồn 1:Bác sĩtrạm Khoachuẩn bị thuốc Tồn kho 2:Nhà thuốc Tồn kho
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
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Y lệnhid
        /// </summary>
        public string yzid { get; set; }
        /// <summary>
        /// Y lệnh Tính chất
        /// </summary>
        public int yzxz { get; set; }
        /// <summary>
        /// Kiểu y lệnh,Thuốc Or Mục
        /// </summary>
        public int yzlx { get; set; }
        /// <summary>
        /// lĩnhthuốcthứ tựsố
        /// </summary>
        public int lyxh { get; set; }
        /// <summary>
        /// Mục thu phí Đơn giá
        /// </summary>
        public Decimal dj { get; set; }
        /// <summary>
        /// Thuốc/Tên mục
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// Table Thời gian thực hiện
        /// </summary>
         public DateTime? zxsj { get; set; }
        /// <summary>
        /// Thực hiện người
        /// </summary>
        public string zxr { get; set; }
        /// <summary>
        /// Số nhóm
        /// </summary>
        public int ? zh { get; set; }
        /// <summary>
        /// là Khôngtínhphí 1:là 0:Không
        /// </summary>
        public string isjf { get; set; }
        public string zh1 { get; set; }
        public string yfztbs { get; set; }
        /// <summary>
        /// Thuốcđếnnguồn 1:Bác sĩtrạm Khoachuẩn bị thuốc Tồn kho  2:Nhà thuốc Tồn kho
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
