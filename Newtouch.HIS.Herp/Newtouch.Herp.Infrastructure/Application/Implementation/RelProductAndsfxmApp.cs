using System.Linq;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.Product;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 物资收费项目对照
    /// </summary>
    public class RelProductAndsfxmApp : AppBase, IRelProductAndsfxmApp
    {
        private readonly IRelProductAndsfxmRepo _relProductAndsfxmRepo;

        public RelProductAndsfxmApp(IRelProductAndsfxmRepo relProductAndsfxmRepo)
        {
            _relProductAndsfxmRepo = relProductAndsfxmRepo;
        }

        public string SubmitRelProductAndsfxm(RelProductAndsfxmEntity entity)
        {
            if (entity == null) return "Nội dung gửi lên không được để trống";
            if (string.IsNullOrWhiteSpace(entity.Id))
            {
                var oldEntitis = _relProductAndsfxmRepo.IQueryable(p => p.OrganizeId == entity.OrganizeId && p.sfdlCode == entity.sfdlCode && p.productId == entity.productId);
                if (oldEntitis != null && oldEntitis.Any()) return "Đã tồn tại quan hệ đối chiếu giống nhau, không được gửi trùng";
                entity.Create(true);
                return _relProductAndsfxmRepo.Insert(entity) > 0 ? "" : "Thêm mới thất bại";
            }
            entity.Modify();
            return _relProductAndsfxmRepo.Update(entity) > 0 ? "" : "Cập nhật thất bại";
        }
    }
}
