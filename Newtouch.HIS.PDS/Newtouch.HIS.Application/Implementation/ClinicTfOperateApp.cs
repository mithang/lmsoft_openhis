using System;
using System.Collections.Generic;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure.Log;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ClinicTfOperate;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Thực hiện y lệnh
    /// </summary>
    public class ClinicTfOperateApp : ProcessorFun<ClinicTfOperateRequest>
    {
        private List<MzCfEntity> _cfEntities;
        private readonly IMzCfRepo _mzCfRepo;
        private readonly IMzCfmxRepo mzCfmxRepo;
        private readonly IPyDmnService _pyDmnService;

        public ClinicTfOperateApp(ClinicTfOperateRequest request) : base(request)
        {
        }

        /// <summary>
        /// Xác thực
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request == null) throw new FailedException("请求报文不能为空");
            if (Request.cfhs == null || Request.cfhs.Count <= 0) throw new FailedException("处方列表不能为空");
            Request.cfhs.ForEach(ValidataYzDetail);
            return new ActResult();
        }

        /// <summary>
        /// Kiểm tra chi tiết y lệnh
        /// </summary>
        /// <param name="detail"></param>
        private static void ValidataYzDetail(CfDetial CfDetial)
        {
            if (string.IsNullOrWhiteSpace(CfDetial.cfh)) throw new FailedException("请传入有效的处方号");
            if (string.IsNullOrWhiteSpace(CfDetial.OrganizeId)) throw new FailedException("请传入有效的组织机构代码");
        }

        /// <summary>
        /// Xử lý chính
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            AssembleTfCfEntity();
            if (_cfEntities != null && _cfEntities.Count > 0)
            {
                foreach (var item in _cfEntities)
                {
                    //Trước khi hoàn phí, trước tiên phán đoán xem đã xếp thuốc hay chưa
                    if (IsPrescribeYp(item.cfh, item.OrganizeId))
                    {
                        List<MzCfmxEntity> Cfmx = mzCfmxRepo.GetCfmxByCfh(item.cfh);
                        //Trường hợp đã xếp thuốc, trước tiên hoàn trả phần đóng băng
                        Cfmx.ForEach(p =>
                        {
                            try
                            {
                                var errorMsg = _pyDmnService.CancelArrangedDrug(p);
                                if (!string.IsNullOrWhiteSpace(errorMsg)) throw new Exception(errorMsg);
                            }
                            catch (Exception ex)
                            {
                                throw new FailedException(ex.Message);
                            }
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Hủy đơn thuốc
        /// </summary>
        /// <param name="actResult"></param>
        protected override void AfterAction(ActResult actResult)
        {            
            var successList = new List<string>();
            _cfEntities.ForEach(p =>
            {
                try
                {
                    int ret = _mzCfRepo.DeleteCf(p.cfh, p.OrganizeId);
                    if (ret > 0)
                    {                        
                        successList.Add(p.cfh);
                    }
                }
                catch (Exception ex)
                {
                    actResult.IsSucceed = false;
                    LogCore.Error("ClinicTfOperateApp UpdateCfByTf Error", ex, addInfo: Tags);
                    actResult.ResultMsg = string.Format("{0} ; 处方号为[{1}]发生内部异常,请尽快联系接口管理员", actResult.ResultMsg, p.cfh);
                    actResult.ResultCode = (int)ResultCode.AllFailure;
                }
            });
            actResult.Data = successList;
        }

        /// <summary>
        /// Lắp ráp dữ liệu đơn thuốc hoàn phí
        /// </summary>
        /// <returns></returns>
        private void AssembleTfCfEntity()
        {
            _cfEntities = new List<MzCfEntity>();
            Request.cfhs.ForEach(p =>
            {
                _cfEntities.Add(new MzCfEntity
                {
                    cfh = p.cfh,
                    OrganizeId = p.OrganizeId,
                    zt = "0"
                });
            });
        }

        /// <summary>
        /// Phán đoán xem đơn thuốc đó đã xếp thuốc hay chưa
        /// </summary>
        /// <param name="cfh"></param>
        /// <returns>true:Đã xếp thuốc;false:Chưa xếp thuốc</returns>
        private bool IsPrescribeYp(string cfh, string OrganizeId)
        {
            var entity = _mzCfRepo.FindEntity(p => p.cfh == cfh &&
                                                   p.OrganizeId == OrganizeId &&
                                                   p.zt == "1" &&
                                                   p.fybz == "1");
            return entity != null && entity.Id > 0;
        }
    }
}
