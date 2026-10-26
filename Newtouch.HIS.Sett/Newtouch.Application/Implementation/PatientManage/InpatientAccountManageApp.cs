using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface.PatientManage;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.PatientManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IRepository.PatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Newtouch.HIS.Application.Implementation.PatientManage
{
    public class InpatientAccountManageApp : AppBase, IInpatientAccountManageApp
    {

        private readonly IInpatientAccountRepo _inpatientAccountRepo;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly IInpatientReserveDmnService _InpatientReserveDmnService;
        private readonly IFinanceReceiptRepo _finRecRepos; //Số chứng từ biên lai bản ghi 
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysPatientBasicInfoRepo _sysPatientBasicInfoRepo;
        private readonly IPatientBasicInfoDmnService _PatientBasicInfoDmnService;

        /// <summary>
        /// Tra cứu bệnh nhân cơ bản thông tin và tài khoản thông tin
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public InpatientReserveDto GetHosPatAccInfo(string zyh)
        {
            if (string.IsNullOrEmpty(zyh.ToString()))
            {
                throw new FailedException("病人内码不存在");
            }
            //Tại cơ bản thông tin bảng trong tra cứu
            var e = _hospPatientBasicInfoRepo.FindEntity(p =>
                 p.zyh == zyh && p.OrganizeId == OrganizeId && p.zt == "1");
            if (e == null)
            {
                throw new FailedException("病人不存在");
            }
            var patInfo = e.MapperTo<HospPatientBasicInfoEntity, InpatientReservePatientInfoDto>();
            var zhentity = _inpatientAccountRepo.FindEntity(p =>
               p.zyh == zyh && p.OrganizeId == OrganizeId && p.zt == "1");
            if (zhentity == null)
            {
                throw new FailedException("缺少账户");
            }
            var hospatDto = new InpatientReserveDto();
            patInfo.zhCode = zhentity.zhCode;
            hospatDto.patInfo = patInfo;
            hospatDto.accPayInfoList = GetPayList(zhentity.zhCode, null);
            return hospatDto;
        }



        /// <summary>
        /// Tra cứu bệnh nhân cơ bản thông tin và tài khoản thông tin
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public InpatientReserveDto GetHosPatAccInfo(string zyh, string zhxz)
        {
            if (string.IsNullOrEmpty(zyh))
            {
                throw new FailedException("住院号不存在");
            }
            //Tại cơ bản thông tin bảng trong tra cứu
            //OutpatAccInfoDto e = _patientBasicInfoDmnService.GetPatInfoByPatid(patid, OrganizeId);

            var e = _hospPatientBasicInfoRepo.FindEntity(p =>
                 p.zyh == zyh && p.OrganizeId == OrganizeId && p.zt == "1");
            if (e == null)
            {
                throw new FailedException("病人不存在");
            }

            int? xz = null;
            if (!string.IsNullOrWhiteSpace(zhxz))
            {
                xz = Convert.ToInt16(zhxz);
            }
            //var patInfo = e.MapperTo<OutpatAccInfoDto, ReservePatientInfoDto>();
            var patInfo = e.MapperTo<HospPatientBasicInfoEntity,InpatientReservePatientInfoDto>();
            var xtbrjbxx=_sysPatientBasicInfoRepo.FindEntity(p =>
                p.patid == patInfo.patid && p.OrganizeId == OrganizeId && p.zt == "1");
            var zhentity = _inpatientAccountRepo.FindEntity(p =>
                p.zyh == zyh && p.zhxz == xz && p.OrganizeId == OrganizeId && p.zt == "1");
            if (zhentity == null)
            {
                throw new FailedException("缺少账户");
            }
            var hospatDto = new InpatientReserveDto();
            patInfo.zhCode = zhentity.zhCode;
            patInfo.zhxz = zhentity.zhxz;
            patInfo.phone=xtbrjbxx.phone;
            hospatDto.patInfo = patInfo;
            hospatDto.accPayInfoList = GetPayList(zhentity.zhCode, zhxz);
            return hospatDto;
        }
        

        /// <summary>
        /// Lấy bệnh nhânthu chi tài khoản bản ghi 
        /// </summary>
        /// <returns></returns>
        public List<InpatientPatAccPayVO> GetPayList(int zh, string zhxz)
        {
            List<InpatientPatAccPayVO> hospayList = _InpatientReserveDmnService.GetAccPayInfo(zh, this.OrganizeId, zhxz);
            foreach (InpatientPatAccPayVO item in hospayList)
            {
                item.szxzmc = ((EnumSZXZ)item.szxz).GetDescription();
            }
            return hospayList;
        }


        /// <summary>
        /// Lấy số chứng từ
        /// </summary>
        //public string GetFinRepSJPZH(out FinanceReceiptEntity outsjhEntity, out string outsjhType)
        //{
        //    FinanceReceiptEntity outEntity = null;
        //    string type = string.Empty;
        //    string sjh = _finRecRepos.getDQSJHNew(UserIdentity.UserCode, this.OrganizeId, out  outEntity, out  type);

        //    outsjhEntity = outEntity;
        //    outsjhType = type;
        //    if (!string.IsNullOrEmpty(sjh))
        //    {
        //        return sjh;
        //    }
        //    return sjh;
        //}

        /// <summary>
        /// Lấy số chứng từ
        /// </summary>
        public string GetFinRepSJPZH()
        {
            FinanceReceiptEntity outEntity = null;
            string type = string.Empty;
            string sjh = _finRecRepos.getDQSJHForpzh(UserIdentity.UserCode, this.OrganizeId);
            
            if (!string.IsNullOrEmpty(sjh))
            {
                return sjh;
            }
            return sjh;
        }



        #region 住院账户操作
        /// <summary>
        /// Nạp tiền
        /// </summary>
        /// <param name="zffsbh"></param>
        /// <param name="zffsmc"></param>
        /// <param name="zfje"></param>
        public bool PayDepositPost(DeposDto dto, out string szid)
        {

            if (string.IsNullOrEmpty(dto.patid.ToString())|| string.IsNullOrEmpty(dto.zyh.ToString()))
            {
                throw new FailedException("病人基本信息不可为空!");
            }

            if (string.IsNullOrEmpty(dto.zffsbh.ToString()) || string.IsNullOrEmpty(dto.zffsmc.ToString()))
            {
                throw new FailedException("充值支付方式不能为空");
            }
            if (string.IsNullOrEmpty(dto.zfje.ToString()) || dto.zfje <= 0)
            {
                throw new FailedException("请输入正确的充值金额(不可低于0元)");
            }
            if (string.IsNullOrEmpty(dto.pzh))
            {
                throw new FailedException("凭证号不能为空");
            }

            InpatientAccountRevenueAndExpenseEntity zhszjl = null;
            InpatientAccountEntity oldzh = null;
            InpatientAccountEntity newzh = null;
            FinanceReceiptEntity oldcwsj = null;
            FinanceReceiptEntity newcwsj = null;

            AccPayDeposit(dto, out zhszjl, out oldzh, out newzh); //Thêm bản ghi thu chi

            //Lấy原来của chứng từ biên lai Entity 
            oldcwsj = _finRecRepos.getSJKEntity(UserIdentity.UserCode, this.OrganizeId);
            _finRecRepos.DetacheEntity(oldcwsj);

            // mới của chứng từ biên lai thông tin
            FinanceReceiptEntity sjEntity = null;
            string type = string.Empty;
            //var pzh =GetFinRepSJPZH(out sjEntity, out type);
            var pzh = GetFinRepSJPZH();
            CheckSJPZH(pzh, out sjEntity, out type);
            //CheckSJPZH(dto.pzh, out sjEntity, out type);
            newcwsj = sjEntity;

            var sqlVO = new PatZyAccDataVO()
            {
                szjl = new InpatientAccountRevenueAndExpenseEntity(),
                oldzh = new InpatientAccountEntity(),
                oldcwsj = new FinanceReceiptEntity()
            };
            sqlVO.szjl = zhszjl;
            sqlVO.oldzh = oldzh;
            sqlVO.oldcwsj = oldcwsj;
            //type giá trị mặc đ ịnh
            //type = type == "" ? "update" : type;
            //Lưu vào cơ sở dữ liệu
            _InpatientReserveDmnService.PatZyAccDBProc(sqlVO, newzh, newcwsj, type);
            szid = zhszjl.Id;
            //实giờ cập nhật số dư tài khoản
            _PatientBasicInfoDmnService.updateZybrxxkExpandZhye(dto.zyh, OrganizeId, newzh.zhye);
            return true;
        }

        /// <summary>
        /// Thêm thanh toán, hoàn tiền bản ghi 
        /// </summary>
        /// <param name="dto"></param>
        private void AccPayDeposit(DeposDto dto, out InpatientAccountRevenueAndExpenseEntity payEntity, out InpatientAccountEntity oldAccEntity, out InpatientAccountEntity newAccEntity)
        {

            payEntity = new InpatientAccountRevenueAndExpenseEntity();
            payEntity.szxz = dto.szxz;
            //payEntity.pzh = dto.pzh;
            payEntity.pzh = dto.pzh;
            payEntity.zhCode = dto.zh;
            payEntity.zyh = dto.zyh;
            payEntity.szje =dto.szxz==2? (-1)*System.Math.Abs(dto.zfje) : dto.zfje; // Hoàn tiền: số âm 
            payEntity.xjzffs = dto.zffsbh.ToString();
            payEntity.zhye = payEntity.szje + GetzhYe(dto.zyh);
            payEntity.memo = dto.memo;
            payEntity.zt = "1";
            payEntity.OrganizeId = OrganizeId;
            payEntity.outTradeNo = dto.outTradeNo;
            ////Lấy số chứng từs
            //// phán đoán 静态tham số cấu hình
            payEntity.pzh = GetFinRepSJPZH();
            payEntity.Create(true);
            //Cập nhậtTài khoảnThông tin
            oldAccEntity = _inpatientAccountRepo.FindEntity(p => p.zyh == dto.zyh && p.OrganizeId == OrganizeId && p.zt == "1");
            _inpatientAccountRepo.DetacheEntity(oldAccEntity);
            newAccEntity = oldAccEntity.Clone();
            newAccEntity.zhye = payEntity.zhye;
        }
        /// <summary>
        /// nhận đ ược nhập viện số dư tài khoản
        /// </summary>
        public decimal GetzhYe(string zyh)
        {
            var zyzhEntity = _inpatientAccountRepo.FindEntity(p => p.zyh == zyh && p.OrganizeId == OrganizeId && p.zt == "1");
            if (zyzhEntity == null)
            {
                throw new FailedException("病人本次住院尚未开通账户");
            }
            return zyzhEntity.zhye;
        }
        /// <summary>
        /// 取 khoản 
        /// </summary>
        /// <param name="zffsbh"></param>
        /// <param name="zffsmc"></param>
        /// <param name="zfje"></param>
        public bool ReturnDepositPost(DeposDto dto)
        {
            if (string.IsNullOrEmpty(dto.patid.ToString()) || string.IsNullOrEmpty(dto.zyh))
            {
                throw new FailedException("患者信息不可为空");
            }
            if (string.IsNullOrEmpty(dto.zffsbh.ToString()) || string.IsNullOrEmpty(dto.zffsmc.ToString()))
            {
                throw new FailedException("支付方式不能为空");
            }
            //if (string.IsNullOrEmpty(dto.zfje.ToString()) || dto.zfje >= 0)
            //{
            // throw new FailedException("Vui lòng nhập chính xác của取 khoản số tiền(số tiền须là负 giá trị)");
            //}
            if (string.IsNullOrEmpty(dto.pzh))
            {
                throw new FailedException("凭证号不能为空");
            }

            InpatientAccountRevenueAndExpenseEntity zhszjl = null;
            InpatientAccountEntity oldzh = null;
            InpatientAccountEntity newzh = null;
            FinanceReceiptEntity oldcwsj = null;
            FinanceReceiptEntity newcwsj = null;

            AccPayDeposit(dto, out zhszjl, out oldzh, out newzh); //Thêm bản ghi thu chi 负của

            // mới của chứng từ biên lai thông tin
            FinanceReceiptEntity sjEntity = null;
            string type = string.Empty;

            //Tạm ứng hoàn tiền có hay không sử dụng凭证 số 
            var isTopzh = _sysConfigRepo.GetBoolValueByCode("Account_Refund_pzh", OrganizeId);

            if (isTopzh is true)
            {
                var pzh = GetFinRepSJPZH();
                CheckSJPZH(pzh, out sjEntity, out type);
                newcwsj = sjEntity;
            }
            else {
                zhszjl.pzh = "";
            }

            var sqlVO = new PatZyAccDataVO()
            {
                szjl = new InpatientAccountRevenueAndExpenseEntity(),
                oldzh = new InpatientAccountEntity(),
                oldcwsj = new FinanceReceiptEntity()
            };
            sqlVO.szjl = zhszjl;
            sqlVO.oldzh = oldzh;
            sqlVO.oldcwsj = oldcwsj;
            //Lưu vào cơ sở dữ liệu
            _InpatientReserveDmnService.PatZyAccDBProc(sqlVO, newzh, newcwsj, type);
            //实giờ cập nhật số dư tài khoản
            _PatientBasicInfoDmnService.updateZybrxxkExpandZhye(dto.zyh, OrganizeId, newzh.zhye);
            return true;
        }

        /// <summary>
        /// số dư hoàn toàn bộ
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool RefundAccount(DeposDto dto)
        {

            if (string.IsNullOrEmpty(dto.patid.ToString())||string.IsNullOrWhiteSpace(dto.zyh))
            {
                throw new FailedException("患者信息不可为空");
            }
            if (string.IsNullOrEmpty(dto.zffsbh.ToString()) || string.IsNullOrEmpty(dto.zffsmc.ToString()))
            {
                throw new FailedException("支付方式不能为空");
            }
            //if (string.IsNullOrEmpty(dto.zfje.ToString()) || dto.zfje >= 0)
            //{
            // throw new FailedException("Không có có thể trả lại số dư ");
            //}

            InpatientAccountRevenueAndExpenseEntity payEntity = null;
            InpatientAccountEntity oldAccEntity = null;
            InpatientAccountEntity newAccEntity = null;

            payEntity = new InpatientAccountRevenueAndExpenseEntity();
            payEntity.szxz = dto.szxz;
            //payEntity.pzh = pzhList;
            payEntity.zhCode = dto.zh;
            payEntity.zyh = dto.zyh;
            payEntity.szje = dto.zfje;
            payEntity.xjzffs = dto.zffsbh.ToString();
            payEntity.zhye = payEntity.szje + GetzhYe(dto.zyh);
            payEntity.zt = "1";
            payEntity.OrganizeId = OrganizeId;
            //payEntity.pzh = GetFinRepSJPZH();//Lấy số chứng từ
            payEntity.Create(true);
            //Cập nhậtTài khoảnThông tin
            oldAccEntity = _inpatientAccountRepo.FindEntity(p => p.zyh == dto.zyh && p.OrganizeId == OrganizeId && p.zt == "1");
            _inpatientAccountRepo.DetacheEntity(oldAccEntity);
            newAccEntity = oldAccEntity.Clone();
            newAccEntity.zhye = payEntity.zhye;
            var sqlVO = new PatZyAccDataVO()
            {
                szjl = new InpatientAccountRevenueAndExpenseEntity(),
                oldzh = new InpatientAccountEntity(),
                oldcwsj = new FinanceReceiptEntity()
            };
            sqlVO.szjl = payEntity;
            sqlVO.oldzh = oldAccEntity;
            sqlVO.oldcwsj = null;
            //Lưu vào cơ sở dữ liệu
            _InpatientReserveDmnService.PatZyAccDBProc(sqlVO, newAccEntity, null, "");
            //实giờ cập nhật số dư tài khoản
            _PatientBasicInfoDmnService.updateZybrxxkExpandZhye(dto.zyh, OrganizeId, newAccEntity.zhye);
            return true;
        }

        #endregion

        #region 发票相关

        /// <summary>
        /// sau máy xác minh số chứng từ biên lai
        /// </summary>
        /// <returns></returns> 
        public bool CheckSJPZH(string pzh, out FinanceReceiptEntity sjEntity, out string type)
        {
            bool flag = false;
            sjEntity = new FinanceReceiptEntity();
            type = string.Empty;

            flag = _finRecRepos.checkSJH(pzh, UserIdentity.UserCode, out sjEntity, out type, this.OrganizeId);
            return flag;
        }
        #endregion
    }
}
