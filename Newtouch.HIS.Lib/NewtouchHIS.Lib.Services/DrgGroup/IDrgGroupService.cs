using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.DRG;

namespace NewtouchHIS.Lib.Services.DrgGroup
{
    public interface IDrgGroupService
    {
        /// <summary>
        /// Truy vấn tình hình bệnh nhân vào nhóm
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<BusResult<DrgGroupResult>> GroupMedRecordAsync(DrgAreaMedicalRecord record);
        /// <summary>
        /// Truy vấn hàng loạt tình hình bệnh nhân vào nhóm
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<BusResult<List<DrgGroupResult>>> GroupMedRecordAsync(DrgAreaMedicalRecordS record);
        /// <summary>
        /// Tải lên tệp phân tích tình hình vào nhóm
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<BusResult<DrgGroupFileResult>> GroupFileRecordAsync(DrgAreaFileInfo record);
    }
}
