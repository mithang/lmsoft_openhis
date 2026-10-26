using System;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class BookkeepInpatientApp : AppBase, IBookkeepInpatientApp
    {
        private readonly IBookkeepInHosDmnService _bookkeepInHosDmnService;
        private readonly IHosPatAccDmnService _hosPatAccDmnService;
        private readonly IOutPatientDmnService _outPatientDmnService;
        private readonly IOutpatientAccountDetailRepo _outpatientAccountDetailRepo;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        #region 康复项目（药品）记账（由GRS收费项目（药品）扩展而来）

        /// <summary>
        /// theo số nhập viện lấy bệnh nhân ghi sổ thông tin
        /// </summary>
        /// <param name="zyh"></param>
        public HosAccountingPatStatusDetailDto GetAccountingStatusDetail(string zyh)
        {
            //Lấy thông tin bệnh nhân ghi sổ nhập viện(cơ bản thông tin, khoaV.v.)
            var patInfo = getHospAccountingPatInfo(zyh);

            //zyh có thể ghi sổ

            //Tra cứu này lần nhập viện Đ ã ghi sổ kế hoạch 
            var jzjhList = _bookkeepInHosDmnService.BeenAccountingPlanQuery(this.OrganizeId, zyh);

            //Tra cứu có hay không có quyết toán giữa chừng
            var ztjsrq = _bookkeepInHosDmnService.GetLastValidMidwaySettTime(zyh, this.OrganizeId);

            var resultDto = new HosAccountingPatStatusDetailDto()
            {
                patInfo = patInfo,
                jzjhList = jzjhList,
                zjrq= ztjsrq,
            };
            return resultDto;
        }

        /// <summary>
        /// GửiKế hoạch ghi sổ
        /// </summary>
        /// <param name="zyh">Số nhập viện</param>
        /// <param name="xmList">Ghi sổ hạng mục(thuốc)danh sách(nếu là đ ơn hạng mục(thuốc),vui lòng trư ớc đóng gói thành nhóm)</param>
        public void SubmitAccountingPlan(string zyh, IList<InpatientAccountingPlanItemDto> xmList)
        {
            if (xmList == null || xmList.Count == 0)
            {
                throw new FailedException("缺少记账计划");
            }

            //Lấykhi trư ớc nhập viện thông tin cơ bản bệnh nhân
            var zyInfo = _hospPatientBasicInfoRepo.IQueryable().FirstOrDefault(p => p.zt == "1" && p.zyh == zyh && p.OrganizeId == this.OrganizeId);

            //if (xmList.Any(p => (p.ysList == null || p.ysList.Count == 0) && p.yzxz == "1"))
            //{
            // throw new FailedException(" vui lòng cho thu phí chọn chuyên viên trị liệu");
            //}

            var jhmxEntityList = new List<HospAccountingPlanDetailEntity>();
            IList<zxGirdDto> itemList = new List<zxGirdDto>();
            var updateToStopJzjhxmIdList = new List<string>();

            // cùng một lần gửi
            var jzjhEntity = new HospAccountingPlanEntity()
            {
                jzjhId = Guid.NewGuid().ToString(),
                OrganizeId = this.OrganizeId,
                zyh = zyh,
            };
            jzjhEntity.Create(true);
            foreach (var item in xmList)
            {
                string ys = null;
                string ysmc = null;
                string ks = null;
                string ksmc = null;
                ys = zyInfo.doctor;
                ks = zyInfo.ks;

                int? zzll = item.sl * item.zll;//toàn bộghi sổ
                int dcsl = 0;// mỗi lần số lư ợng
                int zsl;
                if (item.sfxmCode != null)
                {
                    dcsl = CommmHelper.CalcSfxmSl(item.zll, item.dwjls, item.jjcl);
                    //tổng lư ợng 
                    zsl = (dcsl * item.sl).ToInt();
                }
                item.yzxz = (item.yzxz ?? "").Trim();   //


                //if (item.yzxz == ((int)EnumYZXZ.LSYZ).ToString())
                //{
                //    if (item.EndDate.HasValue && item.EndDate.Value != item.StartDate)
                //    {
                // //Bất thư ờng: tạm thời y lệnh, ngày là một phạm vi
                // throw new FailedException(" tạm thời ghi sổ ngày phạm vi lỗi");
                //    }
                //}
                //else
                //{
                //    if (item.EndDate.HasValue && item.EndDate.Value <= item.StartDate)
                //    {
                // //Bất thư ờng: y lệnh dài hạn, ngày không là một phạm vi
                // throw new FailedException(" dài hạn tínhghi sổ ngày phạm vi lỗi");
                //    }
                //}

                //Hạng mục thu phí
                //Thêm mới bản ghi 
                var jhmxEntity = new HospAccountingPlanDetailEntity()
                {
                    jzjhmxId = Guid.NewGuid().ToString(),
                    OrganizeId = this.OrganizeId,
                    zyh = zyh,
                    jzjhId = jzjhEntity.jzjhId,
                    yzxz = item.yzxz,
                    yzlx = "2",
                    StartDate = item.StartDate < new DateTime(2000, 01, 01) ? Constants.MaxDate : item.StartDate,
                    EndDate = Constants.MaxDate,
                    sfxmCode = item.sfxmCode,
                    duration = item.duration,
                    sl = dcsl,
                    LastEexcutionTime = null,
                    zxzt = (int)EnumJzjhZXZT.None,
                    ys = ys,
                    ysmc = ysmc,
                    ks = ks,
                    ksmc = ksmc,
                    bz = item.bz,
                    dj = item.dj,
                    kflb = item.kflb,
                    zcs = item.sl,
                    zll = item.zll,
                };
                jhmxEntity.Create(true);
                jhmxEntityList.Add(jhmxEntity);
                if (!string.IsNullOrWhiteSpace(item.jzjhmxId))
                {
                    // này là một dòng sửa lịch sử,cập nhậtbản ghi gốctrạng thái đến Đ ã dừng 
                    updateToStopJzjhxmIdList.Add(item.jzjhmxId);
                }
            }

            //đi lĩnh lĩnh vực dịch vụ lớp lưu trữ bền vững dữ liệu
            _bookkeepInHosDmnService.SaveAccountingPlan(jzjhEntity, jhmxEntityList, updateToStopJzjhxmIdList);
        }

        /// <summary>
        /// chờ thực hiện kế hoạch ghi sổ tra cứu
        /// </summary>
        /// <param name="zyhList"></param>
        /// <param name="zxrq"></param>
        public IList<HospAccountingPlanWaitingExeVO> WaitingAccountingPlanQuery(IList<string> zyhList, string from = null)
        {
            if (zyhList == null || zyhList.Count == 0)
            {
                return new List<HospAccountingPlanWaitingExeVO>();
            }
            var zyhStr = "";
            zyhList.ToList().ForEach(p =>
            {
                zyhStr += p + ",";
            });
            zyhStr = zyhStr.Trim(',');
            if (string.IsNullOrWhiteSpace(zyhStr))
            {
                return new List<HospAccountingPlanWaitingExeVO>();
            }
            var isRehabDoctor = _sysUserDmnService.CheckStaffIsBelongDuty(this.UserIdentity.StaffId, "RehabDoctor");
            if (from != "mz")
                return _bookkeepInHosDmnService.WaitingAccountingPlanQuery(OrganizeId, zyhStr, this.UserIdentity.rygh,
                    UserIdentity.DepartmentCode, isRehabDoctor);
            var retdata = _bookkeepInHosDmnService.WaitingOutAccountingPlanQuery(OrganizeId, zyhStr, this.UserIdentity.rygh, this.UserIdentity.DepartmentCode, isRehabDoctor);
            return retdata;
        }

        /// <summary>
        /// thực hiện kế hoạch ghi sổ,tạo tính phí 
        /// </summary>
        /// <param name="jzjhmxIdList">jzjhmxIdDanh sách</param>
        public void ExecuteAccountingPlan(IList<zxGirdDto> zxItem, DateTime? zxrq, string from = null)
        {
            if (zxItem == null || zxItem.Count() == 0)
            {
                throw new FailedException("缺少记账计划参数");
            }
            if (!zxrq.HasValue)
            {
                throw new FailedException("未指定执行日期");
            }
            if (from != null && from == "mz")
            {
                _bookkeepInHosDmnService.ExecuteOutAccountingPlan(this.OrganizeId, zxItem, this.UserIdentity.UserCode, this.UserIdentity.DepartmentCode, zxrq.Value);
                //Cập nhật cần dừng của kế hoạch ghi sổ
                zxItem.ToList().ForEach(p =>
                {
                    if (_bookkeepInHosDmnService.StopOutpatientjzjh(p.jzjhmxId, OrganizeId))
                    {
                        _outPatientDmnService.overAccountingPlan(p.jzjhmxId, OrganizeId, UserIdentity.UserCode);
                    }
                });
            }
            else
            {
                Dictionary<string, string> jfbStr = new Dictionary<string, string>();
                _bookkeepInHosDmnService.ExecuteAccountingPlan(this.OrganizeId, zxItem, this.UserIdentity.UserCode, this.UserIdentity.DepartmentCode, zxrq.Value, out jfbStr);
                //quét một dư ới kế hoạch CủaĐ ã thực hiện số lần, thực hiện trạng thái
                _bookkeepInHosDmnService.updateHospitaljzjh(OrganizeId, zxItem.Select(p => p.jzjhmxId).ToList());
                //Đ ã thực hiện của hạng mục phục h ồiĐ ồng bộ đ ến Tần Hoàng Đ ảo kho trung gian Tần Hoàng Đ ảo giai đoạn 3 chức năng jfbStr mớicó giá trị,skd_SyncInpatientToInterfaceBasic gọi này tồn tại lưu trữ quá trình trư ớc cóphán đoán trống,GRS sẽ không gọi.
                //_bookkeepInHosDmnService.SyncInpatientToInterfaceBasic(jfbStr, OrganizeId, zxrq, UserIdentity.rygh);
            }
        }

        /// <summary>
        /// dừng kế hoạch ghi sổ
        /// </summary>
        /// <param name="jzjhmxIdList">jzjhmxIdDanh sách</param>
        public void StopAccountingPlan(string jzjhmxIdStr, DateTime stopDate)
        {
            var mxIdList = jzjhmxIdStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (mxIdList == null || mxIdList.Length == 0)
            {
                throw new FailedException("缺少记账计划参数");
            }
            _bookkeepInHosDmnService.StopAccountingPlan(mxIdList.ToList(), stopDate, this.UserIdentity.DepartmentCode, this.UserIdentity.UserCode);
        }

        /// <summary>
        /// Hủydự kiến dừng 
        /// </summary>
        /// <param name="jzjhmxIdStr"></param>
        public void CancelPreStopAccountingPlan(string jzjhmxIdStr)
        {
            var mxIdList = jzjhmxIdStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (mxIdList == null || mxIdList.Length == 0)
            {
                throw new FailedException("缺少记账计划参数");
            }
            _bookkeepInHosDmnService.CancelPreStopAccountingPlan(mxIdList.ToList(), this.UserIdentity.DepartmentCode, this.UserIdentity.UserCode);
        }


        #region 住院记账

        /// <summary>
        /// theo số nhập viện lấy bệnh nhân ghi sổ thông tin
        /// </summary>
        /// <param name="zyh"></param>
        public InpatientHosAccountingPatStatusDetailDto GetInpatientAccountingStatusDetail(string zyh)
        {
            //Lấy thông tin bệnh nhân ghi sổ nhập viện(cơ bản thông tin, khoaV.v.)
            var patInfo = getHospAccountingPatInfo(zyh);

            var resultDto = new InpatientHosAccountingPatStatusDetailDto()
            {
                patInfo = patInfo,
            };

            return resultDto;
        }

        #endregion

        #region private methods

        /// <summary>
        /// Ghi sổ nhập viện lấy thông tin cơ bản bệnh nhân(cơ bản thông tin, khoaV.v.) xác minh trạng thái đang nằm viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        private HospPatientInfoVO getHospAccountingPatInfo(string zyh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            var patinfoList = _hosPatAccDmnService.GetHospPatientInfo(zyh, this.OrganizeId);
            if (patinfoList == null || patinfoList.Count == 0)
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (patinfoList.Count > 1)
            {
                throw new FailedCodeException("HOSP_MATCHED_ERROR_MULTI_MATCHED");
            }
            var patinfo = patinfoList.First();

            if (patinfo.zybz == ((int)EnumZYBZ.Wry).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_WRY_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }
            else if (patinfo.zybz == ((int)EnumZYBZ.Ycy).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_YCY_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }
            else if (patinfo.patid == 0)
            {
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (string.IsNullOrWhiteSpace(patinfo.ksmc))
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_DEPT_IS_NO_FOUND");
            }
            return patinfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ysList"></param>
        private void getYsKsInfo(IList<InpatientAccountingPlanItemDoctorDto> ysList
            , ref string ys, ref string ysmc, ref string ks, ref string ksmc)
        {
            ys = ysmc = ks = ksmc = "";
            foreach (var ysItem in ysList)
            {
                if (string.IsNullOrWhiteSpace(ysItem.gh)
                    || string.IsNullOrWhiteSpace(ysItem.Name)
                    || string.IsNullOrWhiteSpace(ysItem.ks)
                    || string.IsNullOrWhiteSpace(ysItem.ksmc))
                {
                    throw new FailedException("治疗师不明确");
                }
                ys += ysItem.gh + ",";
                ysmc += ysItem.Name + ",";
                ks += ysItem.ks + ",";
                ksmc += ysItem.ksmc + ",";
            }
            ys = ys.Trim(',');
            ysmc = ysmc.Trim(',');
            ks = ks.Trim(',');
            ksmc = ksmc.Trim(',');
        }

        #endregion

        #endregion

    }
}
