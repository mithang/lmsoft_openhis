using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Tính phí hạng mục nội trú Hạng mục của Thông tin chi tiết
    /// </summary>
    public class MedicalRecordDeptVO
    {
        public string version { get; set; }
        public List<medicalList> medicalList { get; set; }
    }
    public class medicalList {
        public string Index { get; set; }
        public string gender { get; set; }
        public string age { get; set; }

        public string ageDay { get; set; }
        public string weight { get; set; }
        public string dept { get; set; }
        public string inHospitalTime { get; set; }
        public string leavingType { get; set; }
        public string[] zdList { get; set; }
        public string[] ssList { get; set; }
        public string remark { get; set; }
       
    }
}
