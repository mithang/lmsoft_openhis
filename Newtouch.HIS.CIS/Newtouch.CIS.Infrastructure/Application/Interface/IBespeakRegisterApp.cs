using Newtouch.Domain.Entity;
using Newtouch.Domain.ViewModels;
using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// đặt hẹn đăng ký khám
    /// </summary>
    public interface IBespeakRegisterApp
    {
        /// <summary>
        /// Sửa đặt hẹn Quản lý
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        string UpdateBespeakRegisterManage(List<SysBespeakRegisterEntity> entities);

        /// <summary>
        /// Lấy Mặc định của đặt hẹn đăng ký khám Thời gian
        /// </summary>
        /// <param name="sourceDate"></param>
        /// <returns></returns>
        List<SysBespeakRegisterDateTimeVO> GetRegisterDateByWeek(DateTime sourceDate);

        /// <summary>
        /// Lấy Hệ thống cấu hình đặt hẹn Thời gian đoạn và có thể đặt hẹn tổng số
        /// </summary>
        /// <returns></returns>
        List<SysBespeakRegisterTimeVO> GetConfigureRegTime();

        /// <summary>
        /// lắp ráp đuổi thêm dòng HTML
        /// </summary>
        /// <param name="regDatas"></param>
        /// <param name="regTimes"></param>
        /// <returns></returns>
        string BuildNewRow(List<SysBespeakRegisterDateTimeVO> regDatas, List<SysBespeakRegisterTimeVO> regTimes);

        /// <summary>
        /// Gửi đặt hẹn đăng ký khám Sửa
        /// </summary>
        /// <param name="regData"></param>
        /// <returns></returns>
        string SubmitForm(List<SysBespeakRegisterEntity> regData);

        /// <summary>
        /// Xóa qua kỳ đặt hẹn đăng ký khám
        /// </summary>
        void DeleteExpireRegData();

        /// <summary>
        /// Lấy Khoa đặt hẹn đăng ký khám thông tin
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        string GetRegDataHtmlByKs(string deptCode);

        /// <summary>
        /// Lấy Khoa Mặc định đặt hẹn đăng ký khám
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        string GetDefaultHtmlByKs(string deptCode);

        /// <summary>
        /// Theo Khoa và Mã nhân viên lắp ráp đặt hẹn đăng ký khám thông tin
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        string GetRegDataHtmlByKsAndGh(string deptCode, string gh);

        /// <summary>
        /// Lấy có thể đặt hẹn tổng người số và đã đặt hẹn số
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <param name="regTime"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        object GetBespeakMaxCount(string deptCode, DateTime regDate, string regTime, string gh);

        /// <summary>
        /// Lưu đặt hẹn đăng ký khám
        /// </summary>
        /// <param name="yyId"></param>
        /// <param name="brId"></param>
        /// <param name="blh"></param>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <returns></returns>
        string SaveYY(string yyId, string brId, string blh, int? zjlx, string zjh);

        /// <summary>
        /// tạo đặt hẹn Xếp lịch ngày lịch
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="deptCode"></param>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        string BuildCalendar(int year, int month, string deptCode, string ysgh);
    }
}
