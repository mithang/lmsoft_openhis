/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description: 
// Author:
// CreateDate: 2016/12/23 11:14:24 
//**********************************************************/
using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Quyết toán ngoại trú
    /// </summary>
    public interface IOutPatChargeDmnService
    {
        /// <summary>
        /// Lấy thông tin bệnh nhân thu phí theo số thẻ
        /// </summary>
        /// <param name="kh">Số thẻ</param>
        /// <returns></returns>
        OutPatChargeInfoVO GetChargePatInfo(string kh, string brxz);

        /// <summary>
        /// Loại khoa đăng ký khám
        /// </summary>
        /// <param name="patid">Bệnh nhânpatid</param>
        /// <param name="brxz"></param>
        /// <param name="startDate">Ngày bắt đầu</param>
        /// <param name="endDate">Ngày kết thúc</param>
        /// <returns></returns>
        List<OutPatChargeItemVO> getOutPatItem_ghlx(int patid, string brxz, string startDate, string endDate);

        /// <summary>
        /// Loại khoa đăng ký khám
        /// </summary>
        /// <param name="patid">Bệnh nhânpatid</param>
        /// <param name="brxz"></param>
        /// <param name="startDate">Ngày bắt đầu</param>
        /// <param name="endDate">Ngày kết thúc</param>
        /// <returns></returns>
        List<OutPatChargeItemVO> GetOutPatGhlx(int patid, string brxz, string startDate, string endDate);

        /// <summary>
        /// Khoa có hiệu lực hay không
        /// </summary>
        /// <param name="ks">Khoa</param>
        /// <param name="mjzbz">Cờ khám ngoại trú/cấp cứu</param>
        /// <param name="parmJdrq">Ngày lập hồ sơ</param>
        /// <returns></returns>
        bool getActiveDuration(string ks, string mjzbz, object parmJdrq);

        /// <summary>
        /// Lấy thông tin bác sĩ đăng ký khám hiện tại
        /// </summary>
        /// <param name="ghnm">Mã nội bộ đăng ký khám</param>
        /// <param name="parmKs">Khoa</param>
        /// <param name="type">3</param>
        /// <param name="mjzbz">Cờ khám ngoại trú/cấp cứu</param>
        /// <param name="Isyzybzz">Cờ BHYT</param>
        /// <param name="Istxzzys">Cờ bệnh nặng</param>
        /// <returns></returns>
        OutPatChargeDoctorVO getDoctorInfo(int ghnm, string parmKs, int type, string mjzbz, bool Isyzybzz, bool Istxzzys);

        /// <summary>
        /// Lấy thuốc và hạng mục thu phí
        /// </summary>
        /// <param name="keyword">Từ khóa tra cứu,py,mc</param>
        /// <returns></returns>
        List<ChargeItemDetailVO> getYpItemList(string keyword, string yfPz, string OrganizeId);

        /// <summary>
        /// Lấy số đơn thuốc
        /// </summary>
        /// <returns></returns>
        string getCflsh();

        /// <summary>
        /// Lấy số tiền phụ phí
        /// </summary>
        /// <param name="parmBrxz">Tính chất bệnh nhân</param>
        /// <param name="parmDl">Loại lớn</param>
        /// <param name="parmSfxm">Hạng mục thu phí</param>
        /// <param name="parmDJ">Đơn giá</param>
        /// <returns></returns>
        decimal? Calcfwfjm(string parmBrxz, string parmDl, string parmSfxm, string parmDJ);

        /// <summary>
        /// Quyết toán thu phí ngoại trú Giao dịch
        /// </summary>
        /// <param name="vo"></param>
        void OutPatChargeSettDB(OutPatChargeSettDataVo vo);

        /// <summary>
        /// Tính ra thông tin quyết toán ngoại trú getMzJs
        /// </summary>
        void GetMzJsInfo(SettlementEntityVO jsEntity, SysPatientNatureEntity brxzEntity, out OutpatientSettlementEntity mzjs, out List<OutpatientSettlementCategoryEntity> jsdl);


        /// <summary>
        /// Lấy chi tiết quyết toán
        /// </summary>
        /// <param name="mzjs"></param>
        /// <param name="jsEntity"></param>
        /// <returns></returns>
        List<OutpatientSettlementDetailEntity> getJSMX(OutpatientSettlementEntity mzjs, SettlementEntityVO jsEntity);

        List<OutpatientSettlementDetailEntity> getJSMX(SettlementEntityVO jsEntity, string orgId);

        /// <summary>
        /// Phương thức thanh toán quyết toán ngoại trú
        /// </summary>
        /// <param name="jsnm">Mã nội bộ quyết toán</param>
        /// <param name="xjzffs">Phương thức thanh toán</param>
        /// <param name="xjzffsbh"></param>
        /// <param name="ysk">Khoản phải thu</param>
        /// <returns></returns>
        OutpatientSettlementPaymentModelEntity GetMzJsZffs(int jsnm, string xjzffs, string xjzffsbh, decimal ysk);

        /// <summary>
        /// Ghi sổ ngoại trú Lấy thông tin cơ bản bệnh nhân(Ghi sổ nội trú trích dẫn sửa đổi tuổi chú ý chính)
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="blh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatAccInfoDto> GetChargePatInfoInAcc(string kh, string IsBlh, string mzh, string orgId);

        /// <summary>
        /// Tra cứu thông tin chi tiết bệnh nhân theo số ngoại trú(Thông tin bệnh nhân ở góc trên bên phải thu phí ngoại trú)
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatAccInfoDto> GetOutpatChargePatInfoInAcc(string mzh, string kh,string zjh, string cardType, string orgId);

        /// <summary>
        /// Tra cứu thông tin chi tiết bệnh nhân theo số thẻ và thời gian đăng ký khám(Thông tin bệnh nhân ở góc trên bên phải thu phí ngoại trú)
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="kh"></param>
        /// <param name="cardType"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatAccInfoDto> GetpatientAccountList(DateTime kssj, DateTime jssj, string kh,string zjh, string cardType, string orgId);

        /// <summary>
        /// Theo số thẻ,Lấy thông tin cơ bản bệnh nhân theo số bệnh án và số giấy tờ
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="blh"></param>
        /// <param name="zjh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OutpatAccInfoDto GetChargePatInfoInRegister(string kh, string blh, string zjh, string orgId, string cardType,string ly,string CardId);



        /// <summary>
        /// Giao dịch quyết toán ghi sổ ngoại trú
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="jzjhEntity">Kế hoạch ghi sổ</param>
        /// <param name="jzmxlist">Chi tiết kế hoạch ghi sổ</param>
        void OutPatChargeSettDBInAcc(OutPatChargeSettInAccDataVo vo, OutpatientAccountEntity jzjhEntity,
           List<OutpatientAccountDetailEntity> jzmxlist, Dictionary<string, string> optimaId);


        #region optima记账
        /// <summary>
        /// Theo ngoại trú/Lấy thông tin bệnh nhân theo số nhập viện
        /// </summary>
        /// <param name="admsNum"></param>
        /// <param name="orgId"></param>
        /// <param name="type">Loại bệnh nhân:Khám ngoại trú/Nhập viện</param>
        /// <returns></returns>
        OutpatAccInfoDto GetPatInfoInOptima(string admsNum, string orgId, string type);

        /// <summary>
        /// Lấy hạng mục ghi sổ ngoại trú lịch sử theo số ngoại trú
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OptimAccInfoDto> GetOutAccountInfo(string mzh, string orgId, string kssj, string jssj, string rygh);

        /// <summary>
        /// Lấy hạng mục ghi sổ nội trú lịch sử theo số nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OptimAccInfoDto> GetintAccountInfo(string zyh, string orgId, string kssj, string jssj, string rygh);
        /// <summary>
        /// optimaGửi
        /// </summary>
        /// <param name="outaddvo">Thêm ghi sổ ngoại trú</param>
        /// <param name="outupdatevo">Sửa ghi sổ ngoại trú</param>
        /// <param name="outdelvo">Xóa ghi sổ ngoại trú</param>
        /// <param name="inaddvo">Thêm ghi sổ nội trú</param>
        /// <param name="inupdatevo">Sửa ghi sổ nội trú</param>
        /// <param name="indelvo">Xóa ghi sổ nội trú</param>
        /// <param name="orgId"></param>
        void PatsettDBInOptima(SettInAccOutpatDataVo outaddvo,
           updateInAccOutpatVo outupdatevo,
           delInAccOutpatVo outdelvo,
           SettInAccHospatDataVo inaddvo,
           updateInAccHospatVo inupdatevo,
           delInAccHospatVo indelvo,
           string orgId,
           List<int> cfmxIds,
             Dictionary<string, string> optimaIds);
        #endregion

        #region 门诊收费 Vision-1.3 门诊记账的第三个版本
        /// <summary>
        ///  Lấy tất cả thông tin đăng ký khám bệnh nhân
        /// </summary>
        IList<OutPatAccountingDto> GetRegisterGridJson(string begindate, string enddate, string orgId, string usercode);

        /// <summary>
        /// Lấy hạng mục thu phí lịch sử theo số khám ngoại trú
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OptimAccInfoDto> GetpatientAccountInfo(string mzh, string orgId);

        /// <summary>
        /// Lưu ghi sổ,Lưu CSDL
        /// </summary>
        /// <param name="vo">Tập hợp đối tượng thêm mới</param>
        /// <param name="updatevo">Tập hợp đối tượng sửa đổi</param>
        /// <param name="jsnmList">Tập hợp mã nội bộ quyết toán đã sửa và xóa</param>
        /// <param name="delvo">Tập hợp đối tượng xóa</param>
        /// <param name="orgId"></param>
        void OutPatsettDBInCharge(OutPatSettInAccDataVo vo, updatePatSettInAccDataVo updatevo, List<int> jsnmList, DelPatSettInAccDataVo delvo, string orgId);
        #endregion

        #region 门诊记账，第四个版本，增加单位治疗量逻辑
        /// <summary>
        /// Lấy hạng mục thu phí lịch sử theo số khám ngoại trú,Không bao gồm thuốc và đã hoàn thành,Kế hoạch ghi sổ đã dừng
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OptimAccInfoDto> GetpatientAccountInfoV4(string mzh, string orgId);
        /// <summary>
        /// Lưu ghi sổ,Lưu CSDL
        /// </summary>
        /// <param name="vo">Tập hợp đối tượng thêm mới</param>
        /// <param name="updatevo">Tập hợp đối tượng sửa đổi</param>
        /// <param name="jsnmList">Sửa tập hợp mã nội bộ quyết toán</param>
        /// <param name="orgId"></param>
        void OutPatsettDBInCharge(OutPatSettInAccDataVo4 vo, List<OutpatientItemExeEntity> tempVo, string orgId);

        #endregion

        #region 收费2018
        object GetPatientGridJson(string orgId);

        /// <summary>
        /// Lấy bản ghi quyết toán quyết toán trước khi nợ phí
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OutpatientSettArrearageVO> GetOutpatientSettArrearageVOList(string mzh, string orgId);

        /// <summary>
        /// Theojsnm Lấy danh sách chi tiết quyết toán
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OutpatientSettDetailVO> GetOutpatientSettDetailList(int jsnm, string orgId);

        /// <summary>
        /// Lấy đơn thuốc chưa thu phí theo số ngoại trú
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ChargeLeftDto> GetPrescriptionBymzh(string mzh, string orgId);

        IList<ChargeRightDto> GetPrescriptionDetailBycfnm(IList<int> cfnmList, string orgId);
        IList<ChargeLeftDto> GetNewUnSettedPrescriptionByMzh(string mzh, string orgId);

        /// <summary>
        /// Lấy chờ quyết toán
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ChargeRightDto> GetAllUnSettedListListByMzh(string mzh, string orgId);
        IList<ChargeRightDto> GetNewAllUnSettedListByMzh(string mzh, string cfnms, string orgId);
        /// <summary>
        /// Khi hoàn phí thì tải lên lại để lấy chi tiết
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<GuiAnChargeRightDto> GetAllUnSettedListListByJsnm(string jsnm, string orgId);

        /// <summary>
        /// Lấy hạng mục chờ quyết toán(Không kê đơn)
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ChargeRightDto> GetAllUnSettedSingleItemListByMzh(string mzh, string orgId);

        /// <summary>
        /// Lấy tất cả chưa quyết toán Bảo hiểm nông thôn mới của bệnh nhân được chỉ địnhoutpId
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<string> SelectNotSettedXnhOutpId(int patid, string organizeId);

        /// <summary>
        /// BHYT Quý An,Lấy dữ liệu khi tải lên ngoại trú(Thông tin đầu)
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        GuiAnMzjsPatInfoDto GetGuiAnMzjsPatInfoDto(string mzh, string orgId);

		#endregion

		#region 重庆医保
	    IList<UploadPrescriptionsListInPut> GetCQYBUnSettedListByMzh(Pagination pagination, string mzh, string orgId);
	    IList<Input_2204> GetCQZFUnSettedListByMzh(string mzh, string cfnm, string orgId);
	    Input_2203A GetCQjzdjInfo(string mzh, string orgId);
		IList<Input_2204> GetChongQingGHMzjs(string ghxm, string zlxm, bool isCkf, bool isGbf, string orgId = null);
	    IList<UploadPrescriptionsListInPut> GetCQYBTfListByJsnm(string jsnm, string mzh,  string orgId);
	    IList<UploadPrescriptionsListInPut> GetCQZFTfListByJsnm(string jsnm, string mzh,  string orgId);
        IList<TbbzmlDto> GetMzbzml(string mllx);
        #endregion

        #region 结算查询

        /// <summary>
        /// Tra cứu quyết toán
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        IList<OutpatientSettlementVO> GetSettlementList(Pagination pagination, string orgId, string keyword, DateTime? kssj, DateTime? jssj, bool containsTF);

        #endregion

        /// <summary>
        /// Lấy thông tin cơ bản bệnh nhân hệ thống theo số ngoại trú
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<SysPatientBasicInfoEntity> SelectSysPatientBasicInfoEntities(string mzh, string orgId);
    }
}
