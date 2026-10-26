using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    //Thao tác xếp thuốc cho bệnh nhân khu bệnh
    public class DispenseBQInfoVO
    {
        public int zxId { get; set; }//thực hiện y lệnh Id
        public string zyh { get; set; }//số nội trú
        public Decimal sl { get; set; }//số lượng
        public string lyyf { get; set; }//loại cần nhà thuốc
        public string ypCode { get; set; }//thuốc code
        public short cls { get; set; }// số lượng tách
        public DateTime jdrq { get; set; }//ngày rút hồ sơ, thao tác ngày,lập hồ sơ ngày
    }
    /// <summary>
    /// lấy cần phát thuốc của người dùng của thông tin chi tiết lô, số lô, số lượng
    /// </summary>
    public class DispenseBQInfoZYFYVO
    {// 
        public string ypCode { get; set; }//số hiệu thuốc
        public string ypph { get; set; }//số lô
        public string yppc { get; set; }//lô
        public string ypyjbm { get; set; }//y lệnh mã
        public string ypsl { get; set; }//số lượng
    }
}
