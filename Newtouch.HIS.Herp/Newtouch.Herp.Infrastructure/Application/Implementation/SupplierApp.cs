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
        public void SubmitForm(GysSupplierEntity supplierEntity, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                supplierEntity.Create(true);
                _gysSupplierRepo.Insert(supplierEntity);
            }
            else
            {
                var dbSupplier = _gysSupplierRepo.FindEntity(p => p.Id == keyWord);
                if (dbSupplier == null) return;
                dbSupplier.address = supplierEntity.address;
                dbSupplier.fax = supplierEntity.fax;
                dbSupplier.jszq = supplierEntity.jszq;
                dbSupplier.khh = supplierEntity.khh;
                dbSupplier.khhzh = supplierEntity.khhzh;
                dbSupplier.name = supplierEntity.name;
                dbSupplier.supplierType = supplierEntity.supplierType;
                dbSupplier.py = supplierEntity.py;
                dbSupplier.sh = supplierEntity.sh;
                dbSupplier.tel = supplierEntity.tel;
                dbSupplier.zipCode = supplierEntity.zipCode;
                dbSupplier.zt = supplierEntity.zt;
                // Original stamped Modify() on the incoming entity (a no-op
                // for the persisted row); stamp the row actually updated.
                dbSupplier.Modify();
                _gysSupplierRepo.Update(dbSupplier);
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
