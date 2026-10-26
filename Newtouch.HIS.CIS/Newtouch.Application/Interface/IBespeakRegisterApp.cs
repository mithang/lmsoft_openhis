using Newtouch.Domain.Entity;
using Newtouch.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Đặt lịch hẹn khám
    /// </summary>
    public interface IBespeakRegisterApp
    {
        /// <summary>
        /// Sửa đổi quản lý đặt lịch hẹn
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        string UpdateBespeakRegisterManage(List<SysBespeakRegisterEntity> entities);

        /// <summary>
        /// Lấy thời gian đặt lịch hẹn khám mặc định
        /// </summary>
        /// <param name="sourceDate"></param>
        /// <returns></returns>
        List<SysBespeakRegisterDateTimeVO> GetRegisterDateByWeek(DateTime sourceDate);

        /// <summary>
        /// Lấy khoảng thời gian đặt lịch hẹn và tổng số có thể đặt trong cấu hình hệ thống
        /// </summary>
        /// <returns></returns>
        List<SysBespeakRegisterTimeVO> GetConfigureRegTime();

        /// <summary>
        /// Ghép HTML hàng bổ sung
        /// </summary>
        /// <param name="regDatas"></param>
        /// <param name="regTimes"></param>
        /// <returns></returns>
        string BuildNewRow(List<SysBespeakRegisterDateTimeVO> regDatas, List<SysBespeakRegisterTimeVO> regTimes);

        /// <summary>
        /// Gửi chỉnh sửa đặt lịch hẹn khám
        /// </summary>
        /// <param name="regData"></param>
        /// <returns></returns>
        string SubmitForm(List<SysBespeakRegisterEntity> regData);

        /// <summary>
        /// Xóa đặt lịch hẹn khám đã quá hạn
        /// </summary>
        void DeleteExpireRegData();

        /// <summary>
        /// Lấy thông tin đặt lịch hẹn khám của khoa
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        string GetRegDataHtmlByKs(string deptCode);

        /// <summary>
        /// Lấy đặt lịch hẹn khám mặc định của khoa
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        string GetDefaultHtmlByKs(string deptCode);

        /// <summary>
        /// Ghép thông tin đặt lịch hẹn khám theo khoa và mã nhân viên
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        string GetRegDataHtmlByKsAndGh(string deptCode, string gh);

        /// <summary>
        /// Lấy tổng số người có thể đặt và số đã đặt
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <param name="regTime"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        object GetBespeakMaxCount(string deptCode, DateTime regDate, string regTime, string gh);

        /// <summary>
        /// Lưu đặt lịch hẹn khám
        /// </summary>
        /// <param name="yyId"></param>
        /// <param name="brId"></param>
        /// <param name="blh"></param>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <returns></returns>
        string SaveYY(string yyId, string brId, string blh, int? zjlx, string zjh);

        /// <summary>
        /// Sinh lịch đặt lịch hẹn
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="deptCode"></param>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        string BuildCalendar(int year, int month, string deptCode, string ysgh);
    }
}
