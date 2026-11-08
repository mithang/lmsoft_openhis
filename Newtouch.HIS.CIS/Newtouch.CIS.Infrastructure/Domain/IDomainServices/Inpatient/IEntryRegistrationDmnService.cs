using NewtouchHIS.Lib.Base.Model;
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
        /// Lấy giường ký tự thông tin
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
        /// Bệnh nhân chuyển khu Bệnh nhân vào khu thông tin Sửa
        /// </summary>
        /// <param name="patChangeAreaRequestDto"></param>
        string SaveChangeAreaPatInfo(patChangeAreaRequestDto patChangeAreaRequestDto);
        string UpdatePatInfo(string zyh,string wzjb,string orgId);
        /// <summary>
        /// Lưu vào khu khi Thông tin bệnh nhân
        /// </summary>
        /// <param name="patInfoRes"></param>
        void SavePatInfo(InPatientDetailQueryDto patInfoRes);
        /// <summary>
        /// vào khu khi thu lấy khi ngày giường ký tự gắn Chi phí
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string AddPatBedItemFee(patBedFeeRequestDto req);
        /// <summary>
        /// Lấy chuyển khu Bệnh nhân
        /// </summary>
        /// <param name="bqdm"></param>
        /// <returns></returns>
        List<NewPatInfoVO> GetPatChangeArea(string bqdm);
        /// <summary>
        /// Lấy cần Chèn đến giường ký tự Sử dụng ghi chép bảng của Dữ liệu
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        patBedSyjlInfoVO GetPatBedSyInfo(string zyh);
        /// <summary>
        /// Lưu giường ký tự Sử dụng ghi chép
        /// </summary>
        /// <param name="patBedSyInfo"></param>
        void SavePatBedSYInfo(patBedSyjlInfoVO patBedSyInfo);
        /// <summary>
        /// Tra cứu
        /// </summary>
        /// <param name="pagination">phút trang</param>
        /// <param name="keyword">Từ khóa</param>
        /// <param name="ksrq">Ngày bắt đầu</param>
        /// <param name="jsrq">Ngày kết thúc</param>
        /// <param name="bqdm">Khu bệnh Mã</param>
        /// <returns></returns>
        IList<OutAreapatientInfo> GetOutAreaPatlist(Pagination pagination, string keyword, DateTime? ksrq, DateTime? jsrq, string bqdm, string orgId);
        /// <summary>
        /// ra khỏi khu triệu về
        /// </summary>
        /// <param name="zyh">Số nhập viện</param>
        /// <param name="bqdm">Khu bệnh Mã</param>
        string SaveRecallOutArea(string zyh, string bqdm);
        /// <summary>
        /// kiểm tra Bệnh nhân có thể Không Hủy vào khu
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string GetPatIsCancelInArea(patRequestDto req);

        string PatCancelInArea(patRequestDto req);

        string GetPatIsChangeArea(patRequestDto req);

        string SaveChangeArea(patRequestDto req, string bqdm);
       
        /// <summary>
        /// chuyển giường
        /// </summary>
        /// <param name="reqDto"></param>
        string ChangeBed(patBedRequestDto reqDto);
        /// <summary>
        /// ở Khu bệnh Thông tin bệnh nhân
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="bqdm"></param>
        /// <returns></returns>
        IList<InAreapatientInfo> GetINAreaPatlist(Pagination pagination, string keyword, string bqdm,string orgId);

        /// <summary>
        /// mới vào khu,chuyển khu giường ký tự là Không bị chiếm dùng
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="cwCode"></param>
        /// <param name="bqCode"></param>
        /// <returns></returns>
        bool ValidationBedIsUse(string OrganizeId,string cwCode,string bqCode);
    }
}
