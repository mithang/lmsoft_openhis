using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.DTO.OutputDto.Outpatient.API;
using Newtouch.Domain.ViewModels.Outpatient;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// Ngoại trú đặt hẹn đăng ký khám
    /// </summary>
    public interface IMzyyghDmnService
    {
        /// <summary>
        /// Theo Số bệnh án Tra cứu Ngoại trú đặt hẹn đăng ký khám thông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="blh"></param>
        /// <param name="organizeId"></param>
        /// <param name="regDate"></param>
        /// <returns></returns>
        IList<MzyyghVO> SelectMzyyghDetail(Pagination pagination, string blh, string organizeId, DateTime regDate);

        /// <summary>
        /// Ngoại trú đặt hẹn Tra cứu
        /// </summary>
        /// <param name="blh">Số bệnh án</param>
        /// <param name="kh">Số thẻ</param>
        /// <param name="zjh">Số giấy tờ</param>
        /// <param name="organizeId"></param>
        List<MzBespeakRegisterQueryResponseDTO> MzBespeakRegisterQuery(string blh, string kh, string zjh, string organizeId);

        /// <summary>
        /// Lấy hiện tại Khoa hoặc chuyên gia đã đặt hẹn đăng ký khám tổng số
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="ysgh"></param>
        /// <param name="regDate"></param>
        /// <param name="regTime"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        MzAlreadyBespeakRegisterCountQueryResponseDTO SelectAlreadyBespeakRegisterCount(string ksCode, string ysgh, DateTime regDate, string regTime, string organizeId);

        /// <summary>
        /// Tra cứu đã đặt hẹn đăng ký khám
        /// </summary>
        /// <param name="mzlx">Dấu hiệu ngoại trú/nội trú 1:thông thường Ngoại trú  2:gấp khám   3:chuyên gia Ngoại trú</param>
        /// <param name="departmentCode"></param>
        /// <param name="ysgh"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzyyghDetailVo> SelectMzyyghDetail(string mzlx, string departmentCode, string ysgh, DateTime beginTime, DateTime endTime, string organizeId);
    }
}
