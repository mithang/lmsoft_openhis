using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// Thao tác loại vật tư
    /// </summary>
    public class ProductTypeApp : AppBase, IProductTypeApp
    {
        private readonly IWzTypeRepo _wzTypeRepo;

        public ProductTypeApp(IWzTypeRepo wzTypeRepo)
        {
            _wzTypeRepo = wzTypeRepo;
        }

        public int SubmitForm(WzTypeEntity wzTypeEntity, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                // G11: phong thu — controller da guard ten khi tao moi; neu
                // ten null lọt xuong day thi tra 0 thay vi loi SQL tho.
                if (string.IsNullOrWhiteSpace(wzTypeEntity.name)) return 0;
                wzTypeEntity.Create(true);
                return _wzTypeRepo.Insert(wzTypeEntity);
            }
            var dbType = _wzTypeRepo.FindEntity(p => p.Id == keyWord);
            if (dbType == null) return 0;
            // G11: chi copy field khac null — POST sua thieu field ma copy
            // null se loi SQL (name NOT NULL) hoac xoa sach gia tri cu
            // (remark ve NULL, probe base d6-edit-noremark).
            if (wzTypeEntity.parentId != null) dbType.parentId = wzTypeEntity.parentId;
            if (wzTypeEntity.name != null) dbType.name = wzTypeEntity.name;
            if (wzTypeEntity.remark != null) dbType.remark = wzTypeEntity.remark;
            if (wzTypeEntity.px.HasValue) dbType.px = wzTypeEntity.px;
            if (wzTypeEntity.zt != null) dbType.zt = wzTypeEntity.zt;
            dbType.Modify();
            return _wzTypeRepo.Update(dbType);
        }
    }
}
