using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày: 2018-06-28 15:37
    /// Mô tả: Y lệnh tạm thời
    /// </summary>
    [SugarTable("zy_lsyz")]
    public class InpatientSTATOrderEntity
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
        /// Y lệnh số, cùng một Y lệnh số trong gói chứa nhiều cái yzId
        /// </summary>
        /// <returns></returns>
        public string yzh { get; set; }
        /// <summary>
        /// bản địa Y lệnh phút nhóm thứ tự số
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
        /// tốc độ truyền giọt/Phút
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
        /// Chu kỳ thực hiện Đơn vị -1: không quy thì chu kỳ, 0: ngày, 1: giờ, 2: Phút. Lưu ý: khi là -1 khi, tuần Mã (zdm) là số chữ (1234567) trong của Bất kỳ vài cái số chữ
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
        /// Y lệnh Trạng thái 0 chờ duyệt, 1 Phê duyệt, 2 Thực hiện, 3 Hủy (DC), 4 Dừng
        /// </summary>
        /// <returns></returns>
        public int yzzt { get; set; }
        /// <summary>
        /// Đơn vị liều
        /// </summary>
        /// <returns></returns>
        public string dw { get; set; }
        /// <summary>
        /// 0 Chung 1 tự chuẩn bị
        /// </summary>
        /// <returns></returns>
        public int zbbz { get; set; }
        /// <summary>
        /// Thuốc, Mục nhập khi, tồn tại mỗi ngày Số lượng, Xuất viện mang thuốc, tồn tại Tổng lượng. văn bản nhập Số lượng là 0
        /// </summary>
        /// <returns></returns>
        public int sl { get; set; }
        /// <summary>
        /// Đơn vị Loại 0 Đơn vị liều, 1 Đơn vị kho thuốc, 2 Đơn vị ngoại trú, 3 Đơn vị nhập viện, 4 vào hàng Đơn vị (chỉ hiển thị Y lệnh Đơn vị Loại là 0)
        /// </summary>
        /// <returns></returns>
        public int dwlb { get; set; }
        /// <summary>
        /// bản địa Loại y lệnh 0: Thuốc Y lệnh, 1: Kháng sinh Y lệnh, 2: Kiểm tra Y lệnh, 3: Xét nghiệm Y lệnh, 4: chỉ hiển thị Y lệnh, 5: thông thường Y lệnh, 6 Dừng Y lệnh, 7 Xuất viện mang thuốc, 8 ăn uống Y lệnh, 9 Phẫu thuật Y lệnh 10 trong thảo dược  
        /// </summary>
        /// <returns></returns>
        public int yzlx { get; set; }
        /// <summary>
        /// Bác sĩ dừngMã
        /// </summary>
        /// <returns></returns>
        public string zfysgh { get; set; }
        /// <summary>
        /// zfsj
        /// </summary>
        /// <returns></returns>
        public DateTime? zfsj { get; set; }
        /// <summary>
        /// Dừng Thao tác viên số
        /// </summary>
        /// <returns></returns>
        public string zfr { get; set; }
        /// <summary>
        /// Phê duyệtNgày
        /// </summary>
        /// <returns></returns>
        public DateTime? shsj { get; set; }
        /// <summary>
        /// Phê duyệt Thao tác viên số
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
        /// Thực hiện Ngày (Điều dưỡng)
        /// </summary>
        /// <returns></returns>
        public DateTime? zxsj { get; set; }
        /// <summary>
        /// cho bệnh nhân Thực hiện Y lệnh của Điều dưỡng
        /// </summary>
        /// <returns></returns>
        public string zxr { get; set; }
        /// <summary>
        /// Thuốc Liều lượng
        /// </summary>
        /// <returns></returns>
        public decimal? ypjl { get; set; }
        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        /// <returns></returns>
        public string ypgg { get; set; }
        /// <summary>
        /// Dặn dò nội dung
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
        // QA-phase2 (nhóm B, mục 3): zy_lsyz ở DB version này KHÔNG có cột ztmbId
        // (đã kiểm chứng INFORMATION_SCHEMA cả demo lẫn STG; chỉ thiếu đúng cột này),
        // nên mọi Queryable/Insert/Update qua entity này đều lỗi "Invalid column
        // name 'ztmbId'" (AdviceStop/AdviceDel). Đánh dấu IsIgnore thay vì đổi schema
        // demo; không caller nào đọc/ghi STAT.ztmbId (chỉ SQL thô ở OrderExecution
        // dùng tên cột trực tiếp, không qua entity này).
        [SugarColumn(IsIgnore = true)]
        public string ztmbId { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }

        /// <summary>
        /// Kháng sinh nguyên nhân
        /// </summary>
        public string kssReason { get; set; }
        /// <summary>
        /// Loại đăng ký, truyền đến pacs dùng
        /// </summary>
        public string sqlx { get; set; }
        /// <summary>
        /// Vị trí Phương pháp
        /// </summary>
        public string bwff { get; set; }
        /// <summary>
        /// Số phiếu, kim đối Xét nghiệm Kiểm tra
        /// </summary>
        public string sqdh { get; set; }

        public string lcyx { get; set; }

        public string sqbz { get; set; }
        /// <summary>
        /// Phiếu chỗ lý Trạng thái (0, Đã gửi đơn 1, đã nhận thu 2, đã Hoàn thành) 
        /// </summary>
        public int? sqdzt { get; set; }

        /// <summary>
        /// sắc hộ Cờ
        /// </summary>
        public int? djbz { get; set; }
        /// <summary>
        /// Xuất viện mang thuốc Cờ
        /// </summary>
        public int? cydybz { get; set; }

        /// <summary>
        /// Chuyển sang tự chi trả Cờ
        /// </summary>
        public int? zzfbz { get; set; }
        /// <summary>
        /// Trạng thái đăng ký
        /// </summary>
        public int? syncStatus { get; set; }
        /// <summary>
        /// Đơn thuốc nhãn JI tinh I JII tinh II MZ tê say
        /// </summary>
        public string yztag { get; set; }
        /// <summary>
        /// là Không tính phí (0: Không không tính vào Chi phí chi tiết 1: là)
        /// </summary>
        public int? isjf { get; set; }
        /// <summary>
        /// Thực hiện trong Cờ 1 đang ở Thực hiện
        /// </summary>
        public string zxing { get; set; }
        public string ispscs { get; set; }
        public int? ztsl { get; set; }
        /// <summary>
        /// Mục thu phí Bộ mẫu nhận dạng
        /// </summary>
        public string dcztbs { get; set; }
        /// <summary>
        /// Cách dùng thuốc tương ứng Mục thu phí Bộ mẫu
        /// </summary>
        public string yfztbs { get; set; }
        /// <summary>
        /// Thuốc đến nguồn 1: Khoa Tồn kho 2: Nhà thuốc
        /// </summary>
        public int? yply { get; set; }
        /// <summary>
        /// Y lệnh phụ
        /// </summary>
        public string isfsyz { get; set; }

        public int? Px { get; set; }
    }
}
