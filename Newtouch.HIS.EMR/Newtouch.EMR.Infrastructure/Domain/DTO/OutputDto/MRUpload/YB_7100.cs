using System.ComponentModel.DataAnnotations;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    public class YB_7100
    {
        /// <summary>
        /// Số thứ tự khám
        /// </summary>
        /// <returns></returns>
        [Required]
        public string AKC190 { get; set; }

        /// <summary>
        /// Số thứ tự bản ghi nhập viện
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF263 { get; set; }

        /// <summary>
        /// Số thứ tự nội trú
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKC191 { get; set; }

        /// <summary>
        /// Tên khu bệnh
        /// </summary>
        /// <returns></returns>
        [Required]
        public string AKE021 { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        /// <returns></returns>
        public string AKF001 { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        /// <returns></returns>
        [Required]
        public string AKF002 { get; set; }

        /// <summary>
        /// Số giường bệnh
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKB410 { get; set; }

        /// <summary>
        /// Ngày giờ nhập viện
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKC192 { get; set; }

        /// <summary>
        /// Tên người trình bày bệnh sử
        /// </summary>
        /// <returns></returns>
        public string BKF110 { get; set; }

        /// <summary>
        /// Mã quan hệ giữa người trình bày và bệnh nhân
        /// </summary>
        /// <returns></returns>
        public string BKF654 { get; set; }

        /// <summary>
        /// Dấu hiệu nội dung trình bày có đáng tin cậy hay không
        /// </summary>
        /// <returns></returns>
        public string BKF125 { get; set; }

        /// <summary>
        /// Triệu chứng chính
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF476 { get; set; }

        /// <summary>
        /// Bệnh sử hiện tại
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF360 { get; set; }

        /// <summary>
        /// Dấu hiệu tình trạng sức khỏe chung
        /// </summary>
        /// <returns></returns>
        public string BKF385 { get; set; }

        /// <summary>
        /// Tiền sử bệnh（bao gồm chấn thương）
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF740 { get; set; }

        /// <summary>
        /// Dấu hiệu lây nhiễm của bệnh nhân
        /// </summary>
        /// <returns></returns>
        public string BKF185 { get; set; }

        /// <summary>
        /// Tiền sử bệnh truyền nhiễm
        /// </summary>
        /// <returns></returns>
        public string BKF150 { get; set; }

        /// <summary>
        /// Tiền sử tiêm chủng
        /// </summary>
        /// <returns></returns>
        public string BKF409 { get; set; }

        /// <summary>
        /// Tiền sử phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string BKF285 { get; set; }

        /// <summary>
        /// Tiền sử truyền máu
        /// </summary>
        /// <returns></returns>
        public string BKF299 { get; set; }

        /// <summary>
        /// Tiền sử dị ứng
        /// </summary>
        /// <returns></returns>
        public string BKF678 { get; set; }

        /// <summary>
        /// Tiền sử cá nhân
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF742 { get; set; }

        /// <summary>
        /// Tiền sử hôn nhân và sinh đẻ
        /// </summary>
        /// <returns></returns>
        public string BKF189 { get; set; }

        /// <summary>
        /// Tiền sử kinh nguyệt
        /// </summary>
        /// <returns></returns>
        public string BKF410 { get; set; }

        /// <summary>
        /// Tiền sử gia đình
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF744 { get; set; }

        /// <summary>
        /// Khám thực thể--Nhiệt độ（℃）
        /// </summary>
        /// <returns></returns>
        public string BKF352 { get; set; }

        /// <summary>
        /// Khám thực thể--Mạch（Lần/miSố）
        /// </summary>
        /// <returns></returns>
        public string BKF339 { get; set; }

        /// <summary>
        /// Khám thực thể--Tần số thở
        /// </summary>
        /// <returns></returns>
        public string BKF350 { get; set; }

        /// <summary>
        /// Khám thực thể--Huyết áp tâm thu（mmHg）
        /// </summary>
        /// <returns></returns>
        public string BKF343 { get; set; }

        /// <summary>
        /// Khám thực thể--Huyết áp tâm trương（mmHg）
        /// </summary>
        /// <returns></returns>
        public string BKF344 { get; set; }

        /// <summary>
        /// Khám thực thể--Chiều cao（cm）
        /// </summary>
        /// <returns></returns>
        public string BKF351 { get; set; }

        /// <summary>
        /// Khám thực thể--Cân nặng（kg）
        /// </summary>
        /// <returns></returns>
        public string BKF353 { get; set; }

        /// <summary>
        /// Khám thực thể--Kết quả kiểm tra tình trạng chung
        /// </summary>
        /// <returns></returns>
        public string BKF349 { get; set; }

        /// <summary>
        /// Khám thực thể--Kết quả kiểm tra da và niêm mạc
        /// </summary>
        /// <returns></returns>
        public string BKF340 { get; set; }

        /// <summary>
        /// Khám thực thể--Kết quả kiểm tra hạch bạch huyết nông toàn thân
        /// </summary>
        /// <returns></returns>
        public string BKF341 { get; set; }

        /// <summary>
        /// Khám thực thể--Kết quả kiểm tra đầu và các cơ quan
        /// </summary>
        /// <returns></returns>
        public string BKF346 { get; set; }

        /// <summary>
        /// Khám thực thể--Kết quả kiểm tra cổ
        /// </summary>
        /// <returns></returns>
        public string BKF338 { get; set; }

        /// <summary>
        /// Khám thực thể--Kết quả kiểm tra ngực
        /// </summary>
        /// <returns></returns>
        public string BKF348 { get; set; }

        /// <summary>
        /// Khám thực thể--Kết quả kiểm tra bụng
        /// </summary>
        /// <returns></returns>
        public string BKF335 { get; set; }

        /// <summary>
        /// Khám thực thể--Mô tả kết quả thăm trực tràng bằng ngón tay
        /// </summary>
        /// <returns></returns>
        public string BKF336 { get; set; }

        /// <summary>
        /// Khám thực thể--Kết quả kiểm tra cơ quan sinh dục ngoài
        /// </summary>
        /// <returns></returns>
        public string BKF347 { get; set; }

        /// <summary>
        /// Khám thực thể--Kết quả kiểm tra cột sống
        /// </summary>
        /// <returns></returns>
        public string BKF337 { get; set; }

        /// <summary>
        /// Khám thực thể--Kết quả kiểm tra tứ chi
        /// </summary>
        /// <returns></returns>
        public string BKF345 { get; set; }

        /// <summary>
        /// Khám thực thể--Kết quả kiểm tra hệ thần kinh
        /// </summary>
        /// <returns></returns>
        public string BKF342 { get; set; }

        /// <summary>
        /// Tình trạng chuyên khoa
        /// </summary>
        /// <returns></returns>
        public string BKF547 { get; set; }

        /// <summary>
        /// Kết quả kiểm tra bổ sung
        /// </summary>
        /// <returns></returns>
        public string BKF746 { get; set; }

        /// <summary>
        /// Đông y“Tứ chẩn”Mô tả kết quả quan sát
        /// </summary>
        /// <returns></returns>
        public string BKF458 { get; set; }

        /// <summary>
        /// Mã phân loại biện chứng
        /// </summary>
        /// <returns></returns>
        public string BKF097 { get; set; }

        /// <summary>
        /// Tên phân loại biện chứng
        /// </summary>
        /// <returns></returns>
        public string BKF099 { get; set; }

        /// <summary>
        /// Nguyên tắc và phương pháp điều trị
        /// </summary>
        /// <returns></returns>
        public string BKF440 { get; set; }

        /// <summary>
        /// Mã bác sĩ tiếp nhận
        /// </summary>
        /// <returns></returns>
        public string BKF207 { get; set; }

        /// <summary>
        /// Tên bác sĩ tiếp nhận
        /// </summary>
        /// <returns></returns>
        public string BKF208 { get; set; }

        /// <summary>
        /// Mã bác sĩ nội trú
        /// </summary>
        /// <returns></returns>
        public string BKF693 { get; set; }

        /// <summary>
        /// Tên bác sĩ nội trú
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF692 { get; set; }

        /// <summary>
        /// Mã bác sĩ trưởng
        /// </summary>
        /// <returns></returns>
        public string BKF695 { get; set; }

        /// <summary>
        /// Tên bác sĩ trưởng
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF696 { get; set; }

        /// <summary>
        /// Mã bác sĩ điều trị chính
        /// </summary>
        /// <returns></returns>
        public string BKF050 { get; set; }

        /// <summary>
        /// Tên bác sĩ điều trị chính
        /// </summary>
        /// <returns></returns>
        [Required]
        public string AKC273 { get; set; }

        /// <summary>
        /// Triệu chứng chính
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKE013 { get; set; }

        /// <summary>
        /// Nguyên nhân nhập viện
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF266 { get; set; }

        /// <summary>
        /// Đường nhập viện
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF818 { get; set; }

        /// <summary>
        /// (Apgar) Giá trị điểm
        /// </summary>
        /// <returns></returns>
        public string BKF095 { get; set; }

        /// <summary>
        /// Tình hình ăn uống
        /// </summary>
        /// <returns></returns>
        public string BKF403 { get; set; }

        /// <summary>
        /// Mức độ phát triển
        /// </summary>
        /// <returns></returns>
        public string BKF158 { get; set; }

        /// <summary>
        /// Dấu hiệu trạng thái tinh thần bình thường
        /// </summary>
        /// <returns></returns>
        public string BKF212 { get; set; }

        /// <summary>
        /// Tình trạng giấc ngủ
        /// </summary>
        /// <returns></returns>
        public string BKF323 { get; set; }

        /// <summary>
        /// Tình trạng đặc biệt
        /// </summary>
        /// <returns></returns>
        public string BKF334 { get; set; }

        /// <summary>
        /// Trạng thái tâm lý
        /// </summary>
        /// <returns></returns>
        public string BKF369 { get; set; }

        /// <summary>
        /// Trạng thái dinh dưỡng
        /// </summary>
        /// <returns></returns>
        public string BKF407 { get; set; }

        /// <summary>
        /// Khả năng tự chăm sóc
        /// </summary>
        /// <returns></returns>
        public string BKF560 { get; set; }

        /// <summary>
        /// Tên hạng mục quan sát điều dưỡng
        /// </summary>
        /// <returns></returns>
        public string BKF177 { get; set; }

        /// <summary>
        /// Kết quả quan sát điều dưỡng
        /// </summary>
        /// <returns></returns>
        public string BKF176 { get; set; }

        /// <summary>
        /// Dấu hiệu hút thuốc
        /// </summary>
        /// <returns></returns>
        public string BKF358 { get; set; }

        /// <summary>
        /// Số ngày ngừng hút thuốc
        /// </summary>
        /// <returns></returns>
        public string BKF354 { get; set; }
        /// <summary>
        /// Tình trạng hút thuốc
        /// </summary>
        /// <returns></returns>
        public string BKF359 { get; set; }
        /// <summary>
        /// Lượng hút thuốc mỗi ngày（điếu）
        /// </summary>
        /// <returns></returns>
        public string BKF258 { get; set; }
        /// <summary>
        /// Dấu hiệu uống rượu 0 Không；1 Có
        /// </summary>
        /// <returns></returns>
        public string BKF401 { get; set; }
        /// <summary>
        /// Tần suất uống rượu
        /// </summary>
        /// <returns></returns>
        public string BKF402 { get; set; }
        /// <summary>
        /// Lượng rượu uống mỗi ngày（mL）
        /// </summary>
        /// <returns></returns>
        public string BKF259 { get; set; }
        /// <summary>
        /// Định dạng ngày giờ đánh giá：yyyymmddhhmiss
        /// </summary>
        /// <returns></returns>
        public string BKF249 { get; set; }
        /// <summary>
        /// Tên y tá chịu trách nhiệm
        /// </summary>
        /// <returns></returns>
        public string BKF412 { get; set; }
    }
}
