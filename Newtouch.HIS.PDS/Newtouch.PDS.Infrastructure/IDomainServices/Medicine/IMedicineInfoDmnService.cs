using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IMedicineInfoDmnService
    {
        /// <summary>
        /// Thông tin thuốc của bộ phận này Truy vấn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<MedicineInfoVO> GetMedicineInfoList(Pagination pagination, MedicineInfoParam param, string orgId);

        /// <summary>
        /// Thông tin thuốc của bộ phận này Truy vấn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<MedicineInfoVO> GetMedicineInfoListV2(Pagination pagination, MedicineInfoParam param, string orgId);

        /// <summary>
        /// Đồng bộ thông tin thuốc bộ phận này
        /// </summary>
        /// <param name="yp"></param>
        /// <param name="operateType"></param>
        /// <param name="orgId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        string FreshList(string[] yp, int operateType, string orgId, string yfbmCode);

        /// <summary>
        /// Kiểm soát
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="type">1 Kiểm soát 2 Bình thường</param>
        /// <param name="orgId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        int ControlMedicine(string ypCode, string type, string orgId, string yfbmCode);

        /// <summary>
        /// Truy vấn phát thuốc nội bộ
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        IList<HandOutMedicineQueryVO> GetHandOutMedicineInfoList(Pagination pagination, QueryMedicineInfoReqVO vo);

        /// <summary>
        /// Thông tin ứng viên thuốc đồng bộ bộ phận này Truy vấn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<MedicineInfoVO> GetTbMedicineInfoList(Pagination pagination, MedicineInfoParam param, string orgId);

        /// <summary>
        /// Lấy thông tin thuốc hết hạn bộ phận này
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<MedicineInfoVO> GetExpiredDrugsData(Pagination pagination, MedicineInfoParam param, string orgId);

        /// <summary>
        /// Lấy danh sách nhân viên nhà thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysStaffVEntity> GetStaffListByOrg(string orgId);
    }
}
