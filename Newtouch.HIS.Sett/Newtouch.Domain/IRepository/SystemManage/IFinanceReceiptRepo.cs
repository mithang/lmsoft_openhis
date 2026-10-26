/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description: Phiếu tài chính-Biên lai tạm ứng
// Author:HLF
// CreateDate: 2016/12/15 12:36:16 
//**********************************************************/
using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFinanceReceiptRepo : IRepositoryBase<FinanceReceiptEntity>
    {
        /// <summary>
        /// Lấy số biên lai có thể dùng theo mã nhân viên
        /// </summary>
        /// <param name="gh"></param>
        /// <returns></returns>
        string getDQSJH(string gh, string orgId);
        string getDQSJHNew(string gh, string orgId, out FinanceReceiptEntity outsjhEntity, out string outsjhType);
        string getDQSJHForpzh(string gh, string orgId);

        /// <summary>
        /// Lấy dữ liệu số chứng từ theo mã nhân viênEntity
        /// </summary>
        /// <param name="gh"></param>
        /// <returns></returns>
        FinanceReceiptEntity getSJKEntity(string gh, string orgId);


        /// <summary>
        ///  Xác minh số biên lai
        /// </summary>
        /// <param name="sjhfull">Số biên lai đầy đủ</param>
        /// <param name="gh">Mã nhân viên</param>
        /// <returns></returns>
        bool checkSJH(string sjhfull, string gh, out FinanceReceiptEntity outEntity,out string type, string orgId);

        /// <summary>
        /// Xác định số biên lai hiện tại đã được dùng hay chưa
        /// </summary>
        /// <param name="gh">Mã nhân viên</param>
        /// <param name="szm">Chữ cái đầu</param>
        /// <param name="sjh">Số biên lai</param>
        /// <param name="outEntity">Trả về thực thể số biên lai</param>
        /// <returns></returns>
        bool checkSJIsUsedForUser(string gh, string szm, long sjh, out FinanceReceiptEntity outEntity,out string type, string orgId);

        /// <summary>
        /// Thêm số chứng từ biên lai
        /// </summary>
        /// <param name="sjEntity"></param>
        void AddReceiptInfo(FinanceReceiptEntity sjEntity, string orgId);

        /// <summary>
        /// Sửa số chứng từ biên lai
        /// </summary>
        /// <param name="sjEntity"></param>
        /// <param name="cwsjId"></param>
        void UpdateReceiptInfo(FinanceReceiptEntity sjEntity, string cwsjId);

        #region 分配收据号
        IList<FinanceReceiptEntity> GetListByUserCode(string lyry, string orgId);
        FinanceReceiptEntity GetFinancialInvoiceEntity(string Id, string orgId);
        void SubmitForm(FinanceReceiptEntity financialInvoiceEntity, string Id);
        void DeleteForm(string Id, string orgId);

        void VlidateRepeat(FinanceReceiptEntity entity, string orgId, string Id);
        #endregion

    }
}
