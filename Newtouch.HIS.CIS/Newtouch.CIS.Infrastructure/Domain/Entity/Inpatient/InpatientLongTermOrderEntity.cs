using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày:2018-06-28 15:37
    /// Mô tả:Y lệnh dài hạn
    /// </summary>
    [SugarTable("zy_cqyz")]
    public class InpatientLongTermOrderEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// zyh
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// Y lệnhsố,cùng mộtY lệnhsốtronggóichứanhiềucáiyzId
        /// </summary>
        /// <returns></returns>
        public string yzh { get; set; }
        /// <summary>
        /// bảnđịaY lệnhphútnhómthứ tựsố
        /// </summary>
        /// <returns></returns>
        public int? zh { get; set; }
        /// <summary>
        /// Khu bệnhMã
        /// </summary>
        /// <returns></returns>
        public string WardCode { get; set; }
        /// <summary>
        /// KhoaMã
        /// </summary>
        /// <returns></returns>
        public string DeptCode { get; set; }
        /// <summary>
        /// Bác sĩMã
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }
        /// <summary>
        /// Tần suấtMã
        /// </summary>
        /// <returns></returns>
        public string pcCode { get; set; }

        /// <summary>
        /// tốc độ truyền  giọt/Phút
        /// </summary>
        public int? ds { get; set; }

        /// <summary>
        /// Số lần thực hiện
        /// </summary>
        /// <returns></returns>
        public int? zxcs { get; set; }
        /// <summary>
        /// Chu kỳ thực hiện
        /// </summary>
        /// <returns></returns>
        public int? zxzq { get; set; }
        /// <summary>
        /// Chu kỳ thực hiệnĐơn vị -1:khôngquythìchu kỳ,0:ngày,1:giờ,2:Phút.Lưu ý:khilà-1khi,tuầnMã(zdm)làsốchữ(1234567)trongcủaBất kỳ vàicáisốchữ
        /// </summary>
        /// <returns></returns>
        public int? zxzqdw { get; set; }
        /// <summary>
        /// tuầnMã
        /// </summary>
        /// <returns></returns>
        public string zdm { get; set; }
        /// <summary>
        /// xmdm
        /// </summary>
        /// <returns></returns>
        public string xmdm { get; set; }
        /// <summary>
        /// xmmc
        /// </summary>
        /// <returns></returns>
        public string xmmc { get; set; }
        /// <summary>
        /// Y lệnhTrạng thái 0chờduyệt,1Phê duyệt,2Thực hiện,3Hủy(DC),4Dừng
        /// </summary>
        /// <returns></returns>
        public int yzzt { get; set; }
        /// <summary>
        /// Đơn vị liều
        /// </summary>
        /// <returns></returns>
        public string dw { get; set; }
        /// <summary>
        /// 0 Chung 1 tựchuẩn bị
        /// </summary>
        /// <returns></returns>
        public int zbbz { get; set; }
        /// <summary>
        ///  Thuốc,Mụcnhậpkhi,tồn tạimỗingàySố lượng,Xuất việnmang thuốc,tồn tạiTổng lượng.văn bảnnhậpSố lượnglà0
        /// </summary>
        /// <returns></returns>
        public int sl { get; set; }
        /// <summary>
        /// Đơn vịLoại 0Đơn vị liều,1Đơn vị kho thuốc,2Đơn vị ngoại trú,3Đơn vị nhập viện,4vàohàngĐơn vị(chỉhiển thịY lệnhĐơn vịLoạilà0)
        /// </summary>
        /// <returns></returns>
        public int? dwlb { get; set; }
        /// <summary>
        /// bảnđịaLoại y lệnh 0:ThuốcY lệnh,1:Kháng sinhY lệnh,2:Kiểm traY lệnh,3:Xét nghiệmY lệnh,4:chỉhiển thịY lệnh,5:thông thườngY lệnh , 6 DừngY lệnh, 7  Xuất việnmang thuốc, 8 ăn uốngY lệnh, 9 Phẫu thuậtY lệnh   10  trongthảo dược  
        /// </summary>
        /// <returns></returns>
        public int yzlx { get; set; }
        /// <summary>
        /// Bác sĩ dừngMã
        /// </summary>
        /// <returns></returns>
        public string tzysgh { get; set; }
        /// <summary>
        /// tzsj
        /// </summary>
        /// <returns></returns>
        public DateTime? tzsj { get; set; }
        /// <summary>
        /// DừngThao tácviênsố
        /// </summary>
        /// <returns></returns>
        public string tzr { get; set; }
        /// <summary>
        /// tzyy
        /// </summary>
        /// <returns></returns>
        public string tzyy { get; set; }
        /// <summary>
        /// Phê duyệtNgày
        /// </summary>
        /// <returns></returns>
        public DateTime? shsj { get; set; }
        /// <summary>
        /// Phê duyệtThao tácviênsố
        /// </summary>
        /// <returns></returns>
        public string shr { get; set; }
        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        /// <returns></returns>
        public DateTime kssj { get; set; }
        /// <summary>
        /// Thực hiệnNgày
        /// </summary>
        /// <returns></returns>
        public DateTime? zxsj { get; set; }
        /// <summary>
        /// chobệnh nhânThực hiệnY lệnhcủaĐiều dưỡng
        /// </summary>
        /// <returns></returns>
        public string zxr { get; set; }
        /// <summary>
        /// HủyThao tácviênsố
        /// </summary>
        /// <returns></returns>
        public string dcr { get; set; }
        /// <summary>
        /// ThuốcLiều lượng
        /// </summary>
        /// <returns></returns>
        public decimal? ypjl { get; set; }
        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        /// <returns></returns>
        public string ypgg { get; set; }
        /// <summary>
        /// Dặn dònội dung
        /// </summary>
        /// <returns></returns>
        public string ztnr { get; set; }
        /// <summary>
        /// Nội dung y lệnh
        /// </summary>
        /// <returns></returns>
        public string yznr { get; set; }
        /// <summary>
        /// Cách dùng thuốc
        /// </summary>
        /// <returns></returns>
        public string ypyfdm { get; set; }
        /// <summary>
        /// Khoa thực hiệnMã
        /// </summary>
        /// <returns></returns>
        public string zxksdm { get; set; }
        /// <summary>
        /// printyznr
        /// </summary>
        /// <returns></returns>
        public string printyznr { get; set; }
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

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string hzxm { get; set; }

        public string bw { get; set; }

        public string zxsjd { get; set; }
        public string nlmddm { get; set; }
        /// <summary>
        /// Kháng sinhnguyên nhân
        /// </summary>
        public string kssReason { get; set; }

        /// <summary>
        /// Chuyển sang tự chi trảCờ
        /// </summary>
        public int? zzfbz { get; set; }
        /// <summary>
        ///  Đơn thuốcnhãn JI tinhI JII tinhII MZ têsay
        /// </summary>
        public string yztag { get; set; }
        /// <summary>
        /// làKhôngtínhphí(0:Không khôngtínhvàoChi phíchi tiết 1:là)
        /// </summary>
        public int? isjf { get; set; }
        /// <summary>
        /// Thực hiệntrongCờ  1 đangởThực hiện
        /// </summary>
        public string zxing { get; set; }
        /// <summary>
        /// dathửđothử(0:Không,1:là)
        /// </summary>
        public string ispscs { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }
        public int? ztsl { get; set; }
        /// <summary>
        /// Mục thu phíBộ mẫunhận dạng
        /// </summary>
        public string dcztbs { get; set; }
        /// <summary>
        /// Cách dùng thuốctương ứngMục thu phíBộ mẫu
        /// </summary>
        public string yfztbs { get; set; }
        /// <summary>
        /// Thuốcđếnnguồn 1:KhoaTồn kho 2:Nhà thuốc
        /// </summary>
        public int? yply { get; set; }
        /// <summary>
        /// Y lệnh phụ
        /// </summary>
        public string isfsyz { get; set; }

        public int? Px { get; set; }
    }
}
