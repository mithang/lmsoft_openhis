using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Infrastructure.EF;
using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DTO.DrugStorage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Common;
using Newtouch.Tools;
using Newtouch.Tools.Excel;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// 药库Controller
    /// </summary>
    public class DrugStorageController : ControllerBase
    {

        private readonly ISysMedicinePriceAdjustmentRepo _sysMedicinePriceAdjustmentRepo;
        private readonly IDrugStorageApp _drugStorageApp;
        private readonly IHandOutMedicineDmnService _handOutMedicineDmnService;
        private readonly IDrugStorageDmnService _drugStorageDmnService;
        private readonly ISysPharmacyDepartmentApp _sysPharmacyDepartmentApp;
        private readonly IMedicineInfoDmnService _medicineInfoDmnService;
        private readonly ISysPharmacyDepartmentMedicineRepo sysPharmacyDepartmentMedicineRepo;
        private readonly ISysMedicineStockCarryDownRepo _sysMedicineStockCarryDownRepo;
        private readonly ISysMedicineStockCarryDownDmnService _sysMedicineStockCarryDownDmnService;
        private readonly ISysMedicineCrkfsDmnService _iSysMedicineCrkfsDmnService;
        private readonly IPharmacyDrugStorageDmnService _pharmacyDrugStorageDmnService;

        #region Nhập kho
        /// <summary>
        /// 入库
        /// </summary>
        /// <returns></returns>
        public IActionResult InStorage()
        {
            return View();
        }

        /// <summary>
        /// 获取新的入库单号
        /// </summary>
        /// <returns></returns>
        public IActionResult initialRKDH()
        {
            var rkdh = EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo("药品入库单", Constants.CurrentYfbm.yfbmCode, this.OrganizeId);
            return Content(rkdh);
        }

        /// <summary>
        /// 输入码 药品信息（入库）
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SelectDepartmentMedicineList(string keyword)
        {
            var data = _drugStorageApp.SelectDepartmentMedicineList(keyword, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="crkdj">出入库单据</param>
        /// <param name="crkdjmx">出入库单据明细</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveInStorageInfo(SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailEntity[] crkdjmx)
        {
            var ioReceiptEntity = new SysMedicineStorageIOReceiptEntity
            {
                crkId = Guid.NewGuid().ToString(),
                OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
                Pdh = crkdj.Pdh,
                Rkbm = Constants.CurrentYfbm.yfbmCode,
                Ckbm = crkdj.Ckbm,
                //Rksj = DateTime.Now,
                //Cksj = null,
                Rkczy = OperatorProvider.GetCurrent().UserCode,
                Ckczy = null,
                Crkfsdm = crkdj.Crkfsdm,
                Czsj = DateTime.Now,
                Sqsj = DateTime.Now,
                Shczy = null,
                shzt = "0",
                zt = "1",
                djlx = 1
            };
            ioReceiptEntity.Create();

            var crkdjmxList = crkdjmx.ToList();
            var ioReceiptDetailEntityList = new List<SysMedicineStorageIOReceiptDetailEntity>();
            foreach (var item in crkdjmxList)
            {
                var ioReceiptDetailEntity = new SysMedicineStorageIOReceiptDetailEntity
                {
                    crkmxId = Guid.NewGuid().ToString(),
                    crkId = ioReceiptEntity.crkId,
                    Ypdm = item.Ypdm,
                    Fph = item.Fph,
                    Kprq = item.Kprq,
                    Dprq = null,
                    Ph = item.Ph,
                    Yxq = item.Yxq,
                    Pfj = item.Pfj,
                    Lsj = item.Lsj,
                    Ykpfj = item.Pfj,
                    Yklsj = item.Lsj,
                    Zje = item.Zje,
                    Sl = item.Sl,
                    Rkzhyz = item.Rkzhyz,
                    Rkbmkc = item.Rkbmkc,
                    Ckzhyz = item.Rkzhyz,
                    Ckbmkc = 0,
                    Wg = item.Wg,
                    zbbz = 0,
                    jkzcz = null,
                    hgzm = null,
                    ysjg = item.ysjg,
                    Thyy = null,
                    Cljg = null,
                    scrq = item.scrq,
                    kl = item.kl,
                    jj = item.jj,
                    cd = 0,
                    pc = item.pc,
                    zt = "1",
                    px = null
                };
                ioReceiptDetailEntity.Create();

                ioReceiptDetailEntityList.Add(ioReceiptDetailEntity);
            }
            _drugStorageApp.SaveInStorageInfo(ioReceiptEntity, ioReceiptDetailEntityList);
            return Success();
        }

        #endregion

        #region Xuất kho
        /// <summary>
        /// 出库
        /// </summary>
        /// <returns></returns>
        public IActionResult OutStorage()
        {
            return View();
        }

        /// <summary>
        /// 获取新的出库单号
        /// </summary>
        /// <returns></returns>
        public IActionResult initialCKDH()
        {
            var ckdh = EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo("外部出库单", Constants.CurrentYfbm.yfbmCode, this.OrganizeId);
            return Content(ckdh);
        }

        /// <summary>
        /// 发票查询
        /// </summary>
        /// <param name="fph"></param>
        /// <returns></returns>
        public IActionResult SelectMedicineListByFPH(string fph)
        {
            var data = _drugStorageApp.SelectMedicineListByFPH(fph);
            return Json(data);
        }

        /// <summary>
        /// 输入码 药品信息（出库）
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SelectDepartmentMedicineList2(string keyword, string fph, string gyscode)
        {
            var data = _drugStorageApp.SelectDepartmentMedicineList2(keyword, fph, gyscode);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        public IActionResult SaveOutStorageInfo(SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailVO[] crkdjmx)
        {
            var ioReceiptEntity = new SysMedicineStorageIOReceiptEntity
            {
                crkId = Guid.NewGuid().ToString(),
                OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
                Pdh = crkdj.Pdh,
                Rkbm = crkdj.Rkbm ?? "",
                Ckbm = Constants.CurrentYfbm.yfbmCode,
                //Rksj = null,
                //Cksj = DateTime.Now,
                Rkczy = null,
                Ckczy = OperatorProvider.GetCurrent().UserCode,
                Crkfsdm = crkdj.Crkfsdm ?? "",
                Czsj = DateTime.Now,
                Sqsj = DateTime.Now,
                Shczy = null,
                shzt = ((int)EnumDjShzt.WaitingApprove).ToString(),
                zt = "1",
                djlx = (int)EnumDanJuLX.waibucuku
            };
            ioReceiptEntity.Create();

            var crkdjmxList = crkdjmx.ToList();
            var ioReceiptDetailList = crkdjmxList.Select(item => new SysMedicineStorageIOReceiptDetailVO
            {
                crkmxId = Guid.NewGuid().ToString(),
                crkId = ioReceiptEntity.crkId,
                Ypdm = item.Ypdm,
                Fph = item.Fph,
                Kprq = item.Kprq,
                Dprq = null,
                Ph = item.Ph,
                Yxq = item.Yxq,
                Pfj = item.Pfj,
                Lsj = item.Lsj,
                Ykpfj = item.Pfj,
                Yklsj = item.Lsj,
                Zje = item.Zje,
                Sl = item.Sl,
                Rkzhyz = item.Ckzhyz,
                Rkbmkc = 0,
                Ckzhyz = item.Ckzhyz,
                Ckbmkc = item.Ckbmkc,
                Wg = null,
                zbbz = 1,
                jkzcz = null,
                hgzm = null,
                ysjg = null,
                Thyy = item.Thyy,
                Cljg = item.Cljg,
                scrq = item.scrq,
                kl = item.kl,
                jj = item.jj,
                cd = 0,
                pc = item.pc ?? "",
                zt = "1",
                px = null,
                kcId = item.kcId
            }).ToList();
            _drugStorageApp.SaveOutStorageInfo(ioReceiptEntity, ioReceiptDetailList);
            return Success();

        }

        #endregion

        #region 调价
        /// <summary>
        ///调价申请
        /// </summary>
        /// <returns></returns>
        public IActionResult PriceAdjustmentRequest()
        {
            return View();
        }

        /// <summary>
        ///调价审核
        /// </summary>
        /// <returns></returns>
        public IActionResult PriceAdjustmentApproval()
        {
            return View();
        }

        /// <summary>
        ///调价历史
        /// </summary>
        /// <returns></returns>
        public IActionResult PriceAdjustmentHistory()
        {
            return View();
        }

        /// <summary>
        /// 调价申请 （查询）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public IActionResult SelectAdjustPriceMedicineInfoList(Pagination pagination, string inputCode)
        {
            var list = new
            {
                rows = _drugStorageApp.SelectAdjustPriceMedicineInfoList(pagination, inputCode),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        ///  <summary>
        ///  提交申请
        ///  </summary>
        ///  <param name="ypCode"></param>
        ///  <param name="pfj"></param>
        ///  <param name="lsj"></param>
        ///  <param name="ypfj"></param>
        ///  <param name="ylsj"></param>
        ///  <param name="tzwj"></param>
        /// <param name="zxsj"></param>
        /// --zt:0:未审核 1:已审核 2:已拒绝 3.已撤销
        /// 	--zxzt:0:未执行 1:已执行
        ///  <returns></returns>
        public IActionResult SubmitRequuest(string ypCode, string pfj, string lsj, string ypfj, string ylsj, string tzwj, string zxsj)
        {
            var notApproveEntity = _sysMedicinePriceAdjustmentRepo.GetMedicinePriceAdjustmentNotApproveEntity(ypCode);
            if (notApproveEntity != null)
            {
                throw new FailedCodeException("THERE_ARE_UNRECOGNIZED_RECORDS_OF_THE_DRUG_AND_CAN_NOT_BE_REAPPLIED");
            }
            var entity = new SysMedicinePriceAdjustmentEntity
            {
                yptjId = Guid.NewGuid().ToString(),
                OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
                yfbmCode = Constants.CurrentYfbm.yfbmCode,
                ypCode = ypCode,
                pfj = Convert.ToDecimal(pfj),
                lsj = Convert.ToDecimal(lsj),
                ypfj = Convert.ToDecimal(ypfj),
                ylsj = Convert.ToDecimal(ylsj),
                shzt = "0",
                xglx = "tj",
                tzsj = DateTime.Now,
                tzwj = tzwj,
                zxsj = Convert.ToDateTime(zxsj),
                tzczy = OperatorProvider.GetCurrent().UserCode,
                shczy = null,
                zxczy = null,
                zxbz = "0",
                px = null
            };
            entity.Create();
            _sysMedicinePriceAdjustmentRepo.AddPriceAdjustmentRecord(entity);
            return Success();
        }

        /// <summary>
        /// 药品调价审核查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        public IActionResult SelectMedicineAdjustPriceApprovalInfoList(Pagination pagination, string inputCode, string shzt)
        {
            var list = new
            {
                rows = _drugStorageApp.SelectMedicineAdjustPriceApprovalInfoList(pagination, inputCode, shzt),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        ///  <summary>
        ///  药品审核
        ///  </summary>
        /// <param name="ypCodeStr"></param>
        /// <param name="operationType"></param>
        ///  --shzt:0:未审核 1:已审核 2:已拒绝 3.已撤销
        /// 	--zxbz:0:未执行 1:已执行
        ///  <returns></returns>
        public IActionResult MedicineAdjustPriceApproval(string ypCodeStr, int operationType)
        {
            var ypCodeList = new ArrayList(ypCodeStr.TrimEnd(',').Split(','));

            if (operationType == (int)EnumPriceAdjustOperationType.Execute) //执行
            {
                return Success(_drugStorageApp.ExecteAdjustPrice(ypCodeList));
            }
            var result = _sysMedicinePriceAdjustmentRepo.MedicinePriceAdjustment(ypCodeList, operationType); //Duyệt 拒绝 Thu hồi
            return Success(result);
        }

        /// <summary>
        /// 药品调价历史查询
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectMedicineAdjustPriceHistoryInfoList(Pagination pagination, string inputCode, string startTime, string endTime)
        {
            var list = new
            {
                rows = _drugStorageApp.SelectMedicineAdjustPriceHistoryInfoList(pagination, inputCode, startTime, endTime),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        #endregion

        #region 门诊发药退货
        /// <summary>
        ///  获取科室药品库存记录
        /// </summary>
        /// <param name="ypdm"></param>
        /// <returns></returns>
        public IActionResult GetpcInfoList(string ypdm)
        {
            var data = _handOutMedicineDmnService.GetpcList(ypdm);
            return Content(data.ToJson());
        }
        #endregion

        #region 后台指向页面
        /// <summary>
        ///内部发药
        /// </summary>
        /// <returns></returns>
        public IActionResult HandOutMedicine()
        {
            return View();
        }

        /// <summary>
        ///内部发药查询
        /// </summary>
        /// <returns></returns>
        public IActionResult HandOutMedicineInfo()
        {
            return View();
        }

        /// <summary>
        ///申领出库
        /// </summary>
        /// <returns></returns>
        public IActionResult HandOutMedicineByRequest()
        {
            return View();
        }

        /// <summary>
        /// 申领出库upgrade
        /// </summary>
        /// <returns></returns>
        public IActionResult ApplyDelivery()
        {
            return View();
        }

        /// <summary>
        /// 预览申领出库的药品信息
        /// </summary>
        /// <returns></returns>
        public IActionResult PreViewInfo()
        {
            return View();
        }

        /// <summary>
        ///退货审核
        /// </summary>
        /// <returns></returns>
        public IActionResult ConfirmationOfReturn()
        {
            return View();
        }

        /// <summary>
        /// 药房向科室发药
        /// </summary>
        /// <returns></returns>
        public IActionResult HandOutMedicineToks()
        {
            return View();
        }

        #endregion

        #region 内部发药，Xuất kho trực tiếp

        /// <summary>
        /// 初始化单据号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult InitDjh(string djmc)
        {
            return Success(null, EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo(djmc, Constants.CurrentYfbm.yfbmCode, OrganizeId));
        }

        /// <summary>
        /// 发药
        /// </summary>
        /// <param name="XT_YP_LS_NBFYMXK"></param>
        /// <param name="lybm"></param>
        /// <param name="fydh"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IActionResult ExecHandOutMedicine(List<XT_YP_LS_NBFYMXK> XT_YP_LS_NBFYMXK, string lybm, string fydh, string fyfs, int type)
        {
            var data = _handOutMedicineDmnService.HandOutMedicine(XT_YP_LS_NBFYMXK, lybm, fydh, fyfs, type);
            return string.IsNullOrWhiteSpace(data) ? Success() : Error(data);
        }

        /// <summary>
        /// 药房向科室发药
        /// </summary>
        /// <param name="XT_YP_LS_NBFYMXK"></param>
        /// <param name="lybm"></param>
        /// <param name="fydh"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IActionResult DispensingMedicineToKs(List<XT_YP_LS_NBFYMXK> XT_YP_LS_NBFYMXK, string lybm, string fydh, string fyfs, int type)
        {
            var data = _handOutMedicineDmnService.DispensingMedicineToKs(XT_YP_LS_NBFYMXK, lybm, fydh, fyfs, type);
            return string.IsNullOrWhiteSpace(data) ? Success() : Error(data);
        }
        #endregion

        #region 内部发药查询

        /// <summary>
        /// 内部发药查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req">搜索条件</param>
        /// <returns></returns>
        public IActionResult QueryHandOutMeidicineList(Pagination pagination, QueryMedicineInfoReqVO req)
        {
            var data = new
            {
                rows = _medicineInfoDmnService.GetHandOutMedicineInfoList(pagination, req),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        #endregion

        #region Xuất kho theo đơn lĩnh

        /// <summary>
        /// 获取申领主表信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldh">申领单号</param>
        /// <param name="slbm">申领部门</param>
        /// <param name="ffzt">发药状态</param>
        /// <param name="txtStartDate"></param>
        /// <param name="txtEndDate"></param>
        /// <returns></returns>
        public IActionResult RequestInfo(Pagination pagination, string sldh, string slbm, string ffzt, string txtStartDate, string txtEndDate)
        {
            ffzt = ffzt == "" ? "-1" : ffzt;
            var data = new
            {
                rows = _drugStorageDmnService.GetMedicineRequestInfo(pagination, sldh, slbm, ffzt, txtStartDate, txtEndDate),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 申领出库
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IActionResult ExecHandOutMedicineByReq(Dictionary<string, List<XT_YP_LS_NBFYMXK>> xtYpLsNbfymxk, string fyfs, int type)
        {
            var fydh = EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo("申领发药单", Constants.CurrentYfbm.yfbmCode, OrganizeId);
            var res = _handOutMedicineDmnService.HandOutMedicineByRequest(xtYpLsNbfymxk, fydh, fyfs, type);
            return string.IsNullOrWhiteSpace(res) ? Success() : Error(res);
        }

        /// <summary>
        /// 获取申领单药品信息
        /// </summary>
        /// <param name="sldId"></param>
        /// <returns></returns>
        public IActionResult RequestMedicineInfo(string sldId)
        {
            if (sldId == "undefined")
            {
                sldId = "";
            }
            var data = _drugStorageDmnService.GetMedicineDetail(sldId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// (申领出库)终止
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Abandonzt(string id)
        {
            _drugStorageDmnService.UpgradeStatus(id);
            return Success("Kết thúc thành công！");
        }
        #endregion


        #region 公共调用

        /// <summary>
        /// 输入码自动提示（一级部门提示，没有转换因子，例如药剂科）
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="ckbm">出库部门(发药药房)</param>
        /// <returns></returns>
        public IActionResult HandOutMedicinesrmList(string keyword, string ckbm)
        {
            var data = _handOutMedicineDmnService.GetDrugGroupByPc(keyword, ckbm, OrganizeId).Select(p => new
            {
                ypdm = p.ypcode,
                yplb = p.dlmc,
                ypmc = p.ypmc,
                srm = p.py,
                gg = p.ypgg,
                sccj = p.ycmc,
                bmdw = p.bmdw,
                ph = p.ph,
                kykc = p.klsl,
                lsj = p.lsj,
                pfj = p.pfj,
                yxq = (p.yxq ?? DateTime.MinValue).ToString("yyy-MM-dd"),
                zhyz = p.zhyz,
                pc = p.pc,
                zxsl = p.kcsl,
                zxdw = p.zxdw,
                bzs = p.bzs,
                bmdwsl = p.bmdwsl,
                ycmc = p.ycmc,
                bzdwsl = p.bzdwsl,
                bzdw = p.bzdw
            });
            return Content(data.ToJson());
        }

        /// <summary>
        /// 输入码自动提示（二级部门提示，计算转换因子，例如门诊药房）
        /// </summary>
        /// <returns></returns>
        public IActionResult MedicinesrmSlevelList(string rkbm, string keyword)
        {
            var data = _handOutMedicineDmnService.GetTyypBySrm(Constants.CurrentYfbm.yfbmCode, rkbm, keyword).Select(p => new
            {
                ypdm = p.ypcode,
                yplb = p.dlmc,
                ypmc = p.ypmc,
                srm = p.py,
                gg = p.ypgg,
                sccj = p.ycmc,
                pc = p.pc,
                ph = p.ph,
                kykc = p.bmdwsl,
                lsj = p.yklsj,
                pfj = p.ykpfj,
                yxq = p.yxq,
                zhyz = p.bzs,
                bzdwsl = p.bzdwsl,
                ykdw = p.bzdw,
                deptdw = p.deptdw,
                zxsl = p.kykcsl,
                zxdw = p.zxdw,
                bzdwslstr = p.bzdwslstr
            });
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取药房部门code
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTheLowerYfbmCodeList(string keyword)
        {
            var data = _pharmacyDrugStorageDmnService.GetTheLowerYfbmCodeList(keyword, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 根据配置表获取药房发药的科室
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetTheLowerKsCodeList(string keyword)
        {
            var data = _pharmacyDrugStorageDmnService.GetTheLowerKsCodeList(Constants.CurrentYfbm.yfbmCode, OrganizeId, keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 入库部门是否有权限使用该药
        /// </summary>
        /// <param name="yp"></param>
        /// <param name="rkbm"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CheckRkbmOwnMedicine(string yp, string rkbm)
        {
            var isExist = sysPharmacyDepartmentMedicineRepo.CheckRkbmOwnMedicine(yp, rkbm, OrganizeId);
            return isExist ? Success() : Error("对不起！入库部门无权限使用该药品");
        }


        #endregion

        #region Kết chuyển tồn kho 2018-08

        /// <summary>
        /// 库存结转视图页
        /// </summary>
        /// <returns></returns>
        public IActionResult StockCarryDown()
        {
            return View();
        }

        /// <summary>
        /// 获取历史结转时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetlsjzDateTime()
        {
            var list = _sysMedicineStockCarryDownRepo.GetlsjzDateTime(Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 结转结束时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetJzJssjDateTime()
        {
            var list = _sysMedicineStockCarryDownRepo.GetlsjzDateTime(Constants.CurrentYfbm.yfbmCode, OrganizeId);
            list.Insert(0, new Domain.Entity.V.KeyValueVEntity { key = "", value = "当前" });
            return Content(list.ToJson());
        }

        /// <summary>
        /// 查询上一次结转的时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryLastCarryTime()
        {
            var lastCarryTime = _sysMedicineStockCarryDownRepo.GetLastJzData(Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Success(null, lastCarryTime == null ? "" : lastCarryTime.Jzsj.ToString(Constants.DateTimeFormat));
        }

        /// <summary>
        /// 结转
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CarryOverMedicine()
        {
            _drugStorageApp.CarryOverMedicine(Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Success();
        }

        /// <summary>
        /// 查询已结转药品信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="jzsj"></param>
        /// <param name="keyWork"></param>
        /// <returns></returns>
        public IActionResult SelectCarryDownMedicineList(Pagination pagination, string jzsj, string keyWork)
        {
            var list = new
            {
                rows = _sysMedicineStockCarryDownDmnService.SelectCarryDownMedicineList(pagination, jzsj, keyWork),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        #endregion

        #region Thống kê nhập-xuất-tồn 2018-08

        /// <summary>
        /// 进销存统计视图页
        /// </summary>
        /// <returns></returns>
        public IActionResult PSIStatistics2018()
        {
            ViewBag.YfbmCode = Constants.CurrentYfbm.yfbmCode;
            ViewBag.OrganizeId= this.OrganizeId;
            return View();
        }

        /// <summary>
        /// 进销存统计
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="searchParam"></param>
        /// <returns></returns>
        public IActionResult PSIStatisticsInfoList(Pagination pagination, PsiStatisticsParam searchParam)
        {
            var list = new
            {
                rows = _drugStorageDmnService.GetPsiStatisticsVo(pagination, searchParam, Constants.CurrentYfbm.yfbmCode, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
        #endregion

        public IActionResult ExcelExportChooseColumns()
        {
            return View();
        }
        public IActionResult ExcelGet(string ksjzsj, string jsjzsj, string ypzt, string srm, string dl, string jx, string rate, string noPSI,
            bool? isContainFilter, int colStanWidth, string cols,string jxtext,string lbtext)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            PsiStatisticsParam sticsParam = new PsiStatisticsParam();
            sticsParam.ksjzsj = ksjzsj;
            sticsParam.jsjzsj = jsjzsj;
            sticsParam.ypzt = ypzt;
            sticsParam.srm = srm;
            sticsParam.dl = dl;
            sticsParam.jx = jx;
            sticsParam.rate = rate;
            sticsParam.noPSI = noPSI;
            if (string.IsNullOrWhiteSpace(cols))
            {
                cols = Request.Cookies["ExportExcelCols"];
                if (!string.IsNullOrWhiteSpace(cols))
                {
                    cols = System.Net.WebUtility.UrlDecode(cols);
                    Response.Cookies.Delete("ExportExcelCols");
                }
            }
            if (string.IsNullOrWhiteSpace(cols))
            {
                throw new FailedException("未指定导出列");
            }
            var pagination = new Pagination();
            pagination.sidx = "Ypdm desc"; //时间升序
            pagination.rows = 65536 - 1;    //Excel最大行数
            pagination.page = 1;    //第一页把所有都查出来
            var list = _drugStorageDmnService.GetPsiStatisticsVo(pagination, sticsParam, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            var colList = cols.ToObject<IList<ExcelColumn>>();
            //var kflbItemDetails = _itemDmnService.GetItemsDetailListByOrgIdAndItemCode(orgId, "RehabTreatmentMethod");
            var sheet = new ExcelSheet()
            {
                Title = "Thống kê nhập-xuất-tồn",
                columns = colList,
            };
            //sheet.columns.Where(p => p.Name == "zlrq").ToList().ForEach(p =>
            //{
            //    p.DateTimeFormat = "yyyy-MM-dd";
            //});
            //sheet.columns.Where(p => p.Name == "kflb").ToList().ForEach(t => t.FuncGetCellValue = (obj) =>
            //{
            //    return kflbItemDetails.Where(p => p.Code == obj.ToString()).Select(p => p.Name).FirstOrDefault();
            //});
            sheet.columns.Where(p => p.Name == "qcpfze").ToList().ForEach(t =>
            {
                t.NumberDigits = 2;
            });
            sheet.columns.Where(p => p.Name == "rkpfze").ToList().ForEach(t =>
            {
                t.NumberDigits = 2;
            });
            sheet.columns.Where(p => p.Name == "ckpfze" ).ToList().ForEach(t =>
            {
                t.NumberDigits = 2;
            });
            sheet.columns.Where(p => p.Name == "sypfze").ToList().ForEach(t =>
            {
                t.NumberDigits = 2;
            });
            sheet.columns.Where(p => p.Name == "pyze").ToList().ForEach(t => 
            {
                t.NumberDigits = 2;
            });
            sheet.columns.Where(p => p.Name == "pkze").ToList().ForEach(t => 
            {
                t.NumberDigits = 2;
            });
            sheet.columns.Where(p => p.Name == "qmpfze").ToList().ForEach(t =>
            {
                t.NumberDigits = 2;
            });
            sheet.columns.Where(p => p.Name == "tjsyze").ToList().ForEach(t =>
            {
                t.NumberDigits = 2;
            });
            sheet.columns.ToList().ForEach(p =>
            {
                p.WidthTimes = (double)p.Width / colStanWidth;
                p.Width = 0;    //Width都置为0
            });

            var path = DateTime.Now.ToString("\\\\yyyyMMdd\\\\HHmmssfff")+"Thống kê nhập-xuất-tồn" + ".xls";
            var filePath = CommmHelper.GetLocalFilePath("\\ExcelXuất\\药库查询导出" + path, "D:\\");

            if (isContainFilter == true)
            {
                //筛选条件
                var filterDict = new Dictionary<string, string>();
                filterDict.Add("导出部门", Constants.CurrentYfbm.yfbmmc);
                if (!string.IsNullOrWhiteSpace(sticsParam.ksjzsj))
                {
                    filterDict.Add("Ngày bắt đầu", sticsParam.ksjzsj);
                }
                if (!string.IsNullOrWhiteSpace(sticsParam.jsjzsj))
                {
                    filterDict.Add("Ngày kết thúc", sticsParam.jsjzsj);
                }
                if (!string.IsNullOrWhiteSpace(sticsParam.ypzt))
                {
                    filterDict.Add("Trạng thái thuốc", sticsParam.ypzt=="1"?"Bật":"Ngừng dùng");
                }
                if (!string.IsNullOrWhiteSpace(sticsParam.rate))
                {
                    filterDict.Add("Tỷ lệ chênh lệch bằng 0", sticsParam.rate == "0" ? "Tỷ lệ chênh lệch bằng 0" : "");
                }
                if (!string.IsNullOrWhiteSpace(sticsParam.srm))
                {
                    filterDict.Add("Mã thuốc", sticsParam.srm);
                }
                if (!string.IsNullOrWhiteSpace(jxtext))
                {
                    filterDict.Add("Dạng bào chế", jxtext);
                }
                if (!string.IsNullOrWhiteSpace(lbtext))
                {
                    filterDict.Add("Loại thuốc", lbtext);
                }
                if (!string.IsNullOrWhiteSpace(sticsParam.noPSI))
                {
                    filterDict.Add("Hiển thị thuốc chưa phát sinh nhập-xuất-tồn", sticsParam.noPSI=="0"?"Hiển thị":"Không hiển thị");
                }
                if (filterDict.Count > 0)
                {
                    sheet.filters = filterDict;
                }
            }

            var rest = list.ToExcel(filePath, sheet);

            if (rest)
            {
                ViewBag.filePath = filePath;
                return File(filePath, "application/x-xls", path.Replace("\\", ""));
            }
            else
            {
                return Content("文件导出失败，请返回列表页重试");
            }
        }


        #region 获取本部门药品和库存信息

        /// <summary>
        /// 获取有效期内的药品，不分批次
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="ypYfbmCode"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetValidMedicinesrmList(string srm, string ypYfbmCode)
        {
            return Content(_drugStorageDmnService.GetValidMedicinesrmList(srm, ypYfbmCode, OrganizeId).ToJson());
        }

        #endregion

        #region 快速入库

        /// <summary>
        /// 快速入库 视图
        /// </summary>
        /// <returns></returns>
        public IActionResult BriefInStorage()
        {
            return View();
        }
        #endregion

        #region 批量出库

        /// <summary>
        /// 批量直接出库
        /// </summary>
        /// <returns></returns>
        public IActionResult DirectDeliveryBatch()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// 获取出入库方式
        /// </summary>
        /// <param name="crkbz">0:入库  1：出库</param>
        /// <returns></returns>
        public IActionResult GetCrkfs(string crkbz)
        {
            var result = _iSysMedicineCrkfsDmnService.GetList(crkbz);
            return Content(result.ToJson());
        }

        /// <summary>
        /// 获取直接出库药品信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="rkbm"></param>
        /// <returns></returns>
        public IActionResult GetDirectDeliveryDrugsList(Pagination pagination, string rkbm)
        {
            var list = new
            {
                rows = _handOutMedicineDmnService.GetDirectDeliveryDrugsList(pagination, Constants.CurrentYfbm.yfbmCode, rkbm, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// 提交批量出库
        /// </summary>
        /// <param name="rkbm">入库部门</param>
        /// <param name="ypCodes">药品 按逗号分隔</param>
        /// <param name="djh">单据号</param>
        /// <param name="fyfs">发药方式代码</param>
        /// <returns></returns>
        public IActionResult DirectDeliveryBatchSubmit(string rkbm, string ypCodes, string djh, string fyfs)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ypCodes) || ypCodes.IndexOf(',') <= 0) return Error("药品不能为空");
                var tli = ypCodes.Trim().Split(',');
                var li = tli.Select(i => i.FilterSql()).ToList();
                var param = new DirectDeliveryBatchDTO
                {
                    Organizeid = OrganizeId,
                    djh = djh,
                    crkId = Guid.NewGuid().ToString(),
                    djlx = (int)EnumDanJuLX.piliangchuku,
                    rkbm = rkbm,
                    yfbmCode = Constants.CurrentYfbm.yfbmCode,
                    userCode = OperatorProvider.GetCurrent().UserCode,
                    crkfs = fyfs,
                    shzt = ((int)EnumDjShzt.Approved).ToString(),
                    ypCodes = string.Join(",", li)
                };
                var result = _handOutMedicineDmnService.DirectDeliveryBatchSubmit(param);
                return Success(result);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
        }
        #endregion
    }
}