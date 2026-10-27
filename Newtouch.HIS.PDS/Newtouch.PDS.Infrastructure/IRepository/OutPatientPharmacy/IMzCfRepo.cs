using System;
using NewtouchHIS.Lib.Base.Model;
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
    /// Đơn ngoại trú
    /// </summary>
    public interface IMzCfRepo : IRepositoryBase<MzCfEntity>
    {
        /// <summary>
        /// Sửa cờ phát thuốc Chưa phát=>Đã xếp
        /// </summary>
        /// <param name="cfhs"></param>
        /// <returns></returns>
        int UpdateFybzByCfh(List<string> cfhs);

        /// <summary>
        /// Sửa cờ phát thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int UpdateFybzByCfh(string cfh, EnumFybz fybz, string organizeId);

        /// <summary>
        /// Sửa cờ phát thuốc Đã xếp=>Chưa phát
        /// </summary>
        /// <param name="cfhs"></param>
        /// <returns></returns>
        int UpdateFybzToWFByCfh(List<string> cfhs, string OrganizeId);

        /// <summary>
        /// Lấy đơn thuốc chưa xếp thuốc
        /// </summary>
        /// <returns></returns>
        List<MzCfEntity> GetNoArrangedCfList(DateTime? bT = null, DateTime? eT = null);

        /// <summary>
        /// Lấy đơn thuốc chưa xếp thuốc
        /// </summary>
        /// <returns></returns>
        IList<MzCfEntity> GetCfsByKeyword(string keyword, int fybz = (int)EnumFybz.Yp, Pagination pagination = null);

        /// <summary>
        /// Lấy thông tin đơn thuốc theo số thẻ và họ tên
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        cfInfoVo GetCfInfo(string cardNo, string xm, int fybz = 0, string organizeId = "");

        /// <summary>
        /// Lấy thông tin đơn thuốc theo số thẻ và họ tên
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzCfEntity> GetRpInfo(string yfbmCode, string cardNo, string xm, EnumFybz fybz = EnumFybz.Yp, string organizeId = "");

        /// <summary>
        /// Lấy thông tin đơn thuốc theo số thẻ và họ tên
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzCfEntity> GetCfs(string cardNo, string xm, int fybz = 0, string organizeId = "");

        /// <summary>
        /// Lấy thông tin đơn thuốc theo số thẻ và họ tên
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
        /// Lấy số nhóm theo số đơn thuốc
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
        /// Chèn hàng loạt thông tin đơn thuốc Trả về dòng bị ảnh hưởng
        /// </summary>
        /// <param name="mzCfs"></param>
        /// <returns></returns>
        int InsertBatch(List<MzCfEntity> mzCfs);

        /// <summary>
        /// Xóa thông tin đơn thuốc cũ
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="cfnm"></param>
        /// <returns></returns>
        int DeleteCf(string cfh, long cfnm);

        /// <summary>
        /// Hủy đơn thuốc，Trả phí
        /// </summary>
        /// <param name="cfh">Số đơn thuốc</param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        int DeleteCf(string cfh, string OrganizeId);

        /// <summary>
        /// Trả về họ tên và thời gian thu phí sau khi khử trùng lặp
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <param name="fybz"></param>
        /// <returns></returns>
        IList<patientInfoVO> GetXmAndCardNo(Pagination pagination, string yfbmCode, string organizeId, string keyword = "", EnumFybz fybz = EnumFybz.Wp);

        /// <summary>
        /// Truy vấn đơn thuốc đã phát hoặc đã trả Mặc định hiển thị trước500dòng
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        IList<CfxxVO> SelectDispensedRpList(searchFyInfoReqVO param);

        /// <summary>
        /// Lấy đơn thuốc hiệu lực theo số đơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzCfEntity> SelectRpList(string cfh, string organizeId);

        /// <summary>
        /// Cập nhật giới tính
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="xb"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        int UpdateGender(string cfh, string xb, string orgId);

        List<MzCfEntity> SelectTfRpList(string cfh, string organizeId);

        #region  Đơn điện tử BHYT
        /// <summary>
        /// Trả về họ tên và thời gian thu phí sau khi khử trùng lặp
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <param name="fybz"></param>
        /// <returns></returns>
        IList<patientInfoVO> GetElectronicPrescription(Pagination pagination, string organizeId, string isysh, string keyword = "");
        /// <summary>
        /// Lấy thông tin chi tiết đơn thuốc theo số đơn thuốc và họ tên
        /// </summary>
        /// <returns></returns>
        List<DzcfmxVO> QueryElectronicPrescriptionCfmx(string cfh, string xm, string organizeId);
        Input_2203A GetCQjzdjInfo(string mzh, string orgId);
        int UpdateCfYsshyj(string cfh, string ysshyj, string orgId);

        #endregion
    }
}