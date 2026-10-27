using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Infrastructure.EF;
using FrameworkBase.MultiOrg.DmnService;
using System;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.VO;
using Newtouch.HIS.DomainServices;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Log;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ResourcesOperate;
using Newtouch.Tools;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Đặt trước tài nguyên
    /// </summary>
    public class OutpatientBookApp : ProcessorFun<OutpatientBookRequestDTO>
    {
        private readonly IDispensingDmnService _dispensingDmnService;
        private readonly IPyDmnService _pyDmnService;
        private readonly IResourcesOperateApp _resourcesOperateApp;
        private readonly ISysMedicineRepo _sysMedicineRepo;
        private readonly Newtouch.HIS.Domain.IDomainServices.IBaseDataDmnService _baseDataDmnService;
        private readonly IOutpatientPrescriptionDetailBatchNumberRepo _mxphRepo;

        /// <summary>
        /// bookHạng mục thất bại，Trả về cho bên yêu cầu
        /// </summary>
        private readonly List<FailItemDetail> _failList;
        /// <summary>
        /// bookHạng mục thành công， Khôi phục sau này
        /// </summary>
        private readonly List<BookItemDo> _successList;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="request"></param>
        public OutpatientBookApp(OutpatientBookRequestDTO request) : base(request)
        {
            _failList = new List<FailItemDetail>();
            _successList = new List<BookItemDo>();
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
                    if (ypxx == null) throw new Exception("Không tìm thấy hạng mục【" + p.ItemName + "】。");
                    if (p.Zhyz <= 0)
                    {
                        if (ypxx.mzcls == null) throw new Exception("Hạng mục【" + p.ItemName + "】Số chia lẻ ngoại trú trống。");
                        p.Zhyz = (int)ypxx.mzcls;
                    }

                    p.ItemSpecifications = string.IsNullOrWhiteSpace(p.ItemSpecifications) ? ypxx.ypgg : p.ItemSpecifications;
                    p.ItemManufacturer = string.IsNullOrWhiteSpace(p.ItemManufacturer) ? ypxx.ycmc : p.ItemManufacturer;
                });
            }
            catch (Exception e)
            {
                throw new FailedException(e.Message + (e.InnerException != null ? e.InnerException.Message : ""));
            }
            return new ActResult();
        }

        /// <summary>
        /// Xử lý chính
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            var errorMsg = new StringBuilder();
            Parallel.ForEach(Request.Items, p =>
            {
                var bookItem = new BookItemDo
                {
                    YpCode = p.ItemCode,
                    Sl = (int)p.ItemCount * p.Zhyz,
                    Yfbm = p.Yfbm ?? "",
                    OrganizeId = Request.OrganizeId ?? "",
                    Cfh = p.Cfh ?? "",
                    CreatorCode = Request.CreatorCode ?? "",
                    czh = p.GroupNum
                };
                var result = new DispensingDmnService(new DefaultDatabaseFactory(), false).OutPatientBook(bookItem);
                if (string.IsNullOrWhiteSpace(result))
                {
                    _successList.Add(bookItem);
                }
                else
                {
                    var failItem = p.ToJson().ToObject<FailItemDetail>();
                    if (failItem == null) return;
                    failItem.FailMsg = result;
                    errorMsg.Append(result + " ");
                    _failList.Add(failItem);
                }
            });

            if (_failList.Count <= 0) return;
            actResult.Data = _failList;
            actResult.IsSucceed = false;
            actResult.ResultMsg = errorMsg.ToString();
            if (_successList.Any())
            {
                //Khôi phục Giải đông
                OutPatientBookCancel();
            }
        }

        /// <summary>
        /// Hậu xử lý
        /// </summary>
        /// <param name="actResult"></param>
        protected override void AfterAction(ActResult actResult)
        {
            try
            {
                //Chèn mz_cf、mz_cfmx bảng
                var mzcfxx = new List<MzCfEntity>();
                var mzcfmx = new List<MzCfmxEntity>();
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
                _pyDmnService.InsertOutpatientRpInfo(mzcfxx, mzcfmx);
            }
            catch (Exception e)
            {
                //Khôi phục Giải đông
                OutPatientBookCancel();
                LogCore.Error("OutpatientBookApp AfterAction InsertOutpatientRpInfo error", e);
            }
        }

        /// <summary>
        /// Hủy đặt trước ngoại trú
        /// </summary>
        private void OutPatientBookCancel()
        {
            try
            {
                _successList.ForEach(p =>
                {
                    using (var db = new EFDbTransaction(new DefaultDatabaseFactory()).BeginTrans())
                    {
                        var sql1 = @"SELECT * FROM dbo.mz_cfmxph WHERE cfh=@cfh AND zt='1' AND gjzt='0' AND yp=@ypCode AND fyyf=@yfbmCode AND OrganizeId=@OrganizeId ";
                        var param1 = new SugarParameter[] { new SugarParameter("@cfh", p.Cfh), new SugarParameter("@ypCode", p.YpCode), new SugarParameter("@yfbmCode", p.Yfbm), new SugarParameter("@OrganizeId", p.OrganizeId) };
                        var mxph = db.FindList<OutpatientPrescriptionDetailBatchNumberEntity>(sql1, param1);
                        if (mxph == null || mxph.Count == 0) return;
                        var dd = new List<MzcfphxxVO>();
                        foreach (var o in mxph)
                        {
                            o.zt = "0";
                            o.gjzt = "1";
                            db.Update(o);

                            var it = dd.Find(q => q.ypCode == o.yp && q.OrganizeId == o.OrganizeId && q.pc == o.pc && q.ph == o.ph && q.cfh == o.cfh && q.yfbmCode == o.fyyf);
                            if (it == null)
                            {
                                dd.Add(new MzcfphxxVO
                                {
                                    OrganizeId = o.OrganizeId,
                                    pc = o.pc,
                                    ph = o.ph,
                                    sl = (int)o.sl,
                                    yfbmCode = o.fyyf,
                                    ypCode = o.yp,
                                    cfh = o.cfh
                                });
                            }
                            else
                            {
                                it.sl += (int)o.sl;
                            }
                        }
                        if (dd.Count == 0) return;

                        foreach (var o in dd)
                        {
                            var sql2 = @"SELECT * FROM dbo.xt_yp_kcxx WHERE ypdm=@ypCode AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND yfbmCode=@yfbmCode AND zt='1'";
                            var param2 = new SugarParameter[]
                            {
                                new SugarParameter("@ypCode",o.ypCode),
                                new SugarParameter("@OrganizeId", o.OrganizeId),
                                new SugarParameter("@pc",o.pc),
                                new SugarParameter("@ph", o.ph),
                                new SugarParameter("@yfbmCode",o.yfbmCode)
                            };
                            var kcxx = db.FindList<SysMedicineStockInfoEntity>(sql2, param2);
                            if (kcxx == null || kcxx.Count == 0) continue;
                            if (kcxx.Sum(i => i.djsl) >= o.sl)
                            {
                                var sysl = o.sl;
                                foreach (var k in kcxx)
                                {
                                    if (sysl <= 0) break;

                                    if (k.djsl >= sysl)
                                    {
                                        k.djsl -= sysl;
                                        sysl = 0;
                                    }
                                    else
                                    {
                                        sysl -= k.djsl;
                                        k.djsl = 0;
                                    }
                                    db.Update(k);
                                }
                            }
                            else
                            {
                                throw new FailedException(string.Format("Đơn thuốc【{0}】của thuốc【{1}】Số đã đóng băng nhỏ hơn số đóng băng sắp hủy，Hủy đóng băng thất bại", o.cfh, o.ypCode));
                            }
                        }

                        db.Commit();
                    }
                });
            }
            catch (Exception e)
            {
                LogCore.Error("OutPatientBookCancel error", e, string.Format("_successList:{0}", _successList.ToJson()));
            }
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
