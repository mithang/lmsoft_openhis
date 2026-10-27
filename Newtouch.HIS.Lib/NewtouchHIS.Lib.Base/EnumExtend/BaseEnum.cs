using System.ComponentModel;

namespace NewtouchHIS.Lib.Base
{
    public class BaseEnum
    {
        /// <summary>
        /// Loại mô-đun
        /// </summary>
        public enum ModuTypeEnum
        {
            [Description("Cấp ứng dụng")]
            App = 0,
            [Description("Mô-đun nghiệp vụ")]
            Bus = 1,

        }
        /// <summary>
        /// Cấp độ quyền
        /// </summary>
        public enum AuthLevEnum
        {
            [Description("Từ chối truy cập")]
            deny = 0,
            [Description("Khách")]
            guest = 1001,
            [Description("Người dùng bị hạn chế")]
            limituser = 1002,
            [Description("Người dùng thông thường")]
            user = 1003,
            [Description("Người dùng VIP")]
            vip = 1004,
            [Description("Người dùng đáng tin cậy")]
            trust = 1,
            [Description("Người dùng quản trị")]
            admin = 9998,
            [Description("Quản trị viên cấp cao")]
            root = 9999,
        }
        /// <summary>
        /// Loại ủy quyền
        /// </summary>
        public enum AuthTypeEnum
        {
            [Description("Web")]
            Web = 1,
            [Description("WebAPI")]
            WebApi = 2
        }

        /// <summary>
        /// Trạng thái tin nhắn
        /// </summary>
        [Description("Trạng thái tin nhắn")]
        public enum NoticeStuEnum
        {
            [Description("Chưa gửi")]
            UnSend = 0,
            [Description("Đã gửi")]
            Send = 1,
            [Description("Đã đọc")]
            Read = 2,
            [Description("Chờ xử lý")]
            Wait = 9
        }

        /// <summary>
        /// Giới hạn phạm vi áp dụng tin nhắn
        /// </summary>
        [Description("Phạm vi áp dụng của nhóm tin nhắn")]
        public enum MsgNoticeRangeEnum
        {
            [Description("Không giới hạn")]
            None = 0,
            [Description("Toàn viện")]
            Org = 1,
            [Description("Người dùng chỉ định")]
            User = 2,
            [Description("Trong nhóm tin nhắn")]
            Group = 10,
            [Description("Phạm vi khoa")]
            Dept = 20,
            [Description("Nội bộ khoa")]
            DeptGroup = 21,
            [Description("Phạm vi khu bệnh")]
            Area = 30,
            [Description("Nội bộ khu bệnh")]
            AreaGroup = 31,
        }
        /// <summary>
        /// Loại thực thi hàng đợi
        /// </summary>
        [Description("Loại thực thi hàng đợi")]
        public enum MsgQueueExecTypeEnum
        {
            [Description("Nhiệm vụ ngay lập tức")]
            Immediately = 1,
            [Description("Nhiệm vụ trì hoãn")]
            Waitime = 2,
            [Description("Nhiệm vụ định kỳ")]
            Fixedtime = 3,
        }


        [Description("Loại người nhận")]
        public enum RecipientTypeEnum
        {
            [Description("người dùng")]
            user = 1,
            [Description("Nhóm người dùng")]
            usergroup = 2,
        }
        /// <summary>
        /// Loại nghiệp vụ của nhóm tin nhắn
        /// </summary>
        [Description("Loại nghiệp vụ của nhóm tin nhắn")]
        public enum GroupYwlxEnum
        {
            [Description("Chung")]
            None = 0,
            [Description("Ngoại trú")]
            Mz = 1,
            [Description("Nằm viện")]
            Zy = 2,
        }


        public enum ExpireType
        {
            /// <summary>
            /// Hết hạn tuyệt đối
            /// Lưu ý: tức là hết hạn sau một khoảng thời gian kể từ khi tạo
            /// </summary>
            Absolute,

            /// <summary>
            /// Hết hạn tương đối
            /// Lưu ý: tức là hết hạn sau một khoảng thời gian khi khóa này không được truy cập, nếu khóa này luôn được truy cập thì thời gian hết hạn tự động kéo dài
            /// </summary>
            Relative,
        }

        public enum CacheType
        {
            /// <summary>
            /// Sử dụng bộ nhớ đệm trong bộ nhớ(Không hỗ trợ phân tán)
            /// </summary>
            Memory,

            /// <summary>
            /// Sử dụng bộ nhớ đệm Redis(Hỗ trợ phân tán)
            /// </summary>
            Redis
        }
    }
}
 