using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysItemsDetailRepository : IRepositoryBase<SysItemsDetailEntity>
    {
        /// <summary>
        /// Lấy List mục từ điển chung
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysItemsDetailEntity> GetCommonList(string itemId = null, string keyword = null);

        /// <summary>
        /// Lấy List mục từ điển của một tổ chức duy nhất
        /// </summary>
        /// <param name="orgId">Id cơ sở y tế, khi không truyền thì tra cứu các mục dùng chung</param>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysItemsDetailVO> GetListByOrgId(string orgId = null, string itemId = null, string keyword = null);

        /// <summary>
        /// Cập nhật, lưu mục từ điển
        /// </summary>
        /// <param name="itemsDetailEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysItemsDetailEntity entity, string keyValue);

        List<ProjectZbVO> GetTreeViewdata(string orgId);
        IList<ProjectMxVO> GetProjectMxVO(string orgId, string kmdm);
        IList<XmandYp> GetXmandYp(string orgId, string kmdm, int? xmlx);
        int UpdateMx(string orgid, string kmdm, string xmdm, string czydm);
        int InsertMx(string orgid, string kmdm, string xmdm,string czydm);
        int DeleteMl(string orgid, string kmdm);
        List<ProjectZbVO> GetMlbyKmdm(string kmdm, string orgid);
        int InsertMl(string kmmc,string kmdm, string sjkmdm,int xmlx,int gmlbz,decimal sl, string orgid, string czydm, int zt);
        int UpdateMl(string kmmc,string kmdm, string sjkmdm,int xmlx,int gmlbz,decimal sl, string orgid, string czydm, int zt);
        List<ProjectZbVO> Getsjdm(string orgid);
    }
}
