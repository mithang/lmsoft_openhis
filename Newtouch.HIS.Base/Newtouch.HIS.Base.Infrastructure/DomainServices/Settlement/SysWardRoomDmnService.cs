using Newtouch.HIS.Domain.IDomainServices;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.DomainServices
{
    public class SysWardRoomDmnService: DmnServiceBase,ISysWardRoomDmnService
    {

        public SysWardRoomDmnService()
            
        {
        }

        /// <summary>
        /// Lấy danh sách phòng（Phân trang）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysWardRoomVO> GetPagintionList(Pagination pagination, string organizeId, string keyword)
        {
            //Mục liên kết dù không hiệu lực cũng hiển thị để nhắc
            string sql = @"select a.[bfId],a.[OrganizeId],a.[bfCode],a.[bfNo],a.[bqCode],a.[ksCode],a.[bz],a.[zt],
                            a.[CreatorCode],a.[CreateTime],a.[LastModifyTime],a.[LastModifierCode],a.[px],b.bqmc,c.Name ksmc 
                            from xt_bf a 
                            left join xt_bq b on a.bqCode=b.bqCode and a.organizeid=b.organizeid 
                            left join Sys_Department c on a.organizeid=c.organizeid and a.kscode=c.code 
                            where  a.OrganizeId=@organizeId ";
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " and (charindex(@keyword,a.bfCode,1) >0 or charindex(@keyword,a.bfNo,1) >0 or charindex(@keyword,a.bqCode,1) >0)";
            }

            SqlParameter[] param = new SqlParameter[] {
                 new SqlParameter("@keyword",keyword ?? ""),
                 new SqlParameter("@organizeId",organizeId)
                           
            };
            return this.QueryWithPage<SysWardRoomVO>(sql, pagination, param);
        }

        /// <summary>
        /// Lấy danh sách phòng
        /// </summary>
        /// <param name="bfId"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysWardRoomVO> GetWardRoomList(int? bfId, string organizeId, string keyword)
        {
            //Mục liên kết dù không hiệu lực cũng hiển thị để nhắc
            string sql = @"select a.[bfId],a.[OrganizeId],a.[bfCode],a.[bfNo],a.[bqCode],a.[ksCode],a.[bz],a.[zt],
                            a.[CreatorCode],a.[CreateTime],a.[LastModifyTime],a.[LastModifierCode],a.[px],b.bqmc,c.Name ksmc 
                            from xt_bf a 
                            left join xt_bq b on a.bqCode=b.bqCode and a.organizeid=b.organizeid 
                            left join Sys_Department c on a.organizeid=c.organizeid and a.kscode=c.code 
                            where  a.OrganizeId=@organizeId ";
            if (bfId != null && bfId > 0)
            {
                sql += " and a.[bfId]=@bfId ";
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " and (charindex(@keyword,a.bfCode,1) >0 or charindex(@keyword,a.bfNo,1) >0 or charindex(@keyword,a.bqCode,1) >0)";
            }

            SqlParameter[] param = new SqlParameter[] {
                 new SqlParameter("@keyword",keyword ?? ""),
                 new SqlParameter("@organizeId",organizeId),
                 new SqlParameter("@bfId",bfId)
            };
            return this.FindList<SysWardRoomVO>(sql, param);
        }

        /// <summary>
        /// Tìm phòng bệnh theo khu bệnh
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="bqCode"></param>
        /// <returns></returns>
        public IList<SysWardRoomVO> GetWardRoomListValid(string organizeId, string bqCode)
        {
            //Mục liên kết dù không hiệu lực cũng hiển thị để nhắc
            string sql = @"select a.[bfId],a.[OrganizeId],a.[bfCode],a.[bfNo],a.[bqCode],a.[ksCode],a.[bz],a.[zt],
                            a.[CreatorCode],a.[CreateTime],a.[LastModifyTime],a.[LastModifierCode],a.[px]
                            from xt_bf a 
                            where  a.OrganizeId=@organizeId and a.[zt]=1 ";
            if (bqCode != null && bqCode !="")
            {
                sql += " and a.[bqCode]=@bqCode ";
            }

            SqlParameter[] param = new SqlParameter[] {
                 new SqlParameter("@organizeId",organizeId),
                 new SqlParameter("@bqCode",bqCode)
            };
            return this.FindList<SysWardRoomVO>(sql, param);
        }
    }
}
