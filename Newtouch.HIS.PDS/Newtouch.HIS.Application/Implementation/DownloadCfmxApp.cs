using System;
using System.Collections.Generic;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DTO.OutPatientPharmacy;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset;
using Newtouch.Tools;
using static Newtouch.Common.Web.APIRequestHelper;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Chi tiết đơn thuốc triển khai
    /// </summary>
    public class DownloadCfmxApp : ProcessorFun<string>
    {

        private List<MzCfmxEntity> _mzCfmxEntities;
        private List<OutPatientpyCfmxDTO> _cfmx;
        private readonly IMzCfmxRepo _mzCfmxRepo;
        private readonly ISysMedicineReceiptDmnService _sysMedicineReceiptDmnService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        public DownloadCfmxApp(string request) : base(request)
        {
        }

        /// <summary>
        /// Xác thực
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (string.IsNullOrWhiteSpace(Request) || Request.ToLower().Equals("undefined")) throw new FailedException("处方号不能为空");
            return new ActResult();
        }

        /// <summary>
        /// Tiền xử lý
        /// </summary>
        protected override void BeforeAction(ActResult actResult)
        {
            //_mzCfmxEntities = _mzCfmxRepo.GetCfmxByCfh(Request.cfh);
            //if (_mzCfmxEntities == null || _mzCfmxEntities.Count <= 0)
            //{
            //    _cfmx = QueryCfmx();
            //}
           
            _cfmx = QueryCfmx();
        }

        /// <summary>
        /// Lấy chi tiết đơn thuốc từ hệ thống thanh toán
        /// </summary>
        /// <returns></returns>
        private List<OutPatientpyCfmxDTO> QueryCfmx()
        {
            var reqObj = new
            {
                cfh = Request,
                TimeStamp = DateTime.Now
            };
            var apiResp = SiteSettAPIHelper.Request<object,DefaultResponse>("api/OutpatientPharmacy/WaitingDispenseMedicineDetailQuery", reqObj);
            if (apiResp.code != ResponseResultCode.SUCCESS && apiResp.data == null)
            {
                throw new FailedException("从结算系统获取处方明细失败");
            }
            var cfmxs = apiResp.data.ToString().ToObject<List<OutPatientpyCfmxDTO>>();
            if (cfmxs != null && cfmxs.Count > 0)
            {
                return cfmxs;
            }
            throw new FailedException("从结算系统获取处方明细失败");
        }

        /// <summary>
        /// Xử lý chính
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            AssembleMzCfmxEntityEntities();
            if (_mzCfmxEntities != null && _mzCfmxEntities.Count > 0 && _cfmx != null && _cfmx.Count > 0)
            {
                if (_mzCfmxRepo.Insert(_mzCfmxEntities) > 0) return;
                actResult.IsSucceed = false;
                actResult.ResultMsg = "保存处方明细失败";
            }
        }

        /// <summary>
        /// Lắp ráp chi tiết đơn thuốc
        /// </summary>
        private void AssembleMzCfmxEntityEntities()
        {
            //if (_mzCfmxEntities != null && _mzCfmxEntities.Count > 0) return;
            _mzCfmxEntities = new List<MzCfmxEntity>();
            _cfmx.ForEach(p =>
            {
                //Chi tiết đơn thuốc đó có tồn tại hay không
                if (IsExist(p))
                {
                    _mzCfmxRepo.DeleteCfmx(Request);
                }
                //Khi đồng bộ chi tiết đơn thuốc, phán đoán số lượng lớn hơn0Việc triển khai của
                if (p.sl > 0)
                {
                    var zhyz = _sysMedicineReceiptDmnService.GetZhyz(OperatorProvider.GetCurrent(true).OrganizeId, Constants.CurrentYfbm.yfbmCode, p.yp);
                    if (zhyz == 0) throw new FailedException("药库药品【{0}】转换因子为0", p.ypmc);
                    _mzCfmxEntities.Add(new MzCfmxEntity
                    {
                        bz = p.yszt,
                        cfh = p.sffymxxh,
                        czh = p.czh,
                        dj = p.dj,
                        dw = p.dw,
                        gg = p.ypgg,
                        je = p.je,
                        jl = p.jl,
                        jldw = p.jldw,
                        sl = Convert.ToInt32(p.sl) * (int)p.zhyz / zhyz,
                        ycmc = p.ycmc,
                        yfmc = p.yfmc,
                        ypCode = p.yp,
                        ypmc = p.ypmc,
                        CreateTime = DateTime.Now,
                        CreatorCode = OperatorProvider.GetCurrent().UserCode,
                        OrganizeId = p.OrganizeId,
                        zhyz = (int)p.zhyz
                    });
                }               
            });
        }

        /// <summary>
        /// Phán đoán xem chi tiết đơn thuốc đó có tồn tại hay không
        /// </summary>
        /// <param name="outPatientpyCfmxDto"></param>
        /// <returns></returns>
        private bool IsExist(OutPatientpyCfmxDTO outPatientpyCfmxDto)
        {
            return _mzCfmxRepo.IsExist(outPatientpyCfmxDto.sffymxxh, outPatientpyCfmxDto.yp, OperatorProvider.GetCurrent().OrganizeId, outPatientpyCfmxDto.ypgg);
        }

        /// <summary>
        /// Xử lý sau
        /// </summary>
        protected override void AfterAction(ActResult actResult)
        {
            actResult.Data = _mzCfmxEntities;
        }
    }
}
