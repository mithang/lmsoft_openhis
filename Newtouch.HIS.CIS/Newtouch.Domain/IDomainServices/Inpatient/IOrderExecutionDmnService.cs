using System;
using System.Collections.Generic;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.OutputDto.Outpatient;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Apply;
using Newtouch.PDS.Requset.Zyypyz;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// Thực hiện y lệnh
    /// </summary>
    public interface IOrderExecutionDmnService
    {
        //IList<InpWardPatTreeVO> GetPatWardTree(string staffId, DateTime zxsj);
        IList<InpWardPatTreeVO> GetPatTree(string staffId, DateTime Vzxsj, bool wnes, string orgId);

        /// <summary>
        /// Lấy cây bệnh nhân y lệnh chờ thực hiện(Bao gồm y lệnh văn bản)
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="vzxsj"></param>
        /// <returns></returns>
        IList<InpWardPatTreeVO> GetPatTreeIncludeWzyz(string staffId, DateTime vzxsj);

        IList<InpWardPatTreeVO> GetWardTree(string staffId);
        IList<OrderExecutionVO> GetOrderExecutionYZList(ref Pagination pagination, string patList, string orgId, string Vzxsj, bool wnes, string IsRehabAuthtoNurse = null, bool Iskf = false,string zxks=null);

        /// <summary>
        /// Lấy danh sách y lệnh phân trang(Bao gồm y lệnh văn bản)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="patList"></param>
        /// <param name="orgId"></param>
        /// <param name="vzxsj"></param>
        /// <returns></returns>
        IList<OrderExecutionVO> GetOrderExecutionYZListIncludeWzyz(Pagination pagination, string patList, string orgId, string vzxsj);

        /// <summary>
        /// Thông tin thực hiện y lệnh Thực hiện hạng mục thuốc
        /// </summary>
        /// <param name="user"></param>
        /// <param name="orderList"></param>
        /// <param name="zxsj"></param>
        string OrderExecutionSubmit(OperatorModel user, IList<YzDetail> orderList, int lyxh, DateTime zxrq);
        /// <summary>
        /// Thông tin thực hiện y lệnh Thực hiện hạng mục thuốc by Dài hạn/Tạm thời/Tất cả 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="orderList"></param>
        /// <param name="lyxh"></param>
        /// <param name="zxrq"></param>
        /// <param name="yzlx"></param>
        /// <param name="isyp">0 Không phải thuốc 1 Thuốc -1Tất cả</param>
        /// <returns></returns>
        string OrderExecutionSubmitbyYzxz(OperatorModel user, IList<YzDetail> orderList, int lyxh, DateTime zxrq,int? yzlx, int? isyp);
        /// <summary>
        /// Thực hiện y lệnh không tính phí
        /// </summary>
        /// <param name="user"></param>
        /// <param name="orderList"></param>
        /// <param name="lyxh"></param>
        /// <param name="zxrq"></param>
        /// <returns></returns>
        string NoFeeOrderExecutionSubmit(OperatorModel user, IList<ApiResponseVO> orderList, int lyxh, DateTime zxrq);
        //void OrderExecutionSubmitbyPat(OperatorModel user, string zyh, int yzxz,DateTime zxsj);
        List<YzDetail> GetapiList(OperatorModel user, IList<ApiResponseVO> orderExeList, DateTime zxsj, int lyxh);
        List<ApiResponseVO> GetAllYZ(string zyh, int zxlx, DateTime zxsj, string IsRehabAuthtoNurse = null);

        /// <summary>
        /// Lấy thực hiện tất cả y lệnh bao gồm y lệnh văn bản
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="zxlx"></param>
        /// <param name="Vzxsj"></param>
        /// <returns></returns>
        List<ApiResponseVO> GetAllYZWithWzYz(string orgId, string zyh, int zxlx, DateTime Vzxsj,string IsRehabAuthtoNurse=null);
        List<ApiResponseVO> GetkfYz(string orgId, string zyh, int zxlx, DateTime Vzxsj, string zxks = null);

        /// <summary>
        /// Thực hiện hạng mục chế độ ăn
        /// </summary>
        /// <param name="orderExeList"></param>
        /// <param name="zxsj"></param>
        /// <param name="lyxh"></param>
        string OrderExecutionXM(OperatorModel user, IList<ApiResponseVO> orderExeList, int lyxh, DateTime zxrq,string orgId,string medicalInsurance, int? yzxz = null);

        /// <summary>
        /// Thực hiện hạng mục chung
        /// </summary>
        /// <param name="rygh"></param>
        /// <param name="orderExeList"></param>
        /// <param name="lyxh"></param>
        /// <param name="zxrq"></param>
        /// <returns></returns>
        string OrderExecutionXmWithWzyz(string rygh, IList<ApiResponseVO> orderExeList, int lyxh, DateTime zxrq,string orgId, int? yzxz = null);

        /// <summary>
        /// Thông tin y lệnh có thể thực hiện
        /// </summary>
        /// <param name="OrderList"></param>
        /// <param name="Vzxsj"></param>
        /// <returns></returns>
        string IsOKOrderExecution(IList<ApiResponseVO> OrderList, DateTime Vzxsj,string orgId, string user = null);
        ///// <summary>
        /////  Tạm thời,Dài hạn,Thực hiện tất cả hạng mục chế độ ăn
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="zyh"></param>
        ///// <param name="yzlx"></param>
        ///// <param name="zxsj"></param>
        ///// <param name="yzxz"></param>
        //void OrderExecutionALLXM(OperatorModel user, IList<ApiResponseVO> orderExeList, DateTime zxsj);
        ///// <summary>
        ///// Tạm thời,Dài hạn,Tất cả,Thực hiện hạng mục thuốc
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="orderExeList"></param>
        ///// <param name="zxsj"></param>
        //void OrderExecutionSubmitALLYP(OperatorModel user, IList<ApiResponseVO> orderExeList, DateTime zxsj);

        #region 消息提醒
        IList<SysMSGQueryVO> MSGQuery(string gh,string orgId);


        #endregion

        CheckApplicationfromDTO pushApplicationform(ApiResponseVO orderList, string orgid, string typezt);
        void Updatezy_brxxexpand(string OrganizeId, string zyh);
        void SyncPatFee(string orgId, string zyh, int zxtype);

        #region 医技执行

        IList<JyjcExecVo> GetJyjcSqd(Pagination pagination, string orgId, DateTime kssj, DateTime jssj, string zxzt, string hzlx, string fylx,
            string sqdlx, string keyword = null);

        IList<JyjcExecVo> GetJyjcSqdRecord(Pagination pagination, string orgId, DateTime kssj, DateTime jssj, string hzlx, string fylx,
           string sqdlx, string keyword = null);
        #endregion
    }
}
