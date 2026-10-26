using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    /// <summary>
    /// Đăng ký khám ngoại trú
    /// </summary>
    public class MzBespeakRegisterVO
    {
        /// <summary>
        /// Tổng số đăng ký khám đã hẹn của khoa hoặc chuyên gia đã chọn
        /// </summary>
        public int alreadyBespeakRegisterCount { get; set; }
        
        /// <summary>
        /// Tổng số đã đăng ký của khoa hoặc chuyên gia đã chọn
        /// </summary>
        public int alreadyRegisterCount { get; set; }

        /// <summary>
        /// Nhân viên thao tác hiện tại
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// Số hẹn khám tối đa cả ngày  Tổng số hẹn khám tối đa của mỗi khoảng thời gian hẹn
        /// </summary>
        public int? allBespeakMaxCount { get; set; }

        /// <summary>
        /// Thông tin đăng ký khám của bệnh nhân hiện tại
        /// </summary>
        public BespeakRegisterParamDTO registerInfo { get; set; }

        /// <summary>
        /// Lịch trực đăng ký khám thời gian hẹn được chỉ định của khoa hoặc chuyên gia đã chọn 
        /// </summary>
        public List<MzBespeakRegisterSchedulingQueryResponseDTO> bespeakRegSchedulingDetail { get; set; }

        /// <summary>
        /// Thông tin hẹn đăng ký khám của bệnh nhân hiện tại  Không phân khoa,Chuyên gia,Ngày tháng
        /// </summary>
        public List<MzBespeakRegisterQueryResponseDTO> bespeakRegDetail { get; set; }
    }
}
