using Newtouch.Infrastructure.EF;
using Newtouch.HIS.DomainServices;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.OutPatientPharmacy;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ResourcesOperate;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Cập nhật đơn thuốc
    /// </summary>
    public class UpdateRpApp : ProcessorFun<UpdateRpRequset>
    {
        private readonly IOutpatientPrescriptionDetailBatchNumberDmnService outpatientPrescriptionDetailBatchNumberDmnService;
        private readonly IOutpatientPrescriptionDetailBatchNumberRepo outpatientPrescriptionDetailBatchNumberRepo;
        private readonly IfyDmnService fyDmnService;
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="request"></param>
        public UpdateRpApp(UpdateRpRequset request) : base(request)
        {
        }

        /// <summary>
        /// Kiểm tra
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request.Cfmx == null) throw new FailedException("Chi tiết đơn thuốc không được để trống");
            if (string.IsNullOrWhiteSpace(Request.Cfh)) throw new FailedException("Số đơn thuốc Bắt buộc nhập");
            Request.Cfmx.ForEach(p =>
            {
                if (string.IsNullOrWhiteSpace(p.YpCode)) throw new FailedException("ItemCode(Số hiệu hạng mục) Bắt buộc nhập");
            });
            if (string.IsNullOrWhiteSpace(Request.OrganizeId)) throw new FailedException("OrganizeId(Mã cơ cấu tổ chức) Bắt buộc nhập");
            return new ActResult();
        }

        /// <summary>
        /// Tiền xử lý Xếp kệ thuốc đơn gốc trước
        /// </summary>
        protected override void BeforeAction(ActResult actResult)
        {
            fyDmnService.ReleaseFrozenStock(outpatientPrescriptionDetailBatchNumberRepo.GetList(Request.Cfh, Request.OrganizeId));
            outpatientPrescriptionDetailBatchNumberDmnService.UpdateGjztbyCfh(Request.Cfh, Request.OrganizeId);
        }

        /// <summary>
        /// Xử lý chính
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            var result = new OutpatientBookApp(AssembleBookAppRequest()).Process();
            if (result.IsSucceed) return;
            actResult.IsSucceed = false;
            actResult.ResultMsg = string.Format(@"Giải phóng tồn kho đóng băng gốc thành công，Nhưng đóng băng tồn kho mới thất bại。 \n Lý do thất bại：{0}", result.ResultMsg);
        }

        /// <summary>
        /// Lắp ráp bản tin đặt trước
        /// </summary>
        /// <returns></returns>
        private OutpatientBookRequestDTO AssembleBookAppRequest()
        {
            var item = new System.Collections.Generic.List<ItemDetail>();
            var par = new OutpatientBookRequestDTO
            {
                OrganizeId = Request.OrganizeId,
                CreatorCode = Request.CreatorCode,
                Items = item
            };
            Request.Cfmx.ForEach(p =>
            {
                item.Add(new ItemDetail
                {
                    Cfh = Request.Cfh,
                    DlCode = p.DlCode,
                    ItemCode = p.YpCode,
                    ItemCount = p.YpCount,
                    ItemName = p.YpName,
                    Yfbm = Request.Yfbm
                });
            });
            return par;
        }
    }
}
