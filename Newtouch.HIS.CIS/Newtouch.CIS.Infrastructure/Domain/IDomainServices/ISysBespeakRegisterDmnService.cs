using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// đặt hẹn Quản lý
    /// </summary>
    public interface ISysBespeakRegisterDmnService
    {
        /// <summary>
        /// select SysBespeakregister
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        IList<SysBespeakRegisterVO> SelectSysBespeakregister(Pagination pagination, string keyword, string OrganizeId);

        /// <summary>
        /// select SysBespeakregister
        /// </summary>
        /// <returns></returns>
        IList<SysBespeakRegisterVO> SelectSysBespeakregister(string deptCode, string gh, string OrganizeId, DateTime? regDate, string regTime = "");

        /// <summary>
        /// Theo Khoavà Xếp lịch Ngày Lấy Hệ thốngđặt hẹn Xếp lịch
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysBespeakRegisterVO> SelectSysBespeakregister(string deptCode, DateTime regDate, string organizeId);

        /// <summary>
        /// Theo Khoa Lấy Hệ thốngđặt hẹn Xếp lịch
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysBespeakRegisterVO> SelectSysBespeakregister(string deptCode, string organizeId);
    }
}
