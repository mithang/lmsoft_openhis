using SqlSugar;
using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Chi tiết phiếu lĩnh
    /// </summary>
    public class KfApplyOrderDetailRepo : RepositoryBase<KfApplyOrderDetailEntity>, IKfApplyOrderDetailRepo
    {
        

        /// <summary>
        /// Sửa số lượng đã phát
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sl">Số lượng đơn vị khoa phòng，với bảng kf_applyOrderDetail.zhyz*sl=Số lượng đơn vị nhỏ nhất</param>
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
        /// Sửa số lượng đã phát，và trả về bản ghi thay đổi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sl">Số lượng đơn vị khoa phòng，với bảng kf_applyOrderDetail.zhyz*sl=Số lượng đơn vị nhỏ nhất</param>
        /// <returns></returns>
        public KfApplyOrderDetailEntity UpdateYfslAndSelectChangeRecord(long id, int sl)
        {
            if (UpdateYfsl(id, sl) <= 0) return null;
            return FindEntity(p => p.Id == id && p.zt == "1");
        }

        /// <summary>
        /// Lấy thông tin chi tiết phiếu lĩnh
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