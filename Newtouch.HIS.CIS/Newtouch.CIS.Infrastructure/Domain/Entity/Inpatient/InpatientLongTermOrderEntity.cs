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
        /// Số y lệnh, cùng một số y lệnh trong gói chứa nhiều yzId
        /// </summary>
        /// <returns></returns>
        public string yzh { get; set; }
        /// <summary>
        /// Số thứ tự nhóm y lệnh tại chỗ
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
        /// Tốc độ truyền giọt/phút
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
        /// Đơn vị chu kỳ thực hiện -1: không theo chu kỳ, 0: ngày, 1: giờ, 2: phút. Lưu ý: khi là -1, mã tuần (zdm) là vài chữ số bất kỳ trong (1234567)
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
        /// Trạng thái y lệnh 0 chờ duyệt, 1 phê duyệt, 2 thực hiện, 3 hủy (DC), 4 dừng
        /// </summary>
        /// <returns></returns>
        public int yzzt { get; set; }
        /// <summary>
        /// Đơn vị liều
        /// </summary>
        /// <returns></returns>
        public string dw { get; set; }
        /// <summary>
        /// 0 chung 1 tự chuẩn bị
        /// </summary>
        /// <returns></returns>
        public int zbbz { get; set; }
        /// <summary>
        /// Thuốc, khi nhập mục: có số lượng mỗi ngày, xuất viện mang thuốc, có tổng lượng. Số lượng nhập văn bản là 0
        /// </summary>
        /// <returns></returns>
        public int sl { get; set; }
        /// <summary>
        /// Loại đơn vị 0 đơn vị liều, 1 đơn vị kho thuốc, 2 đơn vị ngoại trú, 3 đơn vị nhập viện, 4 đơn vị nhập kho (chỉ hiển thị khi loại đơn vị y lệnh là 0)
        /// </summary>
        /// <returns></returns>
        public int? dwlb { get; set; }
        /// <summary>
        /// Loại y lệnh tại chỗ 0: y lệnh thuốc, 1: y lệnh kháng sinh, 2: y lệnh kiểm tra, 3: y lệnh xét nghiệm, 4: y lệnh chỉ hiển thị, 5: y lệnh thông thường, 6: y lệnh dừng, 7: xuất viện mang thuốc, 8: y lệnh ăn uống, 9: y lệnh phẫu thuật, 10: y lệnh thảo dược Trung y  
        /// </summary>
        /// <returns></returns>
        public int yzlx { get; set; }
        /// <summary>
        /// Mã bác sĩ dừng
        /// </summary>
        /// <returns></returns>
        public string tzysgh { get; set; }
        /// <summary>
        /// tzsj
        /// </summary>
        /// <returns></returns>
        public DateTime? tzsj { get; set; }
        /// <summary>
        /// Số thao tác viên dừng
        /// </summary>
        /// <returns></returns>
        public string tzr { get; set; }
        /// <summary>
        /// tzyy
        /// </summary>
        /// <returns></returns>
        public string tzyy { get; set; }
        /// <summary>
        /// Ngày phê duyệt
        /// </summary>
        /// <returns></returns>
        public DateTime? shsj { get; set; }
        /// <summary>
        /// Số thao tác viên phê duyệt
        /// </summary>
        /// <returns></returns>
        public string shr { get; set; }
        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        /// <returns></returns>
        public DateTime kssj { get; set; }
        /// <summary>
        /// Ngày thực hiện
        /// </summary>
        /// <returns></returns>
        public DateTime? zxsj { get; set; }
        /// <summary>
        /// Điều dưỡng thực hiện y lệnh cho bệnh nhân
        /// </summary>
        /// <returns></returns>
        public string zxr { get; set; }
        /// <summary>
        /// Số thao tác viên hủy
        /// </summary>
        /// <returns></returns>
        public string dcr { get; set; }
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
        /// Nguyên nhân kháng sinh
        /// </summary>
        public string kssReason { get; set; }

        /// <summary>
        /// Cờ chuyển sang tự chi trả
        /// </summary>
        public int? zzfbz { get; set; }
        /// <summary>
        /// Nhãn đơn thuốc: tinh I, tinh II, gây mê
        /// </summary>
        public string yztag { get; set; }
        /// <summary>
        /// Là không tính phí (0: không, không tính vào chi phí chi tiết; 1: là)
        /// </summary>
        public int? isjf { get; set; }
        /// <summary>
        /// Cờ đang thực hiện (1: đang thực hiện)
        /// </summary>
        public string zxing { get; set; }
        /// <summary>
        /// Thử da (0: không, 1: là)
        /// </summary>
        public string ispscs { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }
        public int? ztsl { get; set; }
        /// <summary>
        /// Nhận dạng bộ mẫu mục thu phí
        /// </summary>
        public string dcztbs { get; set; }
        /// <summary>
        /// Bộ mẫu mục thu phí tương ứng cách dùng thuốc
        /// </summary>
        public string yfztbs { get; set; }
        /// <summary>
        /// Nguồn thuốc đến 1: Khoa tồn kho 2: Nhà thuốc
        /// </summary>
        public int? yply { get; set; }
        /// <summary>
        /// Y lệnh phụ
        /// </summary>
        public string isfsyz { get; set; }

        public int? Px { get; set; }
    }
}
