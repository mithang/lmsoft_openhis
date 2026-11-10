using System.Linq;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.Product;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// Đối chiếu hạng mục thu phí vật tư
    /// </summary>
    public class RelProductAndsfxmApp : AppBase, IRelProductAndsfxmApp
    {
        private readonly IRelProductAndsfxmRepo _relProductAndsfxmRepo;
        private readonly IWzProductRepo _wzProductRepo;
        private readonly IBaseDataDmnService _baseDataDmnService;

        public RelProductAndsfxmApp(IRelProductAndsfxmRepo relProductAndsfxmRepo,
            IWzProductRepo wzProductRepo, IBaseDataDmnService baseDataDmnService)
        {
            _relProductAndsfxmRepo = relProductAndsfxmRepo;
            _wzProductRepo = wzProductRepo;
            _baseDataDmnService = baseDataDmnService;
        }

        public string SubmitRelProductAndsfxm(RelProductAndsfxmEntity entity)
        {
            if (entity == null) return "Nội dung gửi lên không được để trống";
            if (string.IsNullOrWhiteSpace(entity.Id))
            {
                // G11: chan rel mo coi (probe base d11-bogus-product /
                // d11-bogus-sfxm: productId/sfxmCode bogus van Success +
                // 1 dong rel) va loi SQL tho khi thieu field bat buoc
                // (productId/sfdlCode/sfxmCode/zt NOT NULL).
                if (string.IsNullOrWhiteSpace(entity.productId)
                    || string.IsNullOrWhiteSpace(entity.sfxmCode)
                    || string.IsNullOrWhiteSpace(entity.sfdlCode)
                    || string.IsNullOrWhiteSpace(entity.zt))
                    return "Vui lòng nhập đầy đủ vật tư, hạng mục thu phí và trạng thái";
                if (_wzProductRepo.FindEntity(p => p.Id == entity.productId) == null)
                    return "Không tìm thấy vật tư đã chỉ định";
                if (!_baseDataDmnService.SfxmExists(entity.sfxmCode, entity.OrganizeId))
                    return "Không tìm thấy hạng mục thu phí đã chỉ định";
                var oldEntitis = _relProductAndsfxmRepo.IQueryable(p => p.OrganizeId == entity.OrganizeId && p.sfdlCode == entity.sfdlCode && p.productId == entity.productId);
                if (oldEntitis != null && oldEntitis.Any()) return "Đã tồn tại quan hệ đối chiếu giống nhau, không được gửi trùng";
                entity.Create(true);
                return _relProductAndsfxmRepo.Insert(entity) > 0 ? "" : "Thêm mới thất bại";
            }
            // G11: sua phai tim dong cu truoc roi merge field khac null —
            // truoc day Update thang entity moi tu param nen CreatorCode
            // (khong co trong param) ve NULL -> loi SQL tho, va zt thieu
            // cung loi SQL tho (probe base d11-edit-nosfxmmc / d11-edit-nozt).
            var dbEntity = _relProductAndsfxmRepo.FindEntity(p => p.Id == entity.Id && p.OrganizeId == entity.OrganizeId);
            if (dbEntity == null) return "Cập nhật thất bại";
            if (entity.productId != null && entity.productId != dbEntity.productId)
            {
                if (_wzProductRepo.FindEntity(p => p.Id == entity.productId) == null)
                    return "Không tìm thấy vật tư đã chỉ định";
                dbEntity.productId = entity.productId;
            }
            if (entity.sfxmCode != null && entity.sfxmCode != dbEntity.sfxmCode)
            {
                if (!_baseDataDmnService.SfxmExists(entity.sfxmCode, entity.OrganizeId))
                    return "Không tìm thấy hạng mục thu phí đã chỉ định";
                dbEntity.sfxmCode = entity.sfxmCode;
            }
            if (entity.sfdlCode != null) dbEntity.sfdlCode = entity.sfdlCode;
            if (entity.sfdlmc != null) dbEntity.sfdlmc = entity.sfdlmc;
            if (entity.sfxmmc != null) dbEntity.sfxmmc = entity.sfxmmc;
            if (entity.zt != null) dbEntity.zt = entity.zt;
            dbEntity.Modify();
            return _relProductAndsfxmRepo.Update(dbEntity) > 0 ? "" : "Cập nhật thất bại";
        }
    }
}
