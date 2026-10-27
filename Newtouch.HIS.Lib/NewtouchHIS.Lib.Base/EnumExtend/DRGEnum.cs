using System.ComponentModel;

namespace NewtouchHIS.Base.Domain.EnumExtend
{
    public class DRGEnum
    {
        /// <summary>
        /// DRGPhiên bản bộ nhóm
        /// </summary>
        public enum EnumDrgGrouperType
        {
            /// <summary>
            /// CHS-DRG 1.0 bản sửa đổi, bản Tây An, bản Thành Đô
            /// </summary>
            [Description("CHS-DRG 1.0 bản sửa đổi, bản Tây An, bản Thành Đô")]
            chs_drg_10 = 10,
            /// <summary>
            /// CHS-DRG 1.1 bản tiêu chuẩn, bản Đồng Xuyên, bản Lâm Nghi
            /// </summary>
            [Description("CHS-DRG 1.1 bản tiêu chuẩn, bản Đồng Xuyên, bản Lâm Nghi")]
            chs_drg_11 = 11,
            /// <summary>
            /// Bản Bắc Kinh
            /// </summary>
            [Description("Bản Bắc Kinh")]
            beijing_2022 = 1100001,
            /// <summary>
            /// Bản Lâm Phần
            /// </summary>
            [Description("Bản Lâm Phần")]
            linfen_2022 = 1410001,
            /// <summary>
            /// Bản Vô Tích
            /// </summary>
            [Description("Bản Vô Tích")]
            wuxi_2022 = 3202001,
            /// <summary>
            /// Bản Thường Châu
            /// </summary>
            [Description("Bản Thường Châu")]
            changzhou_2022 = 3204001,
            /// <summary>
            /// Bản Tô Châu
            /// </summary>
            [Description("Bản Tô Châu")]
            suzhou_2023 = 3205002,
            /// <summary>
            /// Bản Diêm Thành
            /// </summary>
            [Description("Bản Diêm Thành")]
            yancheng_2023 = 3209002,
            /// <summary>
            /// Bản Thái Châu
            /// </summary>
            [Description("Bản Thái Châu")]
            taizhou_2022 = 3212001,
            /// <summary>
            /// Bản Phúc Châu
            /// </summary>
            [Description("Bản Phúc Châu")]
            fuzhou_2022 = 3501001,
            /// <summary>
            /// Bản Thanh Đảo
            /// </summary>
            [Description("Bản Thanh Đảo")]
            qingdao_2023 = 3702002,
            /// <summary>
            /// Bản Yên Đài
            /// </summary>
            [Description("Bản Yên Đài")]
            yantai_2023 = 3706002,
            /// <summary>
            /// Bản Vũ Hán
            /// </summary>
            [Description("Bản Vũ Hán")]
            wuhan_2022 = 4201001,
            /// <summary>
            /// Bản khu vực Trường - Chu - Đàm - Hành
            /// </summary>
            [Description("Bản khu vực Trường - Chu - Đàm - Hành")]
            changsha_2022 = 4301001,
            /// <summary>
            /// Bản Lan Châu
            /// </summary>
            [Description("Bản Lan Châu")]
            lanzhou_2023 = 6201002,
            /// <summary>
            /// Bản Khánh Dương
            /// </summary>
            [Description("Bản Khánh Dương")]
            qingyang_2023 = 6210002,
            /// <summary>
            /// Bản Ô Lỗ Mộc Tề
            /// </summary>
            [Description("Bản Ô Lỗ Mộc Tề")]
            wlmq_2022 = 6501001,

        }


        public enum EnumDrgGrouperTypePolicy
        {
            /// <summary>
            /// CHS-DRG 1.1 bản tiêu chuẩn, bản Đồng Xuyên, bản Lâm Nghi
            /// </summary>
            [Description("CHS-DRG 1.1 bản tiêu chuẩn, bản Đồng Xuyên, bản Lâm Nghi")]
            ChsDrg11Policy = 11,
            /// <summary>
            /// Bản Vũ Hán
            /// </summary>
            [Description("Bản Vũ Hán")]
            DrgWuhan2022Policy = 4201001,
        }
    }
}
