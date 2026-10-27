

using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Compat;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Repository;
using NewtouchHIS.Lib.Base.Extension;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.DomainServices.Inpatient
{
    public class LevelChargeDmnService : DmnServiceBase, ILevelChargeDmnService
    {
        /// <summary>
        /// Mặc địnhHàm khởi tạosố
        /// </summary>
        /// <param name="databaseFactory">EFtrêndướivănNhà máy</param>
        public LevelChargeDmnService()
            : base()
        {

        }
        /// <summary>
        /// LấyđợicấpChi phí
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="bqCode"></param>
        /// <returns></returns>
        public IList<SysLevelChargeVO> GetLevelCharge(Pagination pagination,string orgId, string LevelCode)
        {
            var sql = @"SELECT  a.LevelCode ,
                        a.sfxmCode ,
                        a.sl ,
                        b.sfxmmc ,
                        a.Id ,
                        c.Name LevelName,
                        b.dj,
                        b.dw
                FROM    [dbo].[zy_LevelChargedy] a
                        INNER JOIN NewtouchHIS_Base..V_S_xt_sfxm b ON a.sfxmCode = b.sfxmCode
                                                                      AND a.OrganizeId = b.OrganizeId
                                                                      AND b.zt = '1'
                        LEFT JOIN NewtouchHIS_Base..V_C_Sys_ItemsDetail c ON c.CateCode = 'BedGrade'
                                                                             AND c.Code = a.LevelCode
                                                                             AND c.zt = '1'
                                                                             AND c.OrganizeId = a.OrganizeId
                WHERE   a.zt = '1'
                        AND a.OrganizeId = @orgId
                        AND ( a.LevelCode = @LevelCode
                        OR ISNULL(@LevelCode, '') = '')";
            return this.QueryWithPage<SysLevelChargeVO>(sql,pagination,
                new[] { new SqlParameter("@orgId", orgId),
                    new SqlParameter("@LevelCode", LevelCode??"")});
        }

        public SysLevelChargeVO GetFormJson(string orgId,string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                throw new FailedException("chưaĐã chọnGiá trị");
            }
            var sql = @"SELECT  a.LevelCode ,
                        a.sfxmCode ,
                        a.sl ,
                        b.sfxmmc ,
                        a.Id ,
                        a.zt
                FROM    [dbo].[zy_LevelChargedy] a
                        INNER JOIN NewtouchHIS_Base..V_S_xt_sfxm b ON a.sfxmCode = b.sfxmCode
                                                                      AND a.OrganizeId = b.OrganizeId
                                                                      AND b.zt = '1'
                WHERE   a.zt = '1'
                        AND a.OrganizeId = @orgId
                        AND a.Id = @keyValue;";
            return this.FirstOrDefault<SysLevelChargeVO>(sql,
                new[] { new SqlParameter("@orgId", orgId),
                    new SqlParameter("@keyValue", keyValue)});
        }
    }
}
