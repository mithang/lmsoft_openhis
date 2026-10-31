using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.Infrastructure;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo:chl
    /// Ngày:2021-03-01 15:24
    /// Mô tả:Đăng ký nhập viện-Chẩn đoán
    /// </summary>
    public class YBInpPatRegInfoDiagRepo : RepositoryBase<YBInpPatRegInfoDiagEntity>, IYBInpPatRegInfoDiagRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh EF</param>

        public void Submit(YBInpPatRegInfoDiagEntity ety)
        {
            if (ety != null)
            {
                if (!string.IsNullOrWhiteSpace(ety.BKF263))
                {
                    this.Update(ety);
                }
                else
                {
                    ety.BKF263 = EFDBBaseFuncHelper.Instance.MedicalRecordLSH(ety.OrganizeId);
                    this.Insert(ety);
                }
            }
        }
    }
}
