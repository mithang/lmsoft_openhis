using NewtouchHIS.Lib.Base.Model;
using Newtouch.EMR.APIRequest.Bljgh.Request;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.IRepository
{
    public interface Ibl_ElementDomainRepo : IRepositoryBase<bl_ElementDomainEntity>
    {
        List<bl_ElementDomainEntity> GetElementTree(string orgId);

        IList<BlysGridVo> GetElementDetail(Pagination pagination, string OrganizeId, string keyword);

        List<BlysListVo> GetBlys(string keyword, string orgId);

        List<TableColumnVo> GetTabColumnList(string tabName, string keyword);
        List<BlysDeatilVo> GetBlysmx(string keyword, string orgId);

        void SubmitForm(bl_ElementDomainEntity entity, string keyValue);
        void DeleteForm(string keyValue, string orgId, string user);

        #region Chuyển đổi dữ liệu bảng dọc cấu trúc bệnh án sang bảng ngang
        void BljghDataDealwith(BljghReq req);

        #endregion
    }
}
