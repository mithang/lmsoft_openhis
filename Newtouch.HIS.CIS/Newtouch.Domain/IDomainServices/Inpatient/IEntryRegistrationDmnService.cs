using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects.Inpatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices
{
    public interface IEntryRegistrationDmnService
    {
        /// <summary>
        /// Lấy thông tin giường bệnh
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<BedInfoViewResponseVO> GetCwInfoViewList(BedInfoViewRequestVO req);
        /// <summary>
        /// Bệnh nhân vào khu
        /// </summary>
        /// <param name="inareaReq"></param>
        /// <returns></returns>
        string SavePatInArea(patInAreaRequestDto inareaReq);
        /// <summary>
        /// Sửa đổi thông tin bệnh nhân chuyển khu/vào khu
        /// </summary>
        /// <param name="patChangeAreaRequestDto"></param>
        string SaveChangeAreaPatInfo(patChangeAreaRequestDto patChangeAreaRequestDto);
        string UpdatePatInfo(string zyh,string wzjb,string orgId);
        /// <summary>
        /// Lưu thông tin bệnh nhân khi vào khu
        /// </summary>
        /// <param name="patInfoRes"></param>
        void SavePatInfo(InPatientDetailQueryDto patInfoRes);
        /// <summary>
        /// Khi vào khu thu phí liên kết giường bệnh trong ngày
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string AddPatBedItemFee(patBedFeeRequestDto req);
        /// <summary>
        /// Lấy bệnh nhân chuyển khu
        /// </summary>
        /// <param name="bqdm"></param>
        /// <returns></returns>
        List<NewPatInfoVO> GetPatChangeArea(string bqdm);
        /// <summary>
        /// Lấy dữ liệu cần chèn vào bảng ghi nhận sử dụng giường bệnh
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        patBedSyjlInfoVO GetPatBedSyInfo(string zyh);
        /// <summary>
        /// Lưu hồ sơ sử dụng giường bệnh
        /// </summary>
        /// <param name="patBedSyInfo"></param>
        void SavePatBedSYInfo(patBedSyjlInfoVO patBedSyInfo);
        /// <summary>
        /// Tra cứu
        /// </summary>
        /// <param name="pagination">Phân trang</param>
        /// <param name="keyword">Từ khóa</param>
        /// <param name="ksrq">Ngày bắt đầu</param>
        /// <param name="jsrq">Ngày kết thúc</param>
        /// <param name="bqdm">Mã khu bệnh</param>
        /// <returns></returns>
        IList<OutAreapatientInfo> GetOutAreaPatlist(Pagination pagination, string keyword, DateTime? ksrq, DateTime? jsrq, string bqdm, string orgId);
        /// <summary>
        /// Gọi lại khi ra khỏi khu
        /// </summary>
        /// <param name="zyh">Số nội trú</param>
        /// <param name="bqdm">Mã khu bệnh</param>
        string SaveRecallOutArea(string zyh, string bqdm);
        /// <summary>
        /// Kiểm tra bệnh nhân có thể hủy vào khu hay không
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string GetPatIsCancelInArea(patRequestDto req);

        string PatCancelInArea(patRequestDto req);

        string GetPatIsChangeArea(patRequestDto req);

        string SaveChangeArea(patRequestDto req, string bqdm);
       
        /// <summary>
        /// Chuyển giường
        /// </summary>
        /// <param name="reqDto"></param>
        string ChangeBed(patBedRequestDto reqDto);
        /// <summary>
        /// Thông tin bệnh nhân tại khu bệnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="bqdm"></param>
        /// <returns></returns>
        IList<InAreapatientInfo> GetINAreaPatlist(Pagination pagination, string keyword, string bqdm,string orgId);

        /// <summary>
        /// Vào khu mới,Chuyển khu Giường bệnh có bị chiếm hay không
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="cwCode"></param>
        /// <param name="bqCode"></param>
        /// <returns></returns>
        bool ValidationBedIsUse(string OrganizeId,string cwCode,string bqCode);
    }
}
