using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity.Purchase;
using Newtouch.HIS.Domain.IRepository.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.Purchase
{
    public class PurchaseLocationRepo : RepositoryBase<PurchaseLocationEntity>, IPurchaseLocationRepo
    {
        public PurchaseLocationRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }


        //Cập nhật hoặc thêm mới
        public string SubmitForm(PurchaseLocationEntity entity, string orgId, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //G29-F4: edit = FindEntity theo Id + OrganizeId (chặn cross-org) + merge-skip-null (field không post không bị wipe thành NULL, họ G2/G18)
                var dbEntity = this.FindEntity(p => p.Id == keyValue && p.OrganizeId == orgId);
                if (dbEntity == null) throw new FailedException("Không tìm thấy điểm giao hàng.");
                MergeLocation(dbEntity, entity);
                dbEntity.psdzt = 1;
                dbEntity.zt = "1";
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
                return keyValue;
            }
            else
            {
                ValidateLocationForCreate(entity);
                entity.Id = Guid.NewGuid().ToString();
                entity.OrganizeId = orgId;
                entity.psdzt = 1;//Đã lưu
                entity.Create(true);
                this.Insert(entity);
                return entity.Id;
            }
        }

        //G29-F4: chỉ ghi đè field nào client post (non-empty); field không post giữ nguyên. Độ dài varchar(50) kiểm tra TV.
        private static void MergeLocation(PurchaseLocationEntity dbEntity, PurchaseLocationEntity entity)
        {
            if (!string.IsNullOrWhiteSpace(entity.psdbm)) { CheckLocationLen(entity.psdbm, "Mã điểm phân phối"); dbEntity.psdbm = entity.psdbm.Trim(); }
            if (!string.IsNullOrWhiteSpace(entity.psdmc)) { CheckLocationLen(entity.psdmc, "Tên điểm phân phối"); dbEntity.psdmc = entity.psdmc.Trim(); }
            if (!string.IsNullOrWhiteSpace(entity.psdz)) { CheckLocationLen(entity.psdz, "Địa chỉ phân phối"); dbEntity.psdz = entity.psdz.Trim(); }
            if (!string.IsNullOrWhiteSpace(entity.lxrxm)) { CheckLocationLen(entity.lxrxm, "Tên người liên hệ"); dbEntity.lxrxm = entity.lxrxm.Trim(); }
            if (!string.IsNullOrWhiteSpace(entity.lxdh)) { CheckLocationLen(entity.lxdh, "Số điện thoại liên hệ"); dbEntity.lxdh = entity.lxdh.Trim(); }
            if (!string.IsNullOrWhiteSpace(entity.yzbm)) { CheckLocationLen(entity.yzbm, "Mã bưu điện"); dbEntity.yzbm = entity.yzbm.Trim(); }
            if (entity.bzxx != null) { CheckLocationLen(entity.bzxx, "Thông tin ghi chú"); dbEntity.bzxx = entity.bzxx.Trim(); }
        }

        //G29-F4: tạo mới bắt buộc đủ 6 field (như DataValidate ở LocationForm) + độ dài ≤50.
        private static void ValidateLocationForCreate(PurchaseLocationEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.psdbm)) throw new FailedException("Mã điểm phân phối là bắt buộc");
            if (string.IsNullOrWhiteSpace(entity.psdmc)) throw new FailedException("Tên điểm phân phối là bắt buộc");
            if (string.IsNullOrWhiteSpace(entity.psdz)) throw new FailedException("Địa chỉ phân phối là bắt buộc");
            if (string.IsNullOrWhiteSpace(entity.lxrxm)) throw new FailedException("Tên người liên hệ là bắt buộc");
            if (string.IsNullOrWhiteSpace(entity.lxdh)) throw new FailedException("Số điện thoại liên hệ là bắt buộc");
            if (string.IsNullOrWhiteSpace(entity.yzbm)) throw new FailedException("Mã bưu điện là bắt buộc");
            CheckLocationLen(entity.psdbm, "Mã điểm phân phối");
            CheckLocationLen(entity.psdmc, "Tên điểm phân phối");
            CheckLocationLen(entity.psdz, "Địa chỉ phân phối");
            CheckLocationLen(entity.lxrxm, "Tên người liên hệ");
            CheckLocationLen(entity.lxdh, "Số điện thoại liên hệ");
            CheckLocationLen(entity.yzbm, "Mã bưu điện");
            CheckLocationLen(entity.bzxx, "Thông tin ghi chú");
            entity.psdbm = entity.psdbm.Trim();
            entity.psdmc = entity.psdmc.Trim();
            entity.psdz = entity.psdz.Trim();
            entity.lxrxm = entity.lxrxm.Trim();
            entity.lxdh = entity.lxdh.Trim();
            entity.yzbm = entity.yzbm.Trim();
            if (entity.bzxx != null) entity.bzxx = entity.bzxx.Trim();
        }

        private static void CheckLocationLen(string value, string fieldName)
        {
            if (!string.IsNullOrEmpty(value) && value.Trim().Length > 50) throw new FailedException(fieldName + " quá dài (tối đa 50 ký tự)");
        }

        //Cập nhật trạng thái điểm giao hàng
        public int LocationStateUpdate(string Id, string orgId, int psdzt)
        {
            //G29-F8: chỉ đổi trạng thái row đúng org (tham số orgId trước đây không dùng); psdzt chỉ nhận 1/2/3
            if (psdzt != 1 && psdzt != 2 && psdzt != 3) throw new FailedException("Trạng thái điểm giao hàng không hợp lệ (chỉ nhận 1, 2 hoặc 3)");
            var dbEntity = this.FindEntity(p => p.Id == Id && p.OrganizeId == orgId);
            if (dbEntity == null) throw new FailedException("Không tìm thấy điểm giao hàng.");
            //properties
            dbEntity.psdzt = psdzt;//psdzt 1Đã lưu 2Đã truyền báo 3Đã hủy bỏ
            dbEntity.Modify(Id);
            return this.Update(dbEntity);
        }
    }
}
