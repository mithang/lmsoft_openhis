/*********************************************************/
// Copyright (C) 2016 Newtouch Giữ bản quyền. 
// Description: Nhập viện quản lý >Nhập viện hoàn phí>
//**********************************************************/
using System.Collections.Generic;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage;
using Newtouch.Common.Operator;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IRepository;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class HospitalRefundApp : AppBase, IHospitalRefundApp
    {
        private readonly IHospitalRefundDmnService _hospitalRefundService;
        private readonly ISysConfigRepo _sysConfigRepo; //Cấu hình hệ thống
        
        /// <summary>
        /// Nhập viện tính phí hạng mục nhóm 
        /// </summary>
        /// <param name="zyh">Số hiệu nhập viện</param>
        /// <param name="zxks">Khoa</param>
        /// <param name="xmmc">Hạng mục thu phí</param>
        /// <param name="yrh">Số trẻ sơ sinh</param>
        public List<GridZyXmYpjfb> GetZyXmjfbGroup(string zyh, string zxks, string xmmc, string yrh)
        {
            List<GridZyXmYpjfb> zyXmjfbDt = _hospitalRefundService.GetZyXmjfbGroup(zyh, zxks, xmmc, yrh);

            return zyXmjfbDt;
        }
             /// <summary>
        /// Nhập viện thuốc tính phí nhóm 
        /// </summary>
        /// <param name="zyh">Số hiệu nhập viện</param>
        /// <param name="zxks">Khoa</param>
        /// <param name="xmmc">Hạng mục thu phí</param>
        /// <param name="yrh">Số trẻ sơ sinh</param>

        public List<GridZyXmYpjfb> GetZyYpjfbGroup(string zyh, string zxks, string xmmc, string yrh)
        {
            List<GridZyXmYpjfb> ZyYpjfb = _hospitalRefundService.GetZyYpjfbGroup(zyh, zxks, xmmc, yrh);

            return ZyYpjfb;
        }
        /// <summary>
        /// Nhập viện thuốc tính phí chi tiết
        /// </summary>
        /// <param name="zyh">Số nhập viện</param>
        /// <param name="xsebh">Số trẻ sơ sinh</param>
        /// <param name="ypbh">ThuốcSố hiệu</param>
        /// <returns></returns>
        public List<gridZyXmYpItemsjfb> GetZyYpjfbList(string zyh, string xsebh, string ypCode, string tdrq)
        {
            List<gridZyXmYpItemsjfb> zyYpjfbDt = _hospitalRefundService.GetZyYpjfbList(zyh, xsebh, ypCode, tdrq);
            //if (zyYpjfbDt.Rows.Count > 0 && loadZyYpjfbListEvent != null)
            //    loadZyYpjfbListEvent(zyYpjfbDt);
            return zyYpjfbDt;
        }
        /// <summary>
        /// Nhập việnTính phí hạng mụcChi tiết
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="xsebh"></param>
        /// <param name="sfxmbh"></param>
        /// <returns></returns>
        public List<gridZyXmYpItemsjfb> GetZyXmjfbList(string zyh, string xsebh, string sfxm, string CreateTime)
        {
            List<gridZyXmYpItemsjfb> zyXmjfbDt = _hospitalRefundService.GetZyXmjfbList(zyh, xsebh, sfxm, CreateTime);

            return zyXmjfbDt;
        }
        /// <summary>
        /// tải trẻ sơ sinh 
        /// </summary>
        /// <param name="zyh">Số nhập viện</param>
        /// <returns>Họ tên trẻ sơ sinh số </returns>
        //public DataTable GetYehByZyh(string zyh)
        //{

        //    DataTable yeh = _hospitalRefundService.GetYehByZyh(zyh);
        //    return yeh;
        //}
        /// theo số nhập viện lấy thông tin bệnh nhân
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public string GetPatInfoByZyh(string zyh)
        {
          
            return _hospitalRefundService.GetPatInfoByZyh( zyh);
        }
        /// theo mã lấy cấu hình thông tin
        public string GetPZInfo()
        {
            return _sysConfigRepo.GetValueByCode(Constants.xtmzpz.ZYTFPZ_INCLUDEYP, OperatorProvider.GetCurrent().OrganizeId);

        }
        /// <summary>
        /// Hoàn phí
        /// </summary>
        /// <param name="returnDict"></param>
        /// <returns></returns>
        public string SaveReturnZyyp(Dictionary<string, decimal> returnDict)
        {
            return _hospitalRefundService.SaveReturnZyyp(returnDict);
        }
        /// <summary>
        /// Hoàn phí
        /// </summary>
        /// <param name="returnDict"></param>
        /// <returns></returns>
        public string SaveReturnZyxm(Dictionary<string, decimal> returnDict)
        {
            return _hospitalRefundService.SaveReturnZyxm(returnDict);
        }
    }
}
