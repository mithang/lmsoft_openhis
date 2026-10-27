using System;
using Newtouch.CIS.Compat;
using System.Linq;
using System.Collections.Generic;

using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.Domain.IRepository.DeptStorageManage;
using Newtouch.Domain.ValueObjects.Storage;

using Newtouch.Infrastructure;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Tồn khokết chuyển
    /// </summary>
    public class KcKcjzRepo : RepositoryBase<KcKcjzEntity>, IKcKcjzRepo
    {
        public KcKcjzRepo() : base()
        {
        }

        /// <summary>
        /// Lấylịch sửkết chuyểnThời gian
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
                    text = "Vui lòng chọnlịch sửkết chuyển",
                    value = ""
                });
            }
            return result;
        }

        /// <summary>
        /// Lấynhấtgầnmột lầnkết chuyểnthông tin
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public KcKcjzEntity GetLastJzData(string warehouseId, string organizeId)
        {
            return IQueryable(p => p.warehouseId == warehouseId && p.OrganizeId == organizeId)
                .OrderByDescending(o => o.jzsj)
                .FirstOrDefault();
        }
    }
}
