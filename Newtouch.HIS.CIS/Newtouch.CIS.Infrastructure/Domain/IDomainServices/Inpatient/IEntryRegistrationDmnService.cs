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
        /// Lấygiường ký tựthông tin
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<BedInfoViewResponseVO> GetCwInfoViewList(BedInfoViewRequestVO req);
        /// <summary>
        /// Bệnh nhânvàokhu
        /// </summary>
        /// <param name="inareaReq"></param>
        /// <returns></returns>
        string SavePatInArea(patInAreaRequestDto inareaReq);
        /// <summary>
        /// Bệnh nhânchuyển khuBệnh nhânvàokhuthông tinSửa
        /// </summary>
        /// <param name="patChangeAreaRequestDto"></param>
        string SaveChangeAreaPatInfo(patChangeAreaRequestDto patChangeAreaRequestDto);
        string UpdatePatInfo(string zyh,string wzjb,string orgId);
        /// <summary>
        /// LưuvàokhukhiThông tin bệnh nhân
        /// </summary>
        /// <param name="patInfoRes"></param>
        void SavePatInfo(InPatientDetailQueryDto patInfoRes);
        /// <summary>
        /// vàokhukhithulấykhingàygiường ký tựgắnChi phí
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string AddPatBedItemFee(patBedFeeRequestDto req);
        /// <summary>
        /// Lấychuyển khuBệnh nhân
        /// </summary>
        /// <param name="bqdm"></param>
        /// <returns></returns>
        List<NewPatInfoVO> GetPatChangeArea(string bqdm);
        /// <summary>
        /// LấycầnChènđếngiường ký tựSử dụngghi chépbảngcủaDữ liệu
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        patBedSyjlInfoVO GetPatBedSyInfo(string zyh);
        /// <summary>
        /// Lưugiường ký tựSử dụngghi chép
        /// </summary>
        /// <param name="patBedSyInfo"></param>
        void SavePatBedSYInfo(patBedSyjlInfoVO patBedSyInfo);
        /// <summary>
        /// Tra cứu
        /// </summary>
        /// <param name="pagination">phúttrang</param>
        /// <param name="keyword">Từ khóa</param>
        /// <param name="ksrq">Ngày bắt đầu</param>
        /// <param name="jsrq">Ngày kết thúc</param>
        /// <param name="bqdm">Khu bệnhMã</param>
        /// <returns></returns>
        IList<OutAreapatientInfo> GetOutAreaPatlist(Pagination pagination, string keyword, DateTime? ksrq, DateTime? jsrq, string bqdm, string orgId);
        /// <summary>
        /// ra khỏi khutriệuvề
        /// </summary>
        /// <param name="zyh">Số nhập viện</param>
        /// <param name="bqdm">Khu bệnhMã</param>
        string SaveRecallOutArea(string zyh, string bqdm);
        /// <summary>
        /// kiểm traBệnh nhâncó thểKhôngHủyvàokhu
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string GetPatIsCancelInArea(patRequestDto req);

        string PatCancelInArea(patRequestDto req);

        string GetPatIsChangeArea(patRequestDto req);

        string SaveChangeArea(patRequestDto req, string bqdm);
       
        /// <summary>
        /// chuyểngiường
        /// </summary>
        /// <param name="reqDto"></param>
        string ChangeBed(patBedRequestDto reqDto);
        /// <summary>
        /// ởKhu bệnhThông tin bệnh nhân
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="bqdm"></param>
        /// <returns></returns>
        IList<InAreapatientInfo> GetINAreaPatlist(Pagination pagination, string keyword, string bqdm,string orgId);

        /// <summary>
        /// mớivàokhu,chuyển khu giường ký tựlàKhôngbịchiếmdùng
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="cwCode"></param>
        /// <param name="bqCode"></param>
        /// <returns></returns>
        bool ValidationBedIsUse(string OrganizeId,string cwCode,string bqCode);
    }
}
