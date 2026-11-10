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
            // G26: chặn sớm field thiếu/vượt độ dài cột DB, tránh lỗi SQL thô
            // (Cannot-insert NULL / truncation) và NRE ở BeforeAction.
            // Apply UI chỉ post djh/ckbm/rkbm + mx(ypdm, sl, rkzhyz) — không
            // post fyfs/ph/pc nên không validate các field đó ở đây.
            if (string.IsNullOrWhiteSpace(Request.djh))
            {
                throw new FailedException("Số chứng từ không được để trống");
            }
            if (Request.djh.Length > 30)
            {
                throw new FailedException("Số chứng từ không được vượt quá 30 ký tự");
            }
            if (string.IsNullOrWhiteSpace(Request.ckbm))
            {
                throw new FailedException("Bộ phận xuất kho không được để trống");
            }
            if (Request.ckbm.Length > 100)
            {
                throw new FailedException("Bộ phận xuất kho không được vượt quá 100 ký tự");
            }
            if (string.IsNullOrWhiteSpace(Request.rkbm))
            {
                throw new FailedException("Bộ phận nhập kho không được để trống");
            }
            if (Request.rkbm.Length > 100)
            {
                throw new FailedException("Bộ phận nhập kho không được vượt quá 100 ký tự");
            }
            foreach (var item in Request.mx)
            {
                if (string.IsNullOrWhiteSpace(item.ypdm))
                {
                    throw new FailedException("Mã thuốc không được để trống");
                }
                if (item.ypdm.Length > 20)
                {
                    throw new FailedException("Mã thuốc không được vượt quá 20 ký tự");
                }
                if (item.sl <= 0)
                {
                    throw new FailedException("Số lượng phải lớn hơn 0");
                }
                if (item.rkzhyz == null || item.rkzhyz <= 0)
                {
                    throw new FailedException("Hệ số quy đổi nhập kho phải lớn hơn 0");
                }
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
                    slsl = p.sl * (int)p.rkzhyz,    //Chuyển thành đơn vị nhỏ nhất,lưu nó
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
