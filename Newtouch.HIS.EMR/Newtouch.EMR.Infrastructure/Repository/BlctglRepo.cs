using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using System.Text;
using System.Linq;
using System;
using Newtouch.EMR.Domain.ValueObjects;
using SqlSugar;
using Newtouch.EMR.Repository;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Người tạo：hyj
    /// Ngày：2018-09-07 15:46
    /// Mô tả：Quản lý từ mục
    /// </summary>
    public class BlctglRepo : RepositoryBase<BlctglEntity>, IBlctglRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh EF</param>


     
        public List<BlctglEntity> GetTreeList(string orgId,int qx, string UserCode, string parentId)
        {
            return this.IQueryable().Where(p => p.OrganizeId == orgId &&p.zt=="1"&&p.qx== qx&&p.CreatorCode== UserCode&& p.parentId == parentId).OrderByDescending(p => p.CreateTime).ToList();
        }
        public BlctglEntity GetCitiaoByID(string orgId, string ID)
        {
              return this.FindEntity(p => p.ID == ID&&p.OrganizeId== orgId);
        }

        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
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
                this.Update(dbEntity);
            }
            else
            {
                entity.ID= Guid.NewGuid().ToString();
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
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


		public List<BLYXFHVO> Getyxfhlist(int ls)
		{
			//var response = new List<DSrepeatResponseDto>();
			var inParameters = new List<SugarParameter>();
			inParameters.Add(new SugarParameter("@ls", ls));
			//inParameters.Add(new SugarParameter("@zyh", zyh));
			//inParameters.Add(new SugarParameter("@yzpartable", SqlDbType.Structured) { Value = req.ToDataTable(), TypeName = "yzpartable" });
			return FindList<BLYXFHVO>("exec usp_se_tsfh @xsl=@ls", inParameters.ToArray());
		}
	}
}
