using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.DTO;
using Newtouch.MR.ManageSystem.Domain.DTO.OutputDto;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using Newtouch.MR.ManageSystem.Domain.Entity;
#pragma warning disable CS0105 // “Newtouch.MR.ManageSystem.Domain.ValueObjects”lệnh using đã xuất hiện trước đây trong không gian tên này
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
#pragma warning restore CS0105 // “Newtouch.MR.ManageSystem.Domain.ValueObjects”lệnh using đã xuất hiện trước đây trong không gian tên này
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.ValueObjects;

namespace Newtouch.MR.ManageSystem.Domain.IDomainServices
{
    public interface ICommonDmnService
    {
        IList<SysZdListDto> ZdList(string orgId, string keyword);
        IList<SysOpListDto> OpList(string orgId, string keyword, bool type = true);
        IList<SysAnesListDto> AnesList(string orgId, string keyword);
        /// <summary>
        /// liềnvết mổ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDicDto> NotchGradeList(string orgId, string keyword);
        /// <summary>
        /// Từ điển chung của bệnh án
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="Rlue"></param>
        /// <returns></returns>
        IList<SysDicDto> DicCommonList(string orgId, string keyword, string Rlue, string code = null);
        /// <summary>
        /// Bệnh nhân hệ thốngngườiTính chất
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysBrxzVO> BrxzList(string orgId, string keyword);
        /// <summary>
        /// Hệ thống-Quốc tịch
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDicDto> DicNationalityList(string orgId, string keyword);
        /// <summary>
        /// Hệ thống-Dân tộc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDicDto> DicNationsList(string orgId, string keyword);
        /// <summary>
        /// Lấy bảng tương ứng loại bệnh án
        /// </summary>
        /// <param name="bllxId"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        string GetBllxTB(string bllx);
        /// <summary>
        /// ngườiviên (vị trí）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dutyCode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysDutyStaffVO> GetStaffByDutyCode(string orgId, string dutyCode, string keyword = null);
        /// <summary>
        /// Lấy danh sách khoa HIS
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        IList<SysDicDto> GetDeptList(string orgId, string code);

        /// <summary>
        /// LấyhisNhóm thu phí lớn
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<HisSfdlVO> GetHisSfdl(string orgId, string keyword);
    }
}
