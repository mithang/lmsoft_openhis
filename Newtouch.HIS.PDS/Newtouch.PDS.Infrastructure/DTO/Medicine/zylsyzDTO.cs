using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.Domain.DTO.Medicine
{
    public class zylsyzDTO
    {
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
        /// Số thứ tự nhóm y lệnh địa phương
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
        /// Tốc độ nhỏ giọt  giọt/phút
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
        /// Đơn vị chu kỳ thực hiện -1:Chu kỳ không đều,0:ngày,1:giờ,2:phút.Chú ý:Khi là-1khi,Mã tuần(zdm)là số(1234567)vài số bất kỳ trong
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
        /// Trạng thái y lệnh 0Chờ xét,1Duyệt,2Thực hiện,3Hủy(DC),4Dừng
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
        /// Thuốc,Khi nhập hạng mục,Lưu Số lượng mỗi ngày,Thuốc mang về khi xuất viện,Lưu tổng lượng.Số lượng nhập bằng chữ là0
        /// </summary>
        /// <returns></returns>
        public int sl { get; set; }
        /// <summary>
        /// Loại đơn vị 0Đơn vị liều lượng,1Đơn vị kho thuốc,2Đơn vị ngoại trú,3Đơn vị nội trú,4Đơn vị nhập hàng(Loại đơn vị y lệnh chỉ thị là0)
        /// </summary>
        /// <returns></returns>
        public int dwlb { get; set; }
        /// <summary>
        /// Loại y lệnh địa phương 0:Y lệnh thuốc,1:Y lệnh kháng sinh,2:Y lệnh kiểm tra,3:Y lệnh xét nghiệm,4:Y lệnh chỉ thị,5:Y lệnh thường , 6 Y lệnh dừng, 7  Thuốc mang về khi xuất viện, 8 Y lệnh ăn uống, 9 Y lệnh phẫu thuật   10  Thuốc thảo dược Trung Quốc  
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
        /// Số nhân viên thao tác dừng
        /// </summary>
        /// <returns></returns>
        public string zfr { get; set; }
        /// <summary>
        /// Ngày duyệt
        /// </summary>
        /// <returns></returns>
        public DateTime? shsj { get; set; }
        /// <summary>
        /// Số nhân viên thao tác duyệt
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
        /// Ngày thực hiện(Y tá)
        /// </summary>
        /// <returns></returns>
        public DateTime? zxsj { get; set; }
        /// <summary>
        /// Y tá thực hiện y lệnh cho bệnh nhân
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
        /// Tên bệnh nhân
        /// </summary>
        public string hzxm { get; set; }

        public string bw { get; set; }

        public string zxsjd { get; set; }

        public string nlmddm { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }

        /// <summary>
        /// Lý do kháng sinh
        /// </summary>
        public string kssReason { get; set; }
        /// <summary>
        /// Loại đơn xin,Truyền tớipacsdùng
        /// </summary>
        public string sqlx { get; set; }
        /// <summary>
        /// Phương pháp vị trí
        /// </summary>
        public string bwff { get; set; }
        /// <summary>
        /// Số đơn xin,Nhằm vào xét nghiệm kiểm tra
        /// </summary>
        public string sqdh { get; set; }

        public string lcyx { get; set; }

        public string sqbz { get; set; }
        /// <summary>
        /// Trạng thái xử lý đơn xin(0,Đã xin 1,Đã nhận 2,Đã hoàn thành) 
        /// </summary>
        public int? sqdzt { get; set; }

        /// <summary>
        /// Cờ sắc hộ
        /// </summary>
        public int? djbz { get; set; }
        /// <summary>
        /// Cờ mang thuốc xuất viện
        /// </summary>
        public int? cydybz { get; set; }

        /// <summary>
        /// Cờ chuyển tự trả
        /// </summary>
        public int? zzfbz { get; set; }
        /// <summary>
        /// Trạng thái xin
        /// </summary>
        public int? syncStatus { get; set; }
        /// <summary>
        ///  Nhãn đơn thuốc JI tinhI JII tinhII MZ Gây mê
        /// </summary>
        public string yztag { get; set; }
        /// <summary>
        /// Có tính phí không(0:Không Không tính vào chi tiết phí 1:Có)
        /// </summary>
        public int? isjf { get; set; }
        /// <summary>
        /// Cờ đang thực hiện  1 Đang thực hiện
        /// </summary>
        public string zxing { get; set; }
        public string ispscs { get; set; }
    }
}