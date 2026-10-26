
using Newtouch.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Linq;
using System;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

namespace Newtouch.Repository
{
    public class BlctglRepo : RepositoryBase<BlctglEntity>, IBlctglRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public BlctglRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


        public List<BlctglEntity> GetTreeList(string orgId, int qx, string UserCode, string parentId)
        {
            return this.IQueryable().Where(p => p.OrganizeId == orgId && p.zt == "1" && p.qx == qx && p.CreatorCode == UserCode && p.parentId == parentId).OrderByDescending(p => p.CreateTime).ToList();
        }
        public BlctglEntity GetCitiaoByID(string orgId, string ID)
        {
            return this.FindEntity(p => p.ID == ID && p.OrganizeId == orgId);
        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(BlctglEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.ctnr = entity.ctnr;
                dbEntity.mc = entity.mc;
                dbEntity.LastModifyTime = DateTime.Now;
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                entity.ID = Guid.NewGuid().ToString();
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void DeleteForm(string keyValue)
        {
            // this.Delete(p => p.ID == keyValue);

            var dbEntity = this.FindEntity(keyValue);
            dbEntity.zt = "0";
            this.Update(dbEntity);
        }

    }
}
