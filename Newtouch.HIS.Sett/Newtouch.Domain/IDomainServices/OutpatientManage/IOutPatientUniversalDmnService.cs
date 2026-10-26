using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Proxy.guian.DTO.S25;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Một số phương pháp chung ngoại trú
    /// Chắc chắn không phân biệt phiên bản
    /// </summary>
    public interface IOutPatientUniversalDmnService
    {
        /// <summary>
        /// Gửi chi tiết(Quan trọng:Nếu là thêm vào đơn thuốc hoặc hủy chi tiết,Theo nghĩa ngoại tuyến thì đây là thay đổi đơn thuốc này,Bác sĩ thường không làm vậy)(Nếu là đơn thuốc thì cập nhật thông tin bảng chính)(Vui lòng tạo trước tham số yêu cầu của nó)(Theo khóa chính chi tiếtxmnm,cfmxIdDùng giá trị để xác định đây là thêm mới,Hay là bản ghi sửa đổi)(Khi là thêm mới,TheocfnmTrường,Lại chia thành thêm mới chi tiết và đơn thuốc,Hay là thêm chi tiết vào đơn thuốc đã có)(Nếu sửa thì cập nhật trực tiếp bản ghi chi tiết CSDL/Phiên bản này tạm thời không hỗ trợ,Nếu không thì vui lòng đi hoàn phí)
        /// </summary>
        /// <param name="orgId">Cơ sở y tếId</param>
        /// <param name="changedCfnmList">Dùng để trả về</param>
        /// <param name="changedXmnmList">Dùng để trả về(Nếu hạng mục không liên kết đơn thuốc)</param>
        /// <param name="updateSkipSettledCfhList">(Đây là một trả về)Cập nhật đơn thuốc đã quyết toán bị bỏ quacfh List</param>
        /// <param name="mzh">Nếu cần thêm mới,Đã chuẩn bị sẵn cho bạn</param>
        /// <param name="ys">Nếu cần thêm mới,Đã chuẩn bị sẵn cho bạn(Khi chưa chỉ định thì đi đăng ký khámys)</param>
        /// <param name="ItemsGroupBOList">Nhóm thông tin tính phí(Thêm đơn thuốc mới/Thêm chi tiết đơn thuốc mới/Thêm chi tiết hạng mục mới)Thông tin đơn thuốc/Thông tin chi tiết đơn thuốc/Thông tin hạng mục</param>
        /// <param name="settledUpdateForbidden">Khi đã quyết toán Có cấm cập nhật hay không</param>
        /// <param name="settledUpdateSkip">(Khi đã quyết toán thì cấm cập nhật)Kiểm tra có hay khôngSkip,trueKhông ném ngoại lệ, bỏ qua,Nếu không thì ném ngoại lệ</param>
        /// <param name="cfly">Nguồn đơn thuốc(0 Nơi thu phí 1 Trạm bác sĩ)</param>
        void SubmitItems(
            string orgId
            , out IList<int> changedCfnmList, out IList<int> changedXmnmList
            , out IList<string> updateSkipSettledCfhList
            , string mzh = null, string ys = null
            , IList<FeeItemsGroupedBO> ItemsGroupBOList = null
            , bool settledUpdateForbidden = true
            , bool settledUpdateSkip = false
            , string cfly = null);

        /// <summary>
        /// Cập nhật đơn thuốc ngoại trú/Thông tin quyết toán hạng mục ngoại trú(Cập nhật thông tin quyết toán)(NàycfnmCó thể đã hủy,Mục đích có thể cũng là hủy thông tin quyết toán)(Trước khi gọi phương thức này,Vui lòng cập nhật trướcmz_cfmx,mz_xm,Và đã được lưu bền vào CSDL)TheocfnmTìm ra tất cả các bảng liên quanmz_js mz_jsmx mz_cf mz_cfmx mz_xm,Theomz_cfmx,mz_xmDùng dữ liệu đúng để cập nhật các bảng khác(Có thể sẽ thêm mớimz_jsmx)(Có thể sẽ thêm mớimz_js)(check mz_xm,mz_cf zt 0,Tương ứngmz_jsmx Cũng cần cập nhật)(Nhiềumz_cfCó thể tương ứng với một lần quyết toán)(Nhưng mộtmz_cfKhông thể tương ứng với nhiều lần quyết toán)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="settlementAddBO"></param>
        /// <param name="feeRelated">Số tiền và thông tin thanh toán</param>
        /// <param name="ybfeeRelated">Chi phí liên quan BHYT</param>
        /// <returns>Trả về mã nội bộ quyết toán mới tạoList</returns>
        IList<int> AddSettlement(string orgId, string mzh
            , OutpatientSettlementAddBO settlementAddBO
            , OutpatientSettFeeRelatedDTO feeRelated = null
            , CQMzjs05Dto ybfeeRelated = null
            , S25ResponseDTO xnhybfeeRelated = null
            , string outTradeNo = null);

        /// <summary>
        /// Hoàn phí ngoại trú(Không cần liên kết đơn thuốc ngoại trú,Hạng mục)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict">Chi tiết hủy quyết toánList jsmxnm:Số lượng trả lại Số lượng trả lại Số dương</param>
        /// <param name="expectedTmxZje">Tổng số tiền hạng mục hoàn phí dự kiến(Đơn giá*Số lượng trả lại),Ngăn dữ liệu thay đổi trong quá trình</param>
        /// <param name="newfph">Số hóa đơn quyết toán mới</param>
        /// <param name="newJszbInfo">Thông tin quyết toán mới được tạo</param>
        /// <param name="outTradeNo">Số giao dịch thanh toán ban đầu</param>
        /// <param name="refundAmount">Số tiền mặt trả lại(Tiền mặt,Alipay,WeChat v.v.)</param>
        /// <param name="hcybjsh">Hoàn phí.Giao dịch BHYT.Số quyết toán đảo hóa đơn đỏ</param>
        /// <param name="newwjybjsh">Hoàn phí.Giao dịch BHYT.Số quyết toán mới</param>
        /// <param name="hcybfeeRelated">Quyết toán đảo hóa đơn đỏ của Liên quan số tiền BHYT quyết toán</param>
        /// <param name="newybfeeRelated">Hoàn phí.Giao dịch BHYT.Quyết toán mới Thông tin chi phí BHYT quyết toán trả về</param>
        /// <param name="guianMztfProDto"></param>
        /// <param name="s25ResponseDto">BHYT Bảo hiểm nông thôn mới Quý An sử dụng,Kết quả quyết toán trả về</param>
        /// <param name="outpId">BHYT Bảo hiểm nông thôn mới Quý An sử dụng,Số thứ tự bồi thường ngoại trú</param>
        void RefundSettlement(string orgId, int jsnm
            , Dictionary<int, decimal> tjsxmDict, decimal expectedTmxZje
            , string newfph
            , out object newJszbInfo, out string outTradeNo, out decimal refundAmount, string hcybjsh = null, string newwjybjsh = null
            , CQMzjs05Dto hcybfeeRelated = null
            , OutpatientSettYbFeeRelatedDTO newybfeeRelated = null
            , CQMzjs05Dto guiannewybfeeRelated = null
            , GuiAnMztfProDto guianMztfProDto = null
            , S25ResponseDTO s25ResponseDto = null
            , string outpId = "");

        /// <summary>
        /// Kế hoạch hoàn phí dừng toàn bộ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jsnm"></param>
        void AccountPlanRefundAll(string orgId, int jsnm);

        /// <summary>
        /// Hủy đơn thuốc(Đã quyết toán thì chắc chắn không thể hủy)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfnmList">Mã nội bộ đơn thuốcList</param>
        /// <param name="cfhList">Số đơn thuốcList</param>
        /// <param name="settledUpdateForbidden">Khi đã quyết toán Có cấm cập nhật hay không</param>
        void CancelPrescription(string orgId
            , IList<int> cfnmList = null
            , IList<string> cfhList = null
            , bool settledUpdateForbidden = true);

        /// <summary>
        /// Theo số ngoại trú v.v. Tra cứu số đơn thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="cfzt"></param>
        /// <param name="cfly"></param>
        /// <param name="cfhs">Nhiều số đơn thuốc,Tách bằng dấu phẩy</param>
        /// <returns></returns>
        IList<OutpatientPrescriptionEntity> GetValidCfListByMzh(string orgId, string mzh
            , string cfzt = null, string cfly = null, string cfhs = null);

        /// <summary>
        /// Cập nhật bản ghi quyết toán trước khi nợ phí thành đã thu phí
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jsnm"></param>
        /// <param name="feeRelated">Số tiền và thông tin thanh toán</param>
        /// <param name="sfrq"></param>
        /// <param name="fph"></param>
        void UpdateArrearageSettlement(string orgId, int jsnm
            , OutpatientSettFeeRelatedDTO feeRelated
            , DateTime sfrq
            , string fph = null);

        /// <summary>
        /// Lấy quyết toán ngoại trú/Hoàn phí Thông tin xuất viện của bệnh nhân(Số quyết toán BHYT,Số BHXH,Lý do khám,Khoa,Bác sĩ,Chẩn đoán)
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OutPatChargePatientInfoVO GetSettPatientInfo(string mzh, string orgId);
        /// <summary>
        /// Lấy quyết toán thu phí Chi tiết bộ
        /// </summary>
        /// <param name="cfnms"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<RefundZtDot> RefundZtDetail(string cfnms, string orgId);

        CheckApplicationfromDTO pushApplicationform(BasicInfoDto2018 bacDto, int cfnm, string orgid, string typezt);

        CheckApplicationfromDTO retApplicationform(string mzh, string cfnmList, string orgid, string typezt);

        void UpdateYPcfxm(List<OutGridInfoDto2018> deletelist, string orgId, string usercode);

        int pushApplicationformRef(string cfh, string orgid,int sqdzt);

		/// <summary>
		///  Số lượng tồn kho đã trừ của hạng mục vật tư đã lập Số lượng đã trừ phần đóng băng
		/// </summary>
		/// <param name="cfh"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		int Sumwzdj(string[] cfh, string orgId, string rygh);

		/// <summary>
		///  Số lượng tồn kho đã trừ của hạng mục vật tư đã lập Số lượng đã trừ phần đóng băng
		/// </summary>
		/// <param name="cfh"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		int TuiHuiwzdj(string[] cfh, string orgId, string rygh);
	}
}
