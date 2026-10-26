/*********************************************************/
// Copyright (C) 2016 Newtouch Giữ bản quyền. 
// Description: Nhập viện quản lý >Quản lý tài khoản>
// Author：HLF
// CreateDate： 2016/12/8 12:01:42 
//**********************************************************/
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountManageApp
    {
        ReserveDto GetHosPatAccInfo(int patid);
        ReserveDto GetHosPatAccInfo(int patid, string zhxz);
        List<PatAccPayVO> GetPayList(int zh, string zhxz);
        string GetFinRepSJPZH();
        bool CheckSJPZH(string pzh, out FinanceReceiptEntity sjEntity, out string type);
        bool PayDepositPost(DeposDto dto, out string szid);
        /// <summary>
        /// Nạp tiền
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Quay lại mới tài khoản thông tin</returns>
        SysAccountEntity PayDepositPostAcnt(DeposDto dto);
        bool ExitDepositPost(DeposDto dto);
        /// <summary>
        /// số dư hoàn toàn bộ
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool RefundAccount(DeposDto dto);
        decimal GetzhYe(int zh);
        
        /// <summary>
        /// nhận đ ược số dư tài khoản
        /// </summary>
        decimal GetZhyeByPatid(int patid);
        decimal GetZhyeByPatid(int patid,int zhxz);

        /// <summary>
        /// Lấy bệnh nhân cơ bản thông tin
        /// add by HLF
        /// theo số nhập viện, số thẻ, họ têntra cứu
        /// </summary>
        /// <param name="zyh">Số nhập viện</param>
        /// <param name="kh">Số thẻ</param>
        /// <param name="xm">Họ tên</param>
        /// <returns></returns>
        HosPatAccountActionVO GetHosPatInfo(string zyh);
    }
}
