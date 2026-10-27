using NewtouchHIS.Lib.Base.Model;
using Newtouch.MRQC.Domain.Entity.QcItemManage;
using Newtouch.MRQC.Domain.ValueObjects;
using Newtouch.MRQC.Domain.ValueObjects.Apply;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Newtouch.MRQC.Domain
{
    public interface IMrApplyDmnService
    {
        IList<MedicalApplyVo> GetBlApplyList(Pagination pagination, string OrganizeId, string status, DateTime ksrq, DateTime jsrq);
        void UpdateApplyStatus(string shbhlist, string OrganizeId, string Deptcode, string Gh);
    }

    public interface IQcBlzkDmnService
    {
        void SubmitForm(ZkxmEntityVo entity);
        IList<ScoreItemVo> Getybbxbldata(string bllx, string orgId, string zyh);
        void SaveScoreDate(List<ScoreEntity> entity, string orgId, string CreatorCode, string bllx, string blmc, string zyh);
    }

    /// <summary>Staff-by-duty row (V_C_Sys_StaffDuty + staff/dept views).</summary>
    public class SysDutyStaffVO
    {
        public string StaffName { get; set; }
        public string StaffPY { get; set; }
        public string StaffGh { get; set; }
        public string ks { get; set; }
        public string kspy { get; set; }
        public string ksmc { get; set; }
    }

    public interface ICommonDmnService
    {
        List<SysDutyStaffVO> GetStaffByDutyCode(string orgId, string dutyCode, string keyword = null);
    }

    public interface IRepositoryBase<T> where T : class, new()
    {
        ISugarQueryable<T> IQueryable();
        ISugarQueryable<T> IQueryable(Expression<Func<T, bool>> predicate);
        int Delete(Expression<Func<T, bool>> predicate);
    }

    public interface IMrApplyRepo : IRepositoryBase<MrApplyEntity>
    {
    }

    public interface IQcItemDataRepo : IRepositoryBase<QcItemDataEntity>
    {
        void DeleteForm(int Id, string orgId);
    }
}
