using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.Domain.DTO.Medicine
{
    public class MZCfYpDyDTO
    {
        public long Id { get; set; }

        /// <summary>
        /// mã nội bộ đơn thuốc
        /// </summary>
        public long cfnm { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// quyết toán trong mã
        /// </summary>
        public long jsnm { get; set; }

        /// <summary>
        /// số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// số hóa đơn
        /// </summary>
        public string Fph { get; set; }

        /// <summary>
        /// tuổi
        /// </summary>
        public int? nl { get; set; }

        /// <summary>
        /// tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// bác sĩ
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// nhà thuốc lĩnh thuốc
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// thời gian thu phí
        /// </summary>
        public DateTime? sfsj { get; set; }

        /// <summary>
        /// 0: chưa xếp; 1: đã xếp; 2: đã phát; 3: đã trả
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// tổ chức Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// đơn thuốc số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 0: không hiệu lực 1: còn hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// người sửa đổi
        /// </summary>
        public string LastModiFierCode { get; set; }

        /// <summary>
        /// ngày sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        public string cfId { get; set; }
        public string mzh { get; set; }
    }
}