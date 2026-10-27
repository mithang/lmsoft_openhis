using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.Infrastructure;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2021-03-01 15:24
    /// 描 述：入院登记-诊断
    /// </summary>
    public class YBInpPatRegInfoDiagRepo : RepositoryBase<YBInpPatRegInfoDiagEntity>, IYBInpPatRegInfoDiagRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>

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
