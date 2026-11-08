using System;
using Newtouch.Tools;
using Newtouch.HIS.Repository;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Data.SqlClient;
using NewtouchHIS.Lib.Base.Exceptions;

namespace Newtouch.HIS.Repository
{
    public class SysWardRoomRepo:RepositoryBase<SysWardRoomEntity>,ISysWardRoomRepo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysWardRoomRepo()
            
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<SysWardRoomEntity> GetPagintionList(Pagination pagination, string organizeId, string keyword = null)
        {
            var sql = @"select * from xt_bf  where OrganizeId=@organizeId 
                       and (isnull(@keyword, '') = '' or bfCode like @searchkeyword 
                        or bfNo like @searchkeyword or bqCode like @searchkeyword )";
            SqlParameter[] param = new SqlParameter[] {
                 new SqlParameter("@keyword",keyword ?? ""),
                            new SqlParameter("@organizeId",organizeId),
                            new SqlParameter("@searchkeyword", "%" + (keyword ?? "") + "%")
            };
            return this.QueryWithPage<SysWardRoomEntity>(sql, pagination, param);
        }

        /// <summary>
        /// Sửa và thêm thông tin khu bệnh
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void submitForm(SysWardRoomEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId
                && p.bfId != keyValue && p.bfCode == entity.bfCode
                ))
                {
                    throw new FailedException("Mã phòng không được trùng lặp!");
                }

                SysWardRoomEntity oldEntity = null;   //Trước khi thay đổi Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.bfId = keyValue.Value;
                // F16-FIX D1: form khong post CreatorCode/CreateTime nen entity mang
                // null/default; this.Update(entity) 1 tham so bind vao overload generic
                // RepositoryBase.Update<T> (khong ignore) thay vi overload virtual co
                // ignore CreatorCode/CreateTime (RepositoryBase.cs:123 vs :331) -> UPDATE
                // gan NULL vao cot CreatorCode NOT NULL. Khoi phuc 2 cot tu dong dang
                // luu (oldEntity da nap san cho log thay doi) truoc khi update.
                if (oldEntity != null)
                {
                    entity.CreatorCode = oldEntity.CreatorCode;
                    entity.CreateTime = oldEntity.CreateTime;
                }
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_bf", oldEntity.bfId.ToString());
                }
            }
            else
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId &&
                p.bfCode == entity.bfCode))
                {
                    throw new FailedException("Mã phòng không được trùng lặp!");
                }
                entity.Create();
                this.Insert(entity);
            }
        }

    }
}
