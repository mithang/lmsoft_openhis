using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// xt_yp_kcxx
    /// </summary>
    public interface ISysMedicineStockInfoRepo : IRepositoryBase<SysMedicineStockInfoEntity>
    {

        /// <summary>
        /// Xuất kho ra ngoài thay đổi số lượng đóng băng tồn kho
        /// </summary>
        /// <param name="kcId"></param>
        /// <param name="djsl"></param>
        void UpdateDjsl(string kcId, int djsl);

        /// <summary>
        /// get xt_yp_kcxx by sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="sqlParameter"></param>
        /// <returns></returns>
        object FindEntity<T>(string sql, SugarParameter[] sqlParameter);

        /// <summary>
        /// get xt_yp_kcxx by yfbmcode,organizeId and pc
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="pc"></param>
        /// <returns></returns>
        SysMedicineStockInfoEntity FindEntity(string yfbmCode, string organizeId, string pc);

        /// <summary>
        /// Sửa trạng thái tồn kho
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        int UpdateZt(string ypdm, string ph, string pc, string zt, string yfbmCode, string organizeId);

        /// <summary>
        /// Tính tồn kho khả dụng Phương thức hiện tại không có nhật ký
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysMedicineStockInfoEntity> SelectData(string ypCode, string yfbmCode, string organizeId);

        /// <summary>
        /// Tính tồn kho khả dụng Phương thức hiện tại không có nhật ký
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="pc"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="ph"></param>
        /// <returns></returns>
        List<SysMedicineStockInfoEntity> SelectData(string ypCode, string ph, string pc, string yfbmCode,
           string organizeId);

        /// <summary>
        /// Giảm tồn kho đóng băng
        /// </summary>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất Số tồn kho cần trừ</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int SubtractForzenKc(int sl, string pc, string ph, string ypCode, string yfbmCode, string organizeId,
           string userCode);

        /// <summary>
        /// Giảmdjslvàkcsl
        /// </summary>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất Số tồn kho cần trừ</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int SubtractDjslAndKcsl(int sl, string pc, string ph, string ypCode, string yfbmCode, string organizeId,
           string userCode);

        /// <summary>
        /// Giảm tồn kho đóng băng
        /// </summary>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất Số tồn kho cần trừ</param>
        /// <param name="kcId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int SubtractForzenKc(int sl, string kcId, string organizeId, string userCode);

        int UpdateExpired(string kcId, string yxq);
    }
}
