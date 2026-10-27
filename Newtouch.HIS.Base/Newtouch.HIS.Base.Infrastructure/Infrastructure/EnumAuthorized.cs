using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Access-registration authorization levels (Registor). Binary-only in the
    /// Framework build: member names are taken from the Registor views that consume
    /// them; integer values could NOT be recovered from source (no decompiler here),
    /// so they are assigned 0-based in declaration order. CAVEAT: if the original
    /// values differ, rows stored before this port will display the wrong label in
    /// the Registor grid (which compares stored ints against these casts) — new rows
    /// are self-consistent. Descriptions are Vietnamese (translation target).
    /// </summary>
    public enum EnumAuthorizedLev
    {
        [Description("Đăng nhập mô phỏng")]
        imitatelogin = 0,
        [Description("Truy cập bằng salt")]
        saltaccess = 1,
        [Description("Truy cập bằng token")]
        tokenaccess = 2
    }

    /// <summary>Authorization validity periods (Registor). Same provenance caveat as above.</summary>
    public enum EnumAuthorizedPeriod
    {
        [Description("Dài hạn")]
        longtime = 0,
        [Description("Tháng")]
        month = 1,
        [Description("Nửa năm")]
        partyear = 2,
        [Description("Năm")]
        year = 3
    }
}
