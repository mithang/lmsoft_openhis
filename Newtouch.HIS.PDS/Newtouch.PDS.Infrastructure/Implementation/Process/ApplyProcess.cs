using Newtouch.Infrastructure.EF;
using Newtouch.HIS.Application.Implementation;
using System;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset;

namespace Newtouch.HIS.Application.Implementation.Process
{
    /// <summary>
    /// Lĩnh nội bộ
    /// </summary>
    public class ApplyProcess : ProcessorFun<DjInfoDTO>
    {
        private SysMedicineRequisitionEntity sysMedicineRequisitionEntity;
        private List<SysMedicineRequisitionDetailEntity> sysMedicineRequisitionDetailEntities;
        private readonly IApplyDmnService applyDmnService;

        public ApplyProcess(DjInfoDTO request) : base(request)
        {
        }

        /// <summary>
        /// Kiểm tra
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request == null)
            {
                throw new FailedException("Thông tin chứng từ không được để trống");
            }
            if (Request.mx == null || Request.mx.Count <= 0)
            {
                throw new FailedException("Chi tiết chứng từ không được để trống");
            }
            return new ActResult();
        }

        /// <summary>
        /// Tiền xử lý
        /// </summary>
        protected override void BeforeAction(ActResult actResult)
        {
            sysMedicineRequisitionEntity = new SysMedicineRequisitionEntity
            {
                sldId = Guid.NewGuid().ToString(),
                Ckbm = Request.ckbm,
                OrganizeId = OrganizeId,
                Sldh = Request.djh,
                Slbm = Request.rkbm,
                ffzt = (int)EnumSLDDeliveryStatus.None,
                zt = "1"
            };
            sysMedicineRequisitionEntity.Create(true, Guid.NewGuid().ToString());
            sysMedicineRequisitionDetailEntities = new List<SysMedicineRequisitionDetailEntity>();
            Request.mx.ForEach(p =>
            {
                var item = new SysMedicineRequisitionDetailEntity
                {
                    sldId = sysMedicineRequisitionEntity.sldId,
                    ypCode = p.ypdm,
                    slsl = p.sl * (int)p.rkzhyz,    //Chuyển thành đơn vị nhỏ nhất，lưu nó
                    Zhyz = 1,
                    yfsl = 0,   //Số lượng đã phát
                    zt = "1"
                };
                item.Create(true, Guid.NewGuid().ToString());
                sysMedicineRequisitionDetailEntities.Add(item);
            });
        }

        /// <summary>
        /// Xử lý chính
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            var result = applyDmnService.SubmitApply(sysMedicineRequisitionEntity, sysMedicineRequisitionDetailEntities);
            if (string.IsNullOrWhiteSpace(result)) return;
            actResult.IsSucceed = false;
            actResult.ResultMsg = result;
        }
    }
}
