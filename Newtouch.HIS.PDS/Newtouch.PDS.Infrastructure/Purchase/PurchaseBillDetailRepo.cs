using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.PharmacyDrugStorage;
using Newtouch.HIS.Domain.Entity.Purchase;
using Newtouch.HIS.Domain.IRepository.Purchase;
using System;
using System.Collections.Generic;
using SqlSugar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.Purchase
{
    public class PurchaseBillDetailRepo : RepositoryBase<PurchaseBillDetailEntity>, IPurchaseBillDetailRepo
    {
        public PurchaseBillDetailRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public int InsertItem(OutputStructYY004 entity, string orgId)
        {
            //G27-F5: base: FPH thiếu/dài → lỗi SQL thô; trùng (FPH+FPMXBH) vẫn insert; SPLX/SFWPSFP/SFCH chỉ map nhãn tiếng Trung, mã số "1"/"0" từ YY rớt thành "".
            if (entity == null) throw new FailedException("Dữ liệu chi tiết hóa đơn không được để trống.");
            if (string.IsNullOrWhiteSpace(entity.FPH) || entity.FPH.Length > 20) throw new FailedException("Số hóa đơn (FPH) không được để trống và không vượt quá 20 ký tự.");
            if (string.IsNullOrWhiteSpace(entity.FPMXBH) || entity.FPMXBH.Length > 20) throw new FailedException("Mã chi tiết hóa đơn (FPMXBH) không được để trống và không vượt quá 20 ký tự.");
            if (!string.IsNullOrEmpty(entity.ZXSPBM) && entity.ZXSPBM.Length > 50) throw new FailedException("Mã sản phẩm (ZXSPBM) không vượt quá 50 ký tự.");
            if (!string.IsNullOrEmpty(entity.SCPH) && entity.SCPH.Length > 50) throw new FailedException("Số lô sản xuất (SCPH) không vượt quá 50 ký tự.");
            if (!string.IsNullOrEmpty(entity.GLMXBH) && entity.GLMXBH.Length > 20) throw new FailedException("Mã chi tiết liên quan (GLMXBH) không vượt quá 20 ký tự.");
            if (!string.IsNullOrEmpty(entity.XSDDH) && entity.XSDDH.Length > 50) throw new FailedException("Số đơn bán hàng (XSDDH) không vượt quá 50 ký tự.");
            if (!string.IsNullOrEmpty(entity.PZWH) && entity.PZWH.Length > 40) throw new FailedException("Số đăng ký (PZWH) không vượt quá 40 ký tự.");
            if (!string.IsNullOrEmpty(entity.FPRQ) && entity.FPRQ.Length > 20) throw new FailedException("Ngày hóa đơn (FPRQ) không vượt quá 20 ký tự.");
            if (!string.IsNullOrEmpty(entity.YQBM) && entity.YQBM.Length > 20) throw new FailedException("Mã khu vực (YQBM) không vượt quá 20 ký tự.");
            if (!string.IsNullOrEmpty(entity.YYBM) && entity.YYBM.Length > 20) throw new FailedException("Mã bệnh viện (YYBM) không vượt quá 20 ký tự.");
            if (!string.IsNullOrEmpty(entity.PSDBM) && entity.PSDBM.Length > 20) throw new FailedException("Mã điểm phân phối (PSDBM) không vượt quá 20 ký tự.");
            if (!string.IsNullOrEmpty(entity.FPBZ) && entity.FPBZ.Length > 100) throw new FailedException("Ghi chú hóa đơn (FPBZ) không vượt quá 100 ký tự.");
            if (!string.IsNullOrEmpty(entity.WPSFPSM) && entity.WPSFPSM.Length > 200) throw new FailedException("Ghi chú không phải hàng hóa (WPSFPSM) không vượt quá 200 ký tự.");
            var dup = this.FindEntity(p => p.fph == entity.FPH && p.fpmxbh == entity.FPMXBH && p.OrganizeId == orgId && p.zt == "1");
            if (dup != null) throw new FailedException("Chi tiết hóa đơn đã tồn tại.");
            var sfwpsfp = "";
            var splx = "";
            var sfch = "";
            if (entity.SFWPSFP == "否" || entity.SFWPSFP == "0") sfwpsfp = "0";
            if (entity.SFWPSFP == "是" || entity.SFWPSFP == "1") sfwpsfp = "1";
            if (entity.SPLX == "药品类" || entity.SPLX == "1") splx = "1";
            if (entity.SPLX == "医用耗材类" || entity.SPLX == "2") splx = "2";
            if (entity.SPLX == "医疗器械类" || entity.SPLX == "3") splx = "3";
            if (entity.SPLX == "其他" || entity.SPLX == "9") splx = "9";
            if (entity.SFCH == "否" || entity.SFCH == "0") sfch = "0";
            if (entity.SFCH == "是" || entity.SFCH == "1") sfch = "1";
            PurchaseBillDetailEntity dbEntity = new PurchaseBillDetailEntity();
            dbEntity.Id = Guid.NewGuid().ToString();
            dbEntity.OrganizeId = orgId;
            dbEntity.fph = entity.FPH;
            dbEntity.fprq = entity.FPRQ;
            dbEntity.fphszje = entity.FPHSZJE;
            dbEntity.yqbm = entity.YQBM;
            dbEntity.yybm = entity.YYBM;
            dbEntity.psdbm = entity.PSDBM;
            dbEntity.dlsgbz = entity.DLSGBZ;
            dbEntity.fpbz = entity.FPBZ;
            dbEntity.sfwpsfp = sfwpsfp;
            dbEntity.wpsfpsm = entity.WPSFPSM;
            dbEntity.fpmxbh = entity.FPMXBH;
            dbEntity.splx = splx;
            dbEntity.sfch = sfch;
            dbEntity.zxspbm = entity.ZXSPBM;
            dbEntity.scph = entity.SCPH;
            dbEntity.scrq = entity.SCRQ;
            dbEntity.spsl = entity.SPSL;
            dbEntity.glmxbh = entity.GLMXBH;
            dbEntity.xsddh = entity.XSDDH;
            dbEntity.sxh = entity.SXH;
            dbEntity.yxrq = entity.YXRQ;
            dbEntity.wsdj = entity.WSDJ;
            dbEntity.hsdj = entity.HSDJ;
            dbEntity.sl = entity.SL;
            dbEntity.se = entity.SE;
            dbEntity.hsje = entity.HSJE;
            dbEntity.pfj = entity.PFJ;
            dbEntity.lsj = entity.LSJ;
            dbEntity.pzwh = entity.PZWH;

            dbEntity.Create(true);
            return this.Insert(dbEntity);
        }

      
    }
}
