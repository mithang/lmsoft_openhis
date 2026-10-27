using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Herp.Infrastructure.Log;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 物资维护. ISupplierApp belongs to the SupplierManage area and has no
    /// implementation in this copy yet: it is injected as an OPTIONAL
    /// constructor dependency (default null) so ProductController stays alive
    /// for grids/forms; only the quick-create-producer path (typed supplier
    /// name with no supplier id) reports blocked-on-integration.
    /// </summary>
    public class ProductApp : AppBase, IProductApp
    {
        private readonly IWzProductRepo wzProductRepo;
        private readonly IRelProductUnitRepo _relProductUnitRepo;
        private readonly IWzUnitRepo _wzUnitRepo;
        private readonly ISupplierApp _supplierApp;
        private readonly IWzProductDmnService _wzProductDmnService;

        public ProductApp(IWzProductRepo wzProductRepo, IRelProductUnitRepo relProductUnitRepo,
            IWzUnitRepo wzUnitRepo, IWzProductDmnService wzProductDmnService,
            ISupplierApp supplierApp = null)
        {
            this.wzProductRepo = wzProductRepo;
            _relProductUnitRepo = relProductUnitRepo;
            _wzUnitRepo = wzUnitRepo;
            _wzProductDmnService = wzProductDmnService;
            _supplierApp = supplierApp;
        }

        public int SubmitForm(VProductInfoEntity source, string keyWord)
        {
            var wzProductEntity = TransformProductEntity(source);
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                wzProductEntity.Create(true);
                if (_wzProductDmnService.InsertProductToSfxm(wzProductEntity) <= 0)
                {
                    throw new Exception("Chèn dữ liệu vật tư thất bại");
                }
                if (SubmitRelProductUnit(AssembleRelProductUnit(wzProductEntity), wzProductEntity.OrganizeId) <= 0)
                {
                    throw new Exception("Khởi tạo đơn vị vật tư thất bại");
                }
                _wzProductDmnService.InsertTOSfxm(wzProductEntity.OrganizeId, wzProductEntity.productCode);
                return 1;
            }
            var dbProduct = wzProductRepo.FindEntity(p => p.Id == keyWord);
            if (dbProduct == null) return 0;
            dbProduct.brand = wzProductEntity.brand;
            dbProduct.gg = wzProductEntity.gg;
            dbProduct.imageUrl = string.IsNullOrWhiteSpace(wzProductEntity.imageUrl) ? dbProduct.imageUrl : wzProductEntity.imageUrl;
            dbProduct.jj = wzProductEntity.jj;
            dbProduct.lsj = wzProductEntity.lsj;
            dbProduct.minUnit = wzProductEntity.minUnit;
            dbProduct.name = wzProductEntity.name;
            dbProduct.py = wzProductEntity.py;
            dbProduct.sffy = wzProductEntity.sffy;
            dbProduct.sfgt = wzProductEntity.sfgt;
            dbProduct.sflkc = wzProductEntity.sflkc;
            dbProduct.supplierId = wzProductEntity.supplierId;
            dbProduct.typeId = wzProductEntity.typeId;
            dbProduct.zczh = wzProductEntity.zczh;
            dbProduct.zt = wzProductEntity.zt;
            dbProduct.zxqds = wzProductEntity.zxqds;
            dbProduct.productCode = wzProductEntity.productCode;
            dbProduct.hcgjybdm = wzProductEntity.hcgjybdm;
            dbProduct.iswzsame = wzProductEntity.iswzsame;
            dbProduct.zfxz = wzProductEntity.zfxz;
            dbProduct.zfbl = wzProductEntity.zfbl;
            dbProduct.gjybdm = wzProductEntity.gjybdm;
            dbProduct.ybdm = wzProductEntity.ybdm;
            dbProduct.zblb = wzProductEntity.zblb;
            dbProduct.hslb = wzProductEntity.hslb;
            dbProduct.kcyjz = wzProductEntity.kcyjz;
            dbProduct.Modify();
            int updatecount = wzProductRepo.Update(dbProduct);
            _wzProductDmnService.InsertTOSfxm(wzProductEntity.OrganizeId, wzProductEntity.productCode);
            return updatecount;
        }

        private WzProductEntity TransformProductEntity(VProductInfoEntity s)
        {
            return new WzProductEntity
            {
                brand = s.brand,
                CreateTime = s.CreateTime,
                CreatorCode = s.CreatorCode,
                gg = s.gg,
                Id = s.Id,
                imageUrl = s.imageUrl,
                jj = s.jj,
                LastModifierCode = s.LastModifierCode,
                LastModifyTime = s.LastModifyTime,
                lsj = s.lsj,
                minUnit = s.minUnit,
                name = s.name,
                OrganizeId = s.OrganizeId,
                py = s.py,
                sffy = s.sffy,
                sfgt = s.sfgt,
                sflkc = s.sflkc,
                supplierId = GetSupplierId(s.supplierId, s.supplierName),
                typeId = s.typeId,
                zczh = s.zczh,
                zt = s.zt,
                zxqds = s.zxqds,
                productCode = s.productCode,
                hcgjybdm = s.hcgjybdm,
                iswzsame = s.iswzsame,
                zfxz = s.zfxz,
                zfbl = s.zfbl,
                gjybdm = s.gjybdm,
                ybdm = s.ybdm,
                zblb = s.zblb,
                hslb = s.hslb,
                kcyjz = s.kcyjz
            };
        }

        private string GetSupplierId(string supplierId, string supplierName)
        {
            if (string.IsNullOrEmpty(supplierName))
            {
                return supplierId;
            }
            if (_supplierApp == null)
            {
                // Blocked on integration (missing ISupplierApp/SupplierManage):
                // only fail when a new producer would have to be created.
                if (string.IsNullOrWhiteSpace(supplierId))
                {
                    throw new Exception("Tạo nhanh nhà sản xuất chưa khả dụng — blocked on integration (missing ISupplierApp)");
                }
                return supplierId;
            }
            return _supplierApp.CreateProducerQuick(supplierId, supplierName, OrganizeId);
        }

        private ProductUnitRelVo AssembleRelProductUnit(WzProductEntity wzProductEntity)
        {
            var unit = _wzUnitRepo.FindEntity(p => p.Id == wzProductEntity.minUnit);
            return new ProductUnitRelVo
            {
                keyWord = wzProductEntity.Id,
                unitName = unit != null ? unit.name : null,
                unitId = wzProductEntity.minUnit,
                zhyz = 1
            };
        }

        /// <summary>
        /// 上传图片 (IFormFile). Files are saved ONLY under this copy's
        /// wwwroot/Upload/productImg (served as /Upload/productImg/...),
        /// never under the source tree.
        /// </summary>
        public string UploadImag(IFormFile file)
        {
            try
            {
                var result = "";
                if (file == null || !ValidateImg(file)) return result;
                var urn = Constants.GetAppConfigValue("imageUrn");
                urn = string.IsNullOrWhiteSpace(urn) ? "/Upload/productImg" : urn;
                var webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var dir = Directory.Exists(webRoot)
                    ? Path.Combine(webRoot, "Upload", "productImg")
                    : Path.Combine(AppContext.BaseDirectory, "Upload", "productImg");
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                var ext = Path.GetExtension(file.FileName);
                var newFileName = Guid.NewGuid().ToString() + ext;
                result = urn.TrimEnd('/') + "/" + newFileName;
                using (var stream = new FileStream(Path.Combine(dir, newFileName), FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogCore.Error("UploadImag error", ex);
                return "";
            }
        }

        public int SubmitRelProductUnit(ProductUnitRelVo rel, string organizeId)
        {
            if (string.IsNullOrWhiteSpace(rel.id))
            {
                var entity = new RelProductUnitEntity
                {
                    OrganizeId = organizeId,
                    productId = rel.keyWord,
                    unit = rel.unitName,
                    unitId = rel.unitId,
                    zhyz = rel.zhyz,
                    zt = ((int)Enumzt.Enable).ToString()
                };
                entity.Create(true);
                return _relProductUnitRepo.Insert(entity);
            }
            else
            {
                var entity = _relProductUnitRepo.FindEntity(p => p.Id == rel.id);
                if (entity == null) return 0;
                entity.productId = rel.keyWord;
                entity.unit = rel.unitName;
                entity.unitId = rel.unitId;
                entity.zhyz = rel.zhyz;
                entity.Modify();
                return _relProductUnitRepo.Update(entity);
            }
        }

        private bool ValidateImg(IFormFile file)
        {
            return ValidateExt(file) && ValidateSize(file);
        }

        private bool ValidateExt(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(ext)) return false;
            switch (ext.ToLowerInvariant())
            {
                case ".jpg":
                case ".bmp":
                case ".png":
                    return true;
                default:
                    return false;
            }
        }

        private bool ValidateSize(IFormFile file)
        {
            var maxSize = Constants.GetAppConfigValue("imageMaxSize");
            maxSize = string.IsNullOrWhiteSpace(maxSize) ? 5.ToString() : maxSize;
            var size = file.Length;
            return size / 1024 / 1024 <= Convert.ToInt32(maxSize);
        }
    }
}
