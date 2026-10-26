using Newtouch.Common;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.HIS.Sett.Request.HospitalizationPharmacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.HospitalizationManage
{
    //Giao diện phát thuốc nội trú
    public interface IDispenseIndexDmnService
    {
        #region PID 住院发药


        /// <summary>
        /// Khu bệnh phát thuốc nội trú
        /// </summary>
        /// <param name="zyyf"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<DispenseBQIndexVO> GetZYFYBQPagList(string zyyf, string organizeId);

        /// <summary>
        /// Bệnh nhân phát thuốc nội trú
        /// </summary>
        /// <param name="zyyf"></param>
        /// <param name="bq"></param>
        /// <returns></returns>
        IList<DispenseBQRYIndexVO> GetZYFYBRPagList(string zyyf, string bq);


        /// <summary>
        /// Tập hợp thông tin phát thuốc nội trú
        /// </summary>
        /// <param name="bq"></param>
        /// <param name="Zyh"></param>
        /// <param name="Kssj"></param>
        /// <param name="Jssj"></param>
        /// <param name="Cl"></param>
        /// <param name="Fyzt"></param>
        /// <returns></returns>
        IList<DispenseBQRYXXIndexVO> GetZYFYXXPagList(WaitingDispenseBRXXMedicineDispenseRequest model);


        /// <summary>
        /// Pha thuốc khu bệnh cho bệnh nhân nội trú
        /// </summary>
        /// <param name="UserLogin"></param>
        /// <param name="Kssj"></param>
        /// <param name="Jssj"></param>
        /// <returns></returns>
        IList<DispenseBQBRPYIndexVO> SetZYBRBQPY(WaitingDispenseBQBRPYMedicineDispenseRequest model);

        /// <summary>
        /// Sau khi bệnh nhân khu bệnh nội trú phát thuốc/trả thuốc thì sửa trạng thái ghi chú phát thuốc
        /// </summary>
        /// <param name="UserLogin"></param>
        /// <param name="Kssj"></param>
        /// <param name="Jssj"></param>
        /// <returns></returns>
        int UpBQPYyzzx(WaitingDispenseBQPYyzzxZTDispenseRequest model);

        #endregion

        #region 住院发药查询
        //Tra cứu phát thuốc nội trú
        IList<DispenseBQRYCXXXIndexVO> GetZYFYCXXXPagList(WaitingDispenseBRCXXXMedicineDispenseRequest model);

        #endregion

        #region 住院退药明细信息
        /// <summary>
        /// Lấy y lệnh trạm bác sĩID ,Bảng y lệnhID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<ModelZyTyYszyzIdInfoVO> GetZYYszysList(WaitingDispenseTYYszyzIdDispenseRequest model);

        IList<DispenseBQRYTYXXIndexVO> GetZYTYXXPagList(WaitingDispenseBRTYXXMedicineDispenseRequest model);
        #endregion
    }
}
