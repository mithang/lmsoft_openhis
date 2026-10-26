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
        /// y lệnh số, 同一医嘱号内包含多个yzId
        /// </summary>
        /// <returns></returns>
        public string yzh { get; set; }
        /// <summary>
        /// 本地医嘱分组序号
        /// </summary>
        /// <returns></returns>
        public int? zh { get; set; }
        /// <summary>
        /// khu bệnh mã
        /// </summary>
        /// <returns></returns>
        public string WardCode { get; set; }
        /// <summary>
        /// khoa mã
        /// </summary>
        /// <returns></returns>
        public string DeptCode { get; set; }
        /// <summary>
        /// bác sĩ mã
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }
        /// <summary>
        /// tần suất mã
        /// </summary>
        /// <returns></returns>
        public string pcCode { get; set; }

        /// <summary>
        /// 滴速  滴/phút
        /// </summary>
        public int? ds { get; set; }

        /// <summary>
        /// thực hiện lần số
        /// </summary>
        /// <returns></returns>
        public int? zxcs { get; set; }
        /// <summary>
        /// 执行周期
        /// </summary>
        /// <returns></returns>
        public int? zxzq { get; set; }
        /// <summary>
        /// 执行周期单位 -1:不规则周期, 0: ngày,1: giờ,2: phút.chú ý: khi là-1 giờ, mã tuần (zdm)là số (1234567)中的任意几个数字
        /// </summary>
        /// <returns></returns>
        public int? zxzqdw { get; set; }
        /// <summary>
        /// mã tuần
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
        /// y lệnh trạng thái 0待审, 1 duyệt, 2 thực hiện, 3 hủy(DC), 4 dừng
        /// </summary>
        /// <returns></returns>
        public int yzzt { get; set; }
        /// <summary>
        /// đơn vị liều dùng
        /// </summary>
        /// <returns></returns>
        public string dw { get; set; }
        /// <summary>
        /// 0 chung 1 自备
        /// </summary>
        /// <returns></returns>
        public int zbbz { get; set; }
        /// <summary>
        /// thuốc, hạng mục nhập giờ, lưu mỗi ngày số lượng, thuốc mang về khi xuất viện, lưu tổng lượng.văn bản nhập số lượng là 0
        /// </summary>
        /// <returns></returns>
        public int sl { get; set; }
        /// <summary>
        /// đơn vị loại 0 đơn vị liều dùng, 1 đơn vị kho thuốc, 2 ngoại trú đơn vị, 3 nội trú đơn vị, 4进货单位(指示医嘱单位类别为0)
        /// </summary>
        /// <returns></returns>
        public int dwlb { get; set; }
        /// <summary>
        /// 本地医嘱类别 0: thuốc y lệnh, 1: kháng sinh y lệnh, 2: kiểm tra y lệnh, 3: xét nghiệm y lệnh, 4: 指示医嘱, 5: thường y lệnh, 6 dừng y lệnh, 7  thuốc mang về khi xuất viện, 8 膳食医嘱, 9 phẫu thuật y lệnh   10  thuốc thảo dược  
        /// </summary>
        /// <returns></returns>
        public int yzlx { get; set; }
        /// <summary>
        /// dừng bác sĩ mã
        /// </summary>
        /// <returns></returns>
        public string zfysgh { get; set; }
        /// <summary>
        /// zfsj
        /// </summary>
        /// <returns></returns>
        public DateTime? zfsj { get; set; }
        /// <summary>
        /// dừng người thao tác số
        /// </summary>
        /// <returns></returns>
        public string zfr { get; set; }
        /// <summary>
        /// ngày duyệt
        /// </summary>
        /// <returns></returns>
        public DateTime? shsj { get; set; }
        /// <summary>
        /// duyệt người thao tác số
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
        /// ngày bắt đầu
        /// </summary>
        /// <returns></returns>
        public DateTime kssj { get; set; }
        /// <summary>
        /// thực hiện ngày(护士)
        /// </summary>
        /// <returns></returns>
        public DateTime? zxsj { get; set; }
        /// <summary>
        /// 给病人执行医嘱的护士
        /// </summary>
        /// <returns></returns>
        public string zxr { get; set; }
        /// <summary>
        /// thuốc liều dùng
        /// </summary>
        /// <returns></returns>
        public decimal? ypjl { get; set; }
        /// <summary>
        /// quy cách thuốc
        /// </summary>
        /// <returns></returns>
        public string ypgg { get; set; }
        /// <summary>
        /// lời dặn nội dung
        /// </summary>
        /// <returns></returns>
        public string ztnr { get; set; }
        /// <summary>
        /// y lệnh nội dung
        /// </summary>
        /// <returns></returns>
        public string yznr { get; set; }
        /// <summary>
        /// cách dùng thuốc
        /// </summary>
        /// <returns></returns>
        public string ypyfdm { get; set; }
        /// <summary>
        /// thực hiện khoa mã
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
        /// họ tên bệnh nhân
        /// </summary>
        public string hzxm { get; set; }

        public string bw { get; set; }

        public string zxsjd { get; set; }

        public string nlmddm { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }

        /// <summary>
        /// kháng sinh nguyên nhân
        /// </summary>
        public string kssReason { get; set; }
        /// <summary>
        /// đề nghị loại, 传到pacs dùng
        /// </summary>
        public string sqlx { get; set; }
        /// <summary>
        /// bộ vị phương thức
        /// </summary>
        public string bwff { get; set; }
        /// <summary>
        /// phiếu đề nghị số, 针对检验检查
        /// </summary>
        public string sqdh { get; set; }

        public string lcyx { get; set; }

        public string sqbz { get; set; }
        /// <summary>
        /// phiếu đề nghị xử lý trạng thái (0, đã đề nghị 1, 已接收 2, đã hoàn thành) 
        /// </summary>
        public int? sqdzt { get; set; }

        /// <summary>
        /// 代煎标志
        /// </summary>
        public int? djbz { get; set; }
        /// <summary>
        /// thuốc mang về khi xuất viện cờ
        /// </summary>
        public int? cydybz { get; set; }

        /// <summary>
        /// 转自费标志
        /// </summary>
        public int? zzfbz { get; set; }
        /// <summary>
        /// đề nghị trạng thái
        /// </summary>
        public int? syncStatus { get; set; }
        /// <summary>
        ///  处方标签 JI tinh I JII tinh II MZ 麻醉
        /// </summary>
        public string yztag { get; set; }
        /// <summary>
        /// 是否计费(0:không 不计入费用明细 1:có)
        /// </summary>
        public int? isjf { get; set; }
        /// <summary>
        /// thực hiện trong cờ  1 đang thực hiện
        /// </summary>
        public string zxing { get; set; }
        public string ispscs { get; set; }
    }
}