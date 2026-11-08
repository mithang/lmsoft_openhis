using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineBaseRepo : RepositoryBase<SysMedicineBaseEntity>, ISysMedicineBaseRepo
    {
        public SysMedicineBaseRepo()
            
        {
        }

        /// <summary>
        /// Lấy danh sách thông tin thuốc theo tổ chức
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysMedicineBaseEntity> GetValidListByOrg(string OrganizeId, string keyword = null)
        {
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                return null;
            }
            var sql = @"select * from xt_yp_base where zt='1' and OrganizeId=@OrganizeId
and (ypCode like @searchKeyword or ypmc like @searchKeyword or py like @searchKeyword)order by py asc";
            return this.FindList<SysMedicineBaseEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@OrganizeId", OrganizeId)
                , new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }

        /// <summary>
        /// Lấy thuốc từ kho từ điển BHYTList
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
                    "' then 'Loại A' when '" + ((int)EnumZFXZv2.Y).ToString() +
                    @"' then 'Loại B' else 'Loại C' end  ypxz, cod.py py, cod.ybdj ybdj, cod.gjybdm gjybdm,cod.ypjx jxmc,cod.pzwh pzwh
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
        /// Tra cứu thông tin bảng tên BHYT
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

            // Cqyb_ybxm chỉ có 12 cột (id, xmmc, pym, ybxj, dfybdm, gjybdm, OrganizeId, zt,
            // CreateTime, CreatorCode, LastModifyTime, LastModifierCode) — KHÔNG có lx, gg, dw,
            // dj, pzwh, sccj, dfxmmc nên câu SELECT cũ lỗi Msg 207. Chỉ SELECT cột đang có;
            // lx không lọc được theo cột nên trả theo mapping từ tham số đầu vào (mã loại
            // khoản thu sfdlCode: 126/00000015/00000016 → Vật tư, còn lại → Hạng mục);
            // gg/dw/dj/pzwh/sccj/dfxmmc chỉ là cột hiển thị trên floating selector nên trả rỗng.
            // organizeId="*" là quy ước "tất cả tổ chức" nên bỏ filter OrganizeId.
            var lxLabel = (lx == "126" || lx == "00000015" || lx == "00000016") ? "Vật tư" : "Hạng mục";
            var sql = @"select top 50 id,xmmc,pym,ybxj,dfybdm,gjybdm,@lxLabel as lx,'' as gg,'' as dw,'' as dj,'' as pzwh,'' as sccj,'' as dfxmmc from Cqyb_ybxm where zt = '1' 
                and (id LIKE @searchKeyword or
                xmmc like @searchKeyword or
                pym like @searchKeyword  or
                dfybdm like @searchKeyword or
                gjybdm like @searchKeyword) ";
            if (OrganizeId != "*")
            {
                sql += " and OrganizeId=@OrganizeId ";
            }
            return this.FindList<GzybNameCodeVO>(sql, new SqlParameter[] {
                    
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%"),
                new SqlParameter("@OrganizeId",OrganizeId),
                new SqlParameter("@lxLabel", lxLabel)
                });
        }
        /// <summary>
        /// Lấy thuốc từ kho từ điển BHYTList
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
        CASE WHEN isBase = '0' THEN 'Không'
             WHEN isBase = '1' THEN 'Có'
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
        
        public  int GetBaseLastYpId()
        {
            var sql = @"select max(ypId) from xt_yp_base";
            
            return  FirstOrDefault<int>(sql);
        }

    }
    }
