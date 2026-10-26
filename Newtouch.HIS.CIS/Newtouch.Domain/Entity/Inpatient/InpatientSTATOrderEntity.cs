using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tạo bởi:张冬林
    /// Ngày:2018-06-28 15:37
    /// Mô tả:Y lệnh tạm thời
    /// </summary>
    [Table("zy_lsyz")]
    public class InpatientSTATOrderEntity : IEntity<InpatientSTATOrderEntity>
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
        /// Số y lệnh,Trong cùng một số y lệnh chứa nhiềuyzId
        /// </summary>
        /// <returns></returns>
        public string yzh { get; set; }
        /// <summary>
        /// Số thứ tự nhóm y lệnh cục bộ
        /// </summary>
        /// <returns></returns>
        public int? zh { get; set; }
        /// <summary>
        /// Mã khu bệnh
        /// </summary>
        /// <returns></returns>
        public string WardCode { get; set; }
        /// <summary>
        /// Mã khoa
        /// </summary>
        /// <returns></returns>
        public string DeptCode { get; set; }
        /// <summary>
        /// Mã bác sĩ
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }
        /// <summary>
        /// Mã tần suất
        /// </summary>
        /// <returns></returns>
        public string pcCode { get; set; }

        /// <summary>
        /// Tốc độ truyền  giọt/Phút
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
        /// Đơn vị chu kỳ thực hiện -1:Chu kỳ không đều,0:Ngày,1:Giờ,2:Phút.Lưu ý:Khi là-1khi,Mã tuần(zdm)là số(1234567)một vài số bất kỳ trong đó
        /// </summary>
        /// <returns></returns>
        public int? zxzqdw { get; set; }
        /// <summary>
        /// Mã tuần
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
        /// Trạng thái y lệnh 0Chờ duyệt,1Duyệt,2Thực hiện,3Hủy(DC),4Dừng
        /// </summary>
        /// <returns></returns>
        public int yzzt { get; set; }
        /// <summary>
        /// Đơn vị liều lượng
        /// </summary>
        /// <returns></returns>
        public string dw { get; set; }
        /// <summary>
        /// 0 Chung 1 Tự chuẩn bị
        /// </summary>
        /// <returns></returns>
        public int zbbz { get; set; }
        /// <summary>
        /// Thuốc,Khi nhập hạng mục,Lưu số lượng mỗi ngày,Thuốc mang về khi xuất viện,Lưu tổng lượng.Số lượng nhập văn bản là0
        /// </summary>
        /// <returns></returns>
        public int sl { get; set; }
        /// <summary>
        /// Loại đơn vị 0Đơn vị liều lượng,1Đơn vị kho thuốc,2Đơn vị ngoại trú,3Đơn vị nội trú,4Đơn vị nhập hàng(Loại đơn vị của y lệnh chỉ thị là0)
        /// </summary>
        /// <returns></returns>
        public int dwlb { get; set; }
        /// <summary>
        /// Loại y lệnh cục bộ 0:Y lệnh thuốc,1:Y lệnh kháng sinh,2:Y lệnh kiểm tra,3:Y lệnh xét nghiệm,4:Y lệnh chỉ thị,5:Y lệnh thường , 6 Y lệnh dừng, 7  Thuốc mang về khi xuất viện, 8 Y lệnh chế độ ăn, 9 Y lệnh phẫu thuật   10  Thuốc thảo dược  
        /// </summary>
        /// <returns></returns>
        public int yzlx { get; set; }
        /// <summary>
        /// Mã bác sĩ dừng
        /// </summary>
        /// <returns></returns>
        public string zfysgh { get; set; }
        /// <summary>
        /// zfsj
        /// </summary>
        /// <returns></returns>
        public DateTime? zfsj { get; set; }
        /// <summary>
        /// Số nhân viên dừng
        /// </summary>
        /// <returns></returns>
        public string zfr { get; set; }
        /// <summary>
        /// Ngày duyệt
        /// </summary>
        /// <returns></returns>
        public DateTime? shsj { get; set; }
        /// <summary>
        /// Số nhân viên duyệt
        /// </summary>
        /// <returns></returns>
        public string shr { get; set; }
        /// <summary>
        /// sssj
        /// </summary>
        /// <returns></returns>
        public DateTime? sssj { get; set; }
        /// <summary>
        /// ssr
        /// </summary>
        /// <returns></returns>
        public string ssr { get; set; }
        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        /// <returns></returns>
        public DateTime kssj { get; set; }
        /// <summary>
        /// Ngày thực hiện(Điều dưỡng)
        /// </summary>
        /// <returns></returns>
        public DateTime? zxsj { get; set; }
        /// <summary>
        /// Điều dưỡng thực hiện y lệnh cho bệnh nhân
        /// </summary>
        /// <returns></returns>
        public string zxr { get; set; }
        /// <summary>
        /// Liều lượng thuốc
        /// </summary>
        /// <returns></returns>
        public decimal? ypjl { get; set; }
        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        /// <returns></returns>
        public string ypgg { get; set; }
        /// <summary>
        /// Nội dung dặn dò
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
        /// Mã khoa thực hiện
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
        /// Họ tên bệnh nhân
        /// </summary>
        public string hzxm { get; set; }

        public string bw { get; set; }

        public string zxsjd { get; set; }

        public string nlmddm { get; set; }
        public string ztmbId { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }

        /// <summary>
        /// Lý do dùng kháng sinh
        /// </summary>
        public string kssReason { get; set; }
        /// <summary>
        /// Loại yêu cầu,Truyền tớipacsdùng
        /// </summary>
        public string sqlx { get; set; }
        /// <summary>
        /// Phương pháp vị trí
        /// </summary>
        public string bwff { get; set; }
        /// <summary>
        /// Số phiếu yêu cầu,Đối với xét nghiệm/kiểm tra
        /// </summary>
        public string sqdh { get; set; }

        public string lcyx { get; set; }

        public string sqbz { get; set; }
        /// <summary>
        /// Trạng thái xử lý phiếu yêu cầu(0,Đã yêu cầu 1,Đã tiếp nhận 2,Đã hoàn thành) 
        /// </summary>
        public int? sqdzt { get; set; }

        /// <summary>
        /// Cờ sắc hộ
        /// </summary>
        public int? djbz { get; set; }
        /// <summary>
        /// Cờ thuốc mang về khi xuất viện
        /// </summary>
        public int? cydybz { get; set; }

        /// <summary>
        /// Cờ chuyển sang tự chi trả
        /// </summary>
        public int? zzfbz { get; set; }
        /// <summary>
        /// Trạng thái yêu cầu
        /// </summary>
        public int? syncStatus { get; set; }
        /// <summary>
        ///  Nhãn đơn thuốc JI Hướng thầnI JII Hướng thầnII MZ Gây mê
        /// </summary>
        public string yztag { get; set; }
        /// <summary>
        /// Có tính phí hay không(0:Không Không tính vào chi tiết chi phí 1:Có)
        /// </summary>
        public int? isjf { get; set; }
        /// <summary>
        /// Cờ đang thực hiện  1 Đang thực hiện
        /// </summary>
        public string zxing { get; set; }
        public string ispscs { get; set; }
        public int? ztsl { get; set; }
        /// <summary>
        /// Dấu hiệu gói hạng mục thu phí
        /// </summary>
        public string dcztbs { get; set; }
        /// <summary>
        /// Gói hạng mục thu phí tương ứng với cách dùng thuốc
        /// </summary>
        public string yfztbs { get; set; }
        /// <summary>
        /// Nguồn gốc thuốc 1:Tồn kho khoa 2:Nhà thuốc
        /// </summary>
        public int? yply { get; set; }
        /// <summary>
        /// Y lệnh phụ thuộc
        /// </summary>
        public string isfsyz { get; set; }

        public int? Px { get; set; }
    }
}