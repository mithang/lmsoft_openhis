using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Domain.ValueObjects;
using Newtouch.EMR.Domain.BusinessObjects;
using Newtouch.EMR.Domain.DTO.OutputDto.MRUpload;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using Newtouch.EMR.Infrastructure;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SqlSugar;
using Newtouch.EMR.Repository;

namespace Newtouch.EMR.DomainServices
{
    public class NursingRecordSSDmnService : RepositoryBase<bl_hljl_ssEntity>, INursingRecordSSDmnService
    {

        

        public NursingRecordSSDmnService()
        {
        }
       
        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(bl_hljl_ssEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable(p => p.zyh == entity.zyh && p.rq == entity.rq && p.sj == entity.sj && p.Id != keyValue && p.zt == "1").Any())
                {
                    throw new FailedException("InpatientVitalSigns_Repeated_Error", "Nhập trùng lặp");
                }
                var dbEntity = this.FindEntity(keyValue);
                if (dbEntity.OrganizeId != entity.OrganizeId)
                {
                    throw new FailedException("Error", "Thao tác bất thường");
                }
                //properties
                var ignoreProps = new List<string>() { "Id", "CreateTime", "CreatorCode" };
                entity.MapperTo(dbEntity, ignoreProps);
                this.Update(dbEntity);
            }
            else
            {
                if (this.IQueryable(p => p.zyh == entity.zyh && p.rq == entity.rq && p.sj == entity.sj && p.zt == "1").Any())
                {
                    throw new FailedException("InpatientVitalSigns_Repeated_Error", "Nhập trùng lặp");
                }
                this.Insert(entity);
            }
        }
        /// <summary>
        /// Truy vấn phân trang điều dưỡng
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<bl_hljl_ssEntity> GetPaginationListSS(Pagination pagination, string orgId, DateTime? kssj, DateTime? jssj, string zyh, string wardCode, string isShowDelete)
        {
            string sql = @"select * from bl_hljl_ss where createtime>=@kssj  and createtime<=@jssj and OrganizeId=@orgId and zt='1'";
            var pars = new List<SugarParameter>();
            pars.Add(new SugarParameter("@orgId", orgId));
            pars.Add(new SugarParameter("@kssj", kssj.Value.ToString()));
            pars.Add(new SugarParameter("@jssj", jssj.Value.AddDays(1).ToString()));
            if (zyh != "")
            {
                sql += " and zyh=@zyh";
                pars.Add(new SugarParameter("@zyh", zyh.Trim()));
            }

            return this.FindList<bl_hljl_ssEntity>(sql, pars.ToArray());


        }
    }
}
