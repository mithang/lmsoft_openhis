using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.BusinessObjects;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
    /// <summary>
    /// Ghi sổ ngoại trú griddto
    /// </summary>
    public class OutpatAccGridInfoDto
    {
        public string newid { get; set; }
        public string sfxmCode { get; set; }
        public int sl { get; set; }
        public int duration { get; set; }
        public decimal dj { get; set; }
        public decimal Zje { get; set; }
        public string sfdlCode { get; set; }
        public DateTime jzsj { get; set; }
        public string bz { get; set; }
        public byte ttbz { get; set; }//1Thể hiện điều trị tập thể
        public string kflb { get; set; }
        public string dw { get; set; }
        public string cfh { get; set; }
        /// <summary>
        /// 1Thể hiện là thuốc 2Thể hiện là hạng mục
        /// </summary>
        public string yzlx { get; set; }
        public IList<InpatientAccountingPlanItemDoctorDto> ysList { get; set; }
        public int? clzt { get; set; }
        public int? jsmxnm { get; set; }
        public string czlx { get; set; }
        public string jfbId { get; set; }
        public int? zll { get; set; }
        /// <summary>
        /// Tính chất y lệnh 1Thể hiện tạm thời,2Thể hiện dài hạn
        /// </summary>
        public string yzxz { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string jzjhmxId { get; set; }
        public int? dwjls { get; set; }
        public int? jjcl { get; set; }
        public int? xmnm { get; set; }
        public decimal? zfbl { get; set; }
        public string zfxz { get; set; }
    }

    public class OutGridInfoDto2018
    {
        public string newid { get; set; }
        public string sfxmCode { get; set; }
        public int sl { get; set; }
        public decimal dj { get; set; }
        public decimal zje { get; set; }
        public string sfdlCode { get; set; }
        public string dw { get; set; }
        public string cfh { get; set; }
        public int cfnm { get; set; }
        /// <summary>
        /// 1Thể hiện là thuốc 2Thể hiện là hạng mục
        /// </summary>
        public string yzlx { get; set; }
        public int? zll { get; set; }
        public int? dwjls { get; set; }
        public int? jjcl { get; set; }
        public int? zlcs { get; set; }
        public decimal? jl { get; set; }
        public string jldw { get; set; }
        public string czlx { get; set; }
        public decimal? zfbl { get; set; }
        public string zfxz { get; set; }
        public string cflx { get; set; }
    }
    public class OutServiceInfoDto2018
    {
        public List<OutxmDto2018> OutxmDto { get; set; }
        public List<OutcfDto2018> OutcfDto { get; set; }
    }

    public class OutxmDto2018
    {
        public string sfxmCode { get; set; }
        public int sl { get; set; }
        public decimal dj { get; set; }
        public decimal zje { get; set; }
        public string sfdlCode { get; set; }
        public string dw { get; set; }
        public int? zll { get; set; }
        public int? dwjls { get; set; }
        public int? jjcl { get; set; }
        public int? zlcs { get; set; }
        public decimal? zfbl { get; set; }
        public string zfxz { get; set; }
    }

    public class OutcfDto2018
    {
        public string cfh { get; set; }
        public int cfnm { get; set; }
        public string czlx { get; set; }
        public IList<OutcfmxDto2018> cfmxDto { get; set; }

    }

    public class OutcfmxDto2018
    {
        public string sfxmCode { get; set; }
        public int sl { get; set; }
        public decimal dj { get; set; }
        public decimal zje { get; set; }
        public string sfdlCode { get; set; }
        public string dw { get; set; }
        public decimal? jl { get; set; }
        public string jldw { get; set; }
        /// <summary>
        /// Bộ phận nhà thuốc
        /// </summary>
        public string yfbm { get; set; }
        public decimal? zfbl { get; set; }
        public string zfxz { get; set; }
    }
}
