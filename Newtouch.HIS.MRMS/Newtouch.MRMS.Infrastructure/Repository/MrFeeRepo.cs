using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Infrastructure;

namespace Newtouch.MR.ManageSystem.Repository
{
    public class MrFeeRepo : RepositoryBase<bafeeEntity>, IMrFeeRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public MrFeeRepo()
            
        {

        }
		
        public int SubmitForm(bafeeVO entity, string keyValue,string OrganizeId,bafeeVO oldEntity)
        {
			// FIX-2 C2-B2: tao moi (keyValue rong) thi controller van goi
			// GetFormJson -> oldEntity null; dereference oldEntity.Lev o day
			// gay NRE. Khi khong co ban cu, flag = false: tao moi di nhanh
			// insert (keyValue rong), sua doi ten thi di nhanh update nhu cu.
			var flag = oldEntity != null && ((oldEntity.Lev == 1 && entity.name1 == oldEntity.name1) || (oldEntity.Lev == 2 && entity.name1 == oldEntity.name1 && entity.name2 == oldEntity.name2));//trueThực hiệnThêm mới

			if ( (!string.IsNullOrEmpty(keyValue)) && (!flag) )
            {
                //if (this.IQueryable().Any(p => p.ssdm == entity.ssdm && p.Id != keyValue && p.zt == "1"))
                //{
                //    throw new FailedException("Số hiệu không được trùng lặp");
                //}
                //var dbEntity = this.FindEntity(keyValue);
                var dbEntity = new bafeeEntity();
				//properties
				dbEntity.Id = keyValue;
                dbEntity.OrganizeId = OrganizeId;
                dbEntity.Name = entity.name;
                dbEntity.py = entity.py;
				var code = getCode(dbEntity.Lev, dbEntity.ParentCode,OrganizeId).ToString();
				dbEntity.ShortCode = entity.ShortCode == null ? "-" : entity.ShortCode; ;
                if (entity.code1 == null)
                {
                    dbEntity.Lev = 1;
                    dbEntity.ParentCode = null;
                }
                else if (entity.code2 == null)
                {
                    dbEntity.Lev = 2;
                    dbEntity.ParentCode = entity.code1;
                }
                else
                {
                    dbEntity.Lev = 3;
                    dbEntity.ParentCode = entity.code2;
                }
                if (entity.Lev == dbEntity.Lev && entity.ParentCode == dbEntity.ParentCode)
                {
                    //Nhóm lớnChưaSửa
                    dbEntity.px = entity.px;
                    dbEntity.Code = entity.code;
                }
                else
                {
                    dbEntity.px = getPx(dbEntity.Lev, dbEntity.ParentCode,OrganizeId);//Lấy px lớn nhất + 1
					if (dbEntity.Lev == 1)
					{
						//dbEntity.Code = dbEntity.px.ToString();
						dbEntity.Code = code;
					}
					else
					{
						if (code == "1")
						{
							dbEntity.Code = dbEntity.ParentCode + code.ToString().PadLeft(2, '0');
						}
						else
						{
							dbEntity.Code = code;
						}
						//dbEntity.Code = dbEntity.ParentCode+dbEntity.px.ToString().PadLeft(2, '0');
					}
                }
                dbEntity.zt = "1";

				dbEntity.Modify(keyValue);
				return Update(dbEntity);
				//return 0;
			}
            else
            {
                var dbEntity = new bafeeEntity();
                dbEntity.OrganizeId = OrganizeId;
                dbEntity.Name = entity.name;
                dbEntity.py = entity.py;
                dbEntity.ShortCode = entity.ShortCode==null?"-": entity.ShortCode;
                if (entity.code1 == null)
                {
                    dbEntity.Lev = 1;
                    dbEntity.ParentCode = null;
                }
                else if (entity.code2 == null)
                {
                    dbEntity.Lev = 2;
                    dbEntity.ParentCode = entity.code1;
                }
                else {
                    dbEntity.Lev = 3;
                    dbEntity.ParentCode = entity.code2;
                }
                dbEntity.px =getPx(dbEntity.Lev,dbEntity.ParentCode,OrganizeId);
				var code= getCode(dbEntity.Lev, dbEntity.ParentCode, OrganizeId).ToString();
				if (dbEntity.Lev == 1)
                {
					//dbEntity.Code = dbEntity.px.ToString();
					dbEntity.Code = code;
				}
                else {
					if (code == "1")
					{
						dbEntity.Code = dbEntity.ParentCode + code.ToString().PadLeft(2, '0');
					}
					else {
						dbEntity.Code = code;
					}
                    //dbEntity.Code = dbEntity.ParentCode+dbEntity.px.ToString().PadLeft(2, '0');
                }
				dbEntity.Create(true);
				return Insert(dbEntity);
				//return 1;
			}
        }

        //public IList<int> SelectPx(int Lev, string parentCode)
        //{
        //    string sql = "select px from mr_dic_bafeetype  where 1=1 and zt='1'";
        //    var para = new List<SugarParameter>();
        //    if (Lev != 0)
        //    {
        //        sql += " and lev=@lev";
        //        para.Add(new SugarParameter("lev", Lev));
        //    }
        //    if (!string.IsNullOrWhiteSpace(parentCode))
        //    {
        //        sql += " and parentCode=@parentCode";
        //        para.Add(new SugarParameter("@parentCode", parentCode));
        //    }
        //    var result = this.FindList<IList<int>>(sql, para.ToArray()).FirstOrDefault();
        //    return result;
        //}

        public int getPx(int Lev, string parentCode,string OrganizeId) {
            string sql = "select isnull(Max(px),0)+1 px  from mr_dic_bafeetype  where zt='1'";
            var para = new List<SugarParameter>();
            if (Lev != 0) {
                sql += " and lev=@lev";
                para.Add(new SugarParameter("lev", Lev));
            }
            if (!string.IsNullOrWhiteSpace(parentCode))
            {
                sql += " and parentCode=@parentCode";
                para.Add(new SugarParameter("@parentCode", parentCode));
			}
			if (!string.IsNullOrWhiteSpace(OrganizeId))
			{
				sql += " and OrganizeId=@OrganizeId";
				para.Add(new SugarParameter("@OrganizeId", OrganizeId));
			}
			var result= this.FindScalarList<int>(sql, para.ToArray()).FirstOrDefault();
            //result = result == null ? 0 : result;
            return result;
        }

		public int getCode(int Lev, string parentCode,string OrganizeId)
		{
			string sql = "select isnull(Max(CONVERT(int,Code)),0)+1 code  from mr_dic_bafeetype  where zt='1' ";
			var para = new List<SugarParameter>();
			if (Lev != 0)
			{
				sql += " and lev=@lev";
				para.Add(new SugarParameter("lev", Lev));
			}
			if (!string.IsNullOrWhiteSpace(parentCode))
			{
				if (Lev == 1)
				{
					sql += " and parentCode is null";
				}
				else
				{
					sql += " and parentCode=@parentCode";
					para.Add(new SugarParameter("@parentCode", parentCode));
				}
			}
			if (!string.IsNullOrWhiteSpace(OrganizeId))
			{
				sql += " and OrganizeId=@OrganizeId";
				para.Add(new SugarParameter("@OrganizeId", OrganizeId));
			}
			var result = this.FindScalarList<int>(sql, para.ToArray()).FirstOrDefault();
			//result = result == null ? 0 : result;
			return result;
		}

		public int DeleteForm(string keyValue)
        {
            var dbEntity = this.FindEntity(keyValue);
            //properties
            dbEntity.zt = "0";
            dbEntity.Modify(keyValue);
            return Update(dbEntity);
        }

		public IList<bafeeEntity> GetAllFeeList(string orgId)
		{
			string sql = @"select * from mr_dic_bafeetype with(nolock) where zt='1'  and organizeid=@orgId";

			return this.FindList<bafeeEntity>(sql, new SugarParameter[] {
				new SugarParameter("@orgId",orgId)
			});
		}
	}
}
