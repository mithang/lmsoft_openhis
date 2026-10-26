using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Core.Common;
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
        /// Tra cứu danh sách sắp xếp phẫu thuật
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        List<ArrangeQueryGridVO> ArrangeQueryGridView(Pagination pagination, ArrangeQueryRequestVO req);

        /// <summary>
        /// Lấy nội dung sắp xếp phẫu thuật
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        ArrangementVO ArrangementForID(Pagination pagination, ArrangementRequestVO req);

        /// <summary>
        /// Lấy bệnh nhân y lệnh phẫu thuật
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="brzybzType"></param>
        /// <returns></returns>
        List<OperatPatVO> GetOperationPatSearchList(string psOrgId);

        /// <summary>
        /// Gửi chi phí bổ sung
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="FeeDetails"></param>
        void InpatientFeeDetailSubmit(string orgId, string zyh, List<InpatientFeeDetailEntity> FeeDetails);

        /// <summary>
        /// Thông qua số nội trú,Lấy thông tin chi phí theo tính chất y lệnh
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="zyh"></param>
        /// <param name="yzxz"></param>
        /// <returns></returns>
        List<OperatFeeVO> GetOperatFee(string OrganizeId, string zyh, string yzxz);
    }
}