using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.ValueObjects;
using Newtouch.EMR.Domain.BusinessObjects;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using System;
using System.Collections.Generic;
using Newtouch.EMR.Domain.IRepository;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface INursingRecordDmnService : IRepositoryBase<bl_hljl_ybEntity>
    {
        IList<InpWardPatTreeVO> GetPatTree(string orgId, string zyzt, string keyword);
        void SubmitForm(bl_hljl_ybEntity entity, string keyValue);
        IList<bl_hljl_ybEntity> GetPaginationList(Pagination pagination, string orgId, DateTime? kssj, DateTime? jssj, string zyh, string wardCode, string isShowDelete);
        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);
        InpWardPatTreeVO GetPatList(string orgId, string zyh);
        int SubmitSrl(string mc, string ml,string dw, string tj, string orgId,string zyh, string rq ,string sj,string bllx, string user);
        int DeleteSrl(string orgId, string zyh, string rq, string sj, string bllx, string user);
        int DeleteScl(string orgId, string zyh, string rq, string sj, string bllx, string user);
        int SubmitScl(string mc, string ml, string dw, string ysxz, string orgId, string zyh, string rq, string sj, string bllx, string user);
        IList<bl_hljl_srsclEntity> GetSrlScl(string orgId, string zyh, string rq, string sj, string bllx);
        IList<bl_hljl_srsclEntity> GetScl(string orgId, string zyh, string rq, string sj, string bllx);
        patientInfoDto GetInfoByZyh(string zyh, string orgId);
    }
}
