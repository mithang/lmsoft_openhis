/**********************************************************/
// Copyright (C) 2016 Newtouch Giữ bản quyền. 
// Description: Đ ăng ký khám ngoại trú thu phí liên quan nghiệp vụ 
// Author：HLF
// CreateDate： 2016/12/26 12:42:17 
//**********************************************************/
using System.Collections.Generic;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Proxy.guian.DTO.S25;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutPatChargeApp
    {
        /// <summary>
        /// theo số thẻ lấy cơ bản thông tin
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="brxz"></param>
        /// <returns></returns>
        OutPatChargeInfoVO GetOutPatChargeInfo(string kh, string brxz);


        /// <summary>
        /// LấyĐ ăng ký khámkhoa bác sĩ
        /// </summary>
        /// <param name="patid">Bệnh nhânpatid</param>
        /// <param name="brxz">Tính chất bệnh nhânSố hiệu</param>
        /// <param name="docVo"></param>
        /// <returns></returns>
        List<OutPatChargeItemVO> GetOutGhInfo(int patid, string brxz, out OutPatChargeDoctorVO docVo);

        /// <summary>
        /// Lấy thuốc và hạng mục thu phí
        /// </summary>
        /// <param name="keyword">Tra cứuTừ khóa</param>
        /// <returns></returns>
        List<ChargeItemDetailVO> GetYpItemInfo(string keyword);

        /// <summary>
        /// Lấy số đ ơn thuốc và phí dịch vụ 
        /// </summary> 
        /// <param name="sfxmtype">Hạng mục thu phí loại</param> 
        /// <param name="lscfh"> tạm thời số đ ơn thuốc</param>
        /// <param name="brxz">Tính chất bệnh nhân</param>
        /// <param name="dl"> loại lớn </param>
        /// <param name="sfxm">Hạng mục thu phí</param>
        /// <param name="dj">Đ ơn giá</param>
        /// <param name="sl">Số lư ợng</param>
        /// <returns></returns> 
        OutPatChargeCFDto GetCfhAndFjfMoney(string sfxmtype, string lscfh, string brxz, string dl, string sfxm, string dj, string sl);

        /// <summary>
        /// Lấy số đ ơn thuốc
        /// </summary>
        /// <param name="sfxmtype">Hạng mục thu phí loại</param>
        /// <returns></returns>
        string GetCfh(string sfxmtype);

        /// <summary>
        /// Lấy hạng mục thu phí模版
        /// </summary>
        /// <param name="ks">Khoa</param>
        /// <returns></returns>
        OutPatChargeItemTempVO GetChargeTemplateAll(string ks);


        /// <summary>
        /// Ngoại trú thu phí Th êmĐ ơn thuốc và quyết toán
        /// </summary>
        /// <param name="gridDto">Đ ơn thuốcdanh sách</param>
        /// <param name="kh">Số thẻ</param>
        /// <param name="fph">Số hóa đ ơn</param> 
        /// <param name="xjzffs">Phư ơng thức thanh toán tiền mặt</param> 
        /// <param name="xjzffsbh">Phư ơng thức thanh toán tiền mặt số hiệu</param> 
        /// <param name="ysk">Tạm thu</param> 
        /// <param name="ssk">Số tiền thực thu</param>
        /// <param name="zl">Trả lại tiền thừa</param>
        /// <returns></returns>
        SettSaveSuccessResultDto OutPatChargeSett(List<OutpatChargeDto> gridDto, string kh, string fph, string xjzffs, string xjzffsbh, decimal ysk, decimal ssk, decimal zl);

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
        OutPatChargeSettDataVo GetOutPatChargeSettDbvo(List<OutpatChargeDto> gridDto, string kh, string fph,
           string xjzffs, string xjzffsbh, decimal ysk, decimal ssk, decimal zl, out SettlementEntityVO jsEntity);

        /// <summary>
        /// Ghi sổ ngoại trú theo số khám ngoại trú lấy cơ bản thông tin
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="blh"></param>
        /// <returns></returns>
        List<OutpatAccInfoDto> GetOutPatBasicInfoInAcc(string IsBlh, string mzh);

        /// <summary>
        /// Đ ăng ký ngoại trú và nhập viện登记 theo số hồ sơ bệnh án hoặc số thẻ tra cứu thông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="blh">Số hồ sơ bệnh án</param>
        /// <param name="kh">Số thẻ</param>
        /// <param name="zjh">Số giấy tờ</param>
        /// <param name="cardType">Loại thẻ</param>
        /// <returns></returns>
        OutpatAccInfoDto GetOutPatBasicInfoInRegister(string blh, string kh, string zjh, string cardType,string ly,string CardId);


        #region 门诊收费 Vision-1.2 门诊记账的第三个版本
        /// <summary>
        /// Ngoại trú thu phí,lấy thông tin cơ bản bệnh nhân và ghi sổ thông tin
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        IList<OptimAccInfoDto> GetInfo(string mzh);
        #endregion

        #region 门诊收费

        /// <summary>
        /// Ngoại trú thu phí2018 gửi ngoại trú thu phí
        /// </summary>
        /// <param name="bacDto"></param>
        /// <param name="feeRelated"></param>
        /// <param name="ybfeeRelated"></param>
        /// <param name="accDto"></param>
        /// <param name="orgId"></param>
        /// <param name="cfnmList"></param>
        /// <param name="jsnmList"></param>
        /// <param name="extxmnmList"> lịch sử Đ ãgửi(kho trong Đ ãcó)</param>
        /// <returns></returns>
        bool submitOutpatCharge(BasicInfoDto2018 bacDto
            , OutpatientSettFeeRelatedDTO feeRelated
            , CQMzjs05Dto ybfeeRelated
            , S25ResponseDTO xnhybfeeRelated
            , OutServiceInfoDto2018 accDto, string orgId
            , IList<int> cfnmList, out IList<int> jsnmList, IList<int> extxmnmList
            , string outTradeNo);

        /// <summary>
        /// 补 tính phí gửi
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="bacDto"></param>
        /// <param name="xmList"></param>
        /// <returns></returns>
        bool submitOutpatAdditional(string orgId, BasicInfoDto2018 bacDto,
            OutServiceInfoDto2018 accDto);

        /// <summary>
        /// 整合 trư ớc máy củathu phí dữ liệu成 sau máy của理想trạng thái
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        OutServiceInfoDto2018 togetherData(List<OutGridInfoDto2018> dto);
        #endregion

        /// <summary>
        /// Chi tiết tải lên đ ến BHYT
        /// </summary>
        /// <param name="mzh"></param>
        void DetailsUploadYb(string mzh);

        guianMainAllOfMzjs GetGuianMainOfMzjs(string mzh);

        guianMainAllOfMzjs GetGuiAnDetailsMzjsYbTfh(string jsnm, Dictionary<string, decimal> tjsxmDict, string mzh);

        

        #region 重庆医保
        void GetChongQingMainOfMzjs(string mzh, string cfnm, out decimal ybzje, out decimal zfzje);
	    UploadPrescriptionsInPut GetCQDetailsMzjsYbTfh(string mzh, string jsnm, Dictionary<string, decimal> tjsxmDict, out decimal ybzje, out decimal zfzje, out decimal tfzje);

	    void SaveCqybUploadInPres(List<UploadPrescriptionsListInPut> cflist, string zymzh, string jytype);
        void SaveCqybUploadInPres(string zymzh, string jytype, string cfh, string pch);
        void UpdateCqyb04InPres(string zymzh, string cfh, string orgId);

        void UpDateUploadPch(string zymzh, string cfh, string pch, string orgId);

        List<RefundPrescriptionsInPut> GetCqyb10Data(string zymzh, string type);

        List<Input_2205> GetCqybMxCxData(string zymzh, string type, string ybver);
        object ZFToYB_Step_1(string mzh);

	    object ZFToYB_Step_3(string mzh, string sbbh, string xm);

	    object ZFToYB_Step_6(string mzh, int patid,ZYToYBDto patInfo);

	    object ZFToYB_Step_8(string mzh);
        #endregion
    }
}
