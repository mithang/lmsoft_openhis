using System.ComponentModel;

namespace NewtouchHIS.Lib.Base.EnumExtend
{
    /// <summary>
    /// Đa thuê bao
    /// </summary>
    public enum DBEnum
    {
        [Description("Cơ sở dữ liệu quản lý hậu trường")]
        BaseDb = 1,
        [Description("Cơ sở dữ liệu hệ thống quyết toán")]
        SettDb = 2,
        [Description("Cơ sở dữ liệu phối hợp y tế - điều dưỡng")]
        CisDb = 3,
        [Description("Cơ sở dữ liệu nhà thuốc - kho thuốc")]
        PdsDb = 4,
        [Description("Cơ sở dữ liệu hệ thống bệnh án điện tử")]
        EmrDb = 5,
        [Description("Cơ sở dữ liệu hệ thống bệnh án")]
        MrmsDb = 6,
        [Description("Cơ sở dữ liệu hệ thống kiểm soát chất lượng")]
        MrQcDb = 7,
        [Description("Cơ sở dữ liệu nền tảng thống nhất")]
        UnionDb = 8,
        [Description("Hệ thống quản lý phẫu thuật")]
        OrDb = 9,
        [Description("Hệ thống quản lý vật tư tiêu hao")]
        HerpDb = 10,
        [Description("Cơ sở dữ liệu giao diện")]
        InterfaceDb = 11,
    }


    public enum JoinTypeEnum
    {
        Inner,
        Left,
        Right,
        Full
    }

    /// <summary>
    /// SqlSugar.DbType
    /// </summary>
    public enum BusinessDBType
    {
        MySql = 0,
        SqlServer = 1,
        Sqlite = 2,
        Oracle = 3,
        PostgreSQL = 4,
        Dm = 5,
        Kdbndp = 6,
        Oscar = 7,
        MySqlConnector = 8,
        Access = 9,
        OpenGauss = 10,
        QuestDB = 11,
        HG = 12,
        ClickHouse = 13,
        GBase = 14,
        Odbc = 0xF,
        OceanBaseForOracle = 0x10,
        TDengine = 17,
        GaussDB = 18,
        OceanBase = 19,
        Tidb = 20,
        Custom = 900
    }

    public enum JoinKeyTypeEnum
    {
        Inner=0,
        Left=1, 
        Right=2, 
        Full=3
    }

    public enum OperatorEnum
    {
        And = 1,
        AndIF = 2,
        Or = 3,
        OrIF = 4
    }
}
