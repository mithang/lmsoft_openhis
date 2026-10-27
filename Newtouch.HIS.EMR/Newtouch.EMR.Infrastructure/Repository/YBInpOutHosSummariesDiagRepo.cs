using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2021-03-01 15:24
    /// Mô tả：Tóm tắt ra viện-Chẩn đoán
    /// </summary>
    public class YBInpOutHosSummariesDiagRepo : RepositoryBase<YBInpOutHosSummariesDiagEntity>, IYBInpOutHosSummariesDiagRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh EF</param>


        public void Submit(YBInpOutHosSummariesDiagEntity ety)
        {
            if (ety != null)
            {
                if (!string.IsNullOrWhiteSpace(ety.Id))
                {
                    this.Update(ety);
                }
                else
                {
                    this.Insert(ety);
                }
            }
        }

    }
}
