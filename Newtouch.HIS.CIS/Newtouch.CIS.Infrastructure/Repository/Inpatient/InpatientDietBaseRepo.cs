using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

using System.Collections.Generic;
using Newtouch.CIS.Compat;
using NewtouchHIS.Lib.Base.Model;
using System.Data.SqlClient;
using System.Text;
using Newtouch.CIS.Repository;
using System.Linq;

namespace Newtouch.Repository
{
    
    public class InpatientDietBaseRepo : RepositoryBase<InpatientDietBaseEntity>, IInpatientDietBaseRepo
    {
        /// <summary>
        /// Mặc địnhHàm khởi tạosố
        /// </summary>
        /// <param name="databaseFactory">EFtrêndướivănNhà máy</param>
        public InpatientDietBaseRepo()
            : base()
        {
            
        }

        /// <summary>
        /// Lưubảngphiếu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void SubmitForm(InpatientDietBaseEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //var dbEntity = this.FindEntity(keyValue);
                //dbEntity.Modify(keyValue);
                //this.Update(dbEntity);
                //entity.Modify(keyValue);
                //this.Update(entity);
                InpatientDietBaseEntity oldEntity = null;   //đổihơntrướcEntity
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

        public  IList<InpatientDietBaseEntity> GetGridList(Pagination pagination, string lb, string keyword,string orgId) {
            var rntlist = new List<InpatientDietBaseEntity>();
            IList<SqlParameter> parlist = new List<SqlParameter>();
            var sql = new StringBuilder();
            sql.Append(@"SELECT  Id ,
                        OrganizeId ,
                        Name ,
                        DietType ,
                        DietGroup ,
                        DietBigType ,
                        py ,
                        ParentId ,
                        bdsfxm ,
                        CreateTime ,
                        CreatorCode,
                        LastModifyTime,
                        LastModifierCode,
                        zt
                FROM    zy_DietBase where 1=1 ");
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and (name like @keyword or py like @keyword)");
                parlist.Add(new SqlParameter("@keyword", "%"+keyword+"%"));
            }
            if (!string.IsNullOrWhiteSpace(lb))
            {
                sql.Append(" and DietBigType = @lb");
                parlist.Add(new SqlParameter("@lb", lb));
            }
            sql.Append(" and OrganizeId = @orgId");
            parlist.Add(new SqlParameter("@orgId", orgId));
            return this.QueryWithPage<InpatientDietBaseEntity>(sql.ToString(), pagination, parlist.ToArray());
        }
    }
}