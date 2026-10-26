using Newtouch.HIS.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.DTO.OutputDto;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// Quyết toán nhập viện Giao diện
    /// </summary>
    public interface IHospSettApp
    {
        /// <summary>
        /// Quyết toán nhập viện,XemThông tin quyết toán
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        HosSettPatStatusDetailDto GetPatHospStatusDetail(string zyh, string kh);

        /// <summary>
        /// Quyết toán nhập viện bệnh nhân phân loạithu phí 预览
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="jsje">Số tiền quyết toán ngăn chặn quá trình trong củachi phí thay đ ổi </param>
        HosSettPatClassifyChargePreviewDto GetHospSettPatClassifyChargePreview(string zyh, DateTime expectedcyrq, decimal jsje);

        /// <summary>
        /// theo số nhập viện lấy lần này quyết toán tính phí theo loại lớn (phân loại hạng mục)
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<HospFeeClassifyWithDLBO> GetHospFeeClassifyWithDLBOList(string zyh);

        /// <summary>
        /// GửiQuyết toán,LưuKết quả
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedjsje"></param>
        /// <param name="expectedzhaoling"></param>
        /// <param name="xjzfListStr"></param>
        SettSaveSuccessResultDto SaveSett(string zyh, DateTime expectedcyrq, string fph, decimal expectedyjjzhye, decimal expectedjsje, decimal expectedzhaoling, string xjzfListStr, decimal shishoukuan);

    }
}
