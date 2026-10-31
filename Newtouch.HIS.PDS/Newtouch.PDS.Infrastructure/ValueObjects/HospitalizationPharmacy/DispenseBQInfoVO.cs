using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    //Thao tác chuẩn bị thuốc bệnh nhân khu bệnh
    public class DispenseBQInfoVO
    {
        public int zxId { get; set; }//Thực hiện y lệnhId
        public string zyh { get; set; }//Số nội trú
        public Decimal sl { get; set; }//Số lượng
        public string lyyf { get; set; }//Loại cần nhà thuốc
        public string ypCode { get; set; }//Thuốccode
        public short cls { get; set; }// Số lượng chia
        public DateTime jdrq { get; set; }//Ngày lấy hồ sơ,Ngày thao tác,Ngày lập hồ sơ
    }
    /// <summary>
    /// Lấy lô thông tin chi tiết người dùng cần phát thuốc,Số lô,Số lượng
    /// </summary>
    public class DispenseBQInfoZYFYVO
    {// 
        public string ypCode { get; set; }//Số hiệu thuốc
        public string ypph { get; set; }//Số lô
        public string yppc { get; set; }//Lô
        public string ypyjbm { get; set; }//Mã y lệnh
        public string ypsl { get; set; }//Số lượng
    }
}
