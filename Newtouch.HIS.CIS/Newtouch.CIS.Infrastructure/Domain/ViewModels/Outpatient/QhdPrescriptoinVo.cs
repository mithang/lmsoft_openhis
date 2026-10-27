using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Domain.ViewModels.Outpatient
{
    [XmlRoot("YBJKDATA")]
    public class QhdPrescriptoinVo
    {
        public RESPONSEDATA RESPONSEDATA { get; set; }
    }
    public class RESPONSEDATA
    {
        public string RETURNNUM { get; set; }
        public string ERRORMSG { get; set; }
        public string REFMSGID { get; set; }
        //public string OUTPUT { get; set; }
        public List<OUTROW> OUTPUT { get; set; }
    }
    //public class OUTPUT
    //{
        
    //}
    public class OUTROW
    {
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string AKC220 { get; set; }
        /// <summary>
        /// Thuốc/Mã hạng mục
        /// </summary>
        public string AKE001 { get; set; }
        /// <summary>
        /// Bảo hiểm y tếThuốc/Tên mục
        /// </summary>
        public string AKE002 { get; set; }
        /// <summary>
        /// 违phảnquythìTên
        /// </summary>
        public string BZE868 { get; set; }
        /// <summary>
        /// vi phạmMô tả
        /// </summary>
        public string AAE013 { get; set; }
        /// <summary>
        /// vi phạmcấploại
        /// </summary>
        public string BAZ969 { get; set; }
        /// <summary>
        /// hạnchếcấploại
        /// </summary>
        public string BAZ970 { get; set; }
    }
}
