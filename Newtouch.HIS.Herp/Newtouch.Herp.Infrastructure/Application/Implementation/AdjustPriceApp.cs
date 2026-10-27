using System;
using System.Linq;
using System.Text;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 调价. The original AdjustPriceExecuteProcess (ProcessorFun, binary-only)
    /// is inlined here: validate the adjustment record, then delegate to
    /// WzPriceAdjustmentDmnService.AdjustPriceExecute.
    /// </summary>
    public class AdjustPriceApp : AppBase, IAdjustPriceApp
    {
        private readonly IWzPriceAdjustmentRepo _wzPriceAdjustmentRepo;
        private readonly IWzProductRepo _wzProductRepo;
        private readonly IWzPriceAdjustmentDmnService _wzPriceAdjustmentDmnService;

        public AdjustPriceApp(IWzPriceAdjustmentRepo wzPriceAdjustmentRepo, IWzProductRepo wzProductRepo,
            IWzPriceAdjustmentDmnService wzPriceAdjustmentDmnService)
        {
            _wzPriceAdjustmentRepo = wzPriceAdjustmentRepo;
            _wzProductRepo = wzProductRepo;
            _wzPriceAdjustmentDmnService = wzPriceAdjustmentDmnService;
        }

        public string AdjustPriceExecute(string ids)
        {
            var result = new StringBuilder();
            if (string.IsNullOrWhiteSpace(ids)) return result.ToString();
            var idList = ids.TrimEnd(',').Split(',').ToList();
            foreach (var id in idList)
            {
                var err = ExecuteOne(id);
                if (!string.IsNullOrWhiteSpace(err)) result.AppendLine(err);
            }
            return result.ToString();
        }

        private string ExecuteOne(string id)
        {
            var tjInfo = _wzPriceAdjustmentRepo.FindEntity(p => p.wztjId == id);
            if (tjInfo == null)
            {
                return string.Format("Không tìm thấy thông tin điều chỉnh giá có khóa chính {0}!", id);
            }
            var wzInfo = _wzProductRepo.FindEntity(p => p.Id == tjInfo.productId && p.OrganizeId == OrganizeId);
            var wzName = wzInfo != null ? wzInfo.name : "";
            if (tjInfo.shzt.Equals(((int)EnumTjShzt.Waiting).ToString())
                || tjInfo.shzt.Equals(((int)EnumTjShzt.Refuse).ToString())
                || tjInfo.shzt.Equals(((int)EnumTjShzt.Revoke).ToString()))
            {
                return string.Format("Vật tư điều chỉnh giá [{0}] đang ở trạng thái chưa duyệt, đã từ chối hoặc đã thu hồi, không thể thực hiện!", wzName);
            }
            if (((int)EnumTjZxbz.Already).ToString().Equals(tjInfo.zxbz))
            {
                return string.Format("Vật tư điều chỉnh giá [{0}] đã thực hiện, không được thực hiện trùng!", wzName);
            }
            if (tjInfo.zxsj <= DateTime.Now)
            {
                return string.Format("Vật tư điều chỉnh giá [{0}] đã quá ngày thực hiện!", wzName);
            }
            return _wzPriceAdjustmentDmnService.AdjustPriceExecute(tjInfo);
        }
    }
}
