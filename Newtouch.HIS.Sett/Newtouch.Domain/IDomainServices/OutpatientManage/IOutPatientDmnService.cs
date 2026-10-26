using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Bao gồm Một phần chức năng trang ghi sổ ngoại trú
    /// </summary>
    public interface IOutPatientDmnService
    {
        /// <summary>
        /// Lịch trực đăng ký khámList
        /// </summary>
        /// <param name="keyword">Khoa/Bác sĩ</param>
        /// <param name="mzlx">Loại ngoại trú Khám ngoại trú thông thường/Cấp cứu/Khám chuyên gia</param>
        /// <returns></returns>
        List<RegScheduleVO> GetRegScheduleList(string keyword, string mzlx, string orgId);

        /// <summary>
        /// Lấy lịch trực mớiList
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="mzlx"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<RegNewScheduleVO> GetNewRegScheduleList(string keyword, string mzlx, string orgId);
        /// <summary>
        /// Lấy lịch trực đăng ký khám mới(Tổ hợp điều trị)
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="mzlx"></param>
        /// <param name="pbks"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<RegNewScheduleVO> GetRegScheduleListbyGroup(string keyword, string mzlx, string pbks, string orgId, DateTime? OutDate, string ys);
        /// <summary>
        /// Đã hẹn đăng ký khámlist
        /// </summary>
        /// <param name="mzxl"></param>
        /// <param name="patid"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<MzghbookScheduleVO> GetIsMzghBookSchedule(Pagination pagination,string mzxl, string patid, string orgId,string isfeegroup, string yyzt, string ghly, string keyValue,
           string ks, DateTime? yykssj = null, DateTime? yyjssj = null);

        string GetInvoiceListByEmpNo(string userCode, string orgId);

        /// <summary>
        /// checkSố hóa đơn đã được dùng hay chưa, Đã được dùng thì trả vềtrue
        /// </summary>
        /// <param name="invoiceNo">Số hóa đơnNo</param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool ExistsForInvoiceNo(string invoiceNo, string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        OutPatientBasicInfoVO GetBasicInfoByCardNo(string keyValue);

        Int16 GetJzxh(string ghlx, string ks, string ys, string ghzb, string userCode, string orgId);
        List<SysPatiNatureEntity2VO> GetValidBrxzList(string text, string ghlx = "", string ybtsdy = "", string syybbf = "", string fw = "", List<string> brxzList = null);

        /// <summary>
        /// Lấy danh sách lịch trực đăng ký khám
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="ghpbId"></param>
        /// <returns></returns>
        List<OutpatientRegistScheduleVO> GetOutpatientRegistScheduleList(string orgId, string keyword = null, int? ghpbId = null);
		/// <summary>
		/// Máy tự phục vụ lấy lịch trực đăng ký khám
		/// </summary>
		/// <param name="mzlx"></param>
		/// <param name="pbrq"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		List<zzjRegScheduleVO> GetZzjRegScheduleList(DateTime pbrq, string orgId);
		#region 门诊记账

		/// <summary>
		/// Thao tác lưu ghi sổ ngoại trú
		/// </summary>
		/// <param name="bacDto"></param>
		/// <param name="accDto"></param>
		/// <param name="orgId"></param>
		/// <param name="isOptimaApi">Có hay khôngOptimaGhi sổ tương ứng với giao diện,Mặc định để trống,1Thể hiện là có(Kế hoạch ghi sổ không xử lý dữ liệu),nullvà0Thể hiện là không</param>
		/// <param name="isClinic">Có phải là ghi sổ phòng khám hay không,Mặc định để trống,1Thể hiện là có(Nhắc số lần bảo hiểm thương mại),nullvà0Thể hiện là không(Nhắc số lần BHYT)</param>
		void SaveOutpatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId, string isOptimaApi = null, string isClinic = null);


        #endregion

        /// <summary>
        /// Ghi sổ ngoại trú Lưu gán giá trị thực thể quyết toán
        /// </summary>
        /// <param name="brxz"></param>
        /// <param name="gridDto"></param>
        /// <param name="jsEntity"></param>
        /// <param name="jzjhmxEntity"></param>
        /// <param name="jzjhEntity"></param>
        /// <param name="orgId"></param>
        /// <param name="patid"></param>
        /// <param name="bacDto"></param>
        /// <param name="mzjsList"></param>
        /// <param name="jzsjList"></param>
        /// <returns></returns>
        OutPatChargeSettInAccDataVo GetOutPatChargeAccuDbvo(int patid, string brxz,
           IList<OutpatAccGridInfoDto> gridDto, out SettlementEntityVO jsEntity,
           out List<OutpatientAccountDetailEntity> jzjhmxEntity, OutpatientAccountEntity jzjhEntity, string orgId,
           OutpatAccBasicInfoDto bacDto, out List<OutpatientSettlementEntity> mzjsList, out List<DateTime> jzsjList, out Dictionary<string, string> optimaId, string isOptimaApi = null, string isClinic = null);

        #region optima记账 包括门诊记账和住院记账，不包含结算表和记账计划 Vision-1.2 门诊记账的第二个版本
        void CommitAccounting(string patientType, OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId);
        #endregion
        #region 门诊收费 Vision-1.3 门诊记账的第三个版本
        /// <summary>
        /// Lưu ghi sổ ngoại trú
        /// </summary>
        void SavepatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId);
        #endregion

        #region Vision-1.4 门诊记账第四个版本 增加临时和长期，单次治疗量
        /// <summary>
        /// Lưu ghi sổ ngoại trú
        /// </summary>
        void SaveoutpatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId, string userCode);
        /// <summary>
        /// Dừng kế hoạch ngoại trú
        /// </summary>
        /// <param name="db"></param>
        /// <param name="jzjhmxId"></param>
        /// <param name="orgId"></param>
        void overAccountingPlan(string jzjhmxId, string orgId, string UserCode);

        /// <summary>
        /// Dừng kế hoạch ghi sổ ngoại trú(mz_jzjhmx)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="jzjhmxId"></param>
        /// <param name="orgId"></param>
        void overjzjhmx(string jzjhmxId, string orgId);

        /// <summary>
        /// Khi kế hoạch chưa thực hiện,Sửa kế hoạch ghi sổ
        /// </summary>
        /// <param name="jzjhmxId"></param>
        /// <param name="orgId"></param>
        /// <param name="type">1Thể hiện Khám ngoại trú 2Thể hiện nội trú</param>
        void delAccountingPlan(Infrastructure.EF.EFDbTransaction db, string jzjhmxId, string orgId, string type);

        /// <summary>
        /// Tra cứu kế hoạch ghi sổ
        /// </summary>
        List<AccountingPlanVO> SelectAccountingPlanList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj
            , int? zxzt, string orgId, int? zsftx, int? sycstx
            , string sfzt);


        /// <summary>
        /// Lưu kế hoạch ghi sổ và chi tiết kế hoạch ghi sổ vào CSDL
        /// </summary>
        /// <param name="jzjh"></param>
        /// <param name="jzjhmxList"></param>
        void OutpatientAccountDBInCharge(OutpatientAccountEntity jzjh, List<OutpatientAccountDetailEntity> jzjhmxList, List<OutpatientItemEntity> mz_xmList);
        /// <summary>
        /// Số lần còn lại của bảo hiểm
        /// </summary>
        /// <param name="jzsjList"></param>
        /// <param name="orgId"></param>
        /// <param name="patid"></param>
        void Execsycs(List<DateTime> jzsjList, string orgId, string patid);
        #endregion



        #region 根据门诊号查询关联处方（但不包括zt=0处方）

        /// <summary>
        /// Tra cứu đơn thuốc thuốc liên kết theo số ngoại trú(Chưa phát thuốc Hoàn phí)(Nhưng không bao gồmzt=0Đơn thuốc)
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <param name="refundDict"></param>
        /// <returns></returns>
        IList<OutpatientMedicineRefundVO> GetRelatedYpDetailListByMzh(string mzh, string orgId, Dictionary<int, decimal> refundDict);

		#endregion

		#region  chongqing医保

	    List<RefundPrescriptionsInPut> GetCqyb10Data(string zymzh, string orgId, string type);
        List<Input_2205> GetCqybMxCxData(string zymzh, string orgId, string type, string ybver);

        #endregion

    }
}
