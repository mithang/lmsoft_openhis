using System;

namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    /// <summary>
    /// Bản tin trả về lịch trực đăng ký khám hẹn trước ngoại trú
    /// </summary>
    public class MzBespeakRegisterSchedulingQueryResponseDTO
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Cơ cấu tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Loại ngoại trú  1:Khám ngoại trú thông thường    2:Cấp cứu     3:Khám chuyên gia
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// Ngày hẹn khám
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// Khoảng thời gian
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// Mã nhân viên chuyên gia
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// Tên chuyên gia
        /// </summary>
        public string zjmc { get; set; }

        /// <summary>
        /// Số hẹn khám tối đa
        /// </summary>
        public int bespeakMaxCount { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Trạng thái     1:Hiệu lực  0:Không hiệu lực
        /// </summary>
        public string zt { get; set; }
    }
}
