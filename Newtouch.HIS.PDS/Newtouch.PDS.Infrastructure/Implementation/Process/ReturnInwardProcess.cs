using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset;

namespace Newtouch.HIS.Application.Implementation
{
    public class ReturnInwardProcess : ProcessorFun<DjInfoDTO>
    {
        private readonly IReturnInwardDmnService returnInwardDmnService;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="request"></param>
        public ReturnInwardProcess(DjInfoDTO request) : base(request)
        {
        }

        private SysMedicineStorageIOReceiptEntity djmain;
        private List<SysMedicineStorageIOReceiptDetailEntity> djmx;

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
            // (Cannot-insert NULL / truncation) và ghi nửa chừng ở service
            // (header mồ côi khi detail insert lỗi — xem PROBE-G26 §3 F3/F4).
            if (string.IsNullOrWhiteSpace(Request.djh))
            {
                throw new FailedException("Số chứng từ không được để trống");
            }
            if (Request.djh.Length > 30)
            {
                throw new FailedException("Số chứng từ không được vượt quá 30 ký tự");
            }
            if (string.IsNullOrWhiteSpace(Request.fyfs))
            {
                throw new FailedException("Phương thức phát thuốc không được để trống");
            }
            if (Request.fyfs.Length > 10)
            {
                throw new FailedException("Phương thức phát thuốc không được vượt quá 10 ký tự");
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
                if (string.IsNullOrWhiteSpace(item.ph))
                {
                    throw new FailedException("Số lô không được để trống");
                }
                if (item.ph.Length > 30)
                {
                    throw new FailedException("Số lô không được vượt quá 30 ký tự");
                }
                if (string.IsNullOrWhiteSpace(item.pc))
                {
                    throw new FailedException("Lô không được để trống");
                }
                if (item.pc.Length > 30)
                {
                    throw new FailedException("Lô không được vượt quá 30 ký tự");
                }
                if (item.ckzhyz == null || item.ckzhyz <= 0)
                {
                    throw new FailedException("Hệ số quy đổi xuất kho phải lớn hơn 0");
                }
            }
            return new ActResult();
        }

        /// <summary>
        /// Tiền xử lý
        /// </summary>
        protected override void BeforeAction(ActResult actResult)
        {
            djmain = new SysMedicineStorageIOReceiptEntity
            {
                Ckbm = Request.ckbm,
                Ckczy = UserIdentity.UserCode,
                CreateTime = DateTime.Now,
                CreatorCode = UserIdentity.UserCode,
                Crkfsdm = Request.fyfs,
                Czsj = DateTime.Now,
                djlx = Request.djlx,
                OrganizeId = OrganizeId,
                Pdh = Request.djh,
                Rkbm = Request.rkbm,
                px = null,
                shzt = ((int)EnumDjShzt.WaitingApprove).ToString(),
                Sqsj = DateTime.Now,
                zt = "1"
            };
            djmain.Create(true, Guid.NewGuid().ToString());
            djmx = new List<SysMedicineStorageIOReceiptDetailEntity>();
            Request.mx.ForEach(p =>
            {
                var item = new SysMedicineStorageIOReceiptDetailEntity
                {
                    cd = null,
                    Ckbmkc = p.ckbmkc ?? 0,
                    Ckzhyz = p.ckzhyz ?? 0,
                    ckdw = p.ckdw,
                    CreateTime = DateTime.Now,
                    CreatorCode = UserIdentity.UserCode,
                    crkId = djmain.crkId,
                    jj = p.jj,
                    Lsj = p.lsj,
                    pc = p.pc,
                    Pfj = p.pfj,
                    Ph = p.ph,
                    Sl = p.sl,
                    Thyy = "",
                    Yklsj = p.yklsj,
                    Ykpfj = p.ykpfj,
                    Ypdm = p.ypdm,
                    Yxq = p.yxq,
                    Zje = p.zje,
                    zt = "1"
                };
                item.Create(true, Guid.NewGuid().ToString());
                djmx.Add(item);
            });
        }

        /// <summary>
        /// Xử lý chính
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            var result = returnInwardDmnService.SubmitReturnInward(djmain, djmx);
            if (!string.IsNullOrWhiteSpace(result))
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = result;
            }
        }
    }
}
