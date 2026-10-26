using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Application.Implementation.OutpatientManage.MzBespeakRegisterProcess;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Đ ăng ký khám ngoại trú nên dùng 
    /// </summary>
    public class OutpatientRegApp : AppBase, IOutpatientRegApp
    {
        private readonly IOutPatientDmnService _outPatientDmnService;
        private readonly IOutPatientSettleDmnService _outPatientSettleDmnService;
        private readonly IFinancialInvoiceRepo _financialInvoiceEntityRepository;
        private readonly ISysConfigRepo _sysConfigRepo;
        private List<MzBespeakRegisterQueryResponseDTO> mzbrDetail;

        /// <summary>
        /// Đ ăng ký khám lịch trực List
        /// </summary>
        /// <param name="keyword">Khoa/Bác sĩ</param>
        /// <param name="mzlx">Loại ngoại trú phòng khám thông thư ờng/急症/Phòng khám chuyên gia</param>
        /// <returns></returns>
        public List<RegScheduleVO> GetRegScheduleList(string keyword, string mzlx)
        {
            return _outPatientDmnService.GetRegScheduleList(keyword, mzlx, this.OrganizeId);
        }
        /// <summary>
        /// theo mã nhân viên lấy số hóa đ ơn 
        /// by caishanshan 
        /// </summary>
        /// <returns></returns>
        public string GetInvoice()
        {
            return _outPatientDmnService.GetInvoiceListByEmpNo(UserIdentity.UserCode, OrganizeId);
        }
        /// <summary>
        /// Xác minh nhập số hóa đ ơn có hay không có thể dùng 
        /// by caishanshan
        /// </summary>
        /// <param name="inputFph"></param>
        public void CheckInvoice(string inputFph)
        {
            FinancialInvoiceEntity fpUpdateEntity, fpInsertEntity;
            _financialInvoiceEntityRepository.UpdateCurrentGetEntitys(inputFph, UserIdentity.UserCode, out fpUpdateEntity, out fpInsertEntity, OrganizeId);
        }

        /// <summary>
        /// Đ ăng ký khám ngoại trú lưu
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="kh"></param>
        /// <param name="ghly"></param>
        /// <param name="mjzbz"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <param name="ksmc"></param>
        /// <param name="ysmc"></param>
        /// <param name="ghxm"></param>
        /// <param name="zlxm"></param>
        /// <param name="fph"></param>
        /// <param name="sfrq"></param>
        /// <param name="isCkf"></param>
        /// <param name="isGbf"></param>
        /// <param name="ghpbId"></param>
        /// <param name="feeRelated"></param>
        /// <param name="brxz">Tính chất bệnh nhân由trang truyền qua 来,因là BHYTbệnh nhân có thể挂tự trả</param>
        /// <param name="notSett">true tạm thời không 结(ngoại trú thu phí giờ cùng nhau 结),nếu không结</param>
        /// <param name="ybjsh">BHYT端số quyết toán</param>
        /// <param name="qzjzxh"> trư ớc 置số thứ tự khám bệnh</param>
        /// <param name="qzmzh"> trư ớc 置số khám ngoại trú</param>
        /// <param name="mzyyghid">Đ ặt lịch đ ăng ký khám ngoại trú ID Tư ơng ứngNewtouch_CIS.dbo.mz_yygh.Id</param>
        /// <returns></returns>
        public void Save(int patid, string kh, string ghly, string mjzbz,
            string ks, string ys, string ksmc, string ysmc, string ghxm, string zlxm
            , string fph, DateTime? sfrq, bool isCkf, bool isGbf, int ghpbId
            , OutpatientSettFeeRelatedDTO feeRelated, string brxz
            , string ybjsh, string mzyyghId, ref short? qzjzxh, ref string qzmzh
            , string jzyy,string jzid, string jzlx, string bzbm, string bzmc,string isjm, out object newJszbInfo)
        {
            if ((qzjzxh ?? 0) <= 0 || string.IsNullOrWhiteSpace(qzmzh))
            {
                var mzhjzxh = _outPatientSettleDmnService.GetMzhJzxh(patid, ghpbId.ToString(), ks, ys,this.OrganizeId, this.UserIdentity.UserCode);
                qzjzxh = mzhjzxh.Item1;
                qzmzh = mzhjzxh.Item2;
            }
            //Lưu
            _outPatientSettleDmnService.Save(this.OrganizeId, patid, kh, ghly, mjzbz,
            ks, ys, ksmc, ysmc, ghxm, zlxm, fph, sfrq, isCkf, isGbf, (int)qzjzxh.Value, ghpbId, feeRelated, brxz, ybjsh, qzmzh, jzyy,jzid,jzlx,bzbm,bzmc, mzyyghId, isjm, out newJszbInfo);
        }

        /// <summary>
        /// 预定
        /// </summary>
        /// <param name="gh"></param>
        /// <param name="ssk"></param>
        /// <param name="fph"></param>
        /// <param name="isCkf"></param>
        /// <param name="isGbf"></param>
        /// <param name="updateBrxz"></param>
        public long Book(OutpatientRegistEntity gh, decimal ssk, string fph, bool isCkf, bool isGbf, string updateBrxz)
        {
            //LấySố thứ tự khám bệnh
            var jzxh = _outPatientDmnService.GetJzxh(gh.ghlx, gh.ks, gh.ys, gh.ghzb, this.UserIdentity.UserCode, this.OrganizeId);
            if (string.IsNullOrEmpty(jzxh.ToString()))
            {
                throw new FailedCodeException("OUTPAT_FAILED_TO_GET_THE_SERIAL_NUMBER");
            }
            gh.jzxh = jzxh;
            //Xác minh tính chất bệnh nhân có hay không áp dụng cho ngoại trú
            CheckBrxz(gh.kh, updateBrxz);
            //Lưu
            return _outPatientSettleDmnService.Book(gh, ssk, fph, isCkf, isGbf);
        }

        /// <summary>
        /// phán đoán tính chất bệnh nhân có hay không áp dụng cho ngoại trú(Đ ăng ký khámtrang sửa tính chất bệnh nhân)
        /// </summary>
        public void CheckBrxz(string kh, string updateBrxz)
        {
            // theo kh lấy thông tin cơ bản bệnh nhân và loại thẻ
            var basicInfoVo = _outPatientDmnService.GetBasicInfoByCardNo(kh);
            var ghlx = basicInfoVo.cardtype;
            if (ghlx == "")
            {
                throw new FailedCodeException("SYS_CARD_TYPE_IS_EMPTY");  //Loại thẻ=loại đ ăng ký khám
            }
            var ybtsdy = string.Empty;
            var syybbf = string.Empty;
            var fw = ((int)Enummzzybz.mz).ToString();
            var validBrxzList = _outPatientDmnService.GetValidBrxzList(string.Empty, ghlx, ybtsdy, syybbf, fw);
            //Xác minh tính chất bệnh nhân có hay không bao gồm Đ ăng ký khám của tính chất bệnh nhân
            var validBrxz = validBrxzList.Any(validBrxzItem => validBrxzItem.brxz == updateBrxz);
            if (!validBrxz)
            {
                throw new FailedCodeException("OUPAT_THE_NATURE_OF_THE_PATIENT_DOES_NOT_APPLY_TO_OUTPATIENT");
            }

        }


        /// <summary>
        /// Đ ặt lịch đ ăng ký khám
        /// </summary>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <param name="blh"></param>
        /// <param name="ksCode"></param>
        /// <param name="mzlx">Đ ăng ký khám ngoại trú loại 1:Phòng khám thông thư ờng 2:Cấp cứu 3:Phòng khám chuyên gia</param>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        public string BespeakRegister(int? zjlx, string zjh, string blh, string ksCode, int mzlx, string ysgh)
        {
            var param = new BespeakRegisterParamDTO
            {
                blh = blh,
                ksCode = ksCode,
                mzlx = mzlx,
                ysgh = ysgh,
                zjh = zjh,
                zjlx = zjlx
            };
            var result = new MzBespeakRegisterProcess(param).Process();
            return result.ResultMsg;
        }
    }
}
