
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Ghi sổ ngoại trú
    /// </summary>
    public interface IOutPatientSettleDmnService
    {
        /// <summary>
        /// Tra cứu đã đăng ký khám trong ngày
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        IList<OutPatientRegInfoVO> GetCurrentDayRegList(string orgId, string creatorCode);

        /// <summary>
        /// Trả số ngoại trú(Đăng ký khám chưa quyết toán)
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        void UnSettedRegBackNum(string mzh, string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ghxm"></param>
        /// <param name="isZzjm"></param>
        /// <param name="isCkf"></param>
        /// <param name="isGbf"></param>
        /// <returns></returns>
        GHZGroupAndFeesVO GetOutpatientFees(string ghxm, string zlxm, bool isCkf, bool isGbf, string orgId = null);
        /// <summary>
        /// Chi phí tổ hợp hạng mục thu phí
        /// </summary>
        /// <param name="ghxm"></param>
        /// <param name="zlxm"></param>
        /// <param name="isCkf"></param>
        /// <param name="isGbf"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        GHZGroupAndFeesVO GetOutpatientFeesbyGroup(string ghxm, string zlxm, bool isCkf, bool isGbf, string orgId = null);
        /// <summary>
        /// Lưu đăng ký khám ngoại trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="patid"></param>
        /// <param name="kh"></param>
        /// <param name="ghly"></param>
        /// <param name="mjzbz"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <param name="ksmc"></param>
        /// <param name="ysmc"></param>
        /// <param name="ghxm"></param>
        /// <param name="zlxm"></param>
        /// <param name="fph"></param>
        /// <param name="sfrq"></param>
        /// <param name="isCkf"></param>
        /// <param name="isGbf"></param>
        /// <param name="jzxh"></param>
        /// <param name="ghpbId"></param>
        /// <param name="feeRelated"></param>
        /// <param name="brxz">Tính chất bệnh nhân do trang truyền qua,Vì bệnh nhân BHYT có thể đăng ký tự chi trả</param>
        /// <param name="ybjsh">Số quyết toán đầu BHYT</param>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <param name="mzyyghId">Đăng ký khám hẹn trước ngoại trúID Tương ứngNewtouch_CIS.dbo.mz_yygh.Id</param>
        /// <returns></returns>
        void Save(string orgId, int patid, string kh, string ghly, string mjzbz,
            string ks, string ys, string ksmc, string ysmc, string ghxm, string zlxm
            , string fph, DateTime? sfrq, bool isCkf, bool isGbf
            , int jzxh
            , int ghpbId
            , OutpatientSettFeeRelatedDTO feeRelated, string brxz
            , string ybjsh, string mzh
            , string jzyy, string jzid, string jzlx, string bzbm, string bzmc, string mzyyghId,string isjm, out object newJszbInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gh"></param>
        /// <param name="ssk"></param>
        /// <param name="fph"></param>
        /// <param name="isCkf"></param>
        /// <param name="isGbf"></param>
        /// <returns></returns>
        long Book(OutpatientRegistEntity gh, decimal ssk, string fph, bool isCkf, bool isGbf);

        /// <summary>
        /// checkBệnh nhân có bản ghi đăng ký khám với chi phí chờ quyết toán hay không(Không phân biệt lần đăng ký khám nào)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="patid"></param>
        /// <returns></returns>
        bool CheckHasUnsettedRegister(string orgId, int patid);

        /// <summary>
        /// checkBệnh nhân có bản ghi chi phí chưa quyết toán hay không(Không phân biệt lần đăng ký khám nào)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="patid"></param>
        /// <returns></returns>
        bool CheckHasUnsetted(string orgId, int patid);

        /// <summary>
        /// Tạo số ngoại trú duy nhất,Số thứ tự khám
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="ghlx"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <returns></returns>
        Tuple<short, string> GetMzhJzxh(int patid, string ghpbId, string ks, string ys, string OrganizeId, string UserCode);
        /// <summary>
        /// Theo thông tin đã biếtjzxh Ngày đăng ký khám Lấy số ngoại trú
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="ghpbId"></param>
        /// <param name="ks"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="UserCode"></param>
        /// <param name="jzxh"></param>
        /// <param name="jzrq"></param>
        /// <returns></returns>
        string GetRegMzh(int patid, string ghpbId, string ks, string OrganizeId, string UserCode, int jzxh,
            string jzrq);

        /// <summary>
        /// TạochongqingSố thứ tự khám/Số ngoại trú
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="ghpbId"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        Tuple<short, string> GetCQMzhJzxh(int patid, string ghpbId, string ks, string ys, string OrganizeId, string UserCode,string mjzbz,string QueueNo,string OutDate);
        CqybGjbmInfoVo GetDepartmentDoctorIdC(string orgId, string ks,string ys);
        List<GhJzInfo> GetRegListJson(Inparameter inparameter, string orgid);
        /// <summary>
        /// Lấy thông tin cần thiết đăng ký khám BHYT
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        CqybGjbmInfoVo GetYbjzdjVo(string mzh,string orgId);
        /// <summary>
        /// Lấy mã phân vùng hành chính bệnh viện để xác định có phải BHYT khác địa phương hay không
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetOrgXzqh(string orgId);
    }
}
