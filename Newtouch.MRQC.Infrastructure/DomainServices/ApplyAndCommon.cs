using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.MRQC.Domain;
using Newtouch.MRQC.Domain.ValueObjects.Apply;
using Newtouch.MRQC.Infrastructure;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.MRQC.DomainServices
{
    public class MrApplyDmnService : IMrApplyDmnService
    {
        // Columns the grid is allowed to sort by (prevents ORDER BY injection
        // from the jqGrid sidx parameter).
        private static readonly HashSet<string> SortableColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Id", "ApplyDept", "ApplyDeptName", "Bed", "PatName", "ApplyDoctor", "ApplyDoctorName",
            "ApplyType", "MedicalName", "CompletionDate", "ApplyDate", "ApplyCompletionDate",
            "ApplyReason", "ApplyStatus", "Approver", "ApproverName", "ApproverDept",
            "ApproverDeptName", "ApprovalDate", "Ryrq", "Cyrq"
        };

        public IList<MedicalApplyVo> GetBlApplyList(Pagination pagination, string OrganizeId, string status, DateTime ksrq, DateTime jsrq)
        {
            string sql = @" select apply.Id,ApplyDept,dept.Name ApplyDeptName,Bed,PatName,ApplyDoctor,staff.Name ApplyDoctorName
,ApplyType,MedicalName,CompletionDate,ApplyDate,ApplyCompletionDate,ApplyReason,ApplyStatus
,Approver,staff1.Name ApproverName,ApproverDept,dept1.Name ApproverDeptName,ApprovalDate
,Ryrq,Cyrq
from [Mr_Apply] apply
left join NewtouchHIS_Base..V_S_Sys_Staff staff
    on apply.ApplyDoctor=staff.gh and apply.OrganizeId=staff.OrganizeId
left join NewtouchHIS_Base..V_S_Sys_Staff staff1
    on apply.Approver=staff1.gh and apply.OrganizeId=staff1.OrganizeId
left join NewtouchHIS_Base..V_S_Sys_Department dept
    on apply.ApplyDept=dept.Code and apply.OrganizeId=dept.OrganizeId
left join NewtouchHIS_Base..V_S_Sys_Department dept1
    on apply.ApproverDept=dept1.Code and apply.OrganizeId=dept1.OrganizeId
where apply.zt='1' and apply.OrganizeId=@OrgId and ApplyDate>=@ksrq and ApplyDate<=@jsrq
";
            var para = new List<SugarParameter>();
            if (!string.IsNullOrWhiteSpace(status))
            {
                sql += " and ApplyStatus=@status ";
                para.Add(new SugarParameter("@status", status));
            }
            para.Add(new SugarParameter("@OrgId", OrganizeId));
            para.Add(new SugarParameter("@ksrq", ksrq));
            para.Add(new SugarParameter("@jsrq", jsrq));

            string sortCol = "ApplyDate";
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                var first = pagination.sidx.Trim().Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(first) && SortableColumns.Contains(first))
                {
                    sortCol = first;
                }
            }
            string sortDir = string.Equals(pagination.sord, "asc", StringComparison.OrdinalIgnoreCase) ? "asc" : "desc";

            // NOTE: ORDER BY must NOT be part of the raw SQL text: SqlSugar's
            // ToPageList wraps the SQL in a derived table for the COUNT query,
            // where ORDER BY is illegal. Order via the queryable instead.
            int total = 0;
            var page = MrqcDb.GetConnection().SqlQueryable<MedicalApplyVo>(sql)
                .AddParameters(para.ToArray())
                .OrderBy($"{sortCol} {sortDir}")
                .ToPageList(pagination.page ?? 1, pagination.rows ?? 15, ref total);
            pagination.records = total;
            return page;
        }

        public void UpdateApplyStatus(string shbhlist, string OrganizeId, string Deptcode, string Gh)
        {
            // PORT FIX: the original used dbo.f_split(@shbhlist,',') but f_split
            // does not exist in the restored Newtouch_MRQC database, so the
            // original statement could never run here. Split in C# instead.
            var ids = (shbhlist ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0 && s.All(c => char.IsLetterOrDigit(c) || c == '-'))
                .ToList();
            if (ids.Count == 0)
            {
                throw new FailedException("审批失败,未找到待审批申请!");
            }
            var inParams = ids.Select((id, i) => new SugarParameter("@id" + i, id)).ToArray();
            var inList = string.Join(",", ids.Select((id, i) => "@id" + i));
            string sql = $@" update Mr_Apply set ApplyStatus=@status ,Approver=@spr,ApproverDept=@dept,ApprovalDate=@sprq
                            where zt='1' and OrganizeId=@OrganizeId and id in ({inList}) ";
            var pars = new List<SugarParameter>(inParams)
            {
                new SugarParameter("@OrganizeId", OrganizeId),
                new SugarParameter("@spr", Gh),
                new SugarParameter("@dept", Deptcode),
                new SugarParameter("@sprq", DateTime.Now),
                new SugarParameter("@status", (int)ApplyStatusEnum.ysp)
            };
            int cnt = MrqcDb.GetConnection().Ado.ExecuteCommand(sql, pars.ToArray());
            if (cnt == 0)
            {
                throw new FailedException("审批失败,未找到待审批申请!");
            }
        }
    }

    public class CommonDmnService : ICommonDmnService
    {
        public List<SysDutyStaffVO> GetStaffByDutyCode(string orgId, string dutyCode, string keyword = null)
        {
            string sql = @"SELECT DISTINCT
                                StaffName ,
                                StaffPY ,
                                StaffGh ,
                                c.Code ks ,
                                c.py kspy ,
                                c.Name ksmc
                        FROM    [NewtouchHIS_Base]..V_C_Sys_StaffDuty(nolock) AS A
                                LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) AS b ON a.StaffGh = b.gh and b.zt = '1' and b.OrganizeId = @OrganizeId
                                INNER JOIN [NewtouchHIS_Base]..V_S_Sys_Department(nolock) AS C ON b.DepartmentCode = C.Code
                                                              AND C.OrganizeId = @OrganizeId
                                                              WHERE A.zt = '1'  AND a.OrganizeId = @OrganizeId";
            var pars = new List<SugarParameter>
            {
                new SugarParameter("@OrganizeId", orgId),
                new SugarParameter("@DutyCode", dutyCode ?? "")
            };
            if (!string.IsNullOrWhiteSpace(dutyCode))
            {
                sql += "  and DutyCode = @DutyCode ";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (StaffName like @searchkeyword or StaffPY like @searchkeyword or StaffGh like @searchkeyword)";
                pars.Add(new SugarParameter("@searchkeyword", "%" + keyword + "%"));
            }
            return MrqcDb.GetConnection().SqlQueryable<SysDutyStaffVO>(sql).AddParameters(pars.ToArray()).ToList();
        }
    }
}
