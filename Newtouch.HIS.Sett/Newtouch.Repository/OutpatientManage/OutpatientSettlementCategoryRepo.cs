using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Quyết toán ngoại trú loại lớn 
    /// </summary>
    public class OutpatientSettlementCategoryRepo : RepositoryBase<OutpatientSettlementCategoryEntity>, IOutpatientSettlementCategoryRepo
    {
        public OutpatientSettlementCategoryRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// theo jsnm tra cứu quyết toán ngoại trú loại lớn 
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public List<OutpatientSettlementCategoryEntity> SelectMzjsdlByJsnm(int jsnm, string orgId)
        {
            return this.IQueryable().Where(a => a.jsnm == jsnm && a.OrganizeId == orgId).ToList();
        }

        /// <summary>
        /// Lấy quyết toán loại lớn 
        /// </summary>
        /// <param name="js"></param>
        /// <param name="ghxmList"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<OutpatientSettlementCategoryEntity> GetJsdl(OutpatientSettlementEntity js, List<OutpatientItemEntity> ghxmList, string orgId)
        {
            var jsdlList = new List<OutpatientSettlementCategoryEntity>();
            #region old Code
            ////Chi phí bảo hiểm y tế
            //foreach (OutpatientRegistItemEntity yb in ghxmList)
            //{
            //    OutpatientSettlementCategoryEntity jsdl = jsdlList.Find(t => t.dl == yb.dl);
            //    if (jsdl == null)
            //    {
            //        jsdl = new OutpatientSettlementCategoryEntity();
            //        jsdlList.Add(jsdl);
            //    }

            //    jsdl.jsnm = js.jsnm;
            // jsdl.dl = yb.dl;// loại lớn 
            //    jsdl.flzffy += yb.flzf;//Chi phí tự trả theo phân loại
            //    jsdl.zlfy += yb.zl + yb.sfzl;//Chi phí tự chi trả
            //    jsdl.jmfy += yb.jmje;//Số tiền giảm trừ
            //    jsdl.jsrq = js.CreateTime;
            //    if (returnValue.ybjsfwze == 0)
            //    {
            //        jsdl.zffy += 0m;
            //        jsdl.kbfy += 0m;
            //    }
            //    else
            //    {
            //        jsdl.zffy += ((yb.jzfy + yb.flzf) / returnValue.ybjsfwze) * js.zffy;//Số tiền theo phân loại/Tổng số tiền theo phân loại*Số tiền tự trả
            //        jsdl.kbfy += ((yb.jzfy + yb.flzf) / returnValue.ybjsfwze) * js.jzfy;//Số tiền theo phân loại/Tổng số tiền theo phân loại*Số tiền tự trả
            //    }
            //    jsdl.zje += jsdl.flzffy + jsdl.zlfy + jsdl.jmfy + jsdl.zffy + jsdl.kbfy;
            //}
            // không phải chi phí bảo hiểm y tế
            #endregion
            foreach (var dl in ghxmList.Select(a => a.dl).Distinct())
            {
                var xmList = ghxmList.Where(t => t.dl == dl).ToList();
                var mzjsdl = new OutpatientSettlementCategoryEntity
                {
                    jsnm = js.jsnm,
                    dl = dl,
                    flzffy = 0,
                    zlfy = xmList.Sum(a => a.dj),
                    jmfy = xmList.Sum(a => a.jmje ?? 0),
                    jsrq = js.CreateTime,
                    zffy = 0,
                    kbfy = 0,
                    zje = xmList.Sum(a => a.dj),
                    OrganizeId = orgId
                };
                mzjsdl.Create(true);
                jsdlList.Add(mzjsdl);
            }

            return jsdlList;
        }

    }
}


