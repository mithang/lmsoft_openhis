using Newtouch.Core.Common;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.DTO.OutputDto.Outpatient.API;
using Newtouch.Domain.ViewModels.Outpatient;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// Đăng ký khám hẹn ngoại trú
    /// </summary>
    public interface IMzyyghDmnService
    {
        /// <summary>
        /// Tra cứu thông tin đăng ký khám hẹn ngoại trú theo số bệnh án
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="blh"></param>
        /// <param name="organizeId"></param>
        /// <param name="regDate"></param>
        /// <returns></returns>
        IList<MzyyghVO> SelectMzyyghDetail(Pagination pagination, string blh, string organizeId, DateTime regDate);

        /// <summary>
        /// Tra cứu hẹn ngoại trú
        /// </summary>
        /// <param name="blh">Số bệnh án</param>
        /// <param name="kh">Số thẻ</param>
        /// <param name="zjh">Số giấy tờ</param>
        /// <param name="organizeId"></param>
        List<MzBespeakRegisterQueryResponseDTO> MzBespeakRegisterQuery(string blh, string kh, string zjh, string organizeId);

        /// <summary>
        /// Lấy tổng số đăng ký khám theo hẹn của khoa hoặc chuyên gia hiện tại
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="ysgh"></param>
        /// <param name="regDate"></param>
        /// <param name="regTime"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        MzAlreadyBespeakRegisterCountQueryResponseDTO SelectAlreadyBespeakRegisterCount(string ksCode, string ysgh, DateTime regDate, string regTime, string organizeId);

        /// <summary>
        /// Tra cứu đăng ký khám đã hẹn
        /// </summary>
        /// <param name="mzlx">Cờ ngoại trú/nội trú 1:Khám thường ngoại trú  2:Cấp cứu   3:Khám chuyên gia</param>
        /// <param name="departmentCode"></param>
        /// <param name="ysgh"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzyyghDetailVo> SelectMzyyghDetail(string mzlx, string departmentCode, string ysgh, DateTime beginTime, DateTime endTime, string organizeId);
    }
}
