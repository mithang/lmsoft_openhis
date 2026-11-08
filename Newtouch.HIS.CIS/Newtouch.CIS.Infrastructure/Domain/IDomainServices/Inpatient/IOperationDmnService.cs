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
        /// Sắp xếp phẫu thuật danh sách Tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        List<ArrangeQueryGridVO> ArrangeQueryGridView(Pagination pagination, ArrangeQueryRequestVO req);

        /// <summary>
        /// Sắp xếp phẫu thuật nội dung Lấy
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        ArrangementVO ArrangementForID(Pagination pagination, ArrangementRequestVO req);

        /// <summary>
        /// Lấy Phẫu thuậtY lệnh bệnh nhân
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="brzybzType"></param>
        /// <returns></returns>
        List<OperatPatVO> GetOperationPatSearchList(string psOrgId);

        /// <summary>
        /// Gửi bổ sungghiChi phí
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="FeeDetails"></param>
        void InpatientFeeDetailSubmit(string orgId, string zyh, List<InpatientFeeDetailEntity> FeeDetails);

        /// <summary>
        /// thông qua Số nhập viện,Y lệnh Tính chất Lấy Chi phí thông tin
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="zyh"></param>
        /// <param name="yzxz"></param>
        /// <returns></returns>
        List<OperatFeeVO> GetOperatFee(string OrganizeId, string zyh, string yzxz);
    }
}
