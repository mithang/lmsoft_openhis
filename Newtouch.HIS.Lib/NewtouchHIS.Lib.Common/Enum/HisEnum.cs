using System.ComponentModel;

namespace NewtouchHIS.Lib.Common
{
    /// <summary>
    /// HIS enum chung 
    /// có thể dùng để chuyển đổi tương tác với bên thứ ba
    /// </summary>
    public static class HisEnum
    {
        /// <summary>
        /// loại thiết bị truy cập
        /// </summary>
        public enum MediaTerminalType
        {
            [Description("电脑浏览器")]
            Browser = 0,
            [Description("手机移动端")]
            Mobile = 1,
        }

        public enum EnumMarriageStu
        {
            [Description("未婚")]
            unmarried =1,
            [Description("已婚")]
            married = 2,
            [Description("不详")]
            unknown = 3,

        }
        /// <summary>
        /// phương thức đo đường huyết
        /// </summary>
        public enum EnumXtclfs
        {
            [Description("空腹血糖")]
            xuetang_kf = 1,
            [Description("餐后2小时")]
            xuetang_ch = 2,
            [Description("随机")]
            xuetang_sj,
        }
        /// <summary>
        /// tính chất bệnh nhân
        /// định nghĩa giống nội bộ：NewtouchHIS.Domain.Enum.EnumBrxz
        /// </summary>
        public enum EnumBrxz
        {
            /// <summary>
            /// tự chi trả
            /// </summary>
            [Description("自费")]
            zf = 0,
            /// <summary>
            /// bảo hiểm y tế công nhân viên
            /// </summary>
            [Description("职工医保")]
            zg = 1,
            /// <summary>
            /// bảo hiểm y tế dân cư
            /// </summary>
            [Description("居民医保")]
            jm = 2,
            /// <summary>
            /// nghỉ hưu
            /// </summary>
            [Description("离休")]
            lx = 3,
            /// <summary>
            /// bảo hiểm y tế thông thường
            /// </summary>
            [Description("普通医保")]
            pt = 11,
        }
        /// <summary>
        /// trạng thái khám từ xa
        /// </summary>
        public enum EmunRemoteTreatedStu
        {
            [Description("待确认")]
            dqr = 1,
            [Description("就诊中")]
            jzz = 2,
            [Description("就诊结束")]
            yjs = 3,
            [Description("已驳回")]
            yth = 4,
            [Description("已撤销")]
            ycx = 5,
            [Description("已发药")]
            yfy = 6,
        }

        #region từ điển liên quan đến thuốc
        /// <summary>
        /// cách dùng thuốc Đông y
        /// </summary>
        public enum EnumTCMUsage
        {
            [Description("先煎")]
            XJ = 20001,
            [Description("后下")]
            HX = 20002,
            [Description("包煎")]
            BJ = 20003,
            [Description("烊化")]
            YH = 20004,
            [Description("打碎")]
            DS = 20005,
            [Description("足浴")]
            ZY = 20006,
            [Description("另包")]
            LB = 20007,
            [Description("水煎服")]
            SJF = 20011,            
            [Description("外敷")]
            WF = 20603,
            [Description("煎药机煎药")]
            JYJ = 20604,
            [Description("冲服")]
            XWZS = 20620,
            [Description("冲服")]
            CF = 20700,
            [Description("泡服")]
            PF = 20701,
        }
       
        #endregion


        #region nằm viện
        /// <summary>
        /// dấu hiệu đang nằm viện
        /// </summary>
        public enum EnumZYBZ
        {
            /// <summary>
            /// đăng ký nhập viện
            /// </summary>
            Xry = 0,
            /// <summary>
            /// trong khu bệnh
            /// </summary>
            Bqz = 1,
            /// <summary>
            /// xuất viện khỏi khu bệnh（ra khỏi khu bệnh）（đang chờ thanh toán）
            /// </summary>
            Djz = 2,
            /// <summary>
            /// đã xuất viện（thanh toán xuất viện）
            /// </summary>
            Ycy = 3,
            /// <summary>
            /// chuyển khu
            /// </summary>
            Zq = 7,

            /// <summary>
            /// bản ghi hủy bỏ/hủy nhập viện
            /// </summary>
            Wry = 9,
        }
        #endregion


        #region CIS
        /// <summary>
        /// loại đơn thuốc
        /// </summary>
        public enum EnumCflx
        {
            /// <summary>
            /// đơn thuốc Tây y
            /// </summary>
            [Description("西药处方")]
            WMPres = 1,
            /// <summary>
            /// đơn thuốc Đông y
            /// </summary>
            [Description("中药处方")]
            TCMPres = 2,
            /// <summary>
            /// đơn thuốc phục hồi
            /// </summary>
            [Description("康复处方")]
            RehabPres = 3,
            /// <summary>
            /// kiểm nghiệm đơn thuốc
            /// </summary>
            [Description("检验处方")]
            InspectionPres = 4,
            /// <summary>
            /// kiểm tra đơn thuốc
            /// </summary>
            [Description("检查处方")]
            ExaminationPres = 5,
            /// <summary>
            /// đơn thuốc hạng mục thông thường
            /// </summary>
            [Description("常规项目处方")]
            RegularItemPres = 6,

            [Description("电子处方")]
            Dzcf = 7,

        }

        public enum EnumYpyf
        {
            [Description("西医")]
            WM = 1,
            [Description("中医")]
            TCM = 2
        }

        /// <summary>
        /// loại chẩn đoán
        /// </summary>
        public enum EnumZdlx
        {
            /// <summary>
            /// chẩn đoán chính
            /// </summary>
            [Description("主诊断")]
            Main = 1,
            /// <summary>
            /// chẩn đoán phụ
            /// </summary>
            [Description("副诊断")]
            Deputy = 2
        }
        #endregion
    }
}
