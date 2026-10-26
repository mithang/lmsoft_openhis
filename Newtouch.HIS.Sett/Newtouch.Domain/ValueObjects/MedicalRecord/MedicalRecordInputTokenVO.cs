using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Tính phí hạng mục nội trú Hạng mục của Thông tin chi tiết
    /// </summary>
    public class MedicalRecordInputTokenVO
    {
       
        public int HttpStatus { get; set; }
        public string Message { get; set; }
        public BusData BusData { get; set; }
        
    }
    public class BusData
    {
        public int code { get; set; }
        public string msg { get; set; }
        public Tokendata data { get; set; }
    }
    public class Tokendata
    {
        public string Token { get; set; }
    }
}
