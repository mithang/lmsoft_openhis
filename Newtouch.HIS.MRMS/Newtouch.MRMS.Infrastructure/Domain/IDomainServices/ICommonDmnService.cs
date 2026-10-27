using Newtouch.MR.ManageSystem.Domain.DTO;
using Newtouch.MR.ManageSystem.Domain.DTO.OutputDto;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using Newtouch.MR.ManageSystem.Domain.Entity;
#pragma warning disable CS0105 // Chỉ thị using của "Newtouch.MR.ManageSystem.Domain.ValueObjects" đã xuất hiện trước đây trong không gian tên này
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
#pragma warning restore CS0105 // Chỉ thị using của "Newtouch.MR.ManageSystem.Domain.ValueObjects" đã xuất hiện trước đây trong không gian tên này
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.MR.ManageSystem.Domain.IDomainServices
{
    public interface ICommonDmnService
    {
        IList<SysZdListDto> ZdList(string orgId, string keyword);
        IList<SysOpListDto> OpList(string orgId, string keyword, bool type = true);
        IList<SysAnesListDto> AnesList(string orgId, string keyword);
        /// <summary>
        /// Mức độ liền
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDicDto> NotchGradeList(string orgId, string keyword);
        /// <summary>
        /// Từ điển chung bệnh án
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="Rlue"></param>
        /// <returns></returns>
        IList<SysDicDto> DicCommonList(string orgId, string keyword, string Rlue, string code = null);
        /// <summary>
        /// Tính chất bệnh nhân hệ thống
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
        /// Nhân viên(Vị trí)
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
        /// Lấy nhóm thu phí lớn HIS
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<HisSfdlVO> GetHisSfdl(string orgId, string keyword);
    }
}
