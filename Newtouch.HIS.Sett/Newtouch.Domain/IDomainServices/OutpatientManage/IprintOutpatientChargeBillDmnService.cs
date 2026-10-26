using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.OutpatientManage
{
    public interface IprintOutpatientChargeBillDmnService
    {
        /// <summary>
        /// Lấy yêu cầu báo cáo phiếu ngoại trú/cấp cứujsonDữ liệu
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        string GetmjzpjJson(string jsnm, string orgId);

        string GetCqmjzpjJson(string jsnm, string orgId);
    }
}
