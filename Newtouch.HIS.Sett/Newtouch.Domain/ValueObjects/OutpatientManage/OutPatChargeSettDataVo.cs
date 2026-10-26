/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description: Dữ liệu quyết toán thu phí ngoại trú
// Author:HLF
// CreateDate: 2016/12/29 13:38:52 
//**********************************************************/

using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OutPatChargeSettDataVo
    {

        //Thêm mới  Đơn thuốc ngoại trú,Chi tiết đơn thuốc ngoại trú,Hạng mục ngoại trú,
        //Cập nhật  Số hóa đơn hiện tại
        //Thêm mới  Quyết toán ngoại trú,Chi tiết quyết toán ngoại trú, Phương thức thanh toán quyết toán ngoại trú,Loại lớn quyết toán ngoại trú

        //Bảng đơn thuốc ngoại trú mz_cf
        //Nếu đổi đơn thuốc  Bảng đơn thuốc cầnList
        public List<OutpatientPrescriptionEntity> mz_cf { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốc ngoại trú  mz_cfmx
        /// </summary>
        public List<OutpatientPrescriptionDetailEntity> mz_cfmxList { get; set; }

        //mz_xm
        /// <summary>
        /// Hạng mục ngoại trú
        /// </summary>
        public List<OutpatientItemEntity> mz_xmList { get; set; }


        //update Hóa đơn tài chính Cập nhật số hóa đơn hiện tại //cw_fp
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Quyết toán ngoại trú mz_js
        /// </summary>
        public OutpatientSettlementEntity mz_js { get; set; }

        /// <summary>
        /// Phương thức thanh toán quyết toán ngoại trú  mz_jszffs
        /// </summary>
        public OutpatientSettlementPaymentModelEntity mz_jszffs { get; set; }

        /// <summary>
        /// Chi tiết quyết toán ngoại trú mz_jsmx
        /// </summary>
        public List<OutpatientSettlementDetailEntity> mz_jsmxList { get; set; }

        /// <summary>
        /// Loại lớn quyết toán ngoại trú mz_jsdl
        /// </summary> 
        public List<OutpatientSettlementCategoryEntity> mz_jsdlList { get; set; }

    }

    public class OutPatChargeSettInAccDataVo
    {

        //Thêm mới  Quyết toán ngoại trú,Chi tiết quyết toán ngoại trú, Phương thức thanh toán quyết toán ngoại trú,Loại lớn quyết toán ngoại trú

        //Bảng đơn thuốc ngoại trú mz_cf
        //Nếu đổi đơn thuốc  Bảng đơn thuốc cầnList
        public List<OutpatientPrescriptionEntity> mz_cf { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốc ngoại trú  mz_cfmx
        /// </summary>
        public List<OutpatientPrescriptionDetailEntity> mz_cfmxList { get; set; }

        //mz_xm
        /// <summary>
        /// Hạng mục ngoại trú
        /// </summary>
        public List<OutpatientItemEntity> mz_xmList { get; set; }


        /// <summary>
        /// Quyết toán ngoại trú mz_js
        /// </summary>
        public List<OutpatientSettlementEntity> mz_js { get; set; }

        /// <summary>
        /// Phương thức thanh toán quyết toán ngoại trú  mz_jszffs
        /// </summary>
        public List<OutpatientSettlementPaymentModelEntity> mz_jszffs { get; set; }

        /// <summary>
        /// Chi tiết quyết toán ngoại trú mz_jsmx
        /// </summary>
        public List<OutpatientSettlementDetailEntity> mz_jsmxList { get; set; }

        /// <summary>
        /// Loại lớn quyết toán ngoại trú mz_jsdl
        /// </summary> 
        public List<OutpatientSettlementCategoryEntity> mz_jsdlList { get; set; }

    }

    #region 门诊收费 Vision-1.3 门诊记账的第三个版本
    public class OutPatSettInAccDataVo
    {

        //Thêm mới  Đơn thuốc ngoại trú Chi tiết đơn thuốc ngoại trú Quyết toán ngoại trú Chi tiết quyết toán ngoại trú Hạng mục ngoại trú Kế hoạch ghi sổ ngoại trú Chi tiết kế hoạch ghi sổ ngoại trú

        //Bảng đơn thuốc ngoại trú mz_cf
        //Nếu đổi đơn thuốc  Bảng đơn thuốc cầnList
        public List<OutpatientPrescriptionEntity> mz_cf { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốc ngoại trú  mz_cfmx
        /// </summary>
        public List<OutpatientPrescriptionDetailEntity> mz_cfmxList { get; set; }

        //mz_xm
        /// <summary>
        /// Hạng mục ngoại trú
        /// </summary>
        public List<OutpatientItemEntity> mz_xmList { get; set; }


        /// <summary>
        /// Quyết toán ngoại trú mz_js
        /// </summary>
        public List<OutpatientSettlementEntity> mz_js { get; set; }

        /// <summary>
        /// Chi tiết quyết toán ngoại trú mz_jsmx
        /// </summary>
        public List<OutpatientSettlementDetailEntity> mz_jsmxList { get; set; }
        public OutpatientAccountEntity mz_jzjh { get; set; }
        public List<OutpatientAccountDetailEntity> mz_jzjhmx { get; set; }

    }

    public class updatePatSettInAccDataVo
    {

        //Thay đổi  Đơn thuốc ngoại trú Chi tiết đơn thuốc ngoại trú Quyết toán ngoại trú Chi tiết quyết toán ngoại trú Hạng mục ngoại trú Kế hoạch ghi sổ ngoại trú Chi tiết kế hoạch ghi sổ ngoại trú

        //Bảng đơn thuốc ngoại trú mz_cf
        //Nếu đổi đơn thuốc  Bảng đơn thuốc cầnList
        public Dictionary<OutpatientPrescriptionEntity, OutpatientPrescriptionEntity> mz_cf { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốc ngoại trú  mz_cfmx
        /// </summary>
        public Dictionary<OutpatientPrescriptionDetailEntity, OutpatientPrescriptionDetailEntity> mz_cfmxList { get; set; }

        //mz_xm
        /// <summary>
        /// Hạng mục ngoại trú
        /// </summary>
        public Dictionary<OutpatientItemEntity, OutpatientItemEntity> mz_xmList { get; set; }


        /// <summary>
        /// Quyết toán ngoại trú mz_js
        /// </summary>
        public Dictionary<OutpatientSettlementEntity, OutpatientSettlementEntity> mz_js { get; set; }

        /// <summary>
        /// Chi tiết quyết toán ngoại trú mz_jsmx
        /// </summary>
        public Dictionary<OutpatientSettlementDetailEntity, OutpatientSettlementDetailEntity> mz_jsmxList { get; set; }
        public Dictionary<OutpatientAccountEntity, OutpatientAccountEntity> mz_jzjh { get; set; }
        public Dictionary<OutpatientAccountDetailEntity, OutpatientAccountDetailEntity> mz_jzjhmx { get; set; }
    }

    public class DelPatSettInAccDataVo
    {

        //Thay đổi  Đơn thuốc ngoại trú Chi tiết đơn thuốc ngoại trú Quyết toán ngoại trú Chi tiết quyết toán ngoại trú Hạng mục ngoại trú Kế hoạch ghi sổ ngoại trú Chi tiết kế hoạch ghi sổ ngoại trú

        //Bảng đơn thuốc ngoại trú mz_cf
        //Nếu đổi đơn thuốc  Bảng đơn thuốc cầnList
        public List<int> mz_cf { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốc ngoại trú  mz_cfmx
        /// </summary>
        public List<int> mz_cfmxList { get; set; }

        //mz_xm
        /// <summary>
        /// Hạng mục ngoại trú
        /// </summary>
        public List<int> mz_xmList { get; set; }


        /// <summary>
        /// Quyết toán ngoại trú mz_js
        /// </summary>
        public List<int> mz_js { get; set; }

        /// <summary>
        /// Chi tiết quyết toán ngoại trú mz_jsmx
        /// </summary>
        public List<int> mz_jsmxList { get; set; }
        public List<string> mz_jzjh { get; set; }
        public List<string> mz_jzjhmx { get; set; }
    }
    #endregion

    #region  optima记账 Vision-1.2 门诊记账的第二个版本（包括门诊记账）
    /// <summary>
    /// Khám ngoại trú
    /// </summary>
    public class SettInAccOutpatDataVo
    {
        //mz_xm
        /// <summary>
        /// Hạng mục ngoại trú
        /// </summary>
        public List<OutpatientItemEntity> mz_xmList { get; set; }

        public List<OutpatientPrescriptionEntity> mz_cf { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốc ngoại trú  mz_cfmx
        /// </summary>
        public List<OutpatientPrescriptionDetailEntity> mz_cfmxList { get; set; }
    }

    /// <summary>
    /// Sửa ghi sổ ngoại trú
    /// </summary>
    public class updateInAccOutpatVo
    {
        //mz_xm
        /// <summary>
        /// Hạng mục ngoại trú
        /// </summary>
        public Dictionary<OutpatientItemEntity, OutpatientItemEntity> mz_xmList { get; set; }

        public Dictionary<OutpatientPrescriptionEntity, OutpatientPrescriptionEntity> mz_cf { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốc ngoại trú  mz_cfmx
        /// </summary>
        public Dictionary<OutpatientPrescriptionDetailEntity, OutpatientPrescriptionDetailEntity> mz_cfmxList { get; set; }
    }

    /// <summary>
    /// Xóa ngoại trú
    /// </summary>
    public class delInAccOutpatVo
    {
        //mz_xm
        /// <summary>
        /// Hạng mục ngoại trú
        /// </summary>
        public List<int> mz_xmList { get; set; }

        public List<int> mz_cf { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốc ngoại trú  mz_cfmx
        /// </summary>
        public List<int> mz_cfmxList { get; set; }
    }
    /// <summary>
    /// Nhập viện
    /// </summary>
    public class SettInAccHospatDataVo
    {
        /// <summary>
        /// Bảng tính phí hạng mục
        /// </summary>
        public List<HospItemBillingEntity> zy_xmjfbList { get; set; }

        /// <summary>
        /// Bảng tính phí thuốc
        /// </summary>
        public List<HospDrugBillingEntity> zy_ypjfbList { get; set; }
    }


    /// <summary>
    /// Sửa ghi sổ nội trú
    /// </summary>
    public class updateInAccHospatVo
    {
        /// <summary>
        /// Bảng tính phí hạng mục
        /// </summary>
        public Dictionary<HospItemBillingEntity, HospItemBillingEntity> zy_xmjfbList { get; set; }

        /// <summary>
        /// Bảng tính phí thuốc
        /// </summary>
        public Dictionary<HospDrugBillingEntity, HospDrugBillingEntity> zy_ypjfbList { get; set; }
    }

    /// <summary>
    /// Xóa ghi sổ nội trú
    /// </summary>
    public class delInAccHospatVo
    {
        /// <summary>
        /// Bảng tính phí hạng mục
        /// </summary>
        public List<string> zy_xmjfbList { get; set; }

        /// <summary>
        /// Bảng tính phí thuốc
        /// </summary>
        public List<string> zy_ypjfbList { get; set; }
    }
    #endregion

    #region 门诊收费 Vision-1.4 门诊记账的第四个版本 
    public class OutPatSettInAccDataVo4
    {

        //Thêm mới  Quyết toán ngoại trú Chi tiết quyết toán ngoại trú Hạng mục ngoại trú Kế hoạch ghi sổ ngoại trú Chi tiết kế hoạch ghi sổ ngoại trú

        //mz_xm
        /// <summary>
        /// Hạng mục ngoại trú
        /// </summary>
        public List<OutpatientItemEntity> mz_xmList { get; set; }


        /// <summary>
        /// Quyết toán ngoại trú mz_js
        /// </summary>
        public List<OutpatientSettlementEntity> mz_js { get; set; }

        /// <summary>
        /// Chi tiết quyết toán ngoại trú mz_jsmx
        /// </summary>
        public List<OutpatientSettlementDetailEntity> mz_jsmxList { get; set; }
        public OutpatientAccountEntity mz_jzjh { get; set; }
        public List<OutpatientAccountDetailEntity> mz_jzjhmx { get; set; }

    }

    public class updatePatSettInAccDataVo4
    {

        //Thay đổi Quyết toán ngoại trú Chi tiết quyết toán ngoại trú Hạng mục ngoại trú Kế hoạch ghi sổ ngoại trú Chi tiết kế hoạch ghi sổ ngoại trú

        //mz_xm
        /// <summary>
        /// Hạng mục ngoại trú
        /// </summary>
        public Dictionary<OutpatientItemEntity, OutpatientItemEntity> mz_xmList { get; set; }


        /// <summary>
        /// Quyết toán ngoại trú mz_js
        /// </summary>
        public Dictionary<OutpatientSettlementEntity, OutpatientSettlementEntity> mz_js { get; set; }

        /// <summary>
        /// Chi tiết quyết toán ngoại trú mz_jsmx
        /// </summary>
        public Dictionary<OutpatientSettlementDetailEntity, OutpatientSettlementDetailEntity> mz_jsmxList { get; set; }
        public Dictionary<OutpatientAccountEntity, OutpatientAccountEntity> mz_jzjh { get; set; }
        public Dictionary<OutpatientAccountDetailEntity, OutpatientAccountDetailEntity> mz_jzjhmx { get; set; }
    }
    #endregion
}
