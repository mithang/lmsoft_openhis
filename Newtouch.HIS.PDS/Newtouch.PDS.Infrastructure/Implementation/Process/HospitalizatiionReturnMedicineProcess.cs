using Newtouch.Infrastructure.EF;
using Newtouch.HIS.Application.Implementation;
using FrameworkBase.MultiOrg.DmnService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.HIS.DomainServices;
using Newtouch.HIS.Repository;
using Newtouch.PDS.Requset;

namespace Newtouch.HIS.Application.Implementation.Process
{
    /// <summary>
    /// Trả thuốc nội trú
    /// </summary>
    public class HospitalizatiionReturnMedicineProcess : ProcessorFun<tyInfo>
    {
        private readonly List<HospitalizationReturnDrugParem> _effectiveLs;

        public HospitalizatiionReturnMedicineProcess(tyInfo request) : base(request)
        {
            _effectiveLs = new List<HospitalizationReturnDrugParem>();
        }

        protected override ActResult Validata()
        {
            if (Request == null || Request.tyDrugDetail.Count == 0) throw new FailedException("Vui lòng truyền thông tin trả thuốc hiệu lực");
            try
            {
                var locker = new object();
                List<ExecuteBatchDetail> execteBatch;
                Parallel.ForEach(Request.tyDrugDetail.Select(p => p.applyNo).Distinct().ToList(), item =>
                {
                    var applyInfo = new ZyReturnDrugApplyBillRepo(new DefaultDatabaseFactory()).SelectData(item, Request.organizeId);
                    if (applyInfo == null) throw new Exception(string.Format("Không tìm thấy đơn xin trả thuốc【{0}】", item));
                    if (applyInfo.ProcessState != 0) throw new Exception(string.Format("Đơn xin trả thuốc【{0}】Đang xử lý，Vui lòng không gửi trùng lặp", item));
                    var applyDetail = new ZyReturnDrugApplyBillDetailRepo(new DefaultDatabaseFactory()).SelectData(applyInfo.Id);
                    if (applyDetail == null || applyDetail.Count <= 0) throw new Exception(string.Format("Đơn xin trả thuốc【{0}】Không tìm thấy chi tiết đơn xin trả thuốc hiệu lực", item));

                    var rpInfo = new List<TyRpInfo>();
                    var effective = new HospitalizationReturnDrugParem
                    {
                        returnDrugBillNo = Request.returnDrugBillNo,
                        applyNo = item,
                        rpInfo = rpInfo,
                        organizeId = Request.organizeId,
                        userCode = Request.userCode,
                        yfbmCode = Request.yfbmCode
                    };
                    lock (locker)
                    {
                        _effectiveLs.Add(effective);
                    }

                    var yzId = Request.tyDrugDetail.Find(p => p.applyNo == item).yzId;

                    execteBatch = new List<ExecuteBatchDetail>();
                    rpInfo.Add(new TyRpInfo
                    {
                        yzId = yzId,
                        executeBatchDetail = execteBatch
                    });

                    //Mỗi lô thực hiện
                    Request.tyDrugDetail.FindAll(n => n.applyNo == item && n.yzId == yzId).Select(i => i.zxId).Distinct().ToList().ForEach(
                        z =>
                        {
                            var rpDetail = new List<RpDetail>();
                            execteBatch.Add(new ExecuteBatchDetail
                            {
                                zxId = z,
                                tyRpDetail = rpDetail
                            });
                            Request.tyDrugDetail.FindAll(n => n.applyNo == item && n.yzId == yzId && n.zxId == z).Select(i => i.ypCode).Distinct().ToList().ForEach(
                                o =>
                                {
                                    var tmp = Request.tyDrugDetail.FindAll(a => a.applyNo == item && a.yzId == yzId && a.zxId == z && a.ypCode == o);
                                    if (!applyDetail.Exists(p => p.ypCode == o)) return;

                                    var sqtysl = applyDetail.FindAll(p => p.ypCode == o).Sum(i => i.tysl * i.zhyz);
                                    if (sqtysl < tmp.Sum(i => i.sl * i.zhyz)) throw new Exception(string.Format("Đơn xin trả thuốc【{0}】của thuốc【{1}】Số lượng trả thuốc không được lớn hơn số lượng xin trả thuốc！", item, tmp[0].ypmc));
                                    rpDetail.Add(new RpDetail
                                    {
                                        ypCode = o,
                                        ypmc = tmp[0].ypmc,
                                        zhyz = tmp[0].zhyz,
                                        zsm = tmp[0].zsm,
                                        sfcl = tmp[0].sfcl,
                                        drugBatch = tmp.Select(b => new DrugBatch { pc = b.pc, ph = b.ph, sl = b.sl }).ToList()
                                    });
                                });
                        });
                });
            }
            catch (Exception e)
            {
                throw new FailedException(e.Message + (e.InnerException == null ? "" : e.InnerException.Message));
            }
            if (_effectiveLs.Count == 0) throw new FailedException("Thông tin trả thuốc truyền vào tất cả không hiệu lực");
            return base.Validata();
        }

        protected override void Action(ActResult actResult)
        {
            SinglethreadingAction(actResult);
        }


        private void SinglethreadingAction(ActResult actResult)
        {
            var errorMsg = new StringBuilder();
            _effectiveLs.ForEach(p =>
            {
                var tmpResult = new DispenseIndexInfoDmnService(new DefaultDatabaseFactory()).HospitalizatiionReturnMedicine(p);
                if (!string.IsNullOrWhiteSpace(tmpResult)) errorMsg.AppendLine(tmpResult);
            });
            if (errorMsg.Length <= 0) return;
            actResult.IsSucceed = false;
            actResult.ResultMsg = errorMsg.ToString();
        }

        private void MultithreadingAction(ActResult actResult)
        {
            try
            {
                var errorMsg = "";
                var locker = new object();
                var cts = new CancellationTokenSource();
                var parent = new Task(() =>
                {
                    var childFactory = new TaskFactory<bool>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                    foreach (var item in _effectiveLs)
                    {
                        childFactory.StartNew(() =>
                        {
                            if (cts.IsCancellationRequested)
                            {
                                return true;
                            }

                            var tmpResult = new DispenseIndexInfoDmnService(new DefaultDatabaseFactory()).HospitalizatiionReturnMedicine(item);
                            if (string.IsNullOrWhiteSpace(tmpResult)) return true;
                            lock (locker)
                            {
                                errorMsg = tmpResult;
                            }
                            cts.Cancel();
                            return false;
                        }, cts.Token);
                    }
                });
                parent.Start();
                parent.Wait();
                if (cts.IsCancellationRequested || errorMsg.Length > 0)
                {
                    throw new Exception(errorMsg);
                }
            }
            catch (Exception e)
            {
                actResult.ResultMsg = e.Message + (e.InnerException != null ? e.InnerException.Message : "");
                actResult.IsSucceed = false;
            }
        }
    }
}