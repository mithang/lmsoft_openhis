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
    /// Bảo trì vật tư. ISupplierApp belongs to the SupplierManage area and has no
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
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                var wzProductEntity = TransformProductEntity(source);
                // G11: don vi nho nhat khong ton tai thi bao loi TRUOC khi chen
                // vat tu/sfxm — neu de SubmitRelProductUnit moi fail thi vat tu
                // + dong xt_sfxm da commit xong, de lai vat tu mo coi.
                if (string.IsNullOrWhiteSpace(wzProductEntity.minUnit)
                    || _wzUnitRepo.FindEntity(p => p.Id == wzProductEntity.minUnit) == null)
                {
                    throw new Exception("Không tìm thấy đơn vị nhỏ nhất của vật tư");
                }
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
            // G11: tim vat tu truoc khi Transform — Transform co the tao nhanh
            // nha san xuat (GetSupplierId) nen sua keyWord bogus truoc day van
            // de lai 1 dong gys_supplier mo coi du tra ve 0.
            var dbProduct = wzProductRepo.FindEntity(p => p.Id == keyWord);
            if (dbProduct == null) return 0;
            var editEntity = TransformProductEntity(source);
            // G11: chi copy field khac null — form luon post du field, con
            // POST thieu field ma copy null se loi SQL (name/typeId/minUnit
            // NOT NULL) hoac xoa sach gia tri cu (py/jj/lsj ve null/0).
            // Danh doi: API khong the xoa sach 1 field ve null/0 bang cach
            // bo field; muon xoa phai gui gia tri rong/0 tuong minh — nhung
            // voi jj/lsj thi gui 0 tuong minh van dat ve 0 (HasValue).
            // zt/sflkc/sffy/sfgt/iswzsame controller luon map "on"->"1" nen
            // khong bao gio null, van copy nhu cu.
            if (editEntity.brand != null) dbProduct.brand = editEntity.brand;
            if (editEntity.gg != null) dbProduct.gg = editEntity.gg;
            if (!string.IsNullOrWhiteSpace(editEntity.imageUrl)) dbProduct.imageUrl = editEntity.imageUrl;
            if (source.jj.HasValue) dbProduct.jj = source.jj.Value;
            if (source.lsj.HasValue) dbProduct.lsj = source.lsj.Value;
            if (editEntity.minUnit != null) dbProduct.minUnit = editEntity.minUnit;
            if (editEntity.name != null) dbProduct.name = editEntity.name;
            if (editEntity.py != null) dbProduct.py = editEntity.py;
            if (editEntity.sffy != null) dbProduct.sffy = editEntity.sffy;
            if (editEntity.sfgt != null) dbProduct.sfgt = editEntity.sfgt;
            if (editEntity.sflkc != null) dbProduct.sflkc = editEntity.sflkc;
            if (editEntity.supplierId != null) dbProduct.supplierId = editEntity.supplierId;
            if (editEntity.typeId != null) dbProduct.typeId = editEntity.typeId;
            if (editEntity.zczh != null) dbProduct.zczh = editEntity.zczh;
            if (editEntity.zt != null) dbProduct.zt = editEntity.zt;
            if (editEntity.zxqds.HasValue) dbProduct.zxqds = editEntity.zxqds;
            if (editEntity.productCode != null) dbProduct.productCode = editEntity.productCode;
            if (editEntity.hcgjybdm != null) dbProduct.hcgjybdm = editEntity.hcgjybdm;
            if (editEntity.iswzsame != null) dbProduct.iswzsame = editEntity.iswzsame;
            if (editEntity.zfxz != null) dbProduct.zfxz = editEntity.zfxz;
            if (editEntity.zfbl.HasValue) dbProduct.zfbl = editEntity.zfbl;
            if (editEntity.gjybdm != null) dbProduct.gjybdm = editEntity.gjybdm;
            if (editEntity.ybdm != null) dbProduct.ybdm = editEntity.ybdm;
            if (editEntity.zblb != null) dbProduct.zblb = editEntity.zblb;
            if (editEntity.hslb != null) dbProduct.hslb = editEntity.hslb;
            if (editEntity.kcyjz.HasValue) dbProduct.kcyjz = editEntity.kcyjz;
            dbProduct.Modify();
            int updatecount = wzProductRepo.Update(dbProduct);
            // G11: dung ma vat tu da merge (cu) thay vi ma tu form — form
            // thieu productCode thi truoc day gui null vao proc dong bo.
            _wzProductDmnService.InsertTOSfxm(dbProduct.OrganizeId, dbProduct.productCode);
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
                jj = s.jj ?? 0m,
                LastModifierCode = s.LastModifierCode,
                LastModifyTime = s.LastModifyTime,
                lsj = s.lsj ?? 0m,
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
        /// Tải lên hình ảnh (IFormFile). Files are saved ONLY under this copy's
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
            // G11: chan rel mo coi (probe base d3-bogus-product: productId
            // bogus van Success + 1 dong rel; d3-bogus-unit: unitId bogus ->
            // unit NULL -> loi SQL tho). Tra 0 -> controller Error.
            if (rel == null) return 0;
            var product = string.IsNullOrWhiteSpace(rel.keyWord) ? null
                : wzProductRepo.FindEntity(p => p.Id == rel.keyWord);
            if (product == null) return 0;
            var unit = string.IsNullOrWhiteSpace(rel.unitId) ? null
                : _wzUnitRepo.FindEntity(p => p.Id == rel.unitId);
            if (unit == null) return 0;
            if (string.IsNullOrWhiteSpace(rel.id))
            {
                var entity = new RelProductUnitEntity
                {
                    OrganizeId = organizeId,
                    productId = rel.keyWord,
                    unit = rel.unitName ?? unit.name,
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
                entity.unit = rel.unitName ?? unit.name;
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
