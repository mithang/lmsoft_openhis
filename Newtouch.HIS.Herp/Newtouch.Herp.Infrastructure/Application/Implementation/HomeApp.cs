using System;
using System.Collections.Generic;
using System.Text;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// Trang chủ Herp — port của HomeApp gốc. HTML dashboard đã dịch sang
    /// tiếng Việt (tiêu đề thẻ + menuName); các id phần tử (h_tjshCount…)
    /// và menuUrl giữ nguyên để JS trang dashboard bám vào.
    /// Lưu ý: TransformRkCount KHÔNG dùng EnumOutOrInStorageBillType.GetDescription()
    /// như bản gốc, vì các Description đó là khóa chức năng (tiền tố số phiếu
    /// qua ReceiptNoManage.GetSpellCode, dạng pinyin) và phải giữ nguyên.
    /// Thay vào đó là bảng ánh xạ hiển thị tiếng Việt riêng, chỉ dùng cho
    /// nhãn biểu đồ trang chủ, không ảnh hưởng logic số phiếu.
    /// </summary>
    public class HomeApp : AppBase, IHomeApp
    {
        private readonly IWzProductDmnService _wzProductDmnService;

        public HomeApp(IWzProductDmnService wzProductDmnService)
        {
            _wzProductDmnService = wzProductDmnService;
        }

        /// <summary>
        /// Lấy HTML khối "cần xử lý" của dashboard (theo cấp kho hiện tại)
        /// </summary>
        public string AssembleNeedDealHtml()
        {
            switch (Constants.CurrentKf.currentKfLevel)
            {
                case (int)EnumWarehouseLevel.OneLevel:
                    return AssembleZkfNeedDelaHtml();
                default:
                    return AssembleKskfNeedDelaHtml();
            }
        }

        /// <summary>
        /// Tổng kho
        /// </summary>
        private string AssembleZkfNeedDelaHtml()
        {
            return new StringBuilder(@"
                    <div class='dashboard-stats' menuName='Duyệt điều chỉnh giá' menuUrl='/ProductManage/PriceAdjustment/PriceAdjustmentApproval'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_tjshCount'></h2>
                            <h5>Duyệt điều chỉnh giá</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-cny fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='Xuất kho chờ duyệt' menuUrl='/BillManage/OutOrInStorageBill/Approval?djlx=").Append((int)EnumOutOrInStorageBillType.Wbck + @"'>
                        <div class='dashboard-stats-item' style='background-color: #65cea7;'>
                            <h2 class='m-top-none' id='h_ckdshCount'></h2>
                            <h5>Xuất kho chờ duyệt</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-truck fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='Nhập kho ngoài chờ duyệt' menuUrl='/BillManage/OutOrInStorageBill/Approval?djlx=").Append((int)EnumOutOrInStorageBillType.Wbrk + @"'>
                        <div class='dashboard-stats-item' style='background-color: #65cea7;'>
                            <h2 class='m-top-none' id='h_wbrkCount'></h2>
                            <h5>Nhập kho ngoài chờ duyệt</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-truck fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='Duyệt trả hàng nội bộ' menuUrl='/BillManage/OutOrInStorageBill/Approval?djlx=").Append((int)EnumOutOrInStorageBillType.Nbth + @"'>
                        <div class='dashboard-stats-item' style='background-color: #65cea7;'>
                            <h2 class='m-top-none' id='h_nbthCount'></h2>
                            <h5>Duyệt trả hàng nội bộ</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-truck fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='Vật tư quá hạn' menuUrl='/StorageManage/Storage/ExpiredStorageQuery'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_expriedWzCount'></h2>
                            <h5>Vật tư quá hạn</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-clock-o fa-3x'></i>
                            </div>
                        </div>
                    </div> ").ToString();
        }

        /// <summary>
        /// Kho khoa phòng
        /// </summary>
        private string AssembleKskfNeedDelaHtml()
        {
            return new StringBuilder(@"
                    <div class='dashboard-stats' menuName='Vật tư điều chỉnh giá' menuUrl='/ProductManage/PriceAdjustment/PriceAdjustmentApproval'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_tjwzCount'></h2>
                            <h5>Vật tư điều chỉnh giá</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-exchange fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='Duyệt nhập kho' menuUrl='/BillManage/OutOrInStorageBill/Approval?djlx=").Append((int)EnumOutOrInStorageBillType.Zjck + @"'>
                        <div class='dashboard-stats-item' style='background-color: #65cea7;'>
                            <h2 class='m-top-none' id='h_rkdshCount'></h2>
                            <h5>Duyệt nhập kho</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-truck fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='Vật tư quá hạn' menuUrl='/StorageManage/Storage/WarehouseStorageQuery?ygq=true'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_expriedWzCount'></h2>
                            <h5>Vật tư quá hạn</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-clock-o fa-3x'></i>
                            </div>
                        </div>
                    </div> ").ToString();
        }

        /// <summary>
        /// Lấy tổng số cần xử lý (theo cấp kho hiện tại)
        /// </summary>
        public NeedDealCountVO GetPendingCount()
        {
            switch (Constants.CurrentKf.currentKfLevel)
            {
                case (int)EnumWarehouseLevel.OneLevel:
                    return _wzProductDmnService.GetNeedDealCountByZkf(Constants.CurrentKf.currentKfId, OrganizeId);
                default:
                    return _wzProductDmnService.GetNeedDealCountByKskf(Constants.CurrentKf.currentKfId, OrganizeId);
            }
        }

        /// <summary>
        /// Tổng hợp số liệu theo tháng trong năm hiện tại (xuất/nhập/lãi lỗ)
        /// </summary>
        public MonthlySummaryDTO GetPsiCountVoByKf(List<VPsiStatisticsByDateEntity> val)
        {
            var monthlySummary = new MonthlySummaryDTO();
            if (val != null && val.Count > 0)
            {
                val.ForEach(p =>
                {
                    monthlySummary.ItemName = p.itemName;
                    var year = DateTime.Now.Year.ToString();
                    if (p.statisticsDate.Equals(year + "-01")) monthlySummary.January = p.sl;
                    else if (p.statisticsDate.Equals(year + "-02")) monthlySummary.February = p.sl;
                    else if (p.statisticsDate.Equals(year + "-03")) monthlySummary.March = p.sl;
                    else if (p.statisticsDate.Equals(year + "-04")) monthlySummary.April = p.sl;
                    else if (p.statisticsDate.Equals(year + "-05")) monthlySummary.May = p.sl;
                    else if (p.statisticsDate.Equals(year + "-06")) monthlySummary.June = p.sl;
                    else if (p.statisticsDate.Equals(year + "-07")) monthlySummary.July = p.sl;
                    else if (p.statisticsDate.Equals(year + "-08")) monthlySummary.August = p.sl;
                    else if (p.statisticsDate.Equals(year + "-09")) monthlySummary.September = p.sl;
                    else if (p.statisticsDate.Equals(year + "-10")) monthlySummary.October = p.sl;
                    else if (p.statisticsDate.Equals(year + "-11")) monthlySummary.November = p.sl;
                    else if (p.statisticsDate.Equals(year + "-12")) monthlySummary.December = p.sl;
                });
            }
            return monthlySummary;
        }

        /// <summary>
        /// Đổi tên loại phiếu sang nhãn hiển thị tiếng Việt cho biểu đồ tròn
        /// trang chủ. Chỉ 6 loại như bản gốc; các loại khác giữ nguyên tên.
        /// </summary>
        public void TransformRkCount(List<ClassificationStatisticsEntity> source)
        {
            if (source == null || source.Count <= 0) return;
            source.ForEach(p =>
            {
                if (!Enum.TryParse(p.name, out EnumOutOrInStorageBillType t)) return;
                switch (t)
                {
                    case EnumOutOrInStorageBillType.Bsby:
                        p.name = "Báo hỏng / báo thừa";
                        break;
                    case EnumOutOrInStorageBillType.Nbth:
                        p.name = "Trả hàng nội bộ";
                        break;
                    case EnumOutOrInStorageBillType.Slck:
                        p.name = "Xuất kho lĩnh";
                        break;
                    case EnumOutOrInStorageBillType.Wbck:
                        p.name = "Xuất kho ngoài";
                        break;
                    case EnumOutOrInStorageBillType.Wbrk:
                        p.name = "Nhập kho ngoài";
                        break;
                    case EnumOutOrInStorageBillType.Zjck:
                        p.name = "Xuất kho trực tiếp";
                        break;
                }
            });
        }
    }
}
