using System.ComponentModel;

namespace NewtouchHIS.Base.Domain.EnumExtend
{
    public class DRGEnum
    {
        /// <summary>
        /// DRGphiên bản bộ phân nhóm
        /// </summary>
        public enum EnumDrgGrouperType
        {
            /// <summary>
            /// CHS-DRG 1.0bản sửa đổi、bản Tây An、bản Thành Đô
            /// </summary>
            [Description("CHS-DRG 1.0修订版、西安版、成都版")]
            chs_drg_10 = 10,
            /// <summary>
            /// CHS-DRG 1.1bản tiêu chuẩn、bản Đồng Xuyên、bản Lâm Nghi
            /// </summary>
            [Description("CHS-DRG 1.1标准版、铜川版、临沂版")]
            chs_drg_11 = 11,
            /// <summary>
            /// bản Bắc Kinh
            /// </summary>
            [Description("北京版")]
            beijing_2022 = 1100001,
            /// <summary>
            /// bản Lâm Phần
            /// </summary>
            [Description("临汾版")]
            linfen_2022 = 1410001,
            /// <summary>
            /// bản Vô Tích
            /// </summary>
            [Description("无锡版")]
            wuxi_2022 = 3202001,
            /// <summary>
            /// bản Thường Châu
            /// </summary>
            [Description("常州版")]
            changzhou_2022 = 3204001,
            /// <summary>
            /// bản Tô Châu
            /// </summary>
            [Description("苏州版")]
            suzhou_2023 = 3205002,
            /// <summary>
            /// bản Diêm Thành
            /// </summary>
            [Description("盐城版")]
            yancheng_2023 = 3209002,
            /// <summary>
            /// bản Thái Châu
            /// </summary>
            [Description("泰州版")]
            taizhou_2022 = 3212001,
            /// <summary>
            /// bản Phúc Châu
            /// </summary>
            [Description("福州版")]
            fuzhou_2022 = 3501001,
            /// <summary>
            /// bản Thanh Đảo
            /// </summary>
            [Description("青岛版")]
            qingdao_2023 = 3702002,
            /// <summary>
            /// bản Yên Đài
            /// </summary>
            [Description("烟台版")]
            yantai_2023 = 3706002,
            /// <summary>
            /// bản Vũ Hán
            /// </summary>
            [Description("武汉版")]
            wuhan_2022 = 4201001,
            /// <summary>
            /// bản khu vực Trường - Chu - Đàm - Hành
            /// </summary>
            [Description("长株潭衡区域版")]
            changsha_2022 = 4301001,
            /// <summary>
            /// bản Lan Châu
            /// </summary>
            [Description("兰州版")]
            lanzhou_2023 = 6201002,
            /// <summary>
            /// bản Khánh Dương
            /// </summary>
            [Description("庆阳版")]
            qingyang_2023 = 6210002,
            /// <summary>
            /// bản Urumqi
            /// </summary>
            [Description("乌鲁木齐版")]
            wlmq_2022 = 6501001,

        }


        public enum EnumDrgGrouperTypePolicy
        {
            /// <summary>
            /// CHS-DRG 1.1bản tiêu chuẩn、bản Đồng Xuyên、bản Lâm Nghi
            /// </summary>
            [Description("CHS-DRG 1.1标准版、铜川版、临沂版")]
            ChsDrg11Policy = 11,
            /// <summary>
            /// bản Vũ Hán
            /// </summary>
            [Description("武汉版")]
            DrgWuhan2022Policy = 4201001,
        }
    }
}
