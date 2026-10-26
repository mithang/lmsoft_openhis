/*********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description: Quản lý nội trú》Quản lý tài khoản》
// Author:HLF
// CreateDate: 2016/12/7 17:01:42 
//**********************************************************/
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IHosPatAccDmnService
    { 

        /// <summary>
        /// Quản lý tài khoản》Lấy thông tin bệnh nhân
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        HosPatAccountActionVO GetHosPatInfo(string zyh,string orgId);


        /// <summary>
        /// Lấy quản lý nội trú》Quản lý tài khoản》Lấy thông tin bản ghi thu chi tài khoản
        /// </summary>
        /// <returns></returns>
        List<HosPatAccPayVO> GetAccPayInfo(int zh, string orgId, string zyh = null);

        /// <summary>
        /// Quyết toán nội trú Lấy thông tin cơ bản bệnh nhân(Thông tin cơ bản,Tài khoản,Khoa v.v.)
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        IList<HospSettPatInfoVO> GetHospSettPatInfo(string zyh, string orgId);

        /// <summary>
        /// Sự vụ lưu thông tin tài khoản
        /// </summary>
        /// <param name="vo"></param>
        void HosPatAccDB(HosPatAccDataVO vo, SysPatientAccountEntity zh, FinanceReceiptEntity cwsj,string type);

        /// <summary>
        /// Lấy thông tin cơ bản bệnh nhân nội trú(Thông tin cơ bản,Khoa v.v.) Xác minh trạng thái đang nằm viện
        /// </summary>
        /// <param name="zyh">Số nhập viện</param> 
        /// <returns></returns>
        IList<HospPatientInfoVO> GetHospPatientInfo(string zyh, string orgId);
    }
}