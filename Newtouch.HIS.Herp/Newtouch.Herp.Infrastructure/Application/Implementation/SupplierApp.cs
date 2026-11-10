using Newtouch.Herp.Application;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;
using System;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// Supplier handling. Port of the Framework SupplierApp; the original
    /// relied on binary field injection, so this port takes IGysSupplierRepo
    /// through an explicit constructor (MS DI).
    /// </summary>
    public class SupplierApp : AppBase, ISupplierApp
    {
        private readonly IGysSupplierRepo _gysSupplierRepo;

        public SupplierApp(IGysSupplierRepo gysSupplierRepo)
        {
            _gysSupplierRepo = gysSupplierRepo;
        }

        /// <summary>
        /// submit Supplier maintenance form
        /// </summary>
        /// <param name="supplierEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns>false khi sửa mà không tìm thấy nhà cung cấp (keyWord bogus)</returns>
        public bool SubmitForm(GysSupplierEntity supplierEntity, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                // py NOT NULL trong DB — form luôn post py (kể cả rỗng), chỉ
                // API thiếu field mới null; mặc định rỗng thay vì lỗi SQL thô.
                supplierEntity.py ??= "";
                supplierEntity.Create(true);
                _gysSupplierRepo.Insert(supplierEntity);
                return true;
            }
            else
            {
                var dbSupplier = _gysSupplierRepo.FindEntity(p => p.Id == keyWord);
                if (dbSupplier == null) return false;
                // Chỉ copy field khác null: form luôn post đủ field, còn
                // POST thiếu field mà copy null sẽ lỗi SQL (cột py NOT NULL).
                if (supplierEntity.address != null) dbSupplier.address = supplierEntity.address;
                if (supplierEntity.fax != null) dbSupplier.fax = supplierEntity.fax;
                if (supplierEntity.jszq != null) dbSupplier.jszq = supplierEntity.jszq;
                if (supplierEntity.khh != null) dbSupplier.khh = supplierEntity.khh;
                if (supplierEntity.khhzh != null) dbSupplier.khhzh = supplierEntity.khhzh;
                if (supplierEntity.name != null) dbSupplier.name = supplierEntity.name;
                if (supplierEntity.supplierType.HasValue) dbSupplier.supplierType = supplierEntity.supplierType;
                if (supplierEntity.py != null) dbSupplier.py = supplierEntity.py;
                if (supplierEntity.sh != null) dbSupplier.sh = supplierEntity.sh;
                if (supplierEntity.tel != null) dbSupplier.tel = supplierEntity.tel;
                if (supplierEntity.zipCode != null) dbSupplier.zipCode = supplierEntity.zipCode;
                if (supplierEntity.zt != null) dbSupplier.zt = supplierEntity.zt;
                // Original stamped Modify() on the incoming entity (a no-op
                // for the persisted row); stamp the row actually updated.
                dbSupplier.Modify();
                _gysSupplierRepo.Update(dbSupplier);
                return true;
            }
        }

        /// <summary>
        /// Quickly create a producer, returning the producer ID.
        /// Used by ProductApp (cross-area) as well as this area.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string CreateProducerQuick(string id, string name, string organizeId)
        {
            var entityOld = _gysSupplierRepo.FindEntity(p => p.Id == id && p.OrganizeId == organizeId);
            if (entityOld != null && name.Equals(entityOld.name)) return id;
            entityOld = _gysSupplierRepo.FindEntity(p => p.name == name && p.OrganizeId == organizeId);
            if (entityOld != null) return entityOld.Id;
            var entity = new GysSupplierEntity
            {
                name = (name ?? "").Trim(),
                supplierType = (int)EnumSupplierType.Producer,
                OrganizeId = organizeId,
                py = "",
                zt = ((int)Enumzt.Enable).ToString()
            };
            entity.Create(true);
            return _gysSupplierRepo.Insert(entity) > 0 ? entity.Id : "";
        }
    }
}
