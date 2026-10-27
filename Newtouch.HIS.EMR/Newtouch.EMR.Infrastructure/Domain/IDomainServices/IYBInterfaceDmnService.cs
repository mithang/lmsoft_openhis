using Newtouch.EMR.Domain.DTO;
using Newtouch.EMR.Domain.DTO.OutputDto.MRUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.EMR.Domain.IRepository;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface IYBInterfaceDmnService
    {
        /// <summary>
        /// Giao diện bảo hiểm y tế của bệnh án nhập viện
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <returns></returns>
        YB_7100 GetRyblInfo(string orgId, string dzblId);
        /// <summary>
        /// Chẩn đoán bệnh án nhập viện
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <returns></returns>
        IList<YB_7110> GetRyblZdInfo(string orgId, string dzblId);
        /// <summary>
        /// Ghi chép diễn biến bệnh
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <returns></returns>
        YB_7200 GetBcjlInfo(string orgId, string dzblId);
        /// <summary>
        /// Tóm tắt ra viện
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <returns></returns>
        YB_7500 GetCyxjInfo(string orgId, string dzblId);
        /// <summary>
        /// Tóm tắt ra viện
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <returns></returns>
        IList<YB_7510> GetCyxjZdInfo(string orgId, string dzblId);
        /// <summary>
        /// Chẩn đoán trang đầu bệnh án
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<YB_7610> GetBasyZdInfo(string orgId, string zyh);
        /// <summary>
        /// Lưu chẩn đoán nhập viện
        /// </summary>
        /// <param name="zdlist"></param>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <param name="zyh"></param>
        string YBInhosDiagInfoSave(List<DiagnoseDocDTO> zdlist, string orgId, string dzblId, string zyh);
        /// <summary>
        /// Lưu chẩn đoán ra viện
        /// </summary>
        /// <param name="zdlist"></param>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <param name="zyh"></param>
        string YBOuthosDiagInfoSave(List<DiagnoseDocDTO> zdlist, string orgId, string dzblId, string zyh);

    }
}
