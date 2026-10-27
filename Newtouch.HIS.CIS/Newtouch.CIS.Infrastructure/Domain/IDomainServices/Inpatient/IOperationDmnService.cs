using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.IDomainServices
{
    public interface IOperationDmnService
    {
        /// <summary>
        /// Sắp xếp phẫu thuậtdanh sáchTra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        List<ArrangeQueryGridVO> ArrangeQueryGridView(Pagination pagination, ArrangeQueryRequestVO req);

        /// <summary>
        /// Sắp xếp phẫu thuậtnội dungLấy
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        ArrangementVO ArrangementForID(Pagination pagination, ArrangementRequestVO req);

        /// <summary>
        /// LấyPhẫu thuậtY lệnhbệnh nhân
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="brzybzType"></param>
        /// <returns></returns>
        List<OperatPatVO> GetOperationPatSearchList(string psOrgId);

        /// <summary>
        /// Gửibổ sungghiChi phí
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="FeeDetails"></param>
        void InpatientFeeDetailSubmit(string orgId, string zyh, List<InpatientFeeDetailEntity> FeeDetails);

        /// <summary>
        /// thông quaSố nhập viện,Y lệnhTính chấtLấyChi phíthông tin
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="zyh"></param>
        /// <param name="yzxz"></param>
        /// <returns></returns>
        List<OperatFeeVO> GetOperatFee(string OrganizeId, string zyh, string yzxz);
    }
}
