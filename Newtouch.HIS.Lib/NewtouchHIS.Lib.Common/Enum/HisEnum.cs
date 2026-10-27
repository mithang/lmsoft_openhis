using System.ComponentModel;

namespace NewtouchHIS.Lib.Common
{
    /// <summary>
    /// HIS Enum chung 
    /// Có thể dùng để chuyển đổi tương tác với bên thứ ba
    /// </summary>
    public static class HisEnum
    {
        /// <summary>
        /// Loại thiết bị truy cập
        /// </summary>
        public enum MediaTerminalType
        {
            [Description("Trình duyệt máy tính")]
            Browser = 0,
            [Description("Thiết bị di động")]
            Mobile = 1,
        }

        public enum EnumMarriageStu
        {
            [Description("Chưa kết hôn")]
            unmarried =1,
            [Description("Đã kết hôn")]
            married = 2,
            [Description("Không rõ")]
            unknown = 3,

        }
        /// <summary>
        /// Cách đo đường huyết
        /// </summary>
        public enum EnumXtclfs
        {
            [Description("Đường huyết lúc đói")]
            xuetang_kf = 1,
            [Description("Sau bữa ăn 2 giờ")]
            xuetang_ch = 2,
            [Description("Ngẫu nhiên")]
            xuetang_sj,
        }
        /// <summary>
        /// Tính chất bệnh nhân
        /// Định nghĩa giống nội bộ:NewtouchHIS.Domain.Enum.EnumBrxz
        /// </summary>
        public enum EnumBrxz
        {
            /// <summary>
            /// Tự trả
            /// </summary>
            [Description("Tự trả")]
            zf = 0,
            /// <summary>
            /// Bảo hiểm y tế cho người lao động
            /// </summary>
            [Description("Bảo hiểm y tế cho người lao động")]
            zg = 1,
            /// <summary>
            /// Bảo hiểm y tế cho cư dân
            /// </summary>
            [Description("Bảo hiểm y tế cho cư dân")]
            jm = 2,
            /// <summary>
            /// Nghỉ hưu
            /// </summary>
            [Description("Nghỉ hưu")]
            lx = 3,
            /// <summary>
            /// Bảo hiểm y tế thông thường
            /// </summary>
            [Description("Bảo hiểm y tế thông thường")]
            pt = 11,
        }
        /// <summary>
        /// Trạng thái khám từ xa
        /// </summary>
        public enum EmunRemoteTreatedStu
        {
            [Description("Chờ xác nhận")]
            dqr = 1,
            [Description("Đang khám")]
            jzz = 2,
            [Description("Kết thúc khám")]
            yjs = 3,
            [Description("Đã bác bỏ")]
            yth = 4,
            [Description("Đã thu hồi")]
            ycx = 5,
            [Description("Đã phát thuốc")]
            yfy = 6,
        }

        #region Từ điển liên quan đến thuốc
        /// <summary>
        /// Cách dùng thuốc đông y
        /// </summary>
        public enum EnumTCMUsage
        {
            [Description("Sắc trước")]
            XJ = 20001,
            [Description("Cho vào sau")]
            HX = 20002,
            [Description("Sắc trong túi")]
            BJ = 20003,
            [Description("Hòa tan")]
            YH = 20004,
            [Description("Đập vụn")]
            DS = 20005,
            [Description("Ngâm chân")]
            ZY = 20006,
            [Description("Gói riêng")]
            LB = 20007,
            [Description("Sắc uống")]
            SJF = 20011,            
            [Description("Đắp ngoài")]
            WF = 20603,
            [Description("Sắc thuốc bằng máy sắc thuốc")]
            JYJ = 20604,
            [Description("Uống pha")]
            XWZS = 20620,
            [Description("Uống pha")]
            CF = 20700,
            [Description("Hãm uống")]
            PF = 20701,
        }
       
        #endregion


        #region Nằm viện
        /// <summary>
        /// Dấu hiệu đang ở viện
        /// </summary>
        public enum EnumZYBZ
        {
            /// <summary>
            /// Đăng ký nhập viện
            /// </summary>
            Xry = 0,
            /// <summary>
            /// Đang ở khu bệnh
            /// </summary>
            Bqz = 1,
            /// <summary>
            /// Xuất khỏi khu bệnh (rời khỏi khu bệnh) (chờ quyết toán)
            /// </summary>
            Djz = 2,
            /// <summary>
            /// Đã xuất viện (quyết toán xuất viện)
            /// </summary>
            Ycy = 3,
            /// <summary>
            /// Chuyển khu
            /// </summary>
            Zq = 7,

            /// <summary>
            /// Hủy bản ghi/Hủy nhập viện
            /// </summary>
            Wry = 9,
        }
        #endregion


        #region CIS
        /// <summary>
        /// Loại đơn thuốc
        /// </summary>
        public enum EnumCflx
        {
            /// <summary>
            /// Đơn thuốc tây
            /// </summary>
            [Description("Đơn thuốc tây")]
            WMPres = 1,
            /// <summary>
            /// Đơn thuốc đông y
            /// </summary>
            [Description("Đơn thuốc đông y")]
            TCMPres = 2,
            /// <summary>
            /// Đơn phục hồi chức năng
            /// </summary>
            [Description("Đơn phục hồi chức năng")]
            RehabPres = 3,
            /// <summary>
            /// Đơn xét nghiệm
            /// </summary>
            [Description("Đơn xét nghiệm")]
            InspectionPres = 4,
            /// <summary>
            /// Đơn kiểm tra
            /// </summary>
            [Description("Đơn kiểm tra")]
            ExaminationPres = 5,
            /// <summary>
            /// Đơn hạng mục thông thường
            /// </summary>
            [Description("Đơn hạng mục thông thường")]
            RegularItemPres = 6,

            [Description("Đơn thuốc điện tử")]
            Dzcf = 7,

        }

        public enum EnumYpyf
        {
            [Description("Tây y")]
            WM = 1,
            [Description("Đông y")]
            TCM = 2
        }

        /// <summary>
        /// Loại chẩn đoán
        /// </summary>
        public enum EnumZdlx
        {
            /// <summary>
            /// Chẩn đoán chính
            /// </summary>
            [Description("Chẩn đoán chính")]
            Main = 1,
            /// <summary>
            /// Chẩn đoán phụ
            /// </summary>
            [Description("Chẩn đoán phụ")]
            Deputy = 2
        }
        #endregion
    }
}
