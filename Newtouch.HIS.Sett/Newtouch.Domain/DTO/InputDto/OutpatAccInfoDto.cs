using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
    public class OutpatAccInfoDto
    {
        public int patid { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string blh { get; set; }
        public DateTime? csny { get; set; }
        public string zjlx { get; set; }
        public string zjh { get; set; }
        public string jsr { get; set; }
        public int? nl { get; set; }
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
        public string zyh { get; set; }
        public string mjzbz { get; set; }
        public string CardId { get; set; }
        public string dz { get; set; }
        public string zybz { get; set; }
        public string brxzlb { get; set; }

    }

    public class OptimAccInfoDto
    {
        public string ghrq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string patientName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfdlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfdlmc { get; set; }

        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Thời lượng đơn vị
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// Đơn giá hiện tại của hạng mục thu phí
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sl { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// do ys ysmc ks ksmc Được tạo thành từ
        /// VOTrích dẫnDTOCó vấn đề
        /// </summary>
        public IList<InpatientAccountingPlanItemDoctorDto> ysList
        {
            get
            {
                var list = new List<InpatientAccountingPlanItemDoctorDto>();
                var ysArr = (ys ?? "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                var ysmcArr = (ysmc ?? "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                var ksArr = (ks ?? "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                var ksmcArr = (ksmc ?? "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (ysArr.Length > 0 && ysArr.Length == ysmcArr.Length && ksArr.Length == ksmcArr.Length && ysArr.Length == ksArr.Length)
                {
                    for (var i = 0; i < ysArr.Length; i++)
                    {
                        list.Add(new InpatientAccountingPlanItemDoctorDto()
                        {
                            gh = ysArr[i],
                            Name = ysmcArr[i],
                            ks = ksArr[i],
                            ksmc = ksmcArr[i],
                        });
                    }
                }
                return list;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }


        /// <summary>
        /// Loại y lệnh 1Thuốc 2Hạng mục
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// Loại phục hồi chức năng
        /// </summary>
        public string kflb { get; set; }

        public DateTime? jzsj { get; set; }
        //public IList<InpatientAccountingPlanItemDoctorDto> ysList { get; set; }
        public int? clzt { get; set; }
        public string newid { get; set; }
        public int? jsmxnm { get; set; }
        public string ttbz { get; set; }
        public string jfbId { get; set; }

        /// <summary>
        /// Liều điều trị mỗi lần
        /// </summary>
        public int? zll { get; set; }
        public int? zxzt { get; set; }
        public string jzjhmxId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string yzxz { get; set; }
        /// <summary>
        /// Số lượng đơn vị đo
        /// </summary>
        public int? dwjls { get; set; }
        /// <summary>
        /// Chính sách tính giá
        /// </summary>
        public int? jjcl { get; set; }

        public int? xmnm { get; set; }
        public string zfxz { get; set; }
        public decimal? zfbl { get; set; }
        public string xnhgrbm { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class OutpatOptimAccInfoDto
    {
        /// <summary>
        /// Hồ sơ khám bệnh
        /// </summary>
        public OutpatAccInfoDto OutpatAccInfoDto { get; set; }
        
        /// <summary>
        /// Tập hợp hồ sơ khám bệnh
        /// </summary>
        public List<OutpatAccInfoDto> OutpatAccListDto { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public IList<OptimAccInfoDto> OptimAccInfoDto { get; set; }
    }
    public class OutpatOptimInfoDto
    {
        public OutpatAccInfoDto OutpatAccInfoDto { get; set; }
        public IList<SyncTreatmentServiceRecordVO> SyncTreatmentServiceRecordVO { get; set; }
    }
}
