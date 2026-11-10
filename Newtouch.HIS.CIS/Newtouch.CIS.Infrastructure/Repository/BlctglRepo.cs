
using Newtouch.Infrastructure;
using System.Collections.Generic;
using Newtouch.CIS.Compat;
using NewtouchHIS.Lib.Base.Model;
using System.Data.SqlClient;
using System.Text;
using Newtouch.CIS.Repository;

using System.Linq;
using System;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

namespace Newtouch.Repository
{
    public class BlctglRepo : RepositoryBase<BlctglEntity>, IBlctglRepo
    {
        /// <summary>
        /// Mặc định Hàm khởi tạo số
        /// </summary>
        /// <param name="databaseFactory">EF trên dưới văn Nhà máy</param>
        public BlctglRepo()
            : base()
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
        /// Lưu bảng phiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thực thể đối tượng</param>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        public void SubmitForm(BlctglEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                if (dbEntity == null)
                {
                    throw new FailedException("Không tìm thấy mẫu bảng phiếu cần sửa, thao tác thất bại.");
                }
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
        /// Xóa Dữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        public void DeleteForm(string keyValue)
        {
            // this.Delete(p => p.ID == keyValue);

            var dbEntity = this.FindEntity(keyValue);
            if (dbEntity == null)
            {
                throw new FailedException("Không tìm thấy mẫu bảng phiếu cần xóa, thao tác thất bại.");
            }
            dbEntity.zt = "0";
            this.Update(dbEntity);
        }

    }
}
