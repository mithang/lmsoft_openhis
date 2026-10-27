using Newtouch.Infrastructure.EF;
using Newtouch.HIS.Application.Implementation;
using FrameworkBase.MultiOrg.DmnService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.DomainServices;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ResourcesOperate;

namespace Newtouch.HIS.Application.Implementation.Process
{
    /// <summary>
    /// Xếp thuốc ngoại trúV2.0 2019-09-18
    /// </summary>
    public class OutpatientBookAppV2 : ProcessorFun<OutpatientBookRequestDTO>
    {
        private List<MzCfEntity> mzcfxx = new List<MzCfEntity>();
        private List<MzCfmxEntity> mzcfmx = new List<MzCfmxEntity>();
        private readonly IPyDmnService _pyDmnService;
        private readonly IDispensingDmnService _dispensingDmnService;

        public OutpatientBookAppV2(OutpatientBookRequestDTO request) : base(request)
        {
        }

        /// <summary>
        /// Kiểm tra
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request.Items == null || Request.Items.Count == 0) throw new FailedException("Hạng mục/Thuốc không được để trống");
            if (string.IsNullOrWhiteSpace(Request.OrganizeId)) throw new FailedException("OrganizeId(Mã cơ cấu tổ chức) Bắt buộc truyền");
            try
            {
                Parallel.ForEach(Request.Items, p =>
                {
                    if (string.IsNullOrWhiteSpace(p.ItemCode)) throw new FailedException("ItemCode(Số hiệu hạng mục) Bắt buộc truyền。");
                    if (string.IsNullOrWhiteSpace(p.Cfh)) throw new FailedException("Cfh(Số đơn thuốc) Bắt buộc nhập。");
                    if (string.IsNullOrWhiteSpace(p.CardNo)) throw new FailedException("CardNo(Số thẻ) Bắt buộc nhập。");
                    var rpCount = new fyDmnService(new DefaultDatabaseFactory(), false).ExistEffectiveRp(p.Cfh, p.Yfbm, Request.OrganizeId);
                    if (rpCount > 0) throw new Exception("Đã tồn tại đơn thuốc【" + p.Cfh + "】，Không thể gửi trùng lặp。");
                    var ypxx = new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(Request.OrganizeId,
                        p.ItemCode);
                    if (ypxx == null) throw new Exception("Không tìm thấy hạng mục【" + (string.IsNullOrWhiteSpace(p.ItemName) ? p.ItemCode : p.ItemName) + "】。");
                    p.ItemName = string.IsNullOrWhiteSpace(p.ItemName) ? ypxx.ypmc : p.ItemName;
                    if (p.Zhyz <= 0)
                    {
                        if (ypxx.mzcls == null) throw new Exception("Hạng mục【" + p.ItemName + "】Số chia lẻ ngoại trú trống。");
                        p.Zhyz = (int)ypxx.mzcls;
                    }

                    p.ItemSpecifications = string.IsNullOrWhiteSpace(p.ItemSpecifications) ? ypxx.ypgg : p.ItemSpecifications;
                    p.ItemManufacturer = string.IsNullOrWhiteSpace(p.ItemManufacturer) ? ypxx.ycmc : p.ItemManufacturer;
                    p.Dosage = p.Dosage ?? 0;
                });
            }
            catch (Exception e)
            {
                throw new FailedException(e.Message + (e.InnerException != null ? e.InnerException.Message : ""));
            }
            return new ActResult();
        }

        /// <summary>
        /// Tiền xử lý Lắp rápmz_cfvàmz_cfmxDữ liệu
        /// </summary>
        /// <param name="actResult"></param>
        protected override void BeforeAction(ActResult actResult)
        {
            Parallel.For(0, 2, i => //Lắp ráp dữ liệu
            {
                switch (i)
                {
                    case 0:
                        mzcfxx = AssembleMzCfEntity();
                        break;
                    case 1:
                        mzcfmx = AssembleMzCfmxEntity();
                        break;
                }
            });
        }

        protected override void Action(ActResult actResult)
        {
            var errorMsg = new StringBuilder();
            errorMsg.Append(_pyDmnService.InsertOutpatientRpInfo(mzcfxx, mzcfmx));
            if (!string.IsNullOrWhiteSpace(errorMsg.ToString()))
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = errorMsg.ToString();
                return;
            }

            foreach (var p in mzcfxx)
            {
                var bookRequest = new BookItemDo
                {
                    Yfbm = p.lyyf,
                    Cfh = p.cfh,
                    OrganizeId = p.OrganizeId,
                    CreatorCode = p.CreatorCode
                };
                errorMsg.Append(_dispensingDmnService.OutPatientBookV2(bookRequest));
                if (!string.IsNullOrWhiteSpace(errorMsg.ToString())) _pyDmnService.DeleteRpInfo(p.cfh, p.OrganizeId, p.lyyf);
            }

            if (string.IsNullOrWhiteSpace(errorMsg.ToString())) return;
            actResult.IsSucceed = false;
            actResult.ResultMsg = errorMsg.ToString();
        }

        /// <summary>
        /// Lắp ráp thông tin đơn ngoại trú
        /// </summary>
        /// <returns></returns>
        private List<MzCfEntity> AssembleMzCfEntity()
        {
            var cfhs = Request.Items.Select(p => p.Cfh).Distinct();
            return (from cfh in cfhs
                    let cfxx = Request.Items.Find(p => p.Cfh == cfh)
                    select new MzCfEntity
                    {
                        cfh = cfh,
                        jsnm = 0,
                        CardNo = cfxx.CardNo ?? "",
                        xm = cfxx.xm ?? "",
                        xb = cfxx.xb ?? "",
                        Fph = "",
                        nl = cfxx.nl,
                        brxzmc = cfxx.brxzmc,
                        ysmc = cfxx.ysmc,
                        ksmc = cfxx.ksmc,
                        je = Request.Items.FindAll(p => p.Cfh == cfh).Sum(s => s.ItemCount * s.ItemUnitPrice),
                        OrganizeId = Request.OrganizeId,
                        lyyf = cfxx.Yfbm,
                        fybz = ((int)EnumFybz.Yp).ToString(),
                        zt = "1",
                        CreateTime = DateTime.Now,
                        CreatorCode = Request.CreatorCode
                    }).ToList();
        }

        /// <summary>
        /// Lắp ráp thông tin chi tiết đơn ngoại trú
        /// </summary>
        /// <returns></returns>
        private List<MzCfmxEntity> AssembleMzCfmxEntity()
        {
            var result = new List<MzCfmxEntity>();
            Request.Items.ForEach(p =>
            {
                result.Add(new MzCfmxEntity
                {
                    cfh = p.Cfh,
                    ypCode = p.ItemCode,
                    ypmc = p.ItemName ?? "",
                    gg = p.ItemSpecifications,
                    sl = (int)p.ItemCount,
                    dw = p.ItemUnitName ?? "",
                    dj = p.ItemUnitPrice / p.Zhyz,
                    ycmc = p.ItemManufacturer,
                    je = p.ItemCount * p.ItemUnitPrice,
                    jl = p.Dosage,
                    jldw = p.DosageUnit,
                    yfmc = p.UsageName,
                    bz = p.Remark,
                    zhyz = p.Zhyz,
                    czh = p.GroupNum,
                    OrganizeId = Request.OrganizeId,
                    CreatorCode = Request.CreatorCode,
                    CreateTime = DateTime.Now,
                    zt = "1"
                });
            });
            return result;
        }
    }
}