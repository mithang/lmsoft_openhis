using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Infrastructure;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo: chl
    /// Ngày: 2021-03-01 15:24
    /// Mô tả: Đăng ký vào viện - Chẩn đoán
    /// </summary>
    public class YBInpPatRegInfoDiagRepo : RepositoryBase<YBInpPatRegInfoDiagEntity>, IYBInpPatRegInfoDiagRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public YBInpPatRegInfoDiagRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }
        public void Submit(YBInpPatRegInfoDiagEntity ety)
        {
            if (ety != null)
            {
                if (!string.IsNullOrWhiteSpace(ety.BKF263))
                {
                    ety.Modify(ety.Id);
                    this.Update(ety);
                }
                else
                {
                    ety.BKF263 = EFDBBaseFuncHelper.Instance.MedicalRecordLSH(ety.OrganizeId);
                    ety.Create(true);
                    this.Insert(ety);
                }
            }
        }
    }
}