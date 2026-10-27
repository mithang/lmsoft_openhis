using NewtouchHIS.Lib.Base.Model;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.IDomainServices
{

    public interface ICommonDmnService
    {
        /// <summary>
        /// Lấy thông tin khoa
        /// </summary>
        IList<SysBqListVO> GetBqlist(string bqcode, string orgId,string staffgh);
        /// <summary>
        /// Danh sách từ điển phẫu thuật
        /// </summary>
        /// <param name="ssdm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OROperationEntity> GetOplist(string ssdm, string orgId);
        IList<OperationDicVO> GetOperations(string ssdm, string orgId);
        IList<OperationDicVO> GetOperations(string[] ssdm, string orgId);
        /// <summary>
        /// Nhân viên trong viện
        /// </summary>
        /// <param name="rygh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<StaffListVO> GetStafflist(string rygh, string orgId);
        //IList<ORStaffEntity> GetStafflist2(string Code, string orgId);
        /// <summary>
        /// Danh sách gây mê
        /// </summary>
        /// <param name="AnesCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ORAnesthesiaEntity> GetAneslistlist(string AnesCode, string orgId);
        /// <summary>
        /// Phòng phẫu thuật
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ORRoomEntity> GetRoomlist(string Code, string orgId);
        /// <summary>
        /// Cấp vết mổ
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ORNotchGradeEntity> GetNotchGradelist(string Code, string orgId);
        /// <summary>
        /// Mã lỗi hệ thống
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        IList<SysFailedCodeMessageMappVO> FailMessage(string orgId, string appId);

        /// <summary>
        /// Danh sách nhân viên theo vị trí (local replacement for the Framework binary
        /// ISysUserDmnService.GetStaffByDutyCode, same Base views as MRQC)
        /// </summary>
        IList<SysDutyStaffVO> GetStaffByDutyCode(string orgId, string dutyCode);

    }
}
