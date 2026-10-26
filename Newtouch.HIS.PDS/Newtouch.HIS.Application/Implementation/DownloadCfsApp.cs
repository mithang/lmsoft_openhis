using System;
using System.Collections.Generic;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DTO.OutPatientPharmacy;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Tải xuống đơn thuốc
    /// </summary>
    public class DownloadCfsApp : ProcessorFun<List<OutPatientpyCfListDTO>>
    {
        private List<MzCfEntity> _cfEntities;
        private readonly IMzCfRepo _mzCfRepo;

        public DownloadCfsApp(List<OutPatientpyCfListDTO> request) : base(request)
        {
        }

        /// <summary>
        /// Xác thực
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request == null || Request.Count <= 0) throw new FailedException("处方列表为空");
            return new ActResult();
        }

        /// <summary>
        /// Xử lý chính
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            AssembleMzCfEntityEntities();
            if (_cfEntities == null || _cfEntities.Count <= 0) return;
            _mzCfRepo.DeleteCf(_cfEntities[0].cfh, _cfEntities[0].cfnm);
            if (_mzCfRepo.Insert(_cfEntities) > 0)
            {
                actResult.Data = _cfEntities;
            }
            else
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = "保存处方信息失败";
            }
        }

        /// <summary>
        /// Lắp ráp thông tin đơn thuốc
        /// </summary>
        private void AssembleMzCfEntityEntities()
        {
            _cfEntities = new List<MzCfEntity>();
            Request.ForEach(p =>
            {
                if (!IsExist(p))
                {
                    _cfEntities.Add(new MzCfEntity
                    {
                        lyyf = Constants.CurrentYfbm.yfbmCode,
                        brxzmc = p.brlxmc,
                        CardNo = p.kh,
                        cfh = p.cfh,
                        cfnm = p.pcfnm,
                        CreateTime = DateTime.Now,
                        CreatorCode = OperatorProvider.GetCurrent().UserCode,
                        Fph = p.Fph,
                        jsnm = p.sfxh,
                        ksmc = p.ksmc,
                        nl = p.nl,
                        sfsj = p.sfsj <= Constants.MinDateTime ? Constants.MinDateTime : p.sfsj,
                        OrganizeId = p.OrganizeId,
                        xm = p.xm,
                        ysmc = p.ys,
                        je = p.je,
                        fybz = ((int)EnumFybz.Wp).ToString(),
                        LastModifyTime = Constants.MinDateTime,
                        LastModiFierCode = "",
                        zt = "1"
                    });
                }
            });
        }

        /// <summary>
        /// Phán đoán xem đơn thuốc đó có tồn tại hay không
        /// </summary>
        /// <param name="outPatientpyCfListDto"></param>
        /// <returns></returns>
        private bool IsExist(OutPatientpyCfListDTO outPatientpyCfListDto)
        {
            var entity = _mzCfRepo.FindEntity(p => p.cfh == outPatientpyCfListDto.cfh &&
                                                   p.jsnm == outPatientpyCfListDto.sfxh &&
                                                   p.cfnm == outPatientpyCfListDto.pcfnm &&
                                                   p.zt == "1");
            return entity != null && entity.Id > 0;
        }
    }
}
