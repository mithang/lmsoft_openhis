using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Thao tác đơn vị vật tư
    /// </summary>
    public class WzUnitApp : AppBase, IWzUnitApp
    {
        private readonly IWzUnitRepo _wzUnitRepo;

        public WzUnitApp(IWzUnitRepo wzUnit)
        {
            _wzUnitRepo = wzUnit;
        }

        /// <summary>
        /// submit unit maintenance form
        /// </summary>
        /// <param name="wzUnitEntity"></param>
        /// <param name="keyWord"></param>
        public int SubmitForm(WzUnitEntity wzUnitEntity, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                wzUnitEntity.Create(true);
                return _wzUnitRepo.Insert(wzUnitEntity);
            }
            var dbUnit = _wzUnitRepo.FindEntity(p => p.Id == keyWord);
            if (dbUnit == null) return 0;
            dbUnit.name = wzUnitEntity.name;
            dbUnit.remark = wzUnitEntity.remark;
            dbUnit.zt = wzUnitEntity.zt;
            dbUnit.Modify();
            return _wzUnitRepo.Update(dbUnit);
        }
    }
}
