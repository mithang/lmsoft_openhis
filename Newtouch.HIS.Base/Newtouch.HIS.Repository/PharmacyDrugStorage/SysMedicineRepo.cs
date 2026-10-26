using Newtouch.Common.Model;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineRepo : RepositoryBase<SysMedicineEntity>, ISysMedicineRepo
    {
        public SysMedicineRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Lấy danh sách thông tin thuốc theo tổ chức
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysMedicineEntity> GetValidListByOrg(string OrganizeId, string keyword = null)
        {
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                return null;
            }
            var sql = @"select * from xt_yp where zt='1' and OrganizeId=@OrganizeId
and (ypCode like @searchKeyword or ypmc like @searchKeyword or py like @searchKeyword)order by py asc";
            return this.FindList<SysMedicineEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@OrganizeId", OrganizeId)
                , new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }

        /// <summary>
        /// Lấy List thuốc trong kho từ điển bảo hiểm y tế
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<GzybybItemCodeVO> GetYbMedicineList(string OrganizeId, string keyword = null)
        {
            //if (string.IsNullOrWhiteSpace(keyword))
            //{
            //    return null;
            //}
            try
         {
                //                var sql = @"select top 50 yka003 ypmc, yka002 ybdm, aka074 gg, yka601 ycmc
                //from NewtouchHIS_Base..Gzyb_ybItemCode
                //where 1=1
                //and (yka003 like @searchKeyword or yka003 like @searchKeyword or yka389 like @searchKeyword or yka601 like @searchKeyword)
                //order by yka389 asc";
                var sql = @"select top 200  cod.ypmc, cod.ypdm ybdm,  cod.gg,  cod.ycmc,cod.ypxz ybxz,case cod.ypxz when '" + ((int)EnumZFXZv2.J).ToString() +
                    "' then '甲类' when '" + ((int)EnumZFXZv2.Y).ToString() +
                    @"' then '乙类' else '丙类' end  ypxz, cod.py py, cod.ybdj ybdj, cod.gjybdm gjybdm,cod.ypjx jxmc,cod.pzwh pzwh
from NewtouchHIS_Base..Cqyb_ybItemCode cod
where 1=1 and cod.zt=1 and cod.OrganizeId=@orgId";
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    sql += " and (cod.ypmc like @searchKeyword  or cod.ypdm like @searchKeyword or cod.py like @searchKeyword or cod.gjybdm like @searchKeyword or cod.pzwh like @searchKeyword or cod.ycmc like @searchKeyword)";

                }

                sql += " order by cod.py desc";
                return this.FindList<GzybybItemCodeVO>(sql, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%"),
                new SqlParameter("@orgId",OrganizeId)});
            }
            catch
            {
                //Không có bảng này
                return null;
            }
        }
        /// <summary>
        /// Truy vấn thông tin bảng tên bảo hiểm y tế
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<GzybNameCodeVO> GetYbName(string OrganizeId, string lx, string keyword = null)
        {
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                return null;
            }

            var sql = @"select top 50 id,xmmc,pym,ybxj,dfybdm,gjybdm,lx,gg,dw,dj,pzwh,sccj,dfxmmc from Cqyb_ybxm where zt = '1' and  OrganizeId=@OrganizeId 
                and (id LIKE @searchKeyword or
                xmmc like @searchKeyword or
                pym like @searchKeyword  or
                dfybdm like @searchKeyword or
                gjybdm like @searchKeyword or
                pzwh like @searchKeyword or
                sccj like @searchKeyword) ";
            if (lx == "126" || lx == "00000015" || lx == "00000016")
            {
                sql += " and lx='材料'";
            }
            else
            {
                sql += " and lx='项目'";
            }
            return this.FindList<GzybNameCodeVO>(sql, new SqlParameter[] {
                    
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%"),
                new SqlParameter("@OrganizeId",OrganizeId)
                });
        }
        /// <summary>
        /// Lấy List thuốc trong kho từ điển bảo hiểm y tế
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
            public IList<GzxnhybItemCodeVO> GetYbXNHMedicineList(string keyword = null)
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return null;
                }
                try
                {
                    var sql = @"SELECT  code ,
        name ,
        dosageForm ,
        CASE WHEN isBase = '0' THEN '否'
             WHEN isBase = '1' THEN '是'
             ELSE ''
        END isBase
FROM    [NewtouchHIS_Base].[dbo].[Gaxnh_S30]
WHERE   (name LIKE @searchKeyword or code like @searchKeyword or pcode like @searchKeyword ) AND zt='1' ";
                    return this.FindList<GzxnhybItemCodeVO>(sql, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
                }
                catch
                {
                    //Không có bảng này
                    return null;
                }
            }

        }
    }
