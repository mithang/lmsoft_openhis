using Newtouch.HIS.Application.Interface;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// (dư ới 拉)chọn dữ liệu源
    /// </summary>
    public class SelectDataSourceApp : ISelectDataSourceApp
    {
        private readonly ISysPatientNatureRepo _sysPatiNatureRepo;
        private readonly ISysChargeCategoryRepo _sysChargeMajorClassRepo;
        public SelectDataSourceApp(ISysPatientNatureRepo sysPatiNatureRepo, ISysChargeCategoryRepo sysChargeMajorClassRepo)
        {
            this._sysPatiNatureRepo = sysPatiNatureRepo;
            this._sysChargeMajorClassRepo = sysChargeMajorClassRepo;
        }

        /// <summary>
        /// LấyTính chất bệnh nhânHiệu lựcDanh sách
        /// </summary>
        /// <returns></returns>
        public string getEffectPatiNatureList()
        {
            return _sysPatiNatureRepo.getEffectPatiNatureList();
        }


    }
}
