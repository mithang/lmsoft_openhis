using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System.Linq;
using SqlSugar;
using System.Collections.Generic;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// Tạo bởi:chl
    /// Ngày:2019-11-06 10:22
    /// Mô tả:Hồ sơ nhân viên phẫu thuật
    /// </summary>
    public class OROpStaffRecordRepo : RepositoryBase<OROpStaffRecordEntity>, IOROpStaffRecordRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh</param>
        public OROpStaffRecordRepo()
        {
            
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(OROpStaffRecordEntity entity, string keyValue)
        {
            var flag = this.IQueryable().Any(p => p.ssxh == entity.ssxh && p.rylb == entity.rylb && p.px == entity.px && p.zt == "1");
            if (flag==true)
            {
                var Id= GetIdByCond(entity.ssxh, entity.rylb, entity.px).Id;
                var dbEntity = this.FindEntity(Id);
                //properties
                //dbEntity.CreateTime = entity.CreateTime;
                //dbEntity.CreatorCode = entity.CreatorCode;
                dbEntity.Id = Id;
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime = entity.LastModifyTime;
                dbEntity.memo = entity.memo;
                dbEntity.OrganizeId = entity.OrganizeId;
                dbEntity.px = entity.px;
                dbEntity.rygh = entity.rygh;
                dbEntity.rylb = entity.rylb;
                dbEntity.ryxm = entity.ryxm;
                dbEntity.ssxh = entity.ssxh;
                dbEntity.zt = entity.zt;
                EntityHelper.Modify(dbEntity, Id);
                return Update(dbEntity);
            }
            else
            {
                OROpStaffRecordEntity ent = new OROpStaffRecordEntity();
                ent.CreateTime = entity.CreateTime;
                ent.CreatorCode = entity.CreatorCode;
                ent.Id = entity.Id;
                //ent.LastModifierCode = entity.LastModifierCode;
                //ent.LastModifyTime = entity.LastModifyTime;
                ent.memo = entity.memo;
                ent.OrganizeId = entity.OrganizeId;
                ent.px = entity.px;
                ent.rygh = entity.rygh;
                ent.rylb = entity.rylb;
                ent.ryxm = entity.ryxm;
                ent.ssxh = entity.ssxh;
                ent.zt = entity.zt;
                EntityHelper.Create(ent, true);
                return Insert(ent);
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int DeleteForm(string keyValue)
        {
            var dbEntity = this.FindEntity(keyValue);
            //properties
            dbEntity.zt = "0";
            EntityHelper.Modify(dbEntity, keyValue);
            return Update(dbEntity);
        }

        public OROpStaffRecordEntity GetIdByCond(string ssxh, string rylb, int px) {
            string sql = "select * from [OR_OpStaffRecord] where zt!=0 ";

            sql += " and ssxh=@ssxh";
            sql += " and rylb=@rylb";
            sql += " and px=@px";
            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@ssxh", ssxh));
            para.Add(new SugarParameter("@rylb", rylb));
            para.Add(new SugarParameter("@px", px));
            return this.FindList<OROpStaffRecordEntity>(sql, para.ToArray()).FirstOrDefault();
        }

        public IList<OROpStaffRecordEntity> getIdBySsxh(string ssxh) {
            string sql = "select * from [dbo].[OR_OpStaffRecord] where zt!=0";
            sql += " and ssxh=@ssxh";
            var result= this.FindList<OROpStaffRecordEntity>(sql, new SugarParameter[] {
                new SugarParameter("@ssxh", ssxh)
            });
            return result;
        }
    }
}