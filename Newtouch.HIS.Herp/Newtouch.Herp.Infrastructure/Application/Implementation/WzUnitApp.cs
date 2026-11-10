using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// Thao tác đơn vị vật tư
    /// </summary>
    public class WzUnitApp : AppBase, IWzUnitApp
    {
        private readonly IWzUnitRepo _wzUnitRepo;

        public WzUnitApp(IWzUnitRepo wzUnitRepo)
        {
            _wzUnitRepo = wzUnitRepo;
        }

        public int SubmitForm(WzUnitEntity wzUnitEntity, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                // G11: phong thu — controller da guard ten khi tao moi.
                if (string.IsNullOrWhiteSpace(wzUnitEntity.name)) return 0;
                wzUnitEntity.Create(true);
                return _wzUnitRepo.Insert(wzUnitEntity);
            }
            var dbUnit = _wzUnitRepo.FindEntity(p => p.Id == keyWord);
            if (dbUnit == null) return 0;
            // G11: chi copy field khac null — POST sua thieu field ma copy
            // null se loi SQL (name NOT NULL, probe base d8-edit-noname).
            if (wzUnitEntity.name != null) dbUnit.name = wzUnitEntity.name;
            if (wzUnitEntity.remark != null) dbUnit.remark = wzUnitEntity.remark;
            if (wzUnitEntity.zt != null) dbUnit.zt = wzUnitEntity.zt;
            dbUnit.Modify();
            return _wzUnitRepo.Update(dbUnit);
        }
    }
}
