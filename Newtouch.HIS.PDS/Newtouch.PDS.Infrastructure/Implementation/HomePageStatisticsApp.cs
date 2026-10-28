using Newtouch.Infrastructure.EF;
using Newtouch.HIS.DomainServices.OutPatientPharmacy;
using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Thống kê trang chủ
    /// </summary>
    public class HomePageStatisticsApp : AppBase, IHomePageStatisticsApp
    {
        private readonly ISysPharmacyDepartmentRepo _sysPharmacyDepartmentRepo;
        private readonly IPyDmnService pyDmnService;
        private string _mzzybz;

        /// <summary>
        /// Lấy việc cần làmHTML
        /// </summary>
        /// <returns></returns>
        public string AssembleNeedDealHtml()
        {
            var result = "";
            if (string.IsNullOrWhiteSpace(_mzzybz))
            {
                _mzzybz = _sysPharmacyDepartmentRepo.GetMzzybzByCode(Constants.CurrentYfbm.yfbmCode, OrganizeId);
            }
            if (string.IsNullOrWhiteSpace(_mzzybz)) return result;
            switch (_mzzybz.Trim())
            {
                case "0":
                    result = AssembleYkNeedDelaHtml();
                    break;
                case "1":
                    result = AssembleMzyfNeedDelaHtmlV2();
                    break;
                case "2":
                    result = AssembleZyyfNeedDelaHtml();
                    break;
                default:
                    result = AssembleMzyfNeedDelaHtmlV2();
                    break;
            }
            return result;
        }

        /// <summary>
        /// Lắp ráp việc cần làm kho thuốcHTML
        /// </summary>
        /// <returns></returns>
        private string AssembleYkNeedDelaHtml()
        {
            return @"
                    <div class='dashboard-statss' menuName='Duyệt điều chỉnh giá' menuUrl='/DrugStorage/PriceAdjustmentApproval'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_tjshCount'></h2>
                            <h5>Duyệt điều chỉnh giá</h5>
                            <div class='stat-icon'>
                                <span style='font-size:42px;font-weight:700;line-height:1;' aria-hidden='true'>₫</span>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-statss' menuName='Xuất kho chờ phê duyệt' menuUrl='/PharmacyDrugStorage/ReceiptApproval?djlx=2'>
                        <div class='dashboard-stats-item' style='background-color: #65cea7;'>
                            <h2 class='m-top-none' id='h_ckdshCount'></h2>
                            <h5>Xuất kho chờ phê duyệt</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-truck fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-statss' menuName='Nhập kho chờ duyệt' menuUrl='/PharmacyDrugStorage/ReceiptApproval'>
                        <div class='dashboard-stats-item' style='background-color: #65cea7;'>
                            <h2 class='m-top-none' id='h_rkdshCount'></h2>
                            <h5>Nhập kho chờ duyệt</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-truck fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-statss' menuName='Đơn lĩnh chưa xuất kho' menuUrl='/DrugStorage/ApplyDelivery'>
                        <div class='dashboard-stats-item' style='background-color: #f3ce85;'>
                            <h2 class='m-top-none' id='h_sldshCount'></h2>
                            <h5>Đơn lĩnh chưa xuất kho</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-shopping-cart fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-statss' style='display: none'  menuName='1thángThuốc hết hạn' menuUrl='/StorageManage/DrugStockSearch/ExpiredDrugsView?type=1'>
                          < div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_todayDrugCount'></h2>
                            <h5>1thángThuốc hết hạn</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-exclamation-triangle fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-statss' menuName='Thuốc hết hạn' menuUrl='/Medicine/ExpiredDrugs'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_expriedDrugCount'></h2>
                            <h5>Thuốc hết hạn</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-exclamation-triangle fa-3x'></i>
                            </div>
                        </div>
                    </div> ";
        }

        /// <summary>
        /// Lắp ráp việc cần làm nhà thuốc ngoại trúHTML
        /// </summary>
        /// <returns></returns>
        private string AssembleMzyfNeedDelaHtml()
        {
            return @"
                    <div class='dashboard-stats' menuName='Thuốc điều giá' menuUrl='/DrugStorage/PriceAdjustmentHistory'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_tjypCount'></h2>
                            <h5>Thuốc điều giá</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-exchange fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='Duyệt nhập kho' menuUrl='/PharmacyDrugStorage/ReceiptApproval'>
                        <div class='dashboard-stats-item' style='background-color: #65cea7;'>
                            <h2 class='m-top-none' id='h_rkdshCount'></h2>
                            <h5>Duyệt nhập kho</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-truck fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='Đơn ngoại trú chờ xếp' menuUrl='/OutPatientPharmacy/DrugArrangement2018'>
                        <div class='dashboard-stats-item' style='background-color: #65cea7;'>
                            <h2 class='m-top-none' id='h_mzdpCount'></h2>
                            <h5>Đơn ngoại trú chờ xếp</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-tasks fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='Đơn ngoại trú chờ phát' menuUrl='/OutPatientPharmacy/DrugDelivery2018'>
                        <div class='dashboard-stats-item' style='background-color: #f3ce85;'>
                            <h2 class='m-top-none' id='h_mzdfCount'></h2>
                            <h5>Đơn ngoại trú chờ phát</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-send-o fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='Thuốc hết hạn' menuUrl='/Medicine/ExpiredDrugs'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_expriedDrugCount'></h2>
                            <h5>Thuốc hết hạn</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-exclamation-triangle fa-3x'></i>
                            </div>
                        </div>
                    </div> ";
        }

        /// <summary>
        /// Lắp ráp việc cần làm nhà thuốc ngoại trúHTML
        /// </summary>
        /// <returns></returns>
        private string AssembleMzyfNeedDelaHtmlV2()
        {
            return @"
                    <div class='dashboard-stats' menuName='Thuốc điều giá' menuUrl='/DrugStorage/PriceAdjustmentHistory'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_tjypCount'></h2>
                            <h5>Thuốc điều giá</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-exchange fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='Duyệt nhập kho' menuUrl='/BillManage/OutOrInStorageBill/Approval'>
                        <div class='dashboard-stats-item' style='background-color: #65cea7;'>
                            <h2 class='m-top-none' id='h_rkdshCount'></h2>
                            <h5>Duyệt nhập kho</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-truck fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='Đơn ngoại trú chờ phát' menuUrl='/OutPatientPharmacy/DrugDelivery2018'>
                        <div class='dashboard-stats-item' style='background-color: #f3ce85;'>
                            <h2 class='m-top-none' id='h_mzdfCount'></h2>
                            <h5>Đơn ngoại trú chờ phát</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-send-o fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' style='display: none' menuName='1thángThuốc hết hạn' menuUrl='/StorageManage/DrugStockSearch/ExpiredDrugsView?type=1'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_todayDrugCount'></h2>
                            <h5>1thángThuốc hết hạn</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-exclamation-triangle fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='Thuốc hết hạn' menuUrl='/Medicine/ExpiredDrugs'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_expriedDrugCount'></h2>
                            <h5>Thuốc hết hạn</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-exclamation-triangle fa-3x'></i>
                            </div>
                        </div>
                    </div> ";
        }

        /// <summary>
        /// Lắp ráp việc cần làm nhà thuốc nội trúHTML
        /// </summary>
        /// <returns></returns>
        private string AssembleZyyfNeedDelaHtml()
        {
            return @"
                    <div class='dashboard-statss' menuName='Thuốc điều giá' menuUrl='/DrugStorage/PriceAdjustmentHistory'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_tjypCount'></h2>
                            <h5>Thuốc điều giá</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-exchange fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-statss' menuName='Duyệt nhập kho' menuUrl='/PharmacyDrugStorage/ReceiptApproval'>
                        <div class='dashboard-stats-item' style='background-color: #65cea7;'>
                            <h2 class='m-top-none' id='h_rkdshCount'></h2>
                            <h5>Duyệt nhập kho</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-truck fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-statss' menuName='Số thuốc nội trú chờ phát' menuUrl='/HospitalizationPharmacy/DispenseIndex'>
                        <div class='dashboard-stats-item' style='background-color: #65cea7;'>
                            <h2 class='m-top-none' id='h_zydfCount'></h2>
                            <h5>Số thuốc nội trú chờ phát</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-send-o fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-statss' menuName='Số thuốc nội trú chờ trả' menuUrl='/HospitalizationPharmacy/RepercussionIndex'>
                        <div class='dashboard-stats-item' style='background-color: #f3ce85;'>
                            <h2 class='m-top-none' id='h_zydtCount'></h2>
                            <h5>Số thuốc nội trú chờ trả</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-rotate-left fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-statss' style='display: none' menuName='1thángThuốc hết hạn' menuUrl='/StorageManage/DrugStockSearch/ExpiredDrugsView?type=1'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_todayDrugCount'></h2>
                            <h5>1thángThuốc hết hạn</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-exclamation-triangle fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-statss' menuName='Thuốc hết hạn' menuUrl='/Medicine/ExpiredDrugs'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_expriedDrugCount'></h2>
                            <h5>Thuốc hết hạn</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-exclamation-triangle fa-3x'></i>
                            </div>
                        </div>
                    </div> ";
        }

        /// <summary>
        /// Lấy tổng số việc cần làm
        /// </summary>
        /// <returns></returns>
        public NeedDealCountVO GetPendingCount()
        {
            var result = new NeedDealCountVO();
            if (string.IsNullOrWhiteSpace(_mzzybz))
            {
                _mzzybz = _sysPharmacyDepartmentRepo.GetMzzybzByCode(Constants.CurrentYfbm.yfbmCode, OrganizeId);
            }
            if (string.IsNullOrWhiteSpace(_mzzybz)) return result;
            switch (_mzzybz.Trim())
            {
                case "0":
                    result = pyDmnService.GetNeedDealCountByYk();
                    break;
                case "1":
                case "2":
                default:
                    result = pyDmnService.GetNeedDealCountByYf();
                    break;
            }
            return result;
        }
    }
}
