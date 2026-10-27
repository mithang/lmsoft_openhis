using System;
using System.Linq;
using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Herp.Infrastructure;
using SqlSugar;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 库存结转
    /// </summary>
    public class KcKcjzRepo : RepositoryBase<KcKcjzEntity>, IKcKcjzRepo
    {
        public KcKcjzRepo()
        {
        }

        /// <summary>
        /// 获取历史结转时间
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<SelectVo> GetlsjzDateTime(string warehouseId, string organizeId)
        {
            var result = new List<SelectVo>();
            var detail = IQueryable(p => p.warehouseId == warehouseId && p.OrganizeId == organizeId)
                .Select(p => p.jzsj)
                .Distinct().ToList();
            if (detail.Count > 0)
            {
                result.AddRange(detail
                    .Select(q => new SelectVo
                    {
                        value = q.ToString(Constants.DateTimeFormat),
                        text = q.ToString(Constants.DateTimeFormat)
                    })
                    .OrderByDescending(o => o.value)
                    .Take(365)
                    .ToList());
            }
            else
            {
                result.Add(new SelectVo
                {
                    text = "Vui lòng chọn lịch sử kết chuyển",
                    value = ""
                });
            }
            return result;
        }

        /// <summary>
        /// 获取最近一次结转信息
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public KcKcjzEntity GetLastJzData(string warehouseId, string organizeId)
        {
            return IQueryable(p => p.warehouseId == warehouseId && p.OrganizeId == organizeId)
                .OrderByDescending(o => o.jzsj)
                .ToList().FirstOrDefault();
        }
    }
}
