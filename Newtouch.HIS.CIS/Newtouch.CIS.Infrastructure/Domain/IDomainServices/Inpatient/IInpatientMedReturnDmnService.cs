using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices
{
    public interface IInpatientMedReturnDmnService
    {
        /// <summary>
        /// Khu bệnhphát thuốcBệnh nhâncây
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        IList<InpWardPatTreeVO> GetPatTree(string keyword,string staffId, string orgId,string ReturnZcyMed=null);
        /// <summary>
        /// Tra cứucó thểtrảthuốcdanh sách
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="patInfo"></param>
        /// <param name="keyword"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        IList<InpatientMedicineGrantDto> GetPatMedReturnList(string patInfo, string keyword, string kssj, string jssj,string orgId, string ReturnZcyMed = null);
        /// <summary>
        /// Đăng kýtrảthuốc
        /// </summary>
        /// <param name="user"></param>
        /// <param name="medList"></param>
        string MedReturnSubmit(OperatorModel user,string medList);








    }
}
