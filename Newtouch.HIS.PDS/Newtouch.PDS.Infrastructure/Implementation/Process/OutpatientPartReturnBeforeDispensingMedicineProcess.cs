using Newtouch.Infrastructure.EF;
using Newtouch.HIS.Application.Implementation;
using FrameworkBase.MultiOrg.DmnService;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.DomainServices;
using Newtouch.HIS.DomainServices.OutPatientPharmacy;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Log;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ResourcesOperate;

namespace Newtouch.HIS.Application.Implementation.Process
{
    /// <summary>
    /// (Trước khi phát thuốc)Trả thuốc một phần
    /// </summary>
    public class OutpatientPartReturnBeforeDispensingMedicineProcess : ProcessorFun<OutpatientPartReturnBeforeDispensingMedicineRequestDTO>
    {
        private readonly IResourcesOperateApp _resourcesOperateApp;

        public OutpatientPartReturnBeforeDispensingMedicineProcess(OutpatientPartReturnBeforeDispensingMedicineRequestDTO request) : base(request)
        {
        }

        protected override ActResult Validata()
        {
            if (Request == null) throw new FailedException("", "Bản tin yêu cầu không được để trống");
            if (Request.Items == null || Request.Items.Count == 0) throw new FailedException("", "Chi tiết trả thuốc không được để trống");
            try
            {
                Parallel.ForEach(Request.Items.Select(p => p.cfh).Distinct().ToList(), p =>
                {
                    var rpInfo = new tyDmnService(new DefaultDatabaseFactory(), false).SelectRpList(p, Request.OrganizeId);
                    if (rpInfo == null || rpInfo.Count == 0) throw new Exception("Không tìm thấy đơn thuốc hiệu lực");
                    if (rpInfo[0].fybz != ((int)EnumFybz.Yp).ToString()) throw new Exception("Thao tác trả thuốc này chỉ áp dụng cho đơn đã xếp chưa phát");
                });
            }
            catch (Exception e)
            {
                LogCore.Error("OutpatientPartReturnBeforeDispensingMedicineProcess Validata error", e,
                    "(Trước khi phát thuốc)Ngoại lệ kiểm tra trả thuốc một phần");
                throw new FailedException("", e.Message + (e.InnerException != null ? e.InnerException.Message : ""));
            }
            return new ActResult();
        }

        protected override void BeforeAction(ActResult actResult)
        {
            try
            {
                Parallel.ForEach(Request.Items, p =>
                {
                    if (p.zhyz != 0) return;
                    var ypxx = new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(Request.OrganizeId, p.ypdm);
                    if (ypxx == null) throw new Exception("Không tìm thấy mã là【" + p.ypdm + "】của thuốc。");
                    if (ypxx.mzcls == null) throw new Exception("Mã thuốc là【" + p.ypdm + "】Số chia lẻ ngoại trú của thuốc trống。");
                    p.zhyz = (int)ypxx.mzcls;
                    if (p.sl <= 0) Request.Items.Remove(p);
                });
            }
            catch (Exception e)
            {
                throw new FailedException(e.Message + (e.InnerException != null ? e.InnerException.Message : ""));
            }
        }

        private static object actionThreadLock = new object();

        protected override void Action(ActResult actResult)
        {
            var errorMsg = "";
            try
            {
                var cts = new CancellationTokenSource();
                var parent = new Task(() =>
                {
                    var childFactory = new TaskFactory<bool>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                    Request.Items.ForEach(p =>
                    {
                        childFactory.StartNew(() =>
                        {
                            if (cts.IsCancellationRequested)
                            {
                                return default(bool);
                            }
                            var tmpResult = new tyDmnService(new DefaultDatabaseFactory(), false).OutpatientPartReturn(p, Request.OrganizeId);
                            if (string.IsNullOrWhiteSpace(tmpResult)) return true;
                            lock (actionThreadLock)
                            {
                                errorMsg = tmpResult;
                            }

                            cts.Cancel();
                            return false;
                        }, cts.Token);
                    });
                });
                parent.Start();
                parent.Wait(cts.Token);
                if (!string.IsNullOrWhiteSpace(errorMsg)) throw new Exception(errorMsg);
            }
            catch (Exception e)
            {
                throw new FailedException(e.Message + (e.InnerException != null ? e.InnerException.Message : "") + errorMsg);
            }
        }
    }
}