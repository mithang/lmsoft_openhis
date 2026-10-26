using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///danh sách thuốc để lựa chọn khi kê đơn——
    ///quy cách：(liều lượng+đơn vị liều lượng)*(số lượng đóng gói+đơn vị đóng gói)/đơn vị định giá
    ///giá bán lẻ：giá bán lẻ/đơn vị định giá
    ///đơn vị：đơn vị lẻ
    ///số tiền：giá bán lẻ*số lượng/số lẻ（ở đây nhân số lượng trước rồi chia cho số lẻ tính số tiền，không phải chia trước cho số lẻ để tính đơn giá nhân tiếp với số lượng để tính số tiền，như vậy có thể giảm sai số。）
    ///giá bán buôn(pfj)：trong hệ thống，định nghĩa tên là“giá nhập”，hủy ý nghĩa giá bán buôn，vì trong quá trình phát triển hệ thống đều sử dụng từ giá bán buôn   ，do đó trường này vẫn dùng tiếp，nhưng thay đổi ý nghĩa của nó。
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_yp", "药品")]
    public partial class SysMedicineEntity:IEntity
    {

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ypId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ypCode不可为空")]
        [StringLength(20, ErrorMessage = "ypCode长度限制为20")]
        public string ypCode { get; set; }

        /// <summary>
        /// Desc:tên chung
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ypmc不可为空")]
        [StringLength(256, ErrorMessage = "ypmc长度限制为256")]
        public string ypmc { get; set; }

        /// <summary>
        /// Desc:ví dụ phía trước nhất：(甲) (乙10%)……
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "ypqz长度限制为10")]
        public string? ypqz { get; set; }

        /// <summary>
        /// Desc:ví dụ phía sau nhất：……
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "yphz长度限制为10")]
        public string? yphz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "spm长度限制为50")]
        public string? spm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "py不可为空")]
        [StringLength(70, ErrorMessage = "py长度限制为70")]
        public string py { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "cfl长度限制为9")]
        public decimal? cfl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "cfdw长度限制为20")]
        public string? cfdw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "jl长度限制为9")]
        public decimal? jl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "jldw长度限制为10")]
        public string? jldw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "bzs不可为空")]
        [StringLength(4, ErrorMessage = "bzs长度限制为4")]
        public decimal bzs { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "bzdw不可为空")]
        [StringLength(20, ErrorMessage = "bzdw长度限制为20")]
        public string bzdw { get; set; }

        /// <summary>
        /// Desc:chuẩn bị bãi bỏ
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "mzcls长度限制为9")]
        public decimal? mzcls { get; set; }

        /// <summary>
        /// Desc:chuẩn bị bãi bỏ，bằng“đơn vị lẻ”thay thế
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "mzcldw长度限制为20")]
        public string? mzcldw { get; set; }

        /// <summary>
        /// Desc:chuẩn bị bãi bỏ
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "zycls长度限制为9")]
        public decimal? zycls { get; set; }

        /// <summary>
        /// Desc:chuẩn bị bãi bỏ
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "zycldw长度限制为20")]
        public string? zycldw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zxdw不可为空")]
        [StringLength(20, ErrorMessage = "zxdw长度限制为20")]
        public string zxdw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "djdw不可为空")]
        [StringLength(20, ErrorMessage = "djdw长度限制为20")]
        public string djdw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "lsj不可为空")]
        [StringLength(11, ErrorMessage = "lsj长度限制为11")]
        public decimal lsj { get; set; }

        /// <summary>
        /// Desc:giá nhập mặc định
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(11, ErrorMessage = "pfj长度限制为11")]
        public decimal? pfj { get; set; }

        /// <summary>
        /// Desc:xem chi tiết bảng hạng mục hệ thống“tỷ lệ tự trả”mô tả
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfbl不可为空")]
        [StringLength(9, ErrorMessage = "zfbl长度限制为9")]
        public decimal zfbl { get; set; }

        /// <summary>
        /// Desc:1 tự lo 2 tự trả theo phân loại	   xem chi tiết bảng hạng mục hệ thống“tính chất tự trả”mô tả	   	   ngoại trú có thể dùng，cấp cứu không dùng được，hoặc ngược lại，điều khiển theo việc khoa dược khác nhau có thuốc hay không
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfxz不可为空")]
        [StringLength(1, ErrorMessage = "zfxz长度限制为1")]
        public string zfxz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "dlCode不可为空")]
        [StringLength(20, ErrorMessage = "dlCode长度限制为20")]
        public string dlCode { get; set; }

        /// <summary>
        /// Desc:xt_ypjx
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "jx长度限制为20")]
        public string? jx { get; set; }

        /// <summary>
        /// Desc:xt_yc  sửa đổi, lưu trực tiếp tên
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ycmc不可为空")]
        [StringLength(100, ErrorMessage = "ycmc长度限制为100")]
        public string ycmc { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? medid { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? medextid { get; set; }

        /// <summary>
        /// Desc:thuốc khi tiêu hao tại các nhà thuốc“cấp đóng gói”hoặc“thông tin số lẻ”
        /// Default:3
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "ypbzdm长度限制为50")]
        public string? ypbzdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "nbdl长度限制为20")]
        public string? nbdl { get; set; }

        /// <summary>
        /// Desc:----0：ngừng sử dụng  1：bật	   ---0：chung，1：khám ngoại trú，2：nội trú  （hủy）	   	   
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "mzzybz不可为空")]
        [StringLength(1, ErrorMessage = "mzzybz长度限制为1")]
        public string mzzybz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "lsbz长度限制为1")]
        public bool? lsbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? mjzbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "yfCode长度限制为20")]
        public string? yfCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "isKss不可为空")]
        [StringLength(1, ErrorMessage = "isKss长度限制为1")]
        public string isKss { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "kssId长度限制为50")]
        public string? kssId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:1
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "jybz不可为空")]
        [StringLength(20, ErrorMessage = "jybz长度限制为20")]
        public string jybz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "bz长度限制为500")]
        public string? bz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(11, ErrorMessage = "cxjje长度限制为11")]
        public decimal? cxjje { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "tsypbz长度限制为50")]
        public string? tsypbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "isYnss长度限制为1")]
        public string? isYnss { get; set; }

    }

}
