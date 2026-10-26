using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.DRG;

namespace NewtouchHIS.Lib.Services.DrgGroup
{
    public interface IDrgGroupService
    {
        /// <summary>
        /// truy vấn tình hình vào nhóm của bệnh nhân
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<BusResult<DrgGroupResult>> GroupMedRecordAsync(DrgAreaMedicalRecord record);
        /// <summary>
        /// truy vấn hàng loạt tình hình vào nhóm của bệnh nhân
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<BusResult<List<DrgGroupResult>>> GroupMedRecordAsync(DrgAreaMedicalRecordS record);
        /// <summary>
        /// tải lên tệp để phân tích tình hình vào nhóm
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<BusResult<DrgGroupFileResult>> GroupFileRecordAsync(DrgAreaFileInfo record);
    }
}
