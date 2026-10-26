using Newtouch.Core.Common;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IMedicineInfoDmnService
    {
        /// <summary>
        /// này bộ phận thông tin thuốc tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<MedicineInfoVO> GetMedicineInfoList(Pagination pagination, MedicineInfoParam param, string orgId);

        /// <summary>
        /// này bộ phận thông tin thuốc tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<MedicineInfoVO> GetMedicineInfoListV2(Pagination pagination, MedicineInfoParam param, string orgId);

        /// <summary>
        /// đồng bộ này bộ phận thông tin thuốc
        /// </summary>
        /// <param name="yp"></param>
        /// <param name="operateType"></param>
        /// <param name="orgId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        string FreshList(string[] yp, int operateType, string orgId, string yfbmCode);

        /// <summary>
        /// kiểm soát
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="type">1 kiểm soát 2 bình thường</param>
        /// <param name="orgId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        int ControlMedicine(string ypCode, string type, string orgId, string yfbmCode);

        /// <summary>
        /// trong bộ phát thuốc tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        IList<HandOutMedicineQueryVO> GetHandOutMedicineInfoList(Pagination pagination, QueryMedicineInfoReqVO vo);

        /// <summary>
        /// 本部门同步药品候选信息 tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<MedicineInfoVO> GetTbMedicineInfoList(Pagination pagination, MedicineInfoParam param, string orgId);

        /// <summary>
        /// lấy này bộ phận hết hạn thông tin thuốc
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<MedicineInfoVO> GetExpiredDrugsData(Pagination pagination, MedicineInfoParam param, string orgId);

        /// <summary>
        /// lấy nhà thuốc nhân viên danh sách
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysStaffVEntity> GetStaffListByOrg(string orgId);
    }
}
