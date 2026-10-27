using System;
using System.Collections.Generic;
using System.Threading;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.ExException;
using Newtouch.Herp.Infrastructure.Log;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// Port of the original ProcessorFun&lt;T&gt; (BillApprovalProcess / process base).
    /// Orchestration is identical to the original: Validata -&gt; BeforeAction -&gt;
    /// Action -&gt; AfterAction -&gt; AsyncAction (queued on the thread pool).
    /// Deliberate adaptations for .NET 6 (no behaviour change for callers):
    /// - No HttpContext: tags are built from the current operator when one
    ///   exists, otherwise left empty (the original swallowed the same case
    ///   in its ctor try/catch).
    /// - No PerformanceMonitoring wrapper (binary-only in the original): the
    ///   steps run directly; exceptions still map exactly as the original.
    /// - LogCore here takes (message, ex, addInfo); messages are Vietnamese.
    /// </summary>
    public class ProcessorFun<T> : IProcessor, IProcess
    {
        protected T Request;
        protected Dictionary<string, string> Tags;

        protected ProcessorFun(T request)
        {
            Request = request;
            Tags = new Dictionary<string, string>();
            try
            {
                var opr = OperatorProvider.GetCurrent();
                if (opr != null)
                {
                    Tags.Add(Constants.OrganizeId, opr.OrganizeId);
                    Tags.Add(Constants.KfId, Constants.CurrentKf.currentKfId);
                    Tags.Add(Constants.CreatorCode, opr.UserCode);
                }
            }
            catch (Exception ex)
            {
                LogCore.Error("Lỗi khởi tạo ProcessorFun", ex, Tags);
            }
        }

        public ActResult Process()
        {
            var result = new ActResult();
            try
            {
                result = Validata();
                if (!result.IsSucceed)
                {
                    return result;
                }
                BeforeAction(result);
                Action(result);
                if (!result.IsSucceed)
                {
                    return result;
                }
                AfterAction(result);
                if (!result.IsSucceed)
                {
                    return result;
                }
                AsyncProcess(result);
            }
            catch (FailedException ex)
            {
                result.IsSucceed = false;
                result.ResultCode = (int)ResultCode.ValidationFailure;
                result.ResultMsg = ex.Msg;
                LogCore.Error("Lỗi xử lý ProcessorFun", ex, Tags);
            }
            catch (ProcessException ex)
            {
                result.IsSucceed = false;
                result.ResultCode = (int)ResultCode.InternalProcessingAbnormality;
                result.ResultMsg = ex.Msg;
                LogCore.Error("Lỗi xử lý ProcessorFun", ex, Tags);
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.ResultCode = (int)ResultCode.Other;
                result.ResultMsg = "Xử lý nội bộ thất bại, vui lòng liên hệ quản trị viên";
                LogCore.Error("Lỗi xử lý ProcessorFun", ex, Tags);
            }
            return result;
        }

        private void AsyncProcess(ActResult result)
        {
            ThreadPool.QueueUserWorkItem(_ => AsyncAction(result));
        }
    }
}
