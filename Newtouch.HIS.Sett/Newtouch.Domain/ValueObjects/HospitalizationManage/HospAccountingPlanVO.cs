using Newtouch.HIS.Domain.BusinessObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Kế hoạch ghi sổ nội trú
    /// </summary>
    public class HospAccountingPlanVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// Tính chất y lệnh
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate { get; set; }

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

        ///// <summary>
        ///// 
        ///// </summary>
        //public decimal? zje { get; set; }

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
        /// Trạng thái thực hiện
        /// </summary>
        public int zxzt { get; set; }

        /// <summary>
        /// Thời gian thực hiện lần cuối
        /// </summary>
        public DateTime? LastEexcutionTime { get; set; }

        /// <summary>
        /// Loại y lệnh 1Thuốc 2Hạng mục
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// Loại phục hồi chức năng
        /// </summary>
        public string kflb { get; set; }
        public int? zll { get; set; }
        /// <summary>
        ///Số lượng đơn vị đo
        /// </summary>
        public int? dwjls { get; set; }
        /// <summary>
        /// Chính sách tính giá
        /// </summary>
        public int? jjcl { get; set; }
        public string zxks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zxksmc { get; set; }
    }
}
