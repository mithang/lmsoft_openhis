using SqlSugar;
using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 申领单明细
    /// </summary>
    public class KfApplyOrderDetailRepo : RepositoryBase<KfApplyOrderDetailEntity>, IKfApplyOrderDetailRepo
    {
        

        /// <summary>
        /// 修改已发数量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sl">部门单位数量，与表kf_applyOrderDetail.zhyz*sl=最小单位数量</param>
        /// <returns></returns>
        // PORT NOTE: the original SQL referenced @userCode without supplying
        // it (and the second method's SET clause used the literal column `sl`
        // instead of the @sl parameter), so both statements failed at runtime.
        // Implemented via entity load/modify/update instead: Modify() stamps
        // LastModifierCode/LastModifyTime from the current operator, matching
        // the SQL's intent.
        public int UpdateYfsl(long id, int sl)
        {
            var entity = FindEntity(p => p.Id == id && p.zt == "1");
            if (entity == null) return 0;
            entity.yfsl = (entity.yfsl ?? 0) + sl;
            entity.Modify();
            return Update(entity);
        }

        /// <summary>
        /// 修改已发数量，并返回变更记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sl">部门单位数量，与表kf_applyOrderDetail.zhyz*sl=最小单位数量</param>
        /// <returns></returns>
        public KfApplyOrderDetailEntity UpdateYfslAndSelectChangeRecord(long id, int sl)
        {
            if (UpdateYfsl(id, sl) <= 0) return null;
            return FindEntity(p => p.Id == id && p.zt == "1");
        }

        /// <summary>
        /// 获取申领单明细信息
        /// </summary>
        /// <param name="sldh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<KfApplyOrderDetailEntity> SelectData(string sldh, string organizeId)
        {
            const string sql = @"
SELECT mx.* 
FROM dbo.kf_applyOrderDetail(NOLOCK) mx
INNER JOIN dbo.kf_applyOrder(NOLOCK) m ON m.Id=mx.sldId AND m.zt='1'
WHERE m.OrganizeId=@OrganizeId 
AND m.sldh=@sldh
AND mx.zt='1'
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@sldh", sldh),
                new SugarParameter("@OrganizeId", organizeId)
            };
            return FindList<KfApplyOrderDetailEntity>(sql, param);
        }
    }
}