
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.EMR.Domain.DTO;
using Newtouch.EMR.Domain.DTO.InputDto.MRHomePage;
using Newtouch.EMR.Domain.DTO.OutputDto.MRHomePage;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using System.Collections.Generic;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface ICommonDmnService
    {
        /// <summary>
        /// Lấy tên và cờ loại lớn của bệnh án theo Id loại bệnh án
        /// </summary>
        /// <param name="bllxId"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        string GetBllxmcFlag(string bllxId, string OrgId);
        /// <summary>
        /// Lấy bảng tương ứng loại bệnh án
        /// </summary>
        /// <param name="bllxId"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        string GetBllxTB(int bllx);
        /// <summary>
        /// Lấy thông tin liên quan đến loại lớn bệnh án qua bllx
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="Code"></param>
        /// <param name="bllx">EnumBllx</param>
        /// <returns></returns>
        IList<MedRecordTypeVO> GetSysItemDicBybllx(string Code, int bllx);
        /// <summary>
        /// Lấy thông tin vị trí tương ứng với mẫu
        /// </summary>
        /// <returns></returns>
        IList<BlmbqxkzEntity> GetblmbDuty(string blmbId, OperatorModel user);
        /// <summary>
        /// Phán đoán có phải quản trị viên mẫu hay không
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool GetIsblManager(OperatorModel user);
        /// <summary>
        /// Chẩn đoán
        /// </summary>  
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<DiagnoseDTO> GetList(string orgId);
        IList<DiagnoseDTO> GetList(Pagination pagination,string orgId, string keyword,int? zdlx, string ybnhlx = "");

        IList<SysZdListDto> ZdList(string orgId, string keyword, string zdlx = "", string ybnhlx = "");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="type">Có tối ưu truy vấn hay không: 1 Có, 0 Không</param>
        /// <returns></returns>
        IList<SysOpListDto> OpList(string orgId, string keyword, bool type = true);
        /// <summary>
        /// Danh sách phẫu thuật phân trang
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysOpListDto> OpListPage(Pagination pagination, string orgId, string keyword);
        IList<SysAnesListDto> AnesList(string orgId, string keyword);
        /// <summary>
        /// Cấp độ liền vết thương
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDicDto> NotchGradeList(string orgId, string keyword);
        /// <summary>
        /// Từ điển dùng chung của hồ sơ bệnh án
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="Rlue"></param>
        /// <returns></returns>
        IList<SysDicDto> DicCommonList(string orgId, string keyword, string Rlue, string code = null);
        ///// <summary>
        ///// Tính chất bệnh nhân của hệ thống
        ///// </summary>
        ///// <param name="orgId"></param>
        ///// <param name="keyword"></param>
        ///// <returns></returns>
        //IList<SysBrxzVO> BrxzList(string orgId, string keyword);
        /// <summary>
        /// Hệ thống - Quốc tịch
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDicDto> DicNationalityList(string orgId, string keyword);
        /// <summary>
        /// Hệ thống - Dân tộc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDicDto> DicNationsList(string orgId, string keyword);
        /// <summary>
        /// Nhân sự (vị trí)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dutyCode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysDutyStaffVO> GetStaffByDutyCode(string orgId, string dutyCode, string keyword = null);
        /// <summary>
        /// Lấy danh sách khoa của his
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        IList<SysDicDto> GetDeptList(string orgId, string code);
        /// <summary>
        /// Lấy danh sách khu bệnh
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        IList<SysDicDto> GetWardList(string orgId, string code);
        IList<MedRecordTypeVO> GetSysItemDic(string OrgId, string Code, string bllxId, string DetailCode = null);
        /// <summary>
        /// Lấy loại lớn bệnh án
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        IList<SysDicMarkDto> GetBllxList(string orgId, string code, string mzbz = null);
        IList<SysDicDto> GetDutyList(string orgId, string code);
        IList<SysDicMarkDto> GetDutyListRelbllx(string orgId, string bllx, string dutycode);
        /// <summary>
        /// Lấy loại bệnh án
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="bllx">Id or bllx</param>
        /// <param name="keyword"></param>
        /// <param name="type">1 root, 2 Thư mục con</param>
        /// <returns></returns>
        IList<BaseBllxVO> GetBllxListDetail(string orgId,string bllx=null,string keyword=null,string type=null);
        /// <summary>
        /// Lấy thông tin nhân viên (chữ ký)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<StaffVO> StaffInfo(string user, string orgId);

        /// <summary>
        /// Khu bệnh
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<xt_bq> GetInpatientArea(string orgId, string keyword);
        /// <summary>
        /// Chứng hậu Đông y
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDiagZyzh> GetZyzhList(string orgId, string keyword);

        #region 医嘱打印
        /// <summary>
        /// In y lệnh dài hạn
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="isSign"></param>
        /// <returns></returns>
        IList<CqyzPrintVo> GetPrintCqyzData(string zyh, string orgId, string isSign);
        IList<LsyzPrintVo> GetPrintLsyzData(string zyh, string orgId, string isSign);
        PatidInfoVo GetInpatInfo(string zyh, string orgId);
        #endregion

        #region 护理单打印
        IList<HljlDataPrintVo> GetPrintHljlData(string zyh, string orgId, string hljb);

        #endregion
    }
}
