using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Sett.Request.OutPatientPharmacy;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.OutpatientManage
{
    public interface IOutpatientPharmacyAPIDmnService
    {
        /// <summary>
        /// Tải thông tin tổng hợp xếp thuốc
        /// </summary>
        /// <param name="ksdm">Mã khoa</param>
        /// <param name="fysjs"></param>
        /// <param name="fybz"></param>
        /// <returns></returns>
        OutPatientpyInfoDTO GetFyhjInfo(string ksdm, DateTime fysjs, int fybz);

        /// <summary>
        /// Tra cứu thông tin xếp thuốc
        /// </summary>
        /// <param name="ksdm"></param>
        /// <param name="yxq"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutPatientpyListDTO> GetFyList(string ksdm, int yxq, string orgId);


        /// <summary>
        /// In bổ sung phiếu phát thuốc Tra cứu thông tin phát thuốc
        /// </summary>
        /// <param name="ksdm"></param>
        /// <param name="fph"></param>
        /// <param name="xm"></param>
        /// <param name="kh"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<OutPatientpyListDTO> GetFyListOnPrint(string ksdm, string fph, string xm, string kh, DateTime kssj, DateTime jssj, string organizeId);

        /// <summary>
        /// Tra cứu thông tin thuốc chi tiết xếp thuốc
        /// </summary>
        /// <param name="cfh">Số đơn thuốc</param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutPatientpyDetailListDTO> GetFyDetailList(string cfh, string orgId);

        /// <summary>
        /// Lấy thông tin thuốc đơn thuốc chi tiết theo mã nội bộ đơn thuốc
        /// </summary>
        /// <param name="cfnm"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<YP_XT_OP_PYListDTO> GetpyParList(string cfnm, string organizeId);

        /// <summary>
        /// Lấy số tiền đơn thuốc,Thông tin nhà thuốc lĩnh thuốc
        /// </summary>
        /// <param name="cfnm"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<GetcfInfoDTO> GetcfInfo(string cfnm, string organizeId);

        /// <summary>
        /// Cập nhật cờ phát thuốc
        /// </summary>
        /// <param name="par"></param>
        bool Updatecfzt(UpdatefyztRequest par);

        /// <summary>
        /// Phát thuốc ngoại trú hiển thị thông tin bảng chính
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<fyMainListRequest> GetfyMainInfoList(string keyword, string orgId);

        /// <summary>
        /// Phát thuốc ngoại trú hiển thị thông tin chi tiết đơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<fyDetailListRequest> GetfyDetailInfoList(string cfh, string organizeId);

        /// <summary>
        /// Phát thuốc ngoại trú,Khi phát thuốc thì lấy thông tin đơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="lyyf"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<fyCfYpInfo> GetfyDetailCFInfo(string cfh, string lyyf, string organizeId);

        /// <summary>
        /// Sau khi phát thuốc xong thì cập nhật cờ phát thuốc của bảng đơn thuốc
        /// </summary>
        /// <param name="cfnm"></param>
        /// <param name="user_code"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        bool UpdatecfztByFY(string cfnm, string user_code, string zt);


        /// <summary>
        /// Trả thuốc ngoại trú hiển thị thông tin bảng chính
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<tyCFMainInfo> GetTyMainInfoList(tyCFYpInfoRequest req, string orgId);

        /// <summary>
        /// Trả thuốc ngoại trú hiển thị thông tin chi tiết đơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<tyDetailListRequest> GettyDetailInfoList(string cfh, string orgId);

        /// <summary>
        /// Trang tra cứu phát thuốc ngoại trú Tra cứu thông tin bảng chính
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<fyQueryMainInfo> GetfyQueryInfoList(searchFyInfoReqVO req, string orgId);

        /// <summary>
        /// Tra cứu phát thuốc ngoại trú hiển thị thông tin chi tiết thuốc
        /// </summary>
        /// <param name="cfnm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<fyDetailListRequest> GetfyQueryDetailInfoList(string cfnm, string orgId);
        /// <summary>
        /// Tra cứu chi tiết đơn thuốc đã quyết toán
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <param name="yfCode">Mã cách dùng</param>
        /// <returns></returns>
        List<OutpatientSettledRpQueryResponseDTO> OutpatientSettledRpQuery(string organizeId, DateTime kssj, DateTime jssj, string fph, string kh, string yfCode,string mzh);

        /// <summary>
        /// Lấy thuốc đã quyết toán
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="cfnm"></param>
        /// <param name="yp"></param>
        /// <param name="sl"></param>
        /// <param name="czh">Số nhóm</param>
        void OutpatientDrugWithdrawalNotify(string organizeId, int cfnm, string yp, decimal sl, string czh);
    }
}
