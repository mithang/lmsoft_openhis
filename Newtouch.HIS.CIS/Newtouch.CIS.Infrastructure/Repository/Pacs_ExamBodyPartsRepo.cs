
using Newtouch.CIS.Repository;
using Newtouch.CIS.Compat;

using Newtouch.Domain.Entity.Outpatient;
using Newtouch.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository
{
    public class Pacs_ExamBodyPartsRepo : RepositoryBase<Pacs_ExamBodyPartsEntity>, IPacs_ExamBodyPartsRepo
    {
        private readonly ICache _cache;

        public Pacs_ExamBodyPartsRepo(ICache cache)
            : base()
        {
            _cache = cache;
        }

        /// <summary>
        /// Lấydanh sách
        /// </summary>
        /// <returns></returns>
        public List<Pacs_ExamBodyPartsEntity> GetListByOrg(string orgId)
        {
            return this.IQueryable().Where(p => p.OrganizeId == orgId).OrderByDescending(p => p.CreateTime).ToList();
        }
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(Pacs_ExamBodyPartsEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.jcbwCode == entity.jcbwCode && p.Id != keyValue))
                {
                    throw new FailedException("Vị tríSố hiệu không được trùng lặp");
                }

                // FIX-2 B-CIS-03 (cung mau AuxiliaryDictionary: edit bang posted
                // entity + full-column update): nap ban goc de giu CreatorCode/
                // CreateTime khi form post khong co cac truong nay.
                var oldEntity = this.FindEntity(keyValue);
                entity.Modify(keyValue);
                if (oldEntity != null)
                {
                    if (string.IsNullOrWhiteSpace(entity.CreatorCode))
                        entity.CreatorCode = oldEntity.CreatorCode;
                    if (entity.CreateTime == default)
                        entity.CreateTime = oldEntity.CreateTime;
                }
                this.Update(entity);
            }
            else
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.jcbwCode == entity.jcbwCode))
                {
                    throw new FailedException("Vị tríSố hiệu không được trùng lặp");
                }
                entity.Create(true);
                this.Insert(entity);
            }
            //Làm mớichậmtồn tại
            _cache.Remove(string.Format(CacheKey.ValidSysBodyPartsSetKey_, entity.OrganizeId));
        }
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            var entity = this.FindEntity(keyValue);
            this.Delete(entity);
        }
    }
}
