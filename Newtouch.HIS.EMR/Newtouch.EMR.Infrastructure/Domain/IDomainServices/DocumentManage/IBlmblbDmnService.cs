using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.EMR.Domain.ValueObjects.DocumentManage;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.EMR.Domain.IRepository;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface IBlmblbDmnService
    {
        /// <summary>
        /// Lấy danh sách mẫu
        /// </summary>
        /// <param name="mbqx"></param>
        /// <param name="ksdm"></param>
        /// <param name="ysgh"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        IList<BlmbListVO> MedRecordTmpList(int mbqx, string bllx, string ksbm, OperatorModel user, string OrgId, string keyword = null);
        /// <summary>
        /// Chuyển tệp bệnh án thành mẫu
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="blId"></param>
        /// <param name="bllxId"></param>
        /// <param name="mbqx"></param>
        /// <returns></returns>
        BlConvertToTemplateVO BlConvertToTemplate(string OrgId, string blId, string bllx, int? mbqx);
        /// <summary>
        /// Thao tác chuyển tệp bệnh án thành mẫu
        /// </summary>
        /// <param name="ety"></param>
        void BlConvertToTemplateProcess(BlmblbEntity ety);
        /// <summary>
        /// Di chuyển tệp
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="aimPath"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        bool CopyDir(string srcPath, string aimPath, string newName,int type);
        /// <summary>
        /// Bảo trì kiểm soát quyền vị trí mẫu
        /// </summary>
        /// <param name="list"></param>
        /// <param name="user"></param>
        void TempCtrlAssigned(string list,string mbId, OperatorModel user);
        IList<TemplateQxkzVO> Getqxkz(string staffId, string mbId,string bllx);

        /// <summary>
        /// Lấy danh sách mẫu bệnh án(bao gồm loại lớn bệnh án)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<BlmbListVO> GetBlmbList(Pagination pagination, string OrganizeId, string keyword);
        IList<BlmbListVO> MedRecordTmpListTree( string OrgId,string keyValue,string mbqx);
        /// <summary>
        /// Bệnh án của bệnh nhân đã có mẫu
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="zyh"></param>
        /// <param name="bllx"></param>
        /// <returns></returns>
        IList<blmbmcVO> getRepeatBl(string OrgId, string zyh, string bllx);

        string CheckBlRules(string OrgId, string zyh, string bllx);
    }
}
