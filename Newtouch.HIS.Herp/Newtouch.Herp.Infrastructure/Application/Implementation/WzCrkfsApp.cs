using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 出入库方式
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
                wzCrkfsEntity.Create(true);
                return _wzCrkfsRepo.Insert(wzCrkfsEntity);
            }
            var dbCrkfs = _wzCrkfsRepo.FindEntity(p => p.Id == keyWord);
            if (dbCrkfs == null) return 0;
            dbCrkfs.crkbz = wzCrkfsEntity.crkbz;
            dbCrkfs.crkfsmc = wzCrkfsEntity.crkfsmc;
            dbCrkfs.zt = wzCrkfsEntity.zt;
            dbCrkfs.Modify();
            return _wzCrkfsRepo.Update(dbCrkfs);
        }
    }
}
