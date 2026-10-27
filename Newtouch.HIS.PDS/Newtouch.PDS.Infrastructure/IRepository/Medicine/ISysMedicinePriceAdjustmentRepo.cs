using Newtouch.HIS.Domain.Entity;
using System.Collections;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicinePriceAdjustmentRepo : IRepositoryBase<SysMedicinePriceAdjustmentEntity>
    {
        /// <summary>
        /// Lấy một điều chỉnh giáentity Chưa duyệt Chưa thực hiện
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        SysMedicinePriceAdjustmentEntity GetMedicinePriceAdjustmentNotApproveEntity(string ypCode);

        /// <summary>
        /// Lấy một điều chỉnh giáentity Đã duyệt Chưa thực hiện
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        SysMedicinePriceAdjustmentEntity GetMedicinePriceAdjustmentNotExecteEntity(string ypCode);

        /// <summary>
        /// Thêm bản ghi điều chỉnh giá thuốc(Xin)
        /// </summary>
        /// <param name="entity"></param>
        void AddPriceAdjustmentRecord(SysMedicinePriceAdjustmentEntity entity);

        /// <summary>
        /// Kiểm tra có dữ liệu chưa xử lý không
        /// </summary>
        /// <param name="ypCodeList"></param>
        string CheckStatus(ArrayList ypCodeList);

        ///  <summary>
        ///  Điều chỉnh giá thuốc
        ///  </summary>
        ///  --shzt:0:Chưa duyệt 1:Đã duyệt 2:Đã từ chối 3.Đã thu hồi
        /// 	--zxbz:0:Chưa thực hiện 1:Đã thực hiện
        /// <param name="ypCodeList"></param>
        /// <param name="operationType"></param>
        string MedicinePriceAdjustment(ArrayList ypCodeList, int operationType);
    }
}
