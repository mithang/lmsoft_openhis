using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///Danh sách thuốc để chọn khi kê đơn——
    ///Quy cách:(Liều lượng+Đơn vị liều lượng)*(Số lượng đóng gói+Đơn vị đóng gói)/Đơn vị định giá
    ///Giá bán lẻ:Giá bán lẻ/Đơn vị định giá
    ///Đơn vị:Đơn vị lẻ
    ///Số tiền:Giá bán lẻ*Số lượng/Số lượng lẻ(ở đây nhân số lượng trước rồi chia cho số lượng lẻ tính số tiền,chứ không phải chia số lượng lẻ trước để tính đơn giá rồi nhân số lượng để tính số tiền,như vậy có thể giảm sai số.)
    ///Giá bán sỉ(pfj):Trong hệ thống,định nghĩa tên là"Giá nhập",hủy ý nghĩa giá bán sỉ,vì trong phát triển hệ thống đều dùng chữ giá bán sỉ   ,nên trường này vẫn dùng tiếp,nhưng đổi ý nghĩa của nó.
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_yp", "Thuốc")]
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
        [Required(ErrorMessage = "ypCode không được để trống")]
        [StringLength(20, ErrorMessage = "ypCode giới hạn độ dài là 20")]
        public string ypCode { get; set; }

        /// <summary>
        /// Desc:Tên chung
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ypmc không được để trống")]
        [StringLength(256, ErrorMessage = "ypmc giới hạn độ dài là 256")]
        public string ypmc { get; set; }

        /// <summary>
        /// Desc:Ví dụ trước đây nhất:(Hạng A) (Hạng B10%)……
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "ypqz giới hạn độ dài là 10")]
        public string? ypqz { get; set; }

        /// <summary>
        /// Desc:Ví dụ sau đây nhất:……
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "yphz giới hạn độ dài là 10")]
        public string? yphz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "spm giới hạn độ dài là 50")]
        public string? spm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "py không được để trống")]
        [StringLength(70, ErrorMessage = "py giới hạn độ dài là 70")]
        public string py { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "cfl giới hạn độ dài là 9")]
        public decimal? cfl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "cfdw giới hạn độ dài là 20")]
        public string? cfdw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "jl giới hạn độ dài là 9")]
        public decimal? jl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "jldw giới hạn độ dài là 10")]
        public string? jldw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "bzs không được để trống")]
        [StringLength(4, ErrorMessage = "bzs giới hạn độ dài là 4")]
        public decimal bzs { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "bzdw không được để trống")]
        [StringLength(20, ErrorMessage = "bzdw giới hạn độ dài là 20")]
        public string bzdw { get; set; }

        /// <summary>
        /// Desc:Chuẩn bị bãi bỏ
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "mzcls giới hạn độ dài là 9")]
        public decimal? mzcls { get; set; }

        /// <summary>
        /// Desc:Chuẩn bị bãi bỏ,bằng"Đơn vị lẻ"thay thế
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "mzcldw giới hạn độ dài là 20")]
        public string? mzcldw { get; set; }

        /// <summary>
        /// Desc:Chuẩn bị bãi bỏ
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "zycls giới hạn độ dài là 9")]
        public decimal? zycls { get; set; }

        /// <summary>
        /// Desc:Chuẩn bị bãi bỏ
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "zycldw giới hạn độ dài là 20")]
        public string? zycldw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zxdw không được để trống")]
        [StringLength(20, ErrorMessage = "zxdw giới hạn độ dài là 20")]
        public string zxdw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "djdw không được để trống")]
        [StringLength(20, ErrorMessage = "djdw giới hạn độ dài là 20")]
        public string djdw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "lsj không được để trống")]
        [StringLength(11, ErrorMessage = "lsj giới hạn độ dài là 11")]
        public decimal lsj { get; set; }

        /// <summary>
        /// Desc:Giá nhập mặc định
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(11, ErrorMessage = "pfj giới hạn độ dài là 11")]
        public decimal? pfj { get; set; }

        /// <summary>
        /// Desc:Xem chi tiết bảng hạng mục hệ thống"Tỷ lệ tự trả"Giải thích
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfbl không được để trống")]
        [StringLength(9, ErrorMessage = "zfbl giới hạn độ dài là 9")]
        public decimal zfbl { get; set; }

        /// <summary>
        /// Desc:1 Tự lo 2 Tự trả theo phân loại	   Xem chi tiết bảng hạng mục hệ thống"Tính chất tự trả"Giải thích	   	   Ngoại trú dùng được,Cấp cứu không dùng được,hoặc ngược lại,khống chế bằng việc khoa dược khác nhau có thuốc hay không
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfxz không được để trống")]
        [StringLength(1, ErrorMessage = "zfxz giới hạn độ dài là 1")]
        public string zfxz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "dlCode không được để trống")]
        [StringLength(20, ErrorMessage = "dlCode giới hạn độ dài là 20")]
        public string dlCode { get; set; }

        /// <summary>
        /// Desc:xt_ypjx
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "jx giới hạn độ dài là 20")]
        public string? jx { get; set; }

        /// <summary>
        /// Desc:xt_yc  Sửa đổi rồi lưu trực tiếp tên
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ycmc không được để trống")]
        [StringLength(100, ErrorMessage = "ycmc giới hạn độ dài là 100")]
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
        /// Desc:Khi thuốc tiêu hao tại các nhà thuốc"Cấp đóng gói"hoặc"Thông tin số lượng lẻ"
        /// Default:3
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "ypbzdm giới hạn độ dài là 50")]
        public string? ypbzdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "nbdl giới hạn độ dài là 20")]
        public string? nbdl { get; set; }

        /// <summary>
        /// Desc:----0:Ngừng dùng  1:Bật	   ---0:Chung,1:Ngoại trú,2:Nội trú  (Hủy)	   	   
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "mzzybz không được để trống")]
        [StringLength(1, ErrorMessage = "mzzybz giới hạn độ dài là 1")]
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
        [StringLength(1, ErrorMessage = "lsbz giới hạn độ dài là 1")]
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
        [StringLength(20, ErrorMessage = "yfCode giới hạn độ dài là 20")]
        public string? yfCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "isKss không được để trống")]
        [StringLength(1, ErrorMessage = "isKss giới hạn độ dài là 1")]
        public string isKss { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "kssId giới hạn độ dài là 50")]
        public string? kssId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:1
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "jybz không được để trống")]
        [StringLength(20, ErrorMessage = "jybz giới hạn độ dài là 20")]
        public string jybz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "bz giới hạn độ dài là 500")]
        public string? bz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(11, ErrorMessage = "cxjje giới hạn độ dài là 11")]
        public decimal? cxjje { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "tsypbz giới hạn độ dài là 50")]
        public string? tsypbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "isYnss giới hạn độ dài là 1")]
        public string? isYnss { get; set; }

    }

}
