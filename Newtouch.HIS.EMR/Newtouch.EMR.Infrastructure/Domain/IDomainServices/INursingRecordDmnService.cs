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
        void DeleteForm(string keyValue, string orgId);
        InpWardPatTreeVO GetPatList(string orgId, string zyh);
        // G18: thay 4 method SubmitSrl/DeleteSrl/SubmitScl/DeleteScl rời rạc
        // (delete ngoài transaction, insert thiếu Id/CreatorCode) bằng 2
        // method batch: xóa cũ + chèn mới trong MỘT transaction.
        void SaveSrlBatch(IList<string[]> segments, string orgId, string zyh, string rq, string sj, string bllx, string user);
        void SaveSclBatch(IList<string[]> segments, string orgId, string zyh, string rq, string sj, string bllx, string user);
        IList<bl_hljl_srsclEntity> GetSrlScl(string orgId, string zyh, string rq, string sj, string bllx);
        IList<bl_hljl_srsclEntity> GetScl(string orgId, string zyh, string rq, string sj, string bllx);
        patientInfoDto GetInfoByZyh(string zyh, string orgId);
    }
}
