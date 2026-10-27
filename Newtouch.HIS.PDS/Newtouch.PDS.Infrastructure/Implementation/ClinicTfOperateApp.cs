using Newtouch.PDS.Requset.ResourcesOperate;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Exceptions;
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
        /// Kiểm tra
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request == null) throw new FailedException("Bản tin yêu cầu không được để trống");
            if (Request.cfhs == null || Request.cfhs.Count <= 0) throw new FailedException("Danh sách đơn thuốc không được để trống");
            Request.cfhs.ForEach(ValidataYzDetail);
            return new ActResult();
        }

        /// <summary>
        /// Chi tiết y lệnh xét nghiệm
        /// </summary>
        /// <param name="detail"></param>
        private static void ValidataYzDetail(CfDetial CfDetial)
        {
            if (string.IsNullOrWhiteSpace(CfDetial.cfh)) throw new FailedException("Vui lòng truyền số đơn thuốc hiệu lực");
            if (string.IsNullOrWhiteSpace(CfDetial.OrganizeId)) throw new FailedException("Vui lòng truyền mã tổ chức hiệu lực");
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
                    //Trước khi trả phí，Kiểm tra trước đã xếp thuốc chưa
                    if (IsPrescribeYp(item.cfh, item.OrganizeId))
                    {
                        List<MzCfmxEntity> Cfmx = mzCfmxRepo.GetCfmxByCfh(item.cfh);
                        //Trường hợp đã xếp thuốc，Trả lại đóng băng trước
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
                    actResult.ResultMsg = string.Format("{0} ; Số đơn thuốc là[{1}]Xảy ra ngoại lệ nội bộ,Vui lòng liên hệ quản trị viên giao diện càng sớm càng tốt", actResult.ResultMsg, p.cfh);
                    actResult.ResultCode = (int)ResultCode.AllFailure;
                }
            });
            actResult.Data = successList;
        }

        /// <summary>
        /// Lắp ráp dữ liệu đơn trả phí
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
        /// Kiểm tra đơn thuốc này đã xếp thuốc chưa
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
