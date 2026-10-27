using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.VO;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// mz_cfmxph
    /// </summary>
    public class OutpatientPrescriptionDetailBatchNumberRepo : RepositoryBase<OutpatientPrescriptionDetailBatchNumberEntity>, IOutpatientPrescriptionDetailBatchNumberRepo
    {
        public OutpatientPrescriptionDetailBatchNumberRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public List<OutpatientPrescriptionDetailBatchNumberEntity> GetList()
        {
            return this.IQueryable().ToList();
        }

        /// <summary>
        /// 根据处方号和组织机构获取未归架的门诊处方明细批号信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="gjzt">归架状态：0-未归架  1-已归架</param>
        /// <returns></returns>
        public List<OutpatientPrescriptionDetailBatchNumberEntity> GetList(string cfh, string organizeId, string gjzt = "0")
        {
            const string sql = @"
SELECT * FROM dbo.mz_cfmxph(NOLOCK) 
WHERE cfh=@cfh
AND gjzt=@gjzt
AND OrganizeId=@OrganizeId
AND zt='1'
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@cfh", cfh),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@gjzt", gjzt)
            };
            return FindList<OutpatientPrescriptionDetailBatchNumberEntity>(sql, param);
        }

        /// <summary>
        /// 根据处方号和组织机构获取门诊处方明细批号信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="gjzt">归架状态：0-未归架  1-已归架</param>
        /// <returns></returns>
        public List<OutpatientPrescriptionDetailBatchNumberEntity> GetList(string cfh, string yfbmCode, string organizeId, string gjzt = "0")
        {
            const string sql = @"
SELECT * FROM dbo.mz_cfmxph(NOLOCK) 
WHERE cfh=@cfh
AND fyyf=@yfbmCode 
AND gjzt=@gjzt
AND OrganizeId=@OrganizeId
AND zt='1'
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@cfh", cfh),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@gjzt", gjzt)
            };
            return FindList<OutpatientPrescriptionDetailBatchNumberEntity>(sql, param);
        }

        /// <summary>
        /// 根据处方号和组织机构获取门诊处方明细批号信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="gjzt">归架状态：0-未归架  1-已归架</param>
        /// <returns></returns>
        public List<OutpatientPrescriptionDetailBatchNumberEntity> GetList(string cfh, string ypCode, string yfbmCode, string organizeId, string gjzt = "0")
        {
            const string sql = @"
SELECT * 
FROM dbo.mz_cfmxph(NOLOCK) 
WHERE cfh=@cfh
AND yp=@ypCode
AND fyyf=@yfbmCode 
AND gjzt=@gjzt
AND OrganizeId=@OrganizeId
AND zt='1'
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@cfh", cfh),
                new SugarParameter("@ypCode", ypCode),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@gjzt", gjzt)
            };
            return _dataContext.Database.SqlQuery<OutpatientPrescriptionDetailBatchNumberEntity>(sql, param).ToList();
        }

        /// <summary>
        /// 根据处方号和组织机构获取门诊处方明细批号信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="gjzt">归架状态：0-未归架  1-已归架</param>
        /// <returns></returns>
        public List<MzcfphxxVO> GetListByGroup(string cfh, string ypCode, string yfbmCode, string organizeId, string gjzt = "0")
        {
            const string sql = @"
SELECT CONVERT(INT, ISNULL(SUM(sl),0)) sl, pc, ph 
FROM dbo.mz_cfmxph(NOLOCK) 
WHERE cfh=@cfh
AND yp=@ypCode
AND fyyf=@yfbmCode 
AND gjzt='0'
AND OrganizeId=@OrganizeId
AND zt='1'
GROUP BY pc, ph
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@cfh", cfh),
                new SugarParameter("@ypCode", ypCode),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@gjzt", gjzt)
            };
            return _dataContext.Database.SqlQuery<MzcfphxxVO>(sql, param).ToList();
        }

        /// <summary>
        /// 归架药品
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int GJDrug(string ypCode, string pc, string ph, string cfh, string yfbmCode, string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.mz_cfmxph SET zt='0', gjzt='1', LastModifyTime=GETDATE(), LastModifierCode=@userCode 
WHERE cfh=@cfh AND yp=@ypCode AND fyyf=@yfbmCode AND OrganizeId=@OrganizeId AND zt='1' AND gjzt='0' AND pc=@pc AND ph=@ph
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@cfh", cfh),
                new SugarParameter("@ypCode",ypCode ),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@pc", pc),
                new SugarParameter("@ph",ph ),
                new SugarParameter("@OrganizeId", organizeId)
            };
            return _dataContext.Database.ExecuteSqlCommand(sql, param);
        }
    }
}
