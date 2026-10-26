using System;
using System.Web;
using System.Collections.Generic;

using Newtouch.Tools;
using Newtouch.Common.Operator;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.His.OrderService.Client;
using Newtouch.His.OrderService.Contact.DTO;

namespace Newtouch.HIS.DomainServices.Proxy
{
    /// <summary>
    /// gọi dư ới đ ơn giao diện
    /// </summary>
    public class CreateOrderProxy
    {
        #region 单例

        private CreateOrderProxy() { }


        public static CreateOrderProxy Instance { get; } = new CreateOrderProxy();

        #endregion

        /// <summary>
        /// tạo tạm lưu đ ơn 
        /// </summary>
        /// <param name="gh">Thông tin đ ăng ký khám</param>
        /// <param name="ghxmList">Đ ăng ký khám hạng mụcchi tiết</param>
        /// <param name="mzjs">Quyết toán ngoại trú thông tin</param>
        /// <returns></returns>
        public OrderCreateResponseDTO Book(OutpatientRegistEntity gh, OutpatientSettlementEntity mzjs, List<OutpatientItemEntity> ghxmList)
        {
            return CreateOrder(BuildBookRequest(gh, mzjs, ghxmList));
        }


        /// <summary>
        /// tạo tạm lưu đ ơn 
        /// </summary>
        /// <param name="orderid"> đ ơn hàng số </param>
        /// <returns></returns>
        public OrderCreateResponseDTO Commit(long orderId)
        {
            return CreateOrder(BuildCommitRequest(orderId));
        }


        /// <summary>
        /// lắp ráp dư ới đ ơn bản tin
        /// </summary>
        /// <param name="gh">Thông tin đ ăng ký khám</param>
        /// <param name="ghxmList">Đ ăng ký khám hạng mụcchi tiết</param>
        /// <param name="mzjs">Quyết toán ngoại trú thông tin</param>
        /// <returns></returns>
        private static OrderCreateRequestDTO BuildBookRequest(OutpatientRegistEntity gh, OutpatientSettlementEntity mzjs, List<OutpatientItemEntity> ghxmList)
        {
            return new OrderCreateRequestDTO
            {
                SubmitOrderType = SubmitOrderTypeEnum.TemporaryOrderNeedPay,
                OrderBasicInfo = BuildOrderBasicInfo(mzjs),
                ContactInfo = BuildContactInfo(gh.patid.ToString()),
                InvoiceInfo = new InvoiceDTO(),
                OrderExtendInfo = new OrderExtendDTO(),
                PaymentInfo = new PaymentDTO(),
                ItemInfo = BuildItems(ghxmList)
            };
        }

        /// <summary>
        /// lắp ráp dư ới đ ơn bản tin
        /// </summary>
        /// <param name="orderid"> đ ơn hàng số </param>
        /// <returns></returns>
        private static OrderCreateRequestDTO BuildCommitRequest(long orderId)
        {
            return new OrderCreateRequestDTO
            {
                OrderId = orderId,
                SubmitOrderType = SubmitOrderTypeEnum.OfficialOrder
            };
        }

        /// <summary>
        /// lắp ráp đ ơn hàng cơ bản thông tin
        /// </summary>
        /// <returns></returns>
        private static OrderBasicDTO BuildOrderBasicInfo(OutpatientSettlementEntity mzjs)
        {
            return new OrderBasicDTO
            {
                Amount = mzjs.zje,
                BookingType = "Offline",
                Eid = OperatorProvider.GetCurrent().UserCode,
                IsDirectlySubmit = false,
                IsOnline = "F",
                IsReceiptFirst = true,
                IsSplitPayment = false,
                IsSuperOrder = false,
                OrderDate = DateTime.Now,
                PkgName = "",
                Remark = "",
                ServerFrom = HttpContext.Current.Request.Url.Host,
                Uid = ""
            };
        }

        /// <summary>
        /// lắp ráp đ ơn hàng liên hệ ngườithông tin
        /// </summary>
        /// <param name="patid">Bệnh nhânID</param>
        /// <returns></returns>
        private static ContactDTO BuildContactInfo(string patid)
        {
            var patientInfo = new SysPatientBasicInfoRepo(new FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure.DefaultDatabaseFactory()).GetInfoByPatid(patid, OperatorProvider.GetCurrent().OrganizeId);
            if (patientInfo == null)
            {
                return null;
            }
            return new ContactDTO
            {
                ContactName = patientInfo.xm,
                ContactMobile = patientInfo.phone,
                ContactTel = patientInfo.dh,
                CountryCode = "86",
                ContactEmail = patientInfo.email,
                ContactFax = "",
                ContactAddress = patientInfo.xian_sheng + patientInfo.xian_shi + patientInfo.xian_xian + patientInfo.xian_dz
            };
        }

        /// <summary>
        /// lắp ráp đ ơn hàng tài nguyênthông tin
        /// </summary>
        /// <param name="ghxmList"></param>
        /// <returns></returns>
        private static List<ItemDTO> BuildItems(List<OutpatientItemEntity> ghxmList)
        {
            var result = new List<ItemDTO>();
            if (ghxmList == null || ghxmList.Count <= 0)
            {
                return result;
            }
            ghxmList.ForEach(p =>
            {
                var item = new ItemDTO
                {
                    ItemID = Convert.ToInt32(p.sfxm),
                    ItemName = "",
                    Quantity = p.sl,
                    Unit = "",
                    UnitPrice = p.dj,
                    TotalPrice = p.je,
                    ResourceType = p.dl,
                    ResourceDesc = "",
                    UseDate = DateTime.Now,
                    ReceiptCode = ""
                };
                result.Add(item);
            });
            return result;
        }

        /// <summary>
        /// tạo tạm lưu đ ơn 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static OrderCreateResponseDTO CreateOrder(OrderCreateRequestDTO request)
        {
            var response = new OrderCreateResponseDTO();
            try
            {
                response = OrderServiceClient.Instance().CreateOrder(request);
                //response = OrderServiceClient.Instance().Test(1);
                return response;
            }
            catch (Exception ex)
            {
                Common.AppLogger.Instance.Error("CreateOrder Error", ex);
                return null;
            }
            finally
            {
                Common.AppLogger.Instance.Info("RequestXML:     " + request.ToJson());
                Common.AppLogger.Instance.Info("ResponseXML:    " + response.ToJson());
            }
        }
    }

}
