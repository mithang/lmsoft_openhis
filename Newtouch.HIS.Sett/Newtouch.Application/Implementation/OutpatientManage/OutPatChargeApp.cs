/**********************************************************/
// Copyright (C) 2016 Newtouch Giữ bản quyền. 
// Description:Đ ăng ký khám ngoại trú thu phí liên quan nghiệp vụ 
// Author：HLF
// CreateDate： 2016/12/23 13:42:32 
//**********************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Proxy.guian.DTO.S25;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using newtouchyibao;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IRepository.PatientManage;
using Newtouch.HIS.Domain.Entity.PatientManage;
using System.Data.SqlClient;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class OutPatChargeApp : AppBase, IOutPatChargeApp
    {
        //GetOutPatRegEntity
        private readonly IOutpatientRegistRepo _outPatRegRepo; //Đ ăng ký khám ngoại trú仓储
        private readonly ISysConfigRepo _sysConfigRepo; //Hệ thống ngoại trú cấu hình
        private readonly ISysPatientChargeWaiverRepo _sysPatiChargeWaiRepo; // Số tiền giảm trừ
        private readonly IOutPatientDmnService _outPatientDmnService; //Số hóa đ ơn
        private readonly ISysPatientNatureRepo _sysNatRepo; //Hệ thốngTính chất bệnh nhân
        private readonly ISysPatientBasicInfoRepo _sysPatiInfoRepo; //Thông tin cơ bản bệnh nhân
        private readonly IBookkeepInHosDmnService _hosTempRepo; //Thu phí模版
        private readonly IOutPatChargeDmnService _outChargeDmnService;
	    private readonly IPatientBasicInfoDmnService _patientBasicInfoDmnService;
        private readonly IOutPatientSettleDmnService _outPatientSettleDmnService;
        private readonly ISysCardRepo _sysCardRepo;

        #region 门诊收费2018
        private readonly IOutPatientUniversalDmnService _outPatientUniversalDmnService;
        private readonly IOutpatientPrescriptionRepo _outpatientPrescriptionRepo;
        #endregion

	    private readonly ICqybUploadInPres04Repo _cqybUploadInPres04Repo;
		/// <summary>
		/// theo số thẻ hoặc số hồ sơ bệnh án lấy thông tin bệnh nhân
		/// </summary>
		/// <param name="kh">Số thẻ</param>
		/// <param name="brxz">Tính chất bệnh nhân</param>
		/// <returns></returns>
		public OutPatChargeInfoVO GetOutPatChargeInfo(string kh, string brxz)
        {
            OutPatChargeInfoVO outchargeInfoVo;
            if (string.IsNullOrEmpty(kh))
            {
                throw new FailedCodeException("OUTPAT_CARDNO_ISINVALID"); //Số thẻ không hiệu lực
            }
            var _kh = kh.Trim().ToUpper();
            //初复诊 phán đoán số thẻ có hiệu lực hay không
            var regEntity = _outPatRegRepo.SelectCFZ(_kh, OrganizeId);
            if (regEntity == null)
            {
                throw new FailedCodeException("OUTPAT_CARDNO_ISINVALID");
            }

            //Tính chất bệnh nhân truyền 入 không là trống,说明 nhiều tính chất bệnh nh ânĐ ãchọn một 作là tham số truyền 入
            if (!string.IsNullOrEmpty(brxz))
            {
                regEntity.brxz = brxz;

                //Cấu hình hệ thống
                var confEntity = _sysConfigRepo.GetByCode(Constants.xtmzpz.BRXZ_YBJC, OperatorProvider.GetCurrent().OrganizeId);
                if (confEntity != null && confEntity.Value.Contains(regEntity.brxz))
                {
                    throw new FailedCodeException("OUTPAT_BEDPATIENT_CHARGE");
                }
                //LấyĐ ăng ký khámthu phí thông tin cơ bản bệnh nhân
                outchargeInfoVo = _outChargeDmnService.GetChargePatInfo(kh, brxz);
                if (outchargeInfoVo == null)
                {
                    throw new FailedCodeException("OUTPAT_PATIENT_BASICINFO_IS_NOT_EXIST");
                    //初始化界面
                }
                if (outchargeInfoVo.dybh == ((int)EnumDY.bd))
                {
                    outchargeInfoVo.dy = EnumDY.bd.GetDescription();
                }
                if (outchargeInfoVo.dybh == ((int)EnumDY.wd))
                {
                    outchargeInfoVo.dy = EnumDY.wd.GetDescription();
                }
            }
            else
            {
                // theo thông tin đ ăng ký khám tra cứu tính chất bệnh nhân
                //Thông tin đ ăng ký khám
                var regEntityList = _outPatRegRepo.GetOutPatRegEntityList(_kh, this.OrganizeId);
                if (regEntityList.Count < 1 || string.IsNullOrEmpty(regEntityList[0].brxz))
                {
                    throw new FailedCodeException("OUTPAT_REGIST_ISINVALID");  //Thông tin đ ăng ký khám không hiệu lực
                }

                //Tính chất bệnh nhânDanh sách
                var propertyList = new List<OutPatChargeProperty>();
                if (regEntityList.Count > 1)
                {
                    //update by hao 2017-2-28 Th êmĐ ăng ký khám nhiều tính chất bệnh nhân của窗口chọn 
                    foreach (var t in regEntityList)
                    {
                        var property = new OutPatChargeProperty
                        {
                            brxz = t.brxz,
                        };
                        property.brxzmc = _sysNatRepo.SelectBrxzByBrxz(property.brxz, OrganizeId).brxzmc;
                        propertyList.Add(property);
                    }

                    for (var j = 0; j < propertyList.Count; j++)
                    {
                        for (var k = propertyList.Count - 1; k > j; k--)
                        {
                            if (propertyList[j].brxz == propertyList[k].brxz)
                            {
                                propertyList.RemoveAt(k);
                            }
                        }
                    }
                }

                outchargeInfoVo = new OutPatChargeInfoVO { brxzList = propertyList };

                if (regEntityList.Count != 1 && propertyList.Count != 1) return outchargeInfoVo;
                if (regEntityList.Count == 1)
                {
                    regEntity.brxz = regEntityList[0].brxz;
                }
                if (propertyList.Count == 1)
                {
                    regEntity.brxz = propertyList[0].brxz;
                }

                //Cấu hình hệ thống
                var confEntity = _sysConfigRepo.GetByCode(Constants.xtmzpz.BRXZ_YBJC, OperatorProvider.GetCurrent().OrganizeId);
                if (confEntity != null && confEntity.Value.Contains(regEntity.brxz))
                {
                    throw new FailedCodeException("OUTPAT_BEDPATIENT_CHARGE");
                }
                //LấyĐ ăng ký khámthu phí thông tin cơ bản bệnh nhân
                outchargeInfoVo = _outChargeDmnService.GetChargePatInfo(kh, regEntity.brxz);
                if (outchargeInfoVo == null)
                {
                    throw new FailedCodeException("OUTPAT_PATIENT_BASICINFO_IS_NOT_EXIST");
                    //初始化界面
                }
                if (outchargeInfoVo.dybh == ((int)EnumDY.bd))
                {
                    outchargeInfoVo.dy = EnumDY.bd.GetDescription();
                }
                if (outchargeInfoVo.dybh == ((int)EnumDY.wd))
                {
                    outchargeInfoVo.dy = EnumDY.wd.GetDescription();
                }
            }

            return outchargeInfoVo;
        }

        /// <summary>
        /// Lấy thông tin đ ăng ký khám
        /// </summary>
        /// <param name="patid">Bệnh nhânpatid</param>
        /// <param name="brxz"></param>
        /// <param name="docVo"></param>
        /// <returns></returns>
        public List<OutPatChargeItemVO> GetOutGhInfo(int patid, string brxz, out OutPatChargeDoctorVO docVo)
        {
            docVo = new OutPatChargeDoctorVO();
            var kssj = DateTime.Now;
            var startKssj = kssj.AddDays(-5).ToString("yyyy-MM-dd HH:mm:ss");
            var endKssj = kssj.ToString("yyyy-MM-dd HH:mm:ss");
            //var itemVo = _outChargeDmnService.getOutPatItem_ghlx(patid, brxz, startKssj, endKssj);
            var itemVo = _outChargeDmnService.GetOutPatGhlx(patid, brxz, startKssj, endKssj);
            if (itemVo != null && itemVo.Count > 0)
            {
                for (var i = 0; i < itemVo.Count; i++)
                {
                    if (itemVo[i].gh_ks == null)
                    {
                        itemVo.Remove(itemVo[i]);
                        i--;
                    }
                    else
                    {
                        itemVo[i].ghName = itemVo[i].gh_ks.Split('＞')[0];
                        itemVo[i].ksName = itemVo[i].gh_ks.Split('＞')[1];
                        // cửa cấp c ứuĐ ăng ký khám tại cấu hình hệ thống bảng có hiệu lực hay không
                        var isYx = _outChargeDmnService.getActiveDuration(itemVo[i].ks, itemVo[i].mjzbz, itemVo[i].CreateTime.ToString(CultureInfo.InvariantCulture).Trim());
                        if (isYx) continue;
                        itemVo.Remove(itemVo[i]);
                        i--;
                    }
                }
                if (itemVo.Count <= 0) return itemVo;
                // phán đoán tính chất bệnh nhân có hay không大病 trư ớc 略
                if (itemVo.Count != 1) return itemVo;
                var item = new OutPatChargeItemVO();

                if (itemVo[0].gh_ks == null)
                {
                    item.ghName = "";
                    item.ksName = "";
                }
                else
                {
                    item.ghName = itemVo[0].gh_ks.Split('＞')[0];
                    item.ksName = itemVo[0].gh_ks.Split('＞')[1];
                }
                item.ghnm = itemVo[0].ghnm;
                item.ksbh = itemVo[0].ksbh;
                item.ks = itemVo[0].ks;
                item.mjzbz = itemVo[0].mjzbz;
                item.ys = itemVo[0].ys;
                //Khi trư ớc là chuyên giaĐ ăng ký khám giờ,trực tiếp gán giá trị bác sĩ và thêm bản ghi 
                if (string.IsNullOrEmpty(item.ys)) return itemVo;
                var doctorVo = _outChargeDmnService.getDoctorInfo
                    (item.ghnm, "", 3, item.mjzbz, false, false);
                if (doctorVo != null)
                {
                    docVo = doctorVo;
                }
                else
                {
                    throw new FailedCodeException("OUTPAT_REGISTERED_DOCTORINFO_NOT_COMPLETE");
                }
            }
            else
            {
                throw new FailedCodeException("OUTPAT_REGISTERED_ABNORMAL");
            }
            return itemVo;
        }



        /// <summary>
        /// theo từ khóa lấy thuốc và hạng mục thu phí thông tin
        /// </summary>
        /// <param name="keyword">Tra cứuTừ khóa</param>
        /// <returns></returns>
        public List<ChargeItemDetailVO> GetYpItemInfo(string keyword)
        {
            //LấyĐ ịa phư ơng cấu hình của nhà thuốc mã
            var yfpz = Constants.CurrentYfbm.yfbmCode;
            if (string.IsNullOrEmpty(yfpz))
            {
                throw new FailedCodeException("OUTPAT_YP_YF_ISNULL"); //如需tra cứu sử dụng thuốc liên quan thông tin,vui lòng trư ớc cấu hình nhà thuốc ！
            }
            //Tra cứu về thuốc và hạng mục
            var itemVo = _outChargeDmnService.getYpItemList(keyword, yfpz, this.OrganizeId);
            return itemVo;
        }

        /// <summary>
        /// Lấy số đ ơn thuốc và附加费số tiền
        /// </summary>
        /// <param name="sfxmtype">Hạng mục thu phí loại</param>
        /// <param name="lscfh"> tạm thời số đ ơn thuốc</param>
        /// <param name="brxz">Tính chất bệnh nhân</param>
        /// <param name="dl"> loại lớn </param>
        /// <param name="sfxm">Hạng mục thu phí</param>
        /// <param name="dj">Đ ơn giá</param>
        /// <param name="sl"></param>
        /// <returns></returns>
        public OutPatChargeCFDto GetCfhAndFjfMoney(string sfxmtype, string lscfh, string brxz, string dl, string sfxm, string dj, string sl)
        {
            var dto = new OutPatChargeCFDto();

            if (sfxmtype == "1")//Hạng mục
            {
                dto.cfh = "";
            }
            if (sfxmtype == "2")//Thuốc
            {
                dto.cfh = !string.IsNullOrEmpty(lscfh) ? lscfh : _outChargeDmnService.getCflsh();
            }
            dto.fwf = _outChargeDmnService.Calcfwfjm(brxz, dl, sfxm, dj);//附 vụ số tiền
            var jeNum = (Convert.ToDouble(dj) + Convert.ToDouble(dto.fwf)) * Convert.ToDouble(sl);
            dto.jeNum = jeNum;

            return dto;
        }

        #region 处方号
        /// <summary>
        /// Lấy số đ ơn thuốc
        /// </summary>
        /// <param name="sfxmtype"></param>
        /// <returns></returns>
        public string GetCfh(string sfxmtype)
        {
            string cfh = "";
            if (sfxmtype == "1")//Hạng mục
            {
                cfh = "";
            }
            if (sfxmtype == "2")//Thuốc
            {
                cfh = _outChargeDmnService.getCflsh(); //Số thứ tự đ ơn thuốc
            }
            return cfh;
        }

        #endregion

        #region 判断同一药品是否一个处方 获取相同药品的组号 俩方法没有检测到引用，创建者如无需要，请删除 add by sunny

        /// <summary>
        /// 阻止 cùng một Đ ơn thuốc录入 giống nhau thuốc
        /// </summary>
        /// <param name="strSfxm"></param>
        /// <param name="strYf"></param>
        /// <param name="gridDto"></param>
        /// <param name="lsCfh"></param>
        /// <returns></returns>
        private string ReturnEqual(string strSfxm, string strYf, List<OutpatChargeDto> gridDto, string lsCfh)
        {
            string res = "";
            if (gridDto.Count > 0)
            {
                foreach (OutpatChargeDto dto in gridDto)
                {
                    if (!string.IsNullOrEmpty(dto.cfh))//Thuốc
                    {
                        if (!string.IsNullOrEmpty(lsCfh) && lsCfh.Equals(dto.cfh))
                        {
                            if (strYf.Equals(dto.yfdm))
                            {
                                if (strSfxm.Equals(dto.sfxm))
                                {
                                    res = "当前处方已存在所选药品，确认再添加吗？";
                                }
                            }
                            else
                            {
                                res = "error";
                            }
                        }
                    }
                    else
                    {
                        if (strSfxm.Equals(dto.sfxm))
                        {
                            res = "当前已存在所选项目，确认再添加吗？";
                        }
                    }
                }
            }
            return res;
        }


        /// <summary>
        /// Lấy giống nhau thuốc của nhóm số 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="strYf"></param>
        /// <param name="gridDto"></param>
        /// <param name="lsCfh"></param>
        /// <returns></returns>
        private string SetCzh(string code, string strYf, List<OutpatChargeDto> gridDto, string lsCfh)
        {
            string res = "";
            if (gridDto.Count > 0)
            {
                foreach (OutpatChargeDto dto in gridDto)
                {
                    if (!string.IsNullOrEmpty(dto.cfh))//Thuốc
                    {
                        if (!string.IsNullOrEmpty(lsCfh) && lsCfh.Equals(dto.cfh))
                        {
                            if (strYf.Equals(dto.yfdm))
                            {
                                if (code.Equals(dto.sfxm))
                                {
                                    if (res == "")
                                    {
                                        res = dto.czh;
                                    }
                                    else
                                    {
                                        res += "," + dto.czh;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return res;
        }
        #endregion

        #region 结算

        /// <summary>
        /// Ngoại trú thu phí quyết toán
        /// </summary>
        /// <param name="gridDto"> cần lưu, quyết toán CủaĐ ơn thuốcdanh sách</param>
        /// <param name="kh">Số thẻ</param>
        /// <param name="fph">Số hóa đ ơn</param>
        /// <param name="xjzffs">Phư ơng thức thanh toán tiền mặt</param>
        /// <param name="xjzffsbh"></param>
        /// <param name="ysk">Tạm thu</param>
        /// <param name="ssk">Số tiền thực thu</param>
        /// <param name="zl">Trả lại tiền thừa</param>
        /// <returns></returns>
        public SettSaveSuccessResultDto OutPatChargeSett(List<OutpatChargeDto> gridDto, string kh, string fph, string xjzffs, string xjzffsbh, decimal ysk, decimal ssk, decimal zl)
        {

            //Tính chất bệnh nhânEntity
            SysPatientNatureEntity brxzEntity;
            //Quyết toánEntity
            SettlementEntityVO jsEntity;
            //Quyết toán thực thể bảng tư ơng ứngmz_js
            OutpatientSettlementEntity mzjs;
            //Chi tiết quyết toán bảng tư ơng ứngmz_jsmx
            //Quyết toán loại lớn 
            List<OutpatientSettlementCategoryEntity> jsdlList;


            //Quyết toán phán đoán 
            SettSaveCheck(gridDto, fph, zl, out brxzEntity);

            //Dữ liệu kho thay đ ổi Đ ối tư ợng
            var sqlVo = GetOutPatChargeSettDbvo(gridDto, kh, fph, xjzffs, xjzffsbh, ysk, ssk, zl, out jsEntity);


            // nhận đ ược cần quyết toán của hạng mục thông tin, thống kê quyết toán loại lớn 
            _outChargeDmnService.GetMzJsInfo(jsEntity, brxzEntity, out mzjs, out jsdlList);
            //Thanh toán tiền mặt
            mzjs.xjzf = ssk;
            mzjs.zje = ssk;
            mzjs.xjzffs = xjzffs;
            mzjs.fph = fph;
            mzjs.ysk = ysk;
            mzjs.zl = zl;
            // mới 加 trư ờng add
            mzjs.xm = gridDto[0].xm; //Họ tên
            mzjs.xb = gridDto[0].xb;
            mzjs.blh = gridDto[0].blh;
            mzjs.csny = gridDto[0].csny;
            mzjs.zjh = gridDto[0].zjh;
            mzjs.zjlx = gridDto[0].zjlx;

            sqlVo.mz_js = mzjs;
            sqlVo.mz_jsdlList = jsdlList;

            //Lấy chi tiết quyết toán
            var jsmxList = _outChargeDmnService.getJSMX(mzjs, jsEntity);
            sqlVo.mz_jsmxList = jsmxList;

            //Phư ơng thức thanh toán quyết toán ngoại trú
            var settPayEntity = _outChargeDmnService.GetMzJsZffs(mzjs.jsnm, xjzffs, xjzffsbh, ysk);
            sqlVo.mz_jszffs = settPayEntity;


            //Lưu vào cơ sở dữ liệu
            _outChargeDmnService.OutPatChargeSettDB(sqlVo);

            //Quay lại ngoại trú thu phí quyết toán dữ liệu
            var reslt = new SettSaveSuccessResultDto()
            {
                yingshoukuan = sqlVo.mz_js.zje.ToString("0.00"),
                ssk = ssk.ToString("0.00"),
                srce = sqlVo.mz_js.xjwc.ToString("0.00"),
                zhaoling = (sqlVo.mz_js.zl ?? 0).ToString("0.00"),
            };

            return reslt;
        }

        /// <summary>
        /// Quyết toán phán đoán 
        /// </summary>
        /// <param name="gridDto"></param>
        /// <param name="fph"></param>
        /// <param name="zl"></param>
        /// <param name="brxzEntity"></param>
        private void SettSaveCheck(IReadOnlyList<OutpatChargeDto> gridDto, string fph, decimal zl, out SysPatientNatureEntity brxzEntity)
        {
            if (string.IsNullOrWhiteSpace(fph))
            {
                throw new FailedCodeException("HOSP_PLEASE_CHOOSE_FINO"); //Vui lòng chọn số hóa đ ơn
            }
            if (_outPatientDmnService.ExistsForInvoiceNo(fph, OrganizeId))
            {
                throw new FailedCodeException("HOSP_FINO_IS_USED"); //Số hóa đ ơnĐ ãsử dụng
            }
            //Trả lại tiền thừa
            if (zl < 0)
            {
                throw new FailedCodeException("HOSP_ZHAOLING_IS_NEGATIVE"); //Trả lại tiền thừa
            }

            //Xác minh một 些thông tin
            if (gridDto == null || gridDto.Count < 0)
            {
                throw new FailedCodeException("OUTPAT_BILLING_ITEM_CAN_NOT_BE_EMPTY"); // không có cần quyết toán hạng mục
            }
            if (gridDto[0].ghnm == 0)
            {
                throw new FailedCodeException("OUTPAT_THE_REGISTERED_MESSAGE_DOES_NOT__EXIST"); //Đ ăng ký khám mã nội bộ là trống 
            }

            // khác nhau tính chất bệnh nhân và cửa cấp cứu không thể cùng nhau quyết toán nhiều thông tin đ ăng ký khám cùng nhau quyết toán,hiện tại không tồn tại tại

            // theo Đ ăng ký khám mã nội bộ lấy thông tin đ ăng ký khám không dùng đ ến 


            //Lấy thông tin bệnh nhân theo mã nội bộ bệnh nhân
            var brjbxx = _sysPatiInfoRepo.GetInfoByPatid(gridDto[0].patid.ToString(), OrganizeId);
            if (brjbxx == null) throw new FailedCodeException("OUTPAT_PATIENT_BASICINFO_IS_NOT_EXIST"); // không tồn tại tại này thông tin bệnh nhân
            // theo tính chất bệnh nhân phán đoán 
            brxzEntity = _sysNatRepo.SelectBrxzByBrxzbh(gridDto[0].brxzbh, OrganizeId);
            if (brxzEntity == null)
            {
                throw new FailedCodeException("HOSP_PATIENT_NATURE_IS_NOT_EXIST"); // này tính chất bệnh nhân không tồn tại tại
            }

            if (brxzEntity.ybjylx == Constants.ybjylx.ybjylx6)
            {
                // BHYT nông thôn mới giao dịch 
            }
            else
            {
                //Tự trả và BHYT giao dịch 

            }
        }

        /// <summary>
        /// Lưu quyết toán thực thể gán giá trị 
        /// </summary>
        /// <param name="gridDto"> cần lưu, quyết toán CủaĐ ơn thuốcdanh sách</param>
        /// <param name="kh"></param>
        /// <param name="fph">Số hóa đ ơn</param>
        /// <param name="xjzffs">Phư ơng thức thanh toán tiền mặt</param>
        /// <param name="xjzffsbh"></param>
        /// <param name="ysk">Tạm thu</param>
        /// <param name="ssk">Số tiền thực thu</param>
        /// <param name="zl"></param>
        /// <param name="jsEntity"></param>
        /// <returns></returns>
        public OutPatChargeSettDataVo GetOutPatChargeSettDbvo(List<OutpatChargeDto> gridDto, string kh, string fph, string xjzffs, string xjzffsbh, decimal ysk, decimal ssk, decimal zl, out SettlementEntityVO jsEntity)
        {
            var sqlVo = new OutPatChargeSettDataVo()
            {
                mz_cf = new List<OutpatientPrescriptionEntity>(),
                mz_cfmxList = new List<OutpatientPrescriptionDetailEntity>(),
                mz_xmList = new List<OutpatientItemEntity>(),
                mz_js = new OutpatientSettlementEntity(),
                mz_jsmxList = new List<OutpatientSettlementDetailEntity>(),
                mz_jsdlList = new List<OutpatientSettlementCategoryEntity>(),
                mz_jszffs = new OutpatientSettlementPaymentModelEntity()
            };

            jsEntity = new SettlementEntityVO(); // tạm lưu thông tin quyết toán 
            var jsxmList = new List<SettleProjectVO>(); //Quyết toánHạng mụcList

            var strArray = new List<string>();  //Tạm lưu số đ ơn thuốc đ ã lưu
            var cFisExit = true; // Đ ơn thuốc mặc đ ịnh không tồn tại
            var cfnm = 0; //Mã nội bộ đ ơn thuốc
            const int ptcfnm = 0;
            var cfh = ""; // Khi trư ớc số đ ơn thuốc

            //Có hay kh ôngĐ ặc biệt dấu hiệu thuốc
            var istsbzS = false;
            var yxYbdm = false; //Hiệu lựcMã BHYT
            decimal outJmbl = 0;
            const decimal outJmblZhxz = 0;
            decimal outJmje = 0;
            const decimal outJmjeZhxz = 0;
            //循环 cần lưu CủaĐ ơn thuốc thuốc và hạng mục dữ liệu
            if (gridDto.Count > 0)
            {
                var gridindex = 0;
                var cfindex = 0;
                foreach (var dto in gridDto)
                {
                    #region 药品处方
                    if (dto.cfh != "" && dto.xmtype == "2")//Số đ ơn thuốc không là trống, tức là là thuốc
                    {

                        #region 药品
                        if (dto.IsZhbrxz)// kết hợp tính chất bệnh nhân
                        {
                            if (dto.zh_sbrxz == dto.brxz)
                            {
                                istsbzS = true;
                            }
                            if (istsbzS)//Đ ặc biệt dấu hiệu 
                            {
                                //BHYTgiảm trừ thuật toán 
                                _sysPatiChargeWaiRepo.Get_Calcjm(dto.zh_sbrxz, dto.dl, dto.sfxm, dto.je, out outJmbl, out outJmje, this.OrganizeId);
                            }
                            else
                            {
                                _sysPatiChargeWaiRepo.Get_Calcjm(dto.zh_mbrxz, dto.dl, dto.sfxm, dto.je, out outJmbl, out outJmje, OrganizeId);

                            }
                        }
                        else
                        {
                            if (dto.sfxm != null)
                            {
                                _sysPatiChargeWaiRepo.Get_Calcjm(dto.brxz, dto.dl, dto.sfxm, dto.je, out outJmbl, out outJmje, OrganizeId);
                            }
                        }

                        if (dto.isYbbr || dto.brxz == "38")
                        {
                            // tạm thời không 做xử lý
                        }
                        else
                        {
                            yxYbdm = true;
                        }

                        //Thêm thông tin bảng chính đ ơn thuốc
                        if (gridindex == 0 || cfindex == 0 || strArray.Count == 0)
                        {
                            cFisExit = true; //Đ ơn thuốc không tồn tại,thêm bảng chính đ ơn thuốc
                        }
                        //Thêm qua Đ ơn thuốc bản ghi 
                        if (strArray != null && strArray.Count > 0)
                        {
                            foreach (var t in strArray)
                            {
                                if (t == dto.cfh)
                                {
                                    //Cho biết đ ã tồn tại thông tin bảng chính của đ ơn thuốc này 
                                    cFisExit = false; // Đ ơn thuốc đ ã tồn tại
                                }
                            }
                        }

                        if (cFisExit)
                        {
                            //Thêm thông tin bảng chính đ ơn thuốc
                            strArray.Add(dto.cfh);

                            #region 添加处方
                            //Đ ơn thuốc ngoại trú
                            var mzcf = new OutpatientPrescriptionEntity
                            {
                                cfnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cf"),
                                ghnm = dto.ghnm,
                                patid = dto.patid,
                                brxz = !yxYbdm ? "00" : (dto.IsZhbrxz ? dto.zh_sbrxz : dto.brxz),
                                lyyf = Constants.CurrentYfbm.yfbmCode,
                                fybz = "0",
                                mjzbz = dto.mjzbz,
                                ys = dto.ys,
                                ks = dto.ks,
                                zje = ysk,
                                cfzt = "1",
                                jsnm = 0,
                                jsrq = DateTime.Now,
                                cfh = dto.cfh,
                                zt = "1",
                                OrganizeId = OperatorProvider.GetCurrent().OrganizeId
                            };
                            mzcf.Create();


                            cfnm = mzcf.cfnm;
                            cfh = mzcf.cfh;
                            #endregion

                            cfindex++;
                            sqlVo.mz_cf.Add(mzcf);
                        }

                        #endregion

                        #region 处方明细

                        var mzcfmx = new OutpatientPrescriptionDetailEntity
                        {
                            cfmxId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cfmx"),
                            cfnm = cfnm,
                            yp = dto.xm
                        };
                        //mzcfmx.cfmxId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyString("mz_cfmx");
                        mzcfmx.yp = dto.sfxm;
                        mzcfmx.dl = dto.dl;
                        mzcfmx.dj = dto.dj;
                        mzcfmx.sl = dto.sl;
                        mzcfmx.je = dto.je;
                        mzcfmx.zfbl = dto.zfbl; //Tỷ lệ tự trả
                        mzcfmx.zfxz = dto.zfxz;
                        mzcfmx.dw = dto.dw; //Đ ơn vị
                        mzcfmx.yfdm = dto.yfdm;
                        mzcfmx.czh = dto.czh;
                        mzcfmx.zt = "1";
                        mzcfmx.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
                        mzcfmx.Create();
                        #endregion

                        sqlVo.mz_cfmxList.Add(mzcfmx);

                        #region 结算对象赋值 

                        var jsxmDto = new SettleProjectVO
                        {
                            dl = dto.dl,
                            sfxm = mzcfmx.yp,
                            dj = dto.dj,
                            sl = dto.sl,
                            je = dto.je,
                            zfbl = dto.zfbl,
                            zfxz = dto.zfxz,
                            jmbl = outJmbl,
                            jmje = outJmje,
                            mxnm = 0,
                            cf_mxnm = mzcfmx.cfmxId,
                            cfh = cfh,
                            czh = mzcfmx.czh,
                            fwfdj = dto.fwfdj,
                            jzrq = DateTime.Now,
                            Hismc = dto.xmmc,
                            Xmbm = dto.nbdl,
                            pt_cfnm = ptcfnm
                        };
                        //jsxmDto.mxbm = mzcfmx.yp;

                        jsxmList.Add(jsxmDto);
                        #endregion

                    }
                    #endregion

                    #region 项目
                    if (dto.xmtype == "1" || dto.xmtype == "4")//Số lư ợng hạng mục thu phí据(Đ ơn thuốc hạng mục ngoại trừ)//Hạng mục
                    {
                        #region 门诊项目

                        var mzxm = new OutpatientItemEntity
                        {
                            xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_xm"),
                            ghnm = dto.ghnm,
                            patid = dto.patid
                        };
                        if (dto.IsZhbrxz)// kết hợp tính chất bệnh nhân
                        {
                            mzxm.brxz = istsbzS ? dto.zh_sbrxz : dto.zh_mbrxz;
                        }
                        else
                        {
                            mzxm.brxz = dto.brxz;
                        }
                        if (!yxYbdm) mzxm.brxz = "00";
                        mzxm.mjzbz = dto.mjzbz;
                        mzxm.ys = dto.ys;
                        mzxm.ks = dto.ks;
                        mzxm.sfxm = dto.sfxm;
                        mzxm.dl = dto.dl;
                        mzxm.dj = dto.dj;
                        mzxm.sl = dto.sl;
                        mzxm.je = dto.je;
                        mzxm.zfbl = dto.zfbl;
                        mzxm.zfxz = dto.zfxz;
                        mzxm.xmzt = "1";
                        mzxm.ssbz = "0";
                        mzxm.ssrq = DateTime.Now;
                        mzxm.jsnm = 0;
                        mzxm.jsrq = DateTime.Now;
                        if (dto.IsZhbrxz)// kết hợp tính chất bệnh nhân
                        {
                            if (istsbzS)//Đ ặc biệt dấu hiệu 
                            {
                                mzxm.jmbl = outJmbl;
                                mzxm.jmje = outJmje;
                            }
                            else
                            {
                                mzxm.jmbl = outJmblZhxz;
                                mzxm.jmje = outJmjeZhxz;
                            }
                        }
                        else
                        {
                            mzxm.jmbl = outJmbl;
                            mzxm.jmje = outJmje;
                        }
                        mzxm.zt = "1";
                        mzxm.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
                        mzxm.Create();

                        #endregion

                        sqlVo.mz_xmList.Add(mzxm);

                        #region 结算对象赋值

                        var jsxmDto = new SettleProjectVO
                        {
                            dl = dto.dl,
                            sfxm = dto.sfxm,
                            dj = dto.dj,
                            sl = dto.sl,
                            je = dto.je,
                            zfbl = dto.zfbl,
                            zfxz = dto.zfxz,
                            jmbl = outJmbl,
                            jmje = outJmje,
                            mxnm = mzxm.xmnm,
                            cf_mxnm = 0,
                            fwfdj = dto.fwfdj,
                            jzrq = DateTime.Now,
                            Hismc = dto.xmmc,
                            Xmbm = dto.nbdl,
                            pt_cfnm = dto.pt_nm
                        };
                        //jsxmDto.mxbm = "";

                        jsxmList.Add(jsxmDto);
                        #endregion
                    }
                    #endregion

                    if (gridindex == 0)
                    {
                        #region 结算基本信息
                        jsEntity.patid = dto.patid;
                        jsEntity.brxz = dto.brxz;
                        jsEntity.ghnm = dto.ghnm;
                        jsEntity.isGh = false;
                        jsEntity.isZf = true;
                        jsEntity.jslx = !string.IsNullOrEmpty(kh) ? "2" : "1";
                        #endregion 
                    }

                    gridindex++;
                }

            }

            jsEntity.jsxmList = jsxmList;

            sqlVo.fph = fph;
            //Thêm mỗi 个Đ ối tư ợng thực thể gán giá trị 
            return sqlVo;

        }

        #endregion


        #region 收费项目模版

        /// <summary>
        /// Lấythu phí mẫu và mẫu dư ới tất cả các hạng mục
        /// </summary>
        /// <param name="ks"></param>
        /// <returns></returns>
        public OutPatChargeItemTempVO GetChargeTemplateAll(string ks)
        {
            var tempAll = new OutPatChargeItemTempVO();
            var tempDetailList = new List<OutPatChargeItemDetailVO>();

            var tempList = GetChargeTemplate(ks);
            if (tempList != null && tempList.Count > 0)
            {
                foreach (var item in tempList)
                {
                    var tempDetail = new OutPatChargeItemDetailVO { ItemCode = item.sfmbbh };
                    var tempItemList = GetChargeItemContent(item.sfmbbh);
                    tempDetail.tempDetailList = tempItemList;
                    tempDetailList.Add(tempDetail);
                }
            }

            tempAll.tempList = tempList;
            tempAll.tempDetailList = tempDetailList;
            return tempAll;
        }


        /// <summary>
        /// Hạng mục thu phí mẫu danh sách
        /// </summary>
        /// <param name="ks">Khoa</param>
        /// <returns></returns>
        private List<ChargeItemTemplateVO> GetChargeTemplate(string ks)
        {
            var tempList = _hosTempRepo.GetChargeTemplate(ks, OrganizeId);
            return tempList;
        }

        /// <summary>
        /// Hạng mục thu phí
        /// </summary>
        /// <param name="sfmb">Thu phí mẫu số hiệu</param>
        /// <returns></returns>
        private List<TemplateContentVO> GetChargeItemContent(string sfmb)
        {
            var tempConList = _hosTempRepo.GetChargeItemContent(sfmb, OrganizeId);
            return tempConList;
        }

        #endregion

        #region 门诊记账

        public List<OutpatAccInfoDto> GetOutPatBasicInfoInAcc(string IsBlh, string mzh)
        {

            if (string.IsNullOrWhiteSpace(mzh))
            {
                throw new FailedException("门诊号无效"); //Số hồ sơ bệnh ánKhông hiệu lực
            }

            //LấyĐ ăng ký khámthu phí thông tin cơ bản bệnh nhân
            var outPatBasicInfoInAcc = _outChargeDmnService.GetChargePatInfoInAcc("", IsBlh, mzh, OrganizeId);
            if (outPatBasicInfoInAcc == null)
            {
                throw new FailedCodeException("OUTPAT_PATIENT_BASICINFO_IS_NOT_EXIST");
                //初始化界面
            }
            return outPatBasicInfoInAcc;
        }
        #endregion

        /// <summary>
        /// Đ ăng ký ngoại trú và nhập viện登记 theo số hồ sơ bệnh án hoặc số thẻ tra cứu thông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="blh">Số hồ sơ bệnh án</param>
        /// <param name="kh">Số thẻ</param>
        /// <param name="zjh">Số giấy tờ</param>
        /// <param name="cardType">Loại thẻ</param>
        /// <returns></returns>
        public OutpatAccInfoDto GetOutPatBasicInfoInRegister(string blh, string kh, string zjh, string cardType,string ly,string CardId)
        {
            if (string.IsNullOrWhiteSpace(blh) && string.IsNullOrWhiteSpace(zjh) && (string.IsNullOrWhiteSpace(kh) || string.IsNullOrWhiteSpace(cardType)))
            {
                throw new FailedException("缺少查询参数：病历号或卡号");
            }
            //LấyĐ ăng ký khámthu phí thông tin cơ bản bệnh nhân
            var outPatBasicInfoInAcc = _outChargeDmnService.GetChargePatInfoInRegister(kh, blh, zjh, OrganizeId, cardType,ly,CardId);
            if (outPatBasicInfoInAcc == null)
            {
                throw new FailedCodeException("OUTPAT_PATIENT_BASICINFO_IS_NOT_EXIST");
                //初始化界面
            }
            outPatBasicInfoInAcc.fzbz = _outPatRegRepo.SelectCFZ(outPatBasicInfoInAcc.kh, this.OrganizeId) == null ? (byte)0 : (byte)1;
            return outPatBasicInfoInAcc;
        }

        #region 门诊收费 Vision-1.3 门诊记账的第三个版本
        /// <summary>
        /// Ngoại trú thu phí,lấy thông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IList<OptimAccInfoDto> GetInfo(string mzh)
        {
            var OptimAccInfoDto = _outChargeDmnService.GetpatientAccountInfo(mzh, OrganizeId);
            return OptimAccInfoDto;
        }
        #endregion

        #region 门诊收费2018

        /// <summary>
        /// Ngoại trú thu phí2018 gửi ngoại trú thu phí
        /// </summary>
        /// <param name="bacDto"></param>
        /// <param name="feeRelated"></param>
        /// <param name="ybfeeRelated"></param>
        /// <param name="xnhybfeeRelated"></param>
        /// <param name="accDto"></param>
        /// <param name="orgId"></param>
        /// <param name="cfnmList"></param>
        /// <param name="jsnmList"></param>
        /// <param name="extxmnmList"> lịch sử Đ ãgửi(kho trong Đ ãcó)</param>
        /// <param name="outTradeNo"></param>
        /// <returns></returns>
        public bool submitOutpatCharge(BasicInfoDto2018 bacDto
            , OutpatientSettFeeRelatedDTO feeRelated
            , CQMzjs05Dto ybfeeRelated
            , S25ResponseDTO xnhybfeeRelated
            , OutServiceInfoDto2018 accDto, string orgId
            , IList<int> cfnmList, out IList<int> jsnmList, IList<int> extxmnmList
            , string outTradeNo)
        {
            if (accDto == null && (cfnmList == null || cfnmList.Count < 1) && (extxmnmList == null || extxmnmList.Count < 1))
            {
                throw new FailedException("不存在记账数据");
            }
            jsnmList = new List<int>();
            var itemsGroupBoList = new List<FeeItemsGroupedBO>();
            if (accDto != null)
            {
                if (accDto.OutxmDto != null && accDto.OutxmDto.Count > 0)
                {
                    //Hạng mục(mới 录入của hạng mục không liên kết Đ ơn thuốc)
                    var itemsGroupBo = new FeeItemsGroupedBO
                    {
                        cfsfrq = bacDto.sfrq,
                        xmList = new List<OutpatientItemEntity>(),//Thêm chi tiết hạng mục
                    };
                    foreach (var item in accDto.OutxmDto)//Hạng mục
                    {
                        //Hạng mục ngoại trú
                        var mzxm = new OutpatientItemEntity
                        {
                            sfxm = item.sfxmCode,
                            dj = item.dj,
                            sl = item.sl,
                            dw = item.dw,
                            sfrq = bacDto.sfrq,
                            zfbl = item.zfbl,
                            zfxz = item.zfxz
                        };
                        if (item.zlcs != null && item.zll != null)// rất 规版 bản 
                        {
                            mzxm.zzll = item.zll * item.zlcs;
                            mzxm.dczll = item.zll;
                            mzxm.zxcs = item.zlcs;
                        }
                        mzxm.Create();
                        itemsGroupBo.xmList.Add(mzxm);
                    }
                    itemsGroupBoList.Add(itemsGroupBo);
                }
                if (accDto.OutcfDto != null && accDto.OutcfDto.Any())
                {
                    //Thu ốcĐ ơn thuốc
                    foreach (var item in accDto.OutcfDto)//Đ ơn thuốc
                    {
                        var cfGroupBo = new FeeItemsGroupedBO
                        {
                            cfh = item.cfh,
                            cfsfrq = bacDto.sfrq,
                            cflx = (int)EnumPrescriptionType.Medicine,
                            ypList = new List<OutpatientPrescriptionDetailEntity>(),
                        };
                        foreach (var cfmx in item.cfmxDto)
                        {
                            var mzcfmx = new OutpatientPrescriptionDetailEntity
                            {
                                dj = cfmx.dj,
                                dw = cfmx.dw,
                                sl = cfmx.sl,
                                yp = cfmx.sfxmCode,
                                jl = cfmx.jl,
                                jldw = cfmx.jldw,
                                zfbl = cfmx.zfbl,
                                zfxz = cfmx.zfxz
                            };
                            mzcfmx.Create();
                            cfGroupBo.ypList.Add(mzcfmx);
                        }
                        itemsGroupBoList.Add(cfGroupBo);
                    }
                }
                if (!itemsGroupBoList.Any())
                {
                    return false;
                }
            }

            IList<int> xmnmList;
            IList<int> cfnmadd;
            IList<string> updateSkipSettledCfhList;
            //Gửichi tiết(Đ ơn thuốc thì cập nhật bảng chính thông tin)
            _outPatientUniversalDmnService.SubmitItems(orgId, out cfnmadd, out xmnmList, out updateSkipSettledCfhList, bacDto.mzh, bacDto.ys, itemsGroupBoList);
            //
            if (extxmnmList != null && extxmnmList.Count > 0)
            {
                
                xmnmList = xmnmList.Union(extxmnmList).ToList();
            }
            //
            if (cfnmList != null && cfnmList.Count() > 0)
            {
                cfnmList = cfnmList.ToList().Union(cfnmadd.ToList()).ToList();
                
            }
            else
            {
                cfnmList = cfnmadd.ToList();
            }
            //Lấy cấu hình ngoại trú thu phí thành công sau đ ó có hay không tự đ ộng tạo kế hoạch 
            var autoGenePlan = _sysConfigRepo.GetBoolValueByCode("Outpatient_ChargeFee_AutoGenePlan", orgId) ?? false;
            // Cập nh ậtĐ ơn thuốc ngoại trú của thông tin quyết toán(cập nhật thông tin quyết toán)
            var settAddBO = new OutpatientSettlementAddBO()
            {
                cfnmList = cfnmList,
                xmnmList = xmnmList,
                autoGenePlan = autoGenePlan,

                isQfyj = bacDto.isQfyj,
                settSfrq = bacDto.sfrq,
                fph = bacDto.fph,
            };
            jsnmList = _outPatientUniversalDmnService.AddSettlement(orgId, bacDto.mzh, settAddBO, feeRelated, ybfeeRelated, xnhybfeeRelated, outTradeNo);
            return true;
        }

        /// <summary>
        /// 补 tính phí gửi
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="bacDto"></param>
        /// <param name="xmList"></param>
        /// <returns></returns>
        public bool submitOutpatAdditional(string orgId, BasicInfoDto2018 bacDto,
            OutServiceInfoDto2018 accDto)
        {
            if (!(accDto != null))
            {
                throw new FailedException("失败，缺少计费明细");
            }

            var itemsGroupBoList = new List<FeeItemsGroupedBO>();
            if (accDto != null)
            {
                if (accDto.OutxmDto != null && accDto.OutxmDto.Count > 0)
                {
                    //Hạng mục(mới 录入của hạng mục không liên kết Đ ơn thuốc)
                    var itemsGroupBo = new FeeItemsGroupedBO
                    {
                        cfsfrq = bacDto.sfrq,
                        xmList = new List<OutpatientItemEntity>(),//Thêm chi tiết hạng mục
                    };
                    foreach (var item in accDto.OutxmDto)//Hạng mục
                    {
                        //Hạng mục ngoại trú
                        var mzxm = new OutpatientItemEntity
                        {
                            sfxm = item.sfxmCode,
                            dj = item.dj,
                            sl = item.sl,
                            dw = item.dw,
                            sfrq = bacDto.sfrq,
                            zfbl = item.zfbl,
                            zfxz = item.zfxz
                        };
                        mzxm.Create();
                        itemsGroupBo.xmList.Add(mzxm);
                    }
                    itemsGroupBoList.Add(itemsGroupBo);
                }
                if (accDto.OutcfDto != null && accDto.OutcfDto.Any())
                {
                    //Thu ốcĐ ơn thuốc
                    foreach (var item in accDto.OutcfDto)//Đ ơn thuốc
                    {
                        var cfGroupBo = new FeeItemsGroupedBO
                        {
                            cfh = item.cfh,
                            cfsfrq = bacDto.sfrq,
                            cflx = (int)EnumPrescriptionType.Medicine,
                            ypList = new List<OutpatientPrescriptionDetailEntity>(),
                        };
                        foreach (var cfmx in item.cfmxDto)
                        {
                            var mzcfmx = new OutpatientPrescriptionDetailEntity
                            {
                                dj = cfmx.dj,
                                dw = cfmx.dw,
                                sl = cfmx.sl,
                                yp = cfmx.sfxmCode,
                                jl = cfmx.jl,
                                jldw = cfmx.jldw,
                                zfbl = cfmx.zfbl,
                                zfxz = cfmx.zfxz
                            };
                            mzcfmx.Create();
                            cfGroupBo.ypList.Add(mzcfmx);
                        }
                        itemsGroupBoList.Add(cfGroupBo);
                    }
                }
                if (!itemsGroupBoList.Any())
                {
                    return false;
                }
            }

            IList<int> xmnmList;
            IList<int> cfnmadd;
            IList<string> updateSkipSettledCfhList;
            //Gửichi tiết(Đ ơn thuốc thì cập nhật bảng chính thông tin)
            _outPatientUniversalDmnService.SubmitItems(orgId, out cfnmadd, out xmnmList, out updateSkipSettledCfhList, bacDto.mzh, bacDto.ys, itemsGroupBoList);

            return true;
        }

        /// <summary>
        /// 整合 trư ớc máy dữ liệu成 sau máy của理想trạng thái
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public OutServiceInfoDto2018 togetherData(List<OutGridInfoDto2018> dto)
        {
            OutServiceInfoDto2018 OutDto = null;
            if (dto != null && dto.Count() > 0)
            {
                OutDto = new OutServiceInfoDto2018
                {
                    OutcfDto = new List<OutcfDto2018>(),
                    OutxmDto = new List<OutxmDto2018>()
                };
                foreach (var item in dto)
                {
                    if (item.yzlx == "1")//Thuốc
                    {
                        if (OutDto.OutcfDto != null && OutDto.OutcfDto.Count() > 0)
                        {
                            // phán đoán Đ ơn thuốc có hay không tồn tại tại
                            OutcfmxDto2018 mzcfmx = new OutcfmxDto2018();
                            mzcfmx.dj = item.dj;
                            mzcfmx.dw = item.dw;
                            mzcfmx.sl = item.sl;
                            mzcfmx.sfxmCode = item.sfxmCode;
                            mzcfmx.jl = item.jl;
                            mzcfmx.jldw = item.jldw;
                            mzcfmx.zfbl = item.zfbl;
                            mzcfmx.zfxz = item.zfxz;
                            mzcfmx.zje = item.zje;
                            var data = OutDto.OutcfDto.FirstOrDefault(p => p.cfh == item.cfh);
                            if (data != null)
                            {
                                data.cfmxDto.Add(mzcfmx);
                            }
                            else
                            {
                                OutcfDto2018 mzcf = new OutcfDto2018
                                {
                                    cfh = item.cfh,
                                    czlx = item.czlx,
                                    cfnm = item.cfnm,
                                    cfmxDto = new List<OutcfmxDto2018>()
                                };
                                mzcf.cfmxDto.Add(mzcfmx);
                                OutDto.OutcfDto.Add(mzcf);
                            }
                        }
                        else
                        {
                            //Thứ một lần 
                            OutcfDto2018 mzcf = new OutcfDto2018
                            {
                                cfh = item.cfh,
                                cfnm = item.cfnm,
                                cfmxDto = new List<OutcfmxDto2018>()
                            };
                            OutcfmxDto2018 mzcfmx = new OutcfmxDto2018();
                            mzcfmx.dj = item.dj;
                            mzcfmx.dw = item.dw;
                            mzcfmx.sl = item.sl;
                            mzcfmx.sfxmCode = item.sfxmCode;
                            mzcfmx.jl = item.jl;
                            mzcfmx.jldw = item.jldw;
                            mzcfmx.zfbl = item.zfbl;
                            mzcfmx.zfxz = item.zfxz;
                            mzcfmx.zje = item.zje;
                            mzcf.cfmxDto.Add(mzcfmx);
                            OutDto.OutcfDto.Add(mzcf);
                        }
                    }
                    else
                    {
                        //Hạng mục
                        OutxmDto2018 mzxm = new OutxmDto2018();
                        mzxm.sfxmCode = item.sfxmCode;
                        mzxm.sfdlCode = item.sfdlCode;
                        mzxm.dj = item.dj;
                        mzxm.sl = item.sl;
                        mzxm.dw = item.dw;
                        mzxm.zlcs = item.zlcs;
                        mzxm.zll = item.zll;
                        mzxm.dwjls = item.dwjls;
                        mzxm.jjcl = item.jjcl;
                        mzxm.zfbl = item.zfbl;
                        mzxm.zfxz = item.zfxz;
                        mzxm.zje = item.zje;
                        OutDto.OutxmDto.Add(mzxm);
                    }
                }
            }
            return OutDto;
        }

        #endregion

        /// <summary>
        /// Chi tiết tải lên đ ến BHYT
        /// </summary>
        /// <param name="mzh"></param>
        public void DetailsUploadYb(string mzh)
        {
            var ghEntity = _outPatRegRepo.SelectOutPatientReg(mzh, this.OrganizeId);
            if (string.IsNullOrWhiteSpace(ghEntity.ybjsh))
            {
                throw new FailedException("明细上传医保失败." + "缺少医保结算号");
            }
            var clearResult = YibaoUniteInterface.ClearDetail(ghEntity.ybjsh);
            if (clearResult.Code != 0)
            {
                throw new FailedException("明细上传医保失败." + clearResult.ErrorMsg);
            }
            var data = _outChargeDmnService.GetAllUnSettedListListByMzh(mzh, OrganizeId);
            var ybDetailList = new List<newtouchyibao.Models.DETAIL>();
            foreach (var item in data)
            {
                if (string.IsNullOrWhiteSpace(item.ybdm))
                {
                    throw new FailedException("明细上传医保失败." + "不能开立非医保项目/药品");
                }
                var detail = new newtouchyibao.Models.DETAIL();
                detail.ITEMID = item.yzlx == "1"
                    ? ("YP" + item.cfmxId.Value.ToString())
                    : ("XM" + item.xmnm.Value.ToString());
                //BHYT0Thuốc 1Hạng mục
                detail.DKE085 = item.yzlx == "1" ? "0" : "1";
                detail.DKE012 = item.sfxmCode;
                detail.AKE227 = item.klsj;
                detail.AKE226 = item.ks;
                detail.AKE228 = item.ys;
                detail.AKE229 = item.ysmc;
                detail.AKE002 = item.sfxmmc;
                //Thuốcquy cách, 生产 đ ịa 
                detail.AKE207 = item.dw;
                detail.AKE212 = item.sl;
                detail.AKE208 = item.dj;
                detail.AKE198 = item.zje;
                //Khác thuốc liên quan 
                ybDetailList.Add(detail);
            }
            var preaccResult = YibaoUniteInterface.PreAccount(ghEntity.ybjsh, ybDetailList);
            if (preaccResult.Code != 0)
            {
                throw new FailedException("明细上传医保失败." + preaccResult.ErrorMsg);
            }
            return;
        }
        /// <summary>
        /// Lấy nơi 需quyết toán của hạng mụcchi tiết,分BHYTvà tự chi trả hai một phần,贵安BHYT logic:BHYTcủa tải lênBHYT, tự chi trả củathanh toán tiền mặt
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public guianMainAllOfMzjs GetGuianMainOfMzjs(string mzh)
        {
            GuiAnMzjsPatInfoDto guiAnMzjsPatInfoDto = _outChargeDmnService.GetGuiAnMzjsPatInfoDto(mzh, this.OrganizeId);

            List<FeejsMx> guianZLMainOfMzjsList = new List<FeejsMx>();
            InPtmzjs guianMainOfMzjsModel = new InPtmzjs();
            List<FeejsMx> guianDetailOfMzjsList = new List<FeejsMx>();

            guianMainOfMzjsModel.prm_akc190 = guiAnMzjsPatInfoDto.prm_akc190;
            guianMainOfMzjsModel.prm_aac001 = guiAnMzjsPatInfoDto.prm_aac001;
            guianMainOfMzjsModel.prm_ykc173 = guiAnMzjsPatInfoDto.prm_ykc173;
            guianMainOfMzjsModel.prm_hisfyze = guiAnMzjsPatInfoDto.prm_hisfyze;
            guianMainOfMzjsModel.prm_aka130 = guiAnMzjsPatInfoDto.prm_aka130;
            guianMainOfMzjsModel.prm_yka110 = guiAnMzjsPatInfoDto.prm_yka110;
            guianMainOfMzjsModel.prm_aae013 = guiAnMzjsPatInfoDto.prm_aae013;
            guianMainOfMzjsModel.prm_aae011 = "0";
            guianMainOfMzjsModel.prm_ykc141 = OperatorProvider.GetCurrent().UserName;
            guianMainOfMzjsModel.prm_ykb065 = guiAnMzjsPatInfoDto.prm_ykb065;
            var data = _outChargeDmnService.GetAllUnSettedListListByMzh(mzh, this.OrganizeId);
            decimal ybzje = Convert.ToDecimal(0.0000);
            decimal zlzje = Convert.ToDecimal(0.0000);
            foreach (var item in data)
            {
                //if (string.IsNullOrWhiteSpace(item.ybdm))
                //{
                // throw new FailedException("LấyBHYTchi tiết thất bại." + " không thể 开立 không phải BHYThạng mục/Thuốc");
                //}
                FeejsMx detailOfMzjs = new FeejsMx();
                detailOfMzjs.yka105 = item.yzlx == "1"
                    ? ("YP" + item.cfmxId.ToString())
                    : ("XM" + item.xmnm.ToString()); //Ghi sổ số thứ tự 
                detailOfMzjs.ykd125 = item.sfxmCode.ToString();//
                detailOfMzjs.ykd126 = item.sfxmmc.ToString();//
                detailOfMzjs.yka002 = string.IsNullOrEmpty(item.ybdm) ? " " : item.ybdm;//
                detailOfMzjs.yka003 = item.sfxmmc.ToString();//
                detailOfMzjs.akc226 = Convert.ToDecimal(item.sl);//
                detailOfMzjs.akc225 = Convert.ToDecimal(item.dj);//
                detailOfMzjs.yka315 = Convert.ToDecimal(item.zje);//
                detailOfMzjs.yka097 = "";//
                detailOfMzjs.yka098 = "";//
                detailOfMzjs.ykd102 = "";//
                detailOfMzjs.yka099 = "";//
                detailOfMzjs.yka100 = "";//
                detailOfMzjs.yka101 = "";//
                detailOfMzjs.ykd106 = "";//
                detailOfMzjs.yka102 = "";//
                detailOfMzjs.yke123 = item.klsj.ToString("yyyy-MM-dd");//
                detailOfMzjs.ykc141 = OperatorProvider.GetCurrent().UserName;
                detailOfMzjs.aae036 = DateTime.Now.ToString("yyyy-MM-dd");
                detailOfMzjs.aae013 = "";//
                detailOfMzjs.yke201 = "";//
                detailOfMzjs.yka295 = "";//
                detailOfMzjs.aka074 = "";//
                detailOfMzjs.aka070 = "";//
                detailOfMzjs.yae374 = "";//
                detailOfMzjs.yke009 = "";//
                detailOfMzjs.yke186 = "1";//

                if (detailOfMzjs.akc226 > 0 && detailOfMzjs.yka315 > 0)
                {
                    if (!string.IsNullOrWhiteSpace(item.ybdm) && item.ybbz == "1")
                    {

                        ybzje += detailOfMzjs.yka315;
                        guianDetailOfMzjsList.Add(detailOfMzjs);
                    }
                    else
                    {
                        zlzje += detailOfMzjs.yka315;
                        guianZLMainOfMzjsList.Add(detailOfMzjs);
                    }
                }

            }

            guianMainOfMzjsModel.row = guianDetailOfMzjsList;
            guianMainAllOfMzjs guianMainAllOfMzjsModel = new guianMainAllOfMzjs()
            {
                zlzje = zlzje,
                ybzje = ybzje,
                guianYBMainOfMzjs = guianMainOfMzjsModel,
                guianZLMainOfMzjs = guianZLMainOfMzjsList
            };

            return guianMainAllOfMzjsModel;
        }

        public guianMainAllOfMzjs GetGuiAnDetailsMzjsYbTfh(string jsnm, Dictionary<string, decimal> tjsxmDict, string mzh)
        {
            GuiAnMzjsPatInfoDto guiAnMzjsPatInfoDto = _outChargeDmnService.GetGuiAnMzjsPatInfoDto(mzh, this.OrganizeId);

            List<FeejsMx> guianZLMainOfMzjsList = new List<FeejsMx>();
            InPtmzjs guianMainOfMzjsModel = new InPtmzjs();
            List<FeejsMx> guianDetailOfMzjsList = new List<FeejsMx>();

            guianMainOfMzjsModel.prm_akc190 = guiAnMzjsPatInfoDto.prm_akc190;
            guianMainOfMzjsModel.prm_aac001 = guiAnMzjsPatInfoDto.prm_aac001;
            guianMainOfMzjsModel.prm_ykc173 = guiAnMzjsPatInfoDto.prm_ykc173;
            guianMainOfMzjsModel.prm_hisfyze = guiAnMzjsPatInfoDto.prm_hisfyze;
            guianMainOfMzjsModel.prm_aka130 = guiAnMzjsPatInfoDto.prm_aka130;
            guianMainOfMzjsModel.prm_yka110 = guiAnMzjsPatInfoDto.prm_yka110;
            guianMainOfMzjsModel.prm_aae013 = guiAnMzjsPatInfoDto.prm_aae013;
            guianMainOfMzjsModel.prm_aae011 = "0";
            guianMainOfMzjsModel.prm_ykc141 = OperatorProvider.GetCurrent().UserName;
            guianMainOfMzjsModel.prm_ykb065 = guiAnMzjsPatInfoDto.prm_ykb065;
            var data = _outChargeDmnService.GetAllUnSettedListListByJsnm(jsnm, this.OrganizeId);
            decimal ybzje = Convert.ToDecimal(0.0000);
            decimal zlzje = Convert.ToDecimal(0.0000);
            decimal tkzje = Convert.ToDecimal(0.0000);
            foreach (var item in data)
            {
                //if (string.IsNullOrWhiteSpace(item.ybdm))
                //{
                // throw new FailedException("LấyBHYTchi tiết thất bại." + " không thể 开立 không phải BHYThạng mục/Thuốc");
                //}
                FeejsMx detailOfMzjs = new FeejsMx();
                detailOfMzjs.yka105 = item.yzlx == "1"
                    ? ("YP" + item.cfmxId.ToString())
                    : ("XM" + item.xmnm.ToString()); //Ghi sổ số thứ tự 
                detailOfMzjs.ykd125 = item.sfxmCode.ToString();//
                detailOfMzjs.ykd126 = item.sfxmmc.ToString();//
                detailOfMzjs.yka002 = string.IsNullOrEmpty(item.ybdm) ? " " : item.ybdm;//
                detailOfMzjs.yka003 = item.sfxmmc.ToString();//
                detailOfMzjs.akc225 = Convert.ToDecimal(item.dj);//
                detailOfMzjs.akc226 = item.sl;
                foreach (var itemtf in tjsxmDict)
                {
                    if (itemtf.Key == item.jsmxnm.ToString())
                    {
                        detailOfMzjs.akc226 = Convert.ToDecimal(item.sl - itemtf.Value);//
                        tkzje += (itemtf.Value * Convert.ToDecimal(item.dj));
                    }
                }
                detailOfMzjs.yka315 = Convert.ToDecimal(detailOfMzjs.akc226 * item.dj);//
                detailOfMzjs.yka097 = "";//
                detailOfMzjs.yka098 = "";//
                detailOfMzjs.ykd102 = "";//
                detailOfMzjs.yka099 = "";//
                detailOfMzjs.yka100 = "";//
                detailOfMzjs.yka101 = "";//
                detailOfMzjs.ykd106 = "";//
                detailOfMzjs.yka102 = "";//
                detailOfMzjs.yke123 = item.klsj.ToString("yyyy-MM-dd");//
                detailOfMzjs.ykc141 = OperatorProvider.GetCurrent().UserName;
                detailOfMzjs.aae036 = DateTime.Now.ToString("yyyy-MM-dd");
                detailOfMzjs.aae013 = "";//
                detailOfMzjs.yke201 = "";//
                detailOfMzjs.yka295 = "";//
                detailOfMzjs.aka074 = "";//
                detailOfMzjs.aka070 = "";//
                detailOfMzjs.yae374 = "";//
                detailOfMzjs.yke009 = "";//
                detailOfMzjs.yke186 = "1";//
                if (detailOfMzjs.akc226 > 0 && detailOfMzjs.yka315 > 0)
                {
                    if (!string.IsNullOrWhiteSpace(item.ybdm) && item.ybbz == "1")
                    {

                        ybzje += detailOfMzjs.yka315;
                        guianDetailOfMzjsList.Add(detailOfMzjs);
                    }
                    else
                    {
                        zlzje += detailOfMzjs.yka315;
                        guianZLMainOfMzjsList.Add(detailOfMzjs);
                    }
                }
            }

            guianMainOfMzjsModel.row = guianDetailOfMzjsList;
            guianMainAllOfMzjs guianMainAllOfMzjsModel = new guianMainAllOfMzjs()
            {
                tkzje = tkzje,
                zlzje = zlzje,
                ybzje = ybzje,
                //prm_ack190= "GZ190425100277497844",
                guianYBMainOfMzjs = guianMainOfMzjsModel,
                guianZLMainOfMzjs = guianZLMainOfMzjsList
            };

            return guianMainAllOfMzjsModel;
        }
        
        #region 重庆医保

        public void GetChongQingMainOfMzjs(string mzh, string cfnm, out decimal ybzje, out decimal zfzje)
	    {
		    var data = _outChargeDmnService.GetCQZFUnSettedListByMzh( mzh, cfnm, this.OrganizeId);

            ybzje = Convert.ToDecimal(0.0000);
		    zfzje = Convert.ToDecimal(0.0000);
		    foreach (var item in data)
		    {
                if (item.issc == 1)
                {
                    ybzje += Convert.ToDecimal(item.je);
                }
                else
                {
                    zfzje += Convert.ToDecimal(item.je);
                }
		    }
		}

	    public UploadPrescriptionsInPut GetCQDetailsMzjsYbTfh(string mzh, string jsnm,
		    Dictionary<string, decimal> tjsxmDict, out decimal ybzje, out decimal zfzje, out decimal tfzje)
	    {
			var ybdata = _outChargeDmnService.GetCQYBTfListByJsnm(jsnm, mzh, this.OrganizeId);
		    var zfdata = _outChargeDmnService.GetCQZFTfListByJsnm(jsnm, mzh, this.OrganizeId);
		    List<UploadPrescriptionsListInPut> yblist = new List<UploadPrescriptionsListInPut>();
            ybzje = Convert.ToDecimal(0.0000);
		    tfzje = Convert.ToDecimal(0.0000);
			zfzje = Convert.ToDecimal(0.0000);
		    foreach (var item in ybdata)
		    {
			    
				foreach (var itemtf in tjsxmDict)
			    {
				    if (itemtf.Key == item.cxmxlsh)
				    {
					    item.sl = Convert.ToDecimal(item.sl) - itemtf.Value;//
					    tfzje += (itemtf.Value * item.dj);
				    }
			    }
			    item.cxmxlsh = "";// tồn tại 放jsmxnm, này trư ờng có thể恒Lànull, đ ể 分trang lấy dữ liệu(chi tiết tải lên个数限制)
			    item.jbr = this.UserIdentity.UserCode;
                if (!string.IsNullOrEmpty(item.sl.ToString()) && !string.IsNullOrEmpty(item.je.ToString()) && Convert.ToDecimal(item.sl) > 0 && Convert.ToDecimal(item.je) > 0)
				{
					item.je = Convert.ToDecimal(item.sl * item.dj);
                    ybzje += Convert.ToDecimal(item.je);
				    yblist.Add(item);
			    }
		    }
		  
				foreach (var item in zfdata)
				{
					foreach (var itemtf in tjsxmDict)
					{
						if (itemtf.Key == item.cxmxlsh)
						{
							item.sl = Convert.ToDecimal(item.sl) - itemtf.Value;//
							tfzje += (itemtf.Value * item.dj);
						}
					}
					if (!string.IsNullOrEmpty(item.sl.ToString()) && !string.IsNullOrEmpty(item.je.ToString()) && Convert.ToDecimal(item.sl) > 0 && Convert.ToDecimal(item.je) > 0)
					{
						item.je = Convert.ToDecimal(item.sl * item.dj);
						zfzje += Convert.ToDecimal(item.je);
					}
				}
			
		   
		    UploadPrescriptionsInPut ChongQingMainAllOfMzjsModel = new UploadPrescriptionsInPut()
		    {
			    zymzh = mzh,
			    cflist = yblist
			};

		    return ChongQingMainAllOfMzjsModel;
		}

	    public void SaveCqybUploadInPres(List<UploadPrescriptionsListInPut> cflist, string zymzh, string jytype)
	    {
		    List<CqybUploadInPres04Entity> entitylist = new List<CqybUploadInPres04Entity>();
			if (cflist != null && cflist.Count>0)
		    {
			    foreach (var item in cflist)
			    {
				    //if (entitylist.FindAll(p=>p.cfh==item.cfh && p.OrganizeId==this.OrganizeId).Count<1)
				    //{

						CqybUploadInPres04Entity entity = new CqybUploadInPres04Entity();
					    entity.OrganizeId = this.OrganizeId;
					    entity.zt = "1";
                        entity.cfh = item.cfh;
					    entity.jytype = jytype;
					    entity.zymzh = zymzh;
                        entity.je = item.je;
                        entity.ysbm = item.ysbm;
                        entity.gjmldm = item.gjmldm;
                        entity.gjysbm = item.gjysbm;
					    entity.Create();
					    entitylist.Add(entity);
				    //}
				}
			    _cqybUploadInPres04Repo.SaveCqybUploadInPres(entitylist);
		    }
        }
        public void SaveCqybUploadInPres(string zymzh, string jytype, string cfh, string pch)
        {
            List<CqybUploadInPres04Entity> entitylist = new List<CqybUploadInPres04Entity>();
            var cfhs = cfh.Split(',');
            for (int i=0;i<cfhs.Length;i++)
            {
                CqybUploadInPres04Entity entity = new CqybUploadInPres04Entity();
                entity.OrganizeId = this.OrganizeId;
                entity.zt = "1";
                entity.cfh = cfhs[i];
                entity.pch = pch;
                entity.jytype = jytype;
                entity.zymzh = zymzh;
                entity.px = 0;
                entity.Create();
                entitylist.Add(entity);
            }
            _cqybUploadInPres04Repo.SaveCqybUploadInPres(entitylist);
        }
        public void UpdateCqyb04InPres(string zymzh, string cfh,string orgId)
        {
            var entity = _cqybUploadInPres04Repo.FindEntity(p => p.zymzh == zymzh && p.cfh == cfh && p.zt == "1" && p.OrganizeId == orgId);
            if (entity!=null)
            {
                entity.zt = "0";
                entity.Modify();
                _cqybUploadInPres04Repo.Update(entity);
            }
        }
        public void UpDateUploadPch(string zymzh, string cfh, string pch, string orgId) {
            var entity = _cqybUploadInPres04Repo.FindEntity(p => p.zymzh == zymzh && p.cfh == cfh && p.pch == pch && p.zt == "1" && p.OrganizeId == orgId);
            if (entity != null)
            {
                entity.zt = "0";
                entity.Modify();
                _cqybUploadInPres04Repo.Update(entity);
            }
        }
        /// <summary>
        /// Lấy số đ ơn thuốc ghi đ ỏ hoàn trả dữ liệu,tra c ứuĐ ãtải lên và chưa quyết toán của số đ ơn thuốc, tiến hành ghi đ ỏ hoàn trả xử lý
        /// </summary>
        /// <param name="zymzh"></param>
        /// <param name="type">1Ngoại trú 2Nhập viện</param>
        /// <returns></returns>
        public List<RefundPrescriptionsInPut> GetCqyb10Data(string zymzh,string type)
	    {
		    return _outPatientDmnService.GetCqyb10Data(zymzh, this.OrganizeId, type);
	    }

        public List<Input_2205> GetCqybMxCxData(string zymzh, string type, string ybver) {
            return _outPatientDmnService.GetCqybMxCxData(zymzh, this.OrganizeId, type,ybver);
        }


        public object ZFToYB_Step_1(string mzh)
	    {
		    if (string.IsNullOrWhiteSpace(mzh))
		    {
			    throw new FailedException("缺少参数.门诊号");
		    }
		    var orgId = this.OrganizeId;
			var ghxx = _outPatRegRepo.IQueryable().Where(p => p.mzh == mzh && p.OrganizeId == orgId).FirstOrDefault();
		    if (ghxx == null)
		    {
			    throw new FailedException("住院信息未找到");
		    }
		    if (ghxx.zt == "0")
		    {
			    throw new FailedException("本次挂号已作废");
		    }
		    if (!(ghxx.brxz == "0"))
		    {
			    throw new FailedException("当前非自费");
		    }

		    return new object();
	    }
	    public object ZFToYB_Step_2(string zyh)
	    {
		    var orgId = this.OrganizeId;
		    //var hasNonYbFee = _hospFeeDmnService.CheckHasNonYbFee(zyh, orgId);
		    //if (hasNonYbFee)
		    //{
		    // throw new FailedException("Đ ã产生 không phải BHYT liên quan chi phí");
		    //}

		    return new object();
	    }

	    public object ZFToYB_Step_3(string mzh, string sbbh, string xm)
	    {
		    var orgId = this.OrganizeId;
		    var ghxx = _outPatRegRepo.IQueryable().Where(p => p.mzh == mzh && p.OrganizeId == orgId).FirstOrDefault();

		    int patid = ghxx.patid;
            SysPatientBasicInfoEntity xtbrxx = null;
            SysCardEntity kxx = null;
		    var xtbrxxList = _sysCardRepo.IQueryable().Where(p => p.CardNo == sbbh && p.brxz != "0" && p.OrganizeId == orgId && p.zt == "1").ToList();
		    if (xtbrxxList.Count == 1)
		    {
                kxx = xtbrxxList[0];
		    }
            xtbrxx = _sysPatiInfoRepo.IQueryable().Where(p => p.patid == patid && p.OrganizeId == orgId && p.zt == "1").First();
            if (xtbrxx.xm != xm)
		    {
			    throw new FailedException("异常，就诊姓名与医保卡姓名不一致，请先修改患者姓名");
		    }

			//var ryzd1 = _hospMultiDiagnosisRepo.IQueryable().Where(p => p.zyh == zyh && p.zt == "1" && p.OrganizeId == orgId).OrderBy(p => p.zdpx).FirstOrDefault();

			//var ksmc = _sysDepartmentRepo.GetNameByCode(zybrxx.ks, orgId);
            //国家 mã 
            var ysInfo = _outPatientSettleDmnService.GetDepartmentDoctorIdC(orgId, ghxx.ks, ghxx.ys);
            return new
			{
				patid = xtbrxx.patid,//patid,
                ghxx = ghxx,
				xtbrxx = xtbrxx,
                yllb= ghxx.mjzbz,
                rygjysbm= ysInfo
            };
		}

	    public object ZFToYB_Step_6(string mzh, int patid, ZYToYBDto patInfo)
	    {
		    var orgId = this.OrganizeId;

		    _patientBasicInfoDmnService.OutPatZFchangetoYB(orgId, mzh, patid, patInfo);

		    return new object();
	    }

	    public object ZFToYB_Step_8(string mzh)
	    {
		    UpdatebrxzRequest par = new UpdatebrxzRequest {mzh=mzh, zyh = mzh, brxzCode = "1", brxzmc = "普通医保" };
		    //Đ ồng bộ dữ liệu tớiCPOE
		    SiteCISAPIHelper.UpdatebrxzInfo(par);
		    return new object();
	    }

        #endregion

    }
}
