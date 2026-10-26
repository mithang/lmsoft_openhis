using Newtouch.Core.Common;
using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Sett.Request.HospitalizationPharmacy
{

    #region 发药
    /// <summary>
    /// Tiếp nhận khu bệnh, phát thuốc nhập viện khu bệnh
    /// </summary>
    public class WaitingDispenseBQMedicineDispenseRequest : RequestBase
    {
        /// <summary>
        /// Khi trư ớc loại zyyf nhập viện nhà thuốc 
        /// </summary>
        public string zyyf { get; set; }
    }
    /// <summary>
    /// Tiếp nhận khu bệnh, phát thuốc nhập viện bệnh nhân
    /// </summary>
    public class WaitingDispenseMedicineDispenseRequest : OrgRequestBase
    {
        public string zyyf { get; set; }
        public string bq { get; set; }
    }

    /// <summary>
    /// Tiếp nhận khu bệnh, phát thuốc nhập viện nhân sự thông tin chi tiết
    /// </summary>
    public class WaitingDispenseBRXXMedicineDispenseRequest : OrgRequestBase
    {//Newtouch.Common
        public Pagination pagination { get; set; } //Phân trang
        public string organizeId { get; set; }// ngư ời dùng quyền hạn 
        public string zyyf { get; set; }// ngư ời dùng Đ ăng nhập loại 
        public string Bq { get; set; }//Khu bệnh
        public string Zyh { get; set; }//Bệnh nhân
        public string ypCode { get; set; }//ThuốcCode
        public string Kssj { get; set; }//Thời gian bắt đ ầu
        public string Jssj { get; set; }//Thời gian kết thúc
        public string Cl { get; set; } //dài hạn/tạm thời 
        public string Fyzt { get; set; }// phát thuốc trạng thái
    }
    #endregion

    #region 配药

    /// <summary>
    /// Cấp phát thuốc bệnh nhân khu bệnh
    /// </summary>
    public class WaitingDispenseBQBRPYMedicineDispenseRequest : RequestBase
    {
        public string Bq { get; set; }//Khu bệnh
        public string lyyf { get; set; }//Đ ăng nhập y lệnh loại 
        public string UserLogin { get; set; }//Thao tác ngư ời dùng 
        public string Kssj { get; set; }//Thời gian bắt đ ầu
        public string Jssj { get; set; }//Thời gian kết thúc

    }

    /// <summary>
    /// Tra cứu ngư ời dùng cấp phát thuốc hoàn thành,sửa phát thuốc chuẩn bị đủtrạng thái
    /// </summary>
    public class WaitingDispenseBQPYyzzxZTDispenseRequest : OrgRequestBase
    {
        public string pyry { get; set; } //Nhân sự cấp phát thuốc 
        public string pyrq { get; set; } //Ngày cấp phát thuốc 
        public string yzId { get; set; } // y lệnh thực hiện ID
        public string tdrq { get; set; } // Ngày rút hồ sơ, ngày lập hồ sơ 
        public string fybz { get; set; } // Khi trư ớc phát thuốc trạng thái
        public string ypdj { get; set; }//Thu ốcĐ ơn giá
        public string ypje { get; set; } //ThuốcSố tiền
        public string zfbl { get; set; }// Tỷ lệ tự trả
        public string zfxz { get; set; }// Tính chất tự trả
    }
    #endregion

    #region 病区发药查询
    /// <summary>
    /// Tiếp nhận khu bệnh, gọi bệnh nhân
    /// </summary>
    public class WaitingDispenseBRCXXXMedicineDispenseRequest : OrgRequestBase
    {//Newtouch.Common
        public Pagination pagination { get; set; } //Phân trang
        public string Bq { get; set; }//Khu bệnh
        public string Zyh { get; set; }//Bệnh nhân
        public string zyyf { get; set; }//Phát thuốc nội trú 
        public string ypCode { get; set; }//ThuốcCode
        public string Kssj { get; set; }//Thời gian bắt đ ầu
        public string Jssj { get; set; }//Thời gian kết thúc
        public string Cl { get; set; } //dài hạn/tạm thời 
        public string Fyzt { get; set; }// phát thuốc trạng thái

        public List<ModelBQBRYZZXTYInfoVO> YpTymxxxList { get; set; }// trả thuốc chi tiết tập hợp List
    }
    #endregion

    #region 退药
    /// <summary>
    /// Tiếp nhận tra cứu dòng vụ trả thuốc 
    /// </summary>
    public class WaitingDispenseBRTYXXMedicineDispenseRequest : OrgRequestBase
    {
        public string organizeId { get; set; }// ngư ời dùng quyền hạn 
        public string zyyf { get; set; }// ngư ời dùng Đ ăng nhập loại 
        public string Bq { get; set; }//Khu bệnh
        public string Zyh { get; set; }//Bệnh nhân
        public string ypCode { get; set; }//ThuốcCode
        public string Kssj { get; set; }//Thời gian bắt đ ầu
        public string Jssj { get; set; }//Thời gian kết thúc
        public string Cl { get; set; } //dài hạn/tạm thời 
        public List<ModelBQBRYZZXTYInfoVO> YpTymxxxList { get; set; }// trả thuốc chi tiết tập hợp List

    }

    /// <summary>
    /// Tiếp nhận bảng chi tiết trả thuốc, bản ghi có thể trả thuốc của bảng lô số y lệnh 
    /// </summary>
    public class ModelBQBRYZZXTYInfoVO
    {
        public string yszyzid { get; set; } // y lệnh ID yszyzid bác sĩ nhóm y lệnh ID
        public DateTime? tdrq { get; set; } // Ngày trả lại hồ sơ 
        public string ypCode { get; set; } //ThuốcCode


        public string tysl { get; set; }// trả thuốc số lư ợng
        public string tymxId { get; set; }// trả thuốc chi tiếtID
        public string tybz { get; set; }// trả thuốc dấu hiệu 
        public DateTime? tyjlrq { get; set; }// trả thuốc ngày ghi chép
        public string tyxh { get; set; }// trả thuốc thứ tự số 
        public string tyry { get; set; }// trả thuốc nhân sự 
        public DateTime? tyrq { get; set; }// trả thuốc ngày 
        public string yppc { get; set; }// y lệnh thực hiện lô số bảng lô lần 
        public string ypph { get; set; }// y lệnh thực hiện lô số bảng lô số 
        public decimal sl { get; set; }//Số lư ợng
        public string tysqczyh { get; set; }
    }

    public class WaitingDispenseTYYszyzIdDispenseRequest : OrgRequestBase
    {
        public string yzdm { get; set; }// y lệnh mã
        public string organizeId { get; set; }// quyền hạn 
        public List<ModelZyTyYszyzIdInfoVO> yszyzList { get; set; }// trả thuốc chi tiết tập hợp List
    }
    public class ModelZyTyYszyzIdInfoVO
    {
        public int yszyzid { get; set; } // y lệnh ID yszyzid trạm bác sĩ y lệnh ID
        public int yzid { get; set; } // y lệnh bảng ID 
    }
    #endregion
}
