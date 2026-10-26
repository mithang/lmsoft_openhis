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
        /// lấy một điều chỉnh giá entity chưa duyệt chưa thực hiện
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        SysMedicinePriceAdjustmentEntity GetMedicinePriceAdjustmentNotApproveEntity(string ypCode);

        /// <summary>
        /// lấy một điều chỉnh giá entity đã duyệt chưa thực hiện
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        SysMedicinePriceAdjustmentEntity GetMedicinePriceAdjustmentNotExecteEntity(string ypCode);

        /// <summary>
        /// thêm thuốc điều chỉnh giá bản ghi(đề nghị)
        /// </summary>
        /// <param name="entity"></param>
        void AddPriceAdjustmentRecord(SysMedicinePriceAdjustmentEntity entity);

        /// <summary>
        /// kiểm tra có hay không có chưa xử lý của dữ liệu
        /// </summary>
        /// <param name="ypCodeList"></param>
        string CheckStatus(ArrayList ypCodeList);

        ///  <summary>
        ///  thuốc điều chỉnh giá
        ///  </summary>
        ///  --shzt:0:chưa duyệt 1:đã duyệt 2:đã từ chối 3.đã thu hồi
        /// 	--zxbz:0:chưa thực hiện 1:đã thực hiện
        /// <param name="ypCodeList"></param>
        /// <param name="operationType"></param>
        string MedicinePriceAdjustment(ArrayList ypCodeList, int operationType);
    }
}
