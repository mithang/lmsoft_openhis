using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.HIS.Domain.VO;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// ngoại trú đơn thuốc
    /// </summary>
    public interface IMzCfRepo : IRepositoryBase<MzCfEntity>
    {
        /// <summary>
        /// sửa phát thuốc cờ chưa phát=>đã xếp
        /// </summary>
        /// <param name="cfhs"></param>
        /// <returns></returns>
        int UpdateFybzByCfh(List<string> cfhs);

        /// <summary>
        /// sửa phát thuốc cờ
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int UpdateFybzByCfh(string cfh, EnumFybz fybz, string organizeId);

        /// <summary>
        /// sửa phát thuốc cờ đã xếp=>chưa phát
        /// </summary>
        /// <param name="cfhs"></param>
        /// <returns></returns>
        int UpdateFybzToWFByCfh(List<string> cfhs, string OrganizeId);

        /// <summary>
        /// lấy chưa xếp thuốc của đơn thuốc
        /// </summary>
        /// <returns></returns>
        List<MzCfEntity> GetNoArrangedCfList(DateTime? bT = null, DateTime? eT = null);

        /// <summary>
        /// lấy chưa xếp thuốc của đơn thuốc
        /// </summary>
        /// <returns></returns>
        IList<MzCfEntity> GetCfsByKeyword(string keyword, int fybz = (int)EnumFybz.Yp, Pagination pagination = null);

        /// <summary>
        /// lấy thông tin đơn thuốc theo số thẻ và họ tên
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        cfInfoVo GetCfInfo(string cardNo, string xm, int fybz = 0, string organizeId = "");

        /// <summary>
        /// lấy thông tin đơn thuốc theo số thẻ và họ tên
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzCfEntity> GetRpInfo(string yfbmCode, string cardNo, string xm, EnumFybz fybz = EnumFybz.Yp, string organizeId = "");

        /// <summary>
        /// lấy thông tin đơn thuốc theo số thẻ và họ tên
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzCfEntity> GetCfs(string cardNo, string xm, int fybz = 0, string organizeId = "");

        /// <summary>
        /// lấy thông tin đơn thuốc theo số thẻ và họ tên
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="ispay"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzCfEntity> GetCf(string yfbmCode, string cardNo, string xm, EnumFybz fybz, bool ispay, string organizeId);

        /// <summary>
        /// đạt số đơn thuốc lấy số nhóm
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="ispay"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<GetMzCfZt> GetZhInOutpatient(string cfh,string type);
        /// <summary>
        /// hàng loạt chèn thông tin đơn thuốc, trả về số dòng bị ảnh hưởng
        /// </summary>
        /// <param name="mzCfs"></param>
        /// <returns></returns>
        int InsertBatch(List<MzCfEntity> mzCfs);

        /// <summary>
        /// xóa cũ của thông tin đơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="cfnm"></param>
        /// <returns></returns>
        int DeleteCf(string cfh, long cfnm);

        /// <summary>
        /// hủy bỏ đơn thuốc, hoàn trả chi phí
        /// </summary>
        /// <param name="cfh">số đơn thuốc</param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        int DeleteCf(string cfh, string OrganizeId);

        /// <summary>
        /// quay lại loại bỏ trùng lặp sau của họ tên và thời gian thu phí
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <param name="fybz"></param>
        /// <returns></returns>
        IList<patientInfoVO> GetXmAndCardNo(Pagination pagination, string yfbmCode, string organizeId, string keyword = "", EnumFybz fybz = EnumFybz.Wp);

        /// <summary>
        /// tra cứu đã phát hoặc người đã trả đơn thuốc mặc định hiển thị trước 500 dòng
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        IList<CfxxVO> SelectDispensedRpList(searchFyInfoReqVO param);

        /// <summary>
        /// theo số đơn thuốc lấy còn hiệu lực của đơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzCfEntity> SelectRpList(string cfh, string organizeId);

        /// <summary>
        /// cập nhật giới tính
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="xb"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        int UpdateGender(string cfh, string xb, string orgId);

        List<MzCfEntity> SelectTfRpList(string cfh, string organizeId);

        #region  医保电子处方
        /// <summary>
        /// quay lại loại bỏ trùng lặp sau của họ tên và thời gian thu phí
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <param name="fybz"></param>
        /// <returns></returns>
        IList<patientInfoVO> GetElectronicPrescription(Pagination pagination, string organizeId, string isysh, string keyword = "");
        /// <summary>
        /// Căn cứ vào số đơn thuốc và họ tên để lấy thông tin chi tiết đơn thuốc
        /// </summary>
        /// <returns></returns>
        List<DzcfmxVO> QueryElectronicPrescriptionCfmx(string cfh, string xm, string organizeId);
        Input_2203A GetCQjzdjInfo(string mzh, string orgId);
        int UpdateCfYsshyj(string cfh, string ysshyj, string orgId);

        #endregion
    }
}