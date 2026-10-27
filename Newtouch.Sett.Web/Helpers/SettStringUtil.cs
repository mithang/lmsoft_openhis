namespace Newtouch.Sett.Web.Helpers
{
    /// <summary>
    /// Core replacement for Framework StringUtil (pinyin helpers, binary-only).
    /// GetChineseSpell falls back to the raw text: no pinyin dictionary is
    /// available on Linux, and the value is only the auto-suggested "py" code
    /// (user-editable in the UI). Documented stub, not fake pinyin.
    /// </summary>
    public static class SettStringUtil
    {
        public static string GetChineseSpell(string text)
        {
            return text;
        }
    }
}
