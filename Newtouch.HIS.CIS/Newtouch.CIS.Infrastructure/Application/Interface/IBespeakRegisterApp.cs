using Newtouch.Domain.Entity;
using Newtouch.Domain.ViewModels;
using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// đặt hẹnđăng ký khám
    /// </summary>
    public interface IBespeakRegisterApp
    {
        /// <summary>
        /// Sửađặt hẹnQuản lý
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        string UpdateBespeakRegisterManage(List<SysBespeakRegisterEntity> entities);

        /// <summary>
        /// LấyMặc địnhcủađặt hẹnđăng ký khámThời gian
        /// </summary>
        /// <param name="sourceDate"></param>
        /// <returns></returns>
        List<SysBespeakRegisterDateTimeVO> GetRegisterDateByWeek(DateTime sourceDate);

        /// <summary>
        /// LấyHệ thốngcấu hìnhđặt hẹnThời gianđoạnvàcó thểđặt hẹntổngsố
        /// </summary>
        /// <returns></returns>
        List<SysBespeakRegisterTimeVO> GetConfigureRegTime();

        /// <summary>
        /// lắp rápđuổithêmdòngHTML
        /// </summary>
        /// <param name="regDatas"></param>
        /// <param name="regTimes"></param>
        /// <returns></returns>
        string BuildNewRow(List<SysBespeakRegisterDateTimeVO> regDatas, List<SysBespeakRegisterTimeVO> regTimes);

        /// <summary>
        /// Gửiđặt hẹnđăng ký khámSửa
        /// </summary>
        /// <param name="regData"></param>
        /// <returns></returns>
        string SubmitForm(List<SysBespeakRegisterEntity> regData);

        /// <summary>
        /// Xóaquakỳđặt hẹnđăng ký khám
        /// </summary>
        void DeleteExpireRegData();

        /// <summary>
        /// LấyKhoađặt hẹnđăng ký khámthông tin
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        string GetRegDataHtmlByKs(string deptCode);

        /// <summary>
        /// LấyKhoaMặc địnhđặt hẹnđăng ký khám
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        string GetDefaultHtmlByKs(string deptCode);

        /// <summary>
        /// TheoKhoavàMã nhân viênlắp rápđặt hẹnđăng ký khámthông tin
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        string GetRegDataHtmlByKsAndGh(string deptCode, string gh);

        /// <summary>
        /// Lấycó thểđặt hẹntổng ngườisốvàđãđặt hẹnsố
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <param name="regTime"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        object GetBespeakMaxCount(string deptCode, DateTime regDate, string regTime, string gh);

        /// <summary>
        /// Lưuđặt hẹnđăng ký khám
        /// </summary>
        /// <param name="yyId"></param>
        /// <param name="brId"></param>
        /// <param name="blh"></param>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <returns></returns>
        string SaveYY(string yyId, string brId, string blh, int? zjlx, string zjh);

        /// <summary>
        /// tạođặt hẹnXếp lịchngàylịch
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="deptCode"></param>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        string BuildCalendar(int year, int month, string deptCode, string ysgh);
    }
}
