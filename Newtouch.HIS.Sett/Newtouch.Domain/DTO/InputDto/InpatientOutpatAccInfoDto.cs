using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
    public class InpatientOutpatAccInfoDto
    {
        public int patid { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string blh { get; set; }
        public DateTime? csny { get; set; }
        public string zjlx { get; set; }
        public string zjh { get; set; }
        public string jsr { get; set; }
        public short? nl { get; set; }
        public string phone { get; set; }
        public string brxz { get; set; }
        public string brxzmc { get; set; }
        public int? sycs { get; set; }
        public int? dybh { get; set; }
        public string kh { get; set; }
        /// <summary>
        /// Loại thẻ
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// Tên loại thẻ
        /// </summary>
        public string CardTypeName { get; set; }
        public string mzh { get; set; }
        public int? ghnm { get; set; }
        public DateTime? ghsj { get; set; }
        public string brly { get; set; }
        public string ghrq { get; set; }

        public string lxr { get; set; }
        public string lxrgx { get; set; }
        public string lxrdh { get; set; }
        public string jjllrgx { get; set; }

        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Khám lần đầu/tái khám
        /// </summary>
        public byte fzbz { get; set; }

        /// <summary>
        /// Bệnh nặng
        /// </summary>
        public string db { get; set; }

        /// <summary>
        /// Chẩn đoán bệnh nặng
        /// </summary>
        public string dbzd { get; set; }

        /// <summary>
        /// Tình trạng hôn nhân Enum EnumHF
        /// </summary>
        public byte? hf { get; set; }

        /// <summary>
        /// Quốc tịch
        /// </summary>
        public string gjCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gjmc { get; set; }

        /// <summary>
        /// Dân tộc
        /// </summary>
        public string mzCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzmc { get; set; }

        /// <summary>
        /// Trạng thái đăng ký khám
        /// </summary>
        public string ghzt { get; set; }

        /// <summary>
        /// Số quyết toán BHYT
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// Lý do khám
        /// </summary>
        public string jzyy { get; set; }

        /// <summary>
        /// Số BHXH
        /// </summary>
        public string sbbh { get; set; }

        /// <summary>
        /// Cờ khám bệnh
        /// </summary>
        public string jiuzhenbiaozhi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>Một
        /// 
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Loại tham gia bảo hiểm
        /// </summary>
        public string cblb { get; set; }
        public string grbh { get; set; }
        public string dwbh { get; set; }
        public string mjzbz { get; set; }


        public string zyh { get; set; }
        public string zybz { get; set; }
        /// <summary>
        /// Mức cảnh báo tạm ứng
        /// </summary>
        public decimal bje { get; set; }

    }
    

}