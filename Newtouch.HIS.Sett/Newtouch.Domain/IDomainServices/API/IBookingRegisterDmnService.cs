using Newtouch.HIS.Domain.ValueObjects.API;
using System.Collections.Generic;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Sett.Request;
using Newtouch.HIS.Sett.Request.Booking.Response;
using Newtouch.HIS.Sett.Request.Booking.Request;
using Newtouch.HIS.Domain.DTO.InputDto;

namespace Newtouch.HIS.Domain.IDomainServices.API
{
    public interface IBookingRegisterDmnService
    {
        IList<CardInfoResp> GetCardInfo(CardInfoReq req);

        /// <summary>
        /// Lấy thông tin khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ks"></param>
        /// <param name="ksmc"></param>
        /// <returns></returns>
        IList<DepartmentResp> GetDepartmentInfo(DepartmentDTO dto);
        /// <summary>
        /// Lịch trực ngoại trú schedule
        /// </summary>
        /// <returns></returns>
        IList<MzpbScheduleResponse> GetMzpbSchedule(DepartmentSchedulingDTO dto);
        /// <summary>
        /// Thông tin lịch trực khoa
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        IList<MzpbResponse> GetMzpb(MzKsPbDto dto);
        /// <summary>
        /// Chi tiết lịch trực khoa khám bệnh
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        IList<MzpbScheduleResponse> GetMzpbDetail(MzKsPbDto dto);
        /// <summary>
        /// Hẹn đăng ký khám
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        MzAppointmentResp OutAppointment(MzAppointmentReq dto);
        /// <summary>
        /// Điểm danh
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        RegSettResp OutpatRegSett(RegSettReq dto);
        /// <summary>
        /// Tra cứu thông tin hẹn khám theo số hẹn khám
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        MzAppointmentRecordResp QueryAppRecord(MzAppointmentRecordReq req);

        /// <summary>
        /// Hẹn khámlist
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<MzAppointmentRecordListResp> QueryAppRecordList(MzAppointmentRecordListReq req);
        /// <summary>
        /// Hủy hẹn khám
        /// </summary>
        /// <param name="req"></param>
        int CancelOutApp(MzAppointmentRecordReq req);
        /// <summary>
        /// Lấy bác sĩ/Bác sĩ khoa
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<DeptDoctorResp> GetStaffInfo(DeptDoctorReq req);
        /// <summary>
        /// Lập thẻ
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        CardInfoResp SysPatInfoSet(RegisterReq req);
        /// <summary>
        /// Hủy quyết toán
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        CancalSettResp CancalSett(CancalSettReq req);
        /// <summary>
        /// Đơn hàng chờ nộp phí
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<CostOrderResp> QueryCostOrder(CostOrderReq req);
        /// <summary>
        /// Chi tiết đơn hàng chờ nộp phí
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        CostOrderDetailResp QueryCostOrderDetail(CostOrderDetailReq req);
        /// <summary>
        /// Hủy đơn hàng
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        int CancalOrde(CancalOrderReq req);
        /// <summary>
        /// Đặt lại trạng thái đơn hàng
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        int ErrOrderCancal(CancalOrderReq req);
        /// <summary>
        /// Quyết toán trước
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        PreSettResp OutPreSett(PreSettReq req);
        /// <summary>
        /// Quyết toán đơn thuốc
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        void OutSett(SettReq req, int jsnm,string orgId);
        /// <summary>
        /// Thông tin đối soát
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<ContrastBill> ContrastBill(ContrastBillReq req);

        string ValidFee(string mzh, string orgId);
        List<CfOrderVo> GetOrderAmount(string mzh, string orgId);
        BasicInfoDto2018 getPatInfo(string mzh, string orgId);
        List<int> getCfnmList(string mzh, string orgId);
    }
}
