using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo: chl
    /// Ngày: 2021-03-01 15:24
    /// Mô tả: Tóm tắt ra viện - Chẩn đoán
    /// </summary>
    public class YBInpOutHosSummariesDiagRepo : RepositoryBase<YBInpOutHosSummariesDiagEntity>, IYBInpOutHosSummariesDiagRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public YBInpOutHosSummariesDiagRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        public void Submit(YBInpOutHosSummariesDiagEntity ety)
        {
            if (ety != null)
            {
                if (!string.IsNullOrWhiteSpace(ety.Id))
                {
                    ety.Modify(ety.Id);
                    this.Update(ety);
                }
                else
                {
                    ety.Create(true, ety.Id);
                    this.Insert(ety);
                }
            }
        }

    }
}