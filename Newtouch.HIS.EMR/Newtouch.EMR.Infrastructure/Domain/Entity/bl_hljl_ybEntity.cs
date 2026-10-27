using System;
using SqlSugar;
namespace Newtouch.EMR.Domain.Entity
{

    [SugarTable("bl_hljl_yb")]
    public  class bl_hljl_ybEntity 
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        /// <summary>
        /// Ngày
        /// </summary>
        public string rq { get; set; }
        /// <summary>
        /// Thời gian
        /// </summary>
        public string sj { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Cấp độ điều dưỡng: 1 Đặc biệt, 2 Cấp một, 3 Cấp hai, 4 Cấp ba
        /// </summary>
        public string hldj { get; set; }
        /// <summary>
        /// Loại điều dưỡng: 1 Điều dưỡng cơ bản, 2 Điều dưỡng đặc biệt, 3 Điều dưỡng theo biến chứng, 4 Khác
        /// </summary>
        public string hllx { get; set; }
        /// <summary>
        /// Tiền sử dị ứng
        /// </summary>
        public string gms { get; set; }
        /// <summary>
        /// Cân nặng
        /// </summary>
        public string tz { get; set; }
        /// <summary>
        /// Nhiệt độ
        /// </summary>
        public string tw { get; set; }
        /// <summary>
        /// Tần số hô hấp
        /// </summary>
        public string hxpl { get; set; }
        /// <summary>
        /// Mạch
        /// </summary>
        public string ml { get; set; }
        /// <summary>
        /// Huyết áp tâm thu
        /// </summary>
        public string ssy { get; set; }
        /// <summary>
        /// Huyết áp tâm trương
        /// </summary>
        public string szy { get; set; }
        /// <summary>
        /// Độ bão hòa oxy trong máu
        /// </summary>
        public string xybhd { get; set; }
        /// <summary>
        /// Thở oxy
        /// </summary>
        public string xy { get; set; }
        /// <summary>
        /// Mạch động mạch mu chân
        /// </summary>
        public string zbdmbz { get; set; }
        /// <summary>
        /// Tình hình ăn uống: 1 Tốt, 2 Bình thường, 3 Kém
        /// </summary>
        public string ysqk { get; set; }
        /// <summary>
        /// Mô tả chăm sóc ống thông
        /// </summary>
        public string dghl { get; set; }
        /// <summary>
        /// Mô tả chăm sóc khí quản
        /// </summary>
        public string qghl { get; set; }
        /// <summary>
        /// Chăm sóc tư thế
        /// </summary>
        public string twhl { get; set; }
        /// <summary>
        /// Chăm sóc da
        /// </summary>
        public string pfhl { get; set; }
        /// <summary>
        /// Chăm sóc dinh dưỡng
        /// </summary>
        public string yyhl { get; set; }
        /// <summary>
        /// Hướng dẫn ăn uống: 01 Ăn thường, 02 Ăn mềm, 03 Ăn nửa lỏng, 04 Ăn lỏng, 05 Nhịn ăn, 06 Nhịn ăn và nước, 07 Ăn qua ống mũi, 08 Ăn ít muối ít mỡ, 09 Ăn cho bệnh tiểu đường, 99 Khác
        /// </summary>
        public string yszd { get; set; }
        /// <summary>
        /// Chăm sóc tâm lý: 1 Thực hiện chăm sóc tâm lý theo trạng thái tâm lý của bệnh nhân, 2 Hỗ trợ tâm lý cho gia đình
        /// </summary>
        public string xlhl { get; set; }
        /// <summary>
        /// Chăm sóc an toàn: 1 Thường xuyên tuần tra phòng bệnh, 2 Thêm thanh chắn giường, 3 Cố định tứ chi
        /// </summary>
        public string aqhl { get; set; }
        /// <summary>
        /// Tình trạng bệnh tóm tắt
        /// </summary>
        public string jybq { get; set; }
        /// <summary>
        /// Tên hạng mục quan sát điều dưỡng
        /// </summary>
        public string hlgcxm { get; set; }
        /// <summary>
        /// Kết quả quan sát điều dưỡng
        /// </summary>
        public string hlgcjg { get; set; }
        /// <summary>
        /// Tên thao tác điều dưỡng
        /// </summary>
        public string hlczmc { get; set; }
        /// <summary>
        /// Tên danh mục hạng mục thao tác điều dưỡng
        /// </summary>
        public string hlczxmmc { get; set; }
        /// <summary>
        /// Kết quả quan sát điều dưỡng
        /// </summary>
        public string hlczjg { get; set; }
        /// <summary>
        /// Phát bảng kiểm tra an toàn phẫu thuật
        /// </summary>
        public string fcssaqb { get; set; }
        /// <summary>
        /// Thu hồi bảng kiểm tra an toàn phẫu thuật
        /// </summary>
        public string shssaqb { get; set; }
        /// <summary>
        /// Phát bảng đánh giá rủi ro phẫu thuật
        /// </summary>
        public string fcssfxpgb { get; set; }
        /// <summary>
        /// Thu hồi bảng đánh giá rủi ro phẫu thuật
        /// </summary>
        public string shssfxpgb { get; set; }
        /// <summary>
        /// Có cách ly
        /// </summary>
        public string glbz { get; set; }
        /// <summary>
        /// Loại cách ly: 1 Cách ly đường tiêu hóa, 2 Cách ly đường hô hấp, 3 Cách ly tiếp xúc, 4 Cách ly máu - dịch cơ thể, 5 Cách ly nghiêm ngặt, 6 Cách ly côn trùng, 7 Cách ly bảo vệ
        /// </summary>
        public string glzl { get; set; }
        /// <summary>
        /// Chữ ký điều dưỡng
        /// </summary>
        public string hsqm { get; set; }
        /// <summary>
        /// Thời gian ký của điều dưỡng
        /// </summary>
        public string hsqmrqsj { get; set; }
        /// <summary>
        /// Ý thức
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// Đồng tử - trái
        /// </summary>
        public string tk_z { get; set; }
        /// <summary>
        /// Đồng tử - phải
        /// </summary>
        public string tk_y { get; set; }
        /// <summary>
        /// Phản ứng với ánh sáng - trái
        /// </summary>
        public string dgfs_z { get; set; }
        /// <summary>
        /// Phản ứng với ánh sáng - phải
        /// </summary>
        public string dgfs_y { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string OrganizeId { get; set; }
    }
}
