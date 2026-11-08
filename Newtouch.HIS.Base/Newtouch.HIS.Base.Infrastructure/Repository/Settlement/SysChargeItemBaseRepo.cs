using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Tools;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Hạng mục thu phí
    /// </summary>
    public class SysChargeItemBaseRepo : RepositoryBase<SysChargeItemBaseEntity>, ISysChargeItemBaseRepo
    {
        public SysChargeItemBaseRepo()
            
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysChargeItemBaseEntity GetForm(int keyValue)
        {
            return this.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<SysChargeItemBaseEntity> GetList(string keyValue, string orgId)
        {
            return this.IQueryable().Where(a => a.OrganizeId == orgId).OrderBy(a => a.px).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysChargeItemBaseEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                //Code Kiểm tra trùng lặp
                if (this.IQueryable().Any(p => p.sfxmCode == entity.sfxmCode && p.sfxmId != keyValue.Value && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                SysChargeItemBaseEntity oldEntity = null;   //Trước khi thay đổi Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.sfxmId = keyValue.Value;
                // F16-FIX D1: form khong post CreatorCode/CreateTime nen entity mang
                // null/default; this.Update(entity) 1 tham so bind vao overload generic
                // RepositoryBase.Update<T> (khong ignore) thay vi overload virtual co
                // ignore CreatorCode/CreateTime (RepositoryBase.cs:123 vs :331) -> UPDATE
                // ghi de lang 2 cot (xt_sfxm_Base NULLABLE: CreatorCode->NULL,
                // CreateTime->0001-01-01, mat audit am tham). Khoi phuc 2 cot tu dong
                // dang luu (oldEntity da nap san cho log thay doi) truoc khi update.
                if (oldEntity != null)
                {
                    entity.CreatorCode = oldEntity.CreatorCode;
                    entity.CreateTime = oldEntity.CreateTime;
                }
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_sfxm", oldEntity.sfxmId.ToString());
                }
            }
            else
            {
                //Code Kiểm tra trùng lặp
                if (this.IQueryable().Any(p => p.sfxmCode == entity.sfxmCode && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                entity.Create();
                this.Insert(entity);
            }
        }

        /// <summary>
        /// Đồng bộ BHYT sửa thời gian tải lên cuối cùng
        /// </summary>
        /// <param name="ypId"></param>
        /// <returns></returns>
        public bool YibaoUpload(int sfxmId, out string error)
        {
            try
            {
                var ypsxEntity = this.IQueryable().Where(p => p.sfxmId == sfxmId).FirstOrDefault();
                if (ypsxEntity == null)
                {
                    error = "HIS không tìm thấy khoản thu phí này";
                    return false;
                }
                ypsxEntity.LastYBUploadTime = System.DateTime.Now;
                this.Update(ypsxEntity);
                error = "";
                return true;
            }
            catch
            {
                error = "HIS cập nhật thời gian đồng bộ BHYT của thuốc thất bại";
                return false;
            }
        }
    }
}
