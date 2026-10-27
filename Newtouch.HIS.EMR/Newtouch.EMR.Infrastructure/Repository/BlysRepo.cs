using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using System.Text;
using System.Linq;
using System;
using SqlSugar;
namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo：hyj
    /// Ngày：2018-10-12 17:43
    /// Mô tả：Bảng phần tử
    /// </summary>
    public class BlysRepo : RepositoryBase<BlysEntity>, IBlysRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh EF</param>

        public List<BlysEntity> GetYsTree(string orgId,string yssjid)
        {
            return this.IQueryable().Where(p => p.zt == "1"&&p.yssjid== yssjid).OrderBy(p=>p.ysmc).ToList();

        }
        public List<BlysEntity> GetYsTreeV2(string orgId,string keyword)
        {
            string sql = @"select * from [dbo].[bl_ys] a 
where  a.zt='1' and (a.OrganizeId=@orgId or a.OrganizeId='*')
 and a.ysmc like @keyword and a.yssjid!='-1'
 union all
 select * from [dbo].[bl_ys] b
where  b.zt='1' and (b.OrganizeId=@orgId or b.OrganizeId='*')
  and b.yssjid='-1'  order by yssjid ,px";
            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@orgId", orgId));
            para.Add(new SugarParameter("@keyword", "%" + keyword + "%"));
            return this.FindList<BlysEntity>(sql, para.ToArray()).OrderBy(p=>p.px).ToList();
        }
        public List<BlysMXEntity> GetYsMX(string orgId, string YsId)
        {
            var para = new List<SugarParameter>();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(" select * from bl_ysmx where zt='1'");
            if (!string.IsNullOrWhiteSpace(YsId))
            {
                sqlstr.Append(" and YsId =@YsId");
                para.Add(new SugarParameter("@YsId",YsId));
            }
            sqlstr.Append(" and (OrganizeId=@orgId or OrganizeId='*')");
            para.Add(new SugarParameter("@orgId", orgId));
           
            return this.FindList<BlysMXEntity>(sqlstr.ToString(), para.ToArray());
        }
        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(BlysEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);            

        
                dbEntity.ysmc = entity.ysmc;
                dbEntity.BackgroundText = entity.BackgroundText;
                dbEntity.DataSource = entity.DataSource;
                dbEntity.BindingPath = entity.BindingPath;
                dbEntity.yscode = entity.yscode;
                dbEntity.AutoUpdate = entity.AutoUpdate;
                dbEntity.Readonly = entity.Readonly;
                dbEntity.TypeName = entity.TypeName;
                dbEntity.Description = entity.Description;
                dbEntity.ListSource = entity.ListSource;
                dbEntity.UserEditable = entity.UserEditable;
                dbEntity.EditStyle = entity.EditStyle;
                dbEntity.Value = entity.Value;
                dbEntity.Style = entity.Style;
                dbEntity.Format = entity.Format;

                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime = DateTime.Now;
                this.Update(dbEntity);
            }
            else
            {
                this.Insert(entity);
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void DeleteForm(string keyValue)
        {
            //  this.Delete(p => p.Id == keyValue);
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.zt = "0";
                this.Update(dbEntity);
            }
        }

    }
}
