using System;
using System.Text;

namespace Newtouch.Herp.Infrastructure.Common
{
    /// <summary>
    /// Quản lý số chứng từ
    /// </summary>
    public static class ReceiptNoManage
    {
        public static Random random = new Random();

        /// <summary>
        /// Tạo số ngẫu nhiên Thời điểm(Năm tháng ngày giờ phút giây)+Số ngẫu nhiên 3 chữ số
        /// </summary>
        /// <returns></returns>
        public static string GetNewOrderNoV1()
        {
            return string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now) + random.Next(100, 1000);
        }

        /// <summary>
        /// Lấy số chứng từ nhập kho vật tư
        /// </summary>
        /// <param name="djmc"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetNewReceiptNo(string djmc, string userId = "")
        {
            return GetSpellCode(djmc) + string.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + userId;
        }

        /// <summary>
        /// Trong danh sách chuỗi đã chỉ định CnStr tìm chuỗi chỉ mục pinyin phù hợp
        /// </summary>
        /// <param name="cnStr">Chuỗi chữ Hán</param>
        /// <returns>Chuỗi chữ cái đầu pinyin Hán ngữ tương ứng</returns>
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
