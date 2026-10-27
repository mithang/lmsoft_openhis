using System;
using System.Text;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// phiếusố phiếuQuản lý
    /// </summary>
    public static class ReceiptNoManage
    {
        public static Random random = new Random();

        /// <summary>
        /// tạoNgẫu nhiênsố Thời gianđiểm（Ngày tháng nămkhiphútgiây）+ba ký tựNgẫu nhiênsố
        /// </summary>
        /// <returns></returns>
        public static string GetNewOrderNoV1()
        {
            return string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now) + random.Next(100, 1000);
        }

        /// <summary>
        /// Lấyvật tưNhập khophiếusố phiếu
        /// </summary>
        /// <param name="djmc"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetNewReceiptNo(string djmc, string userId = "")
        {
            return GetSpellCode(djmc) + string.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + userId;
        }

        /// <summary>
        /// ởchỉđịnhcủachữkýchuỗidanh sáchCnStrtrongTìm kiếmkýhợpPinyintìmdẫnchữkýchuỗi
        /// </summary>
        /// <param name="cnStr">Hánchữchữkýchuỗi</param>
        /// <returns>nhautương ứngcủaHánngữPinyinđầuchữcáichuỗi</returns>
                /// <summary>
        /// Receipt prefix from a Vietnamese description: first letter of each word,
        /// diacritics stripped, upper-case. Descriptions used to be Chinese, so this
        /// previously returned pinyin initials; it now returns Vietnamese initials.
        /// Numbers already issued keep their old prefixes (stored strings); only
        /// numbers generated after this change use the new prefixes.
        /// </summary>
        private static string GetSpellCode(string str)
        {
            var sb = new System.Text.StringBuilder();
            foreach (var word in StripDiacritics(str).Split(new[] { ' ', '-', '/', '(', ')', '[', ']', '.', ',' }, System.StringSplitOptions.RemoveEmptyEntries))
            {
                if (char.IsLetter(word[0]))
                {
                    sb.Append(char.ToUpperInvariant(word[0]));
                }
            }
            return sb.ToString();
        }

        private static string StripDiacritics(string s)
        {
            var normalized = s.Normalize(System.Text.NormalizationForm.FormD);
            var sb = new System.Text.StringBuilder();
            foreach (var ch in normalized)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(ch) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }
            return sb.ToString().Replace('đ', 'd').Replace('Đ', 'D');
        }
    }
}
