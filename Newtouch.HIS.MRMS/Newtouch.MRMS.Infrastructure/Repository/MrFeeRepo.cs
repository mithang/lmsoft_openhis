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
		
        public int SubmitForm(bafeeVO entity, string keyValue, string OrganizeId)
        {
            // G21: name la cot NOT NULL; form khong post name thi loi SQL tho.
            // Sua chi validate field duoc post (merge-skip-null).
            if (string.IsNullOrEmpty(keyValue))
            {
                if (string.IsNullOrWhiteSpace(entity.name))
                {
                    throw new FailedException("Tên nhóm thu phí không được để trống");
                }
            }
            else if (entity.name != null && string.IsNullOrWhiteSpace(entity.name))
            {
                throw new FailedException("Tên nhóm thu phí không được để trống");
            }
            if (entity.name != null && entity.name.Length > 50)
            {
                throw new FailedException("Tên nhóm thu phí tối đa 50 ký tự");
            }
            if (entity.ShortCode != null && entity.ShortCode.Length > 50)
            {
                throw new FailedException("Mã viết tắt tối đa 50 ký tự");
            }
            if (entity.py != null && entity.py.Length > 50)
            {
                throw new FailedException("Pinyin tối đa 50 ký tự");
            }

            if (!string.IsNullOrEmpty(keyValue))
            {
                // Sua: keyValue co + row ton tai moi duoc update. Bo flag cu so sanh
                // name1/name2: doi ten khong doi parent thi flag=true -> roi nhanh
                // INSERT, tao row trung lap thay vi sua row cu. Sua luon load row cu
                // (merge-skip-null) thay vi tao entity moi: entity moi co CreateTime
                // null -> Update loi SQL tho "Cannot insert NULL into CreateTime".
                // Cung bo luon GetFormJson(keyValue, Lev=0) tra row sai o controller.
                var dbEntity = this.FindEntity(keyValue);
                if (dbEntity == null)
                {
                    throw new FailedException("Không tìm thấy thông tin nhóm thu phí.");
                }
                if (!string.IsNullOrWhiteSpace(OrganizeId) && dbEntity.OrganizeId != OrganizeId)
                {
                    throw new FailedException("Không có quyền sửa dữ liệu của tổ chức khác.");
                }
                var oldLev = dbEntity.Lev;
                var oldParentCode = dbEntity.ParentCode;
                if (entity.name != null)
                {
                    dbEntity.Name = entity.name;
                }
                if (entity.py != null)
                {
                    dbEntity.py = entity.py;
                }
                if (entity.ShortCode != null)
                {
                    dbEntity.ShortCode = entity.ShortCode;
                }
                int newLev;
                string newParentCode;
                if (entity.code1 == null)
                {
                    newLev = 1;
                    newParentCode = null;
                }
                else if (entity.code2 == null)
                {
                    newLev = 2;
                    newParentCode = entity.code1;
                }
                else
                {
                    newLev = 3;
                    newParentCode = entity.code2;
                }
                dbEntity.Lev = newLev;
                dbEntity.ParentCode = newParentCode;
                if (newLev == oldLev && newParentCode == oldParentCode)
                {
                    // Chi doi ten: giu nguyen px/Code cu
                }
                else
                {
                    // Doi cap/parent: tinh lai px/Code SAU khi da co Lev/ParentCode moi
                    // (code cu goi getCode voi Lev=0/ParentCode=null -> Code max toan org).
                    dbEntity.px = getPx(newLev, newParentCode, OrganizeId);
                    var code = getCode(newLev, newParentCode, OrganizeId).ToString();
                    if (newLev == 1)
                    {
                        dbEntity.Code = code;
                    }
                    else
                    {
                        if (code == "1")
                        {
                            dbEntity.Code = newParentCode + code.PadLeft(2, '0');
                        }
                        else
                        {
                            dbEntity.Code = code;
                        }
                    }
                }
                dbEntity.zt = "1";

                dbEntity.Modify(keyValue);
                return Update(dbEntity);
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

		public int DeleteForm(string keyValue, string organizeId = null)
        {
            // G21: FindEntity null -> loi sach thay vi NRE; truyen organizeId thi
            // chan xoa row cua to chuc khac.
            var dbEntity = this.FindEntity(keyValue);
            if (dbEntity == null)
            {
                throw new FailedException("Không tìm thấy thông tin nhóm thu phí.");
            }
            if (!string.IsNullOrWhiteSpace(organizeId) && dbEntity.OrganizeId != organizeId)
            {
                throw new FailedException("Không có quyền xóa dữ liệu của tổ chức khác.");
            }
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
