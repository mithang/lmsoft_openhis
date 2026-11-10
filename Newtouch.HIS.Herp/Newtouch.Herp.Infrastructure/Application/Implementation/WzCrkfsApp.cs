using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// Phương thức nhập/xuất kho
    /// </summary>
    public class WzCrkfsApp : AppBase, IWzCrkfsApp
    {
        private readonly IWzCrkfsRepo _wzCrkfsRepo;

        public WzCrkfsApp(IWzCrkfsRepo wzCrkfsRepo)
        {
            _wzCrkfsRepo = wzCrkfsRepo;
        }

        public int SubmitForm(WzCrkfsEntity wzCrkfsEntity, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                // G11: phong thu — controller da guard khi tao moi.
                if (string.IsNullOrWhiteSpace(wzCrkfsEntity.crkfsmc)
                    || string.IsNullOrWhiteSpace(wzCrkfsEntity.crkbz)) return 0;
                wzCrkfsEntity.Create(true);
                return _wzCrkfsRepo.Insert(wzCrkfsEntity);
            }
            var dbCrkfs = _wzCrkfsRepo.FindEntity(p => p.Id == keyWord);
            if (dbCrkfs == null) return 0;
            // G11: chi copy field khac null — POST sua thieu field ma copy
            // null se loi SQL (crkfsmc/crkbz NOT NULL, probe base
            // d10-edit-nocrkbz).
            if (wzCrkfsEntity.crkbz != null) dbCrkfs.crkbz = wzCrkfsEntity.crkbz;
            if (wzCrkfsEntity.crkfsmc != null) dbCrkfs.crkfsmc = wzCrkfsEntity.crkfsmc;
            if (wzCrkfsEntity.zt != null) dbCrkfs.zt = wzCrkfsEntity.zt;
            dbCrkfs.Modify();
            return _wzCrkfsRepo.Update(dbCrkfs);
        }
    }
}
