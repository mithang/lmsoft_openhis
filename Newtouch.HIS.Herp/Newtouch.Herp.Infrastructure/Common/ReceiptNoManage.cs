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

        private static readonly System.Text.Encoding GbEncoding;

        static ReceiptNoManage()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            GbEncoding = System.Text.Encoding.GetEncoding("GBK");
        }

        /// <summary>
        /// Tạo số ngẫu nhiên Thời điểm（Năm tháng ngày giờ phút giây）+Số ngẫu nhiên 3 chữ số
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
        private static string GetSpellCode(string cnStr)
        {
            var strTemp = "";
            var iLen = cnStr.Length;
            for (var i = 0; i <= iLen - 1; i++)
            {
                strTemp += GetCharSpellCode(cnStr.Substring(i, 1));
            }
            return strTemp;
        }

        /// <summary>
        /// Lấy chữ cái pinyin đầu tiên của một chữ Hán，Nếu là chữ cái tiếng Anh thì trả về trực tiếp chữ hoa
        /// </summary>
        /// <param name="cnChar">Một chữ Hán đơn lẻ</param>
        /// <returns>Một chữ hoa đơn lẻ</returns>
        private static string GetCharSpellCode(string cnChar)
        {
            var zw = GbEncoding.GetBytes(cnChar);
            //Nếu là chữ cái，thì trả về trực tiếp chữ cái đầu
            if (zw.Length == 1)
            {
                return cnChar.ToUpper();
            }
            // get the array of byte from the single char
            int i1 = zw[0];
            int i2 = zw[1];
            long iCnChar = i1 * 256 + i2;
            // iCnChar match the constant
            if (iCnChar >= 45217 && iCnChar <= 45252)
            {
                return "A";
            }
            if (iCnChar >= 45253 && iCnChar <= 45760)
            {
                return "B";
            }
            if (iCnChar >= 45761 && iCnChar <= 46317)
            {
                return "C";
            }
            if (iCnChar >= 46318 && iCnChar <= 46825)
            {
                return "D";
            }
            if ((iCnChar >= 46826) && iCnChar <= 47009)
            {
                return "E";
            }
            if (iCnChar >= 47010 && iCnChar <= 47296)
            {
                return "F";
            }
            if (iCnChar >= 47297 && iCnChar <= 47613)
            {
                return "G";
            }
            if (iCnChar >= 47614 && iCnChar <= 48118)
            {
                return "H";
            }
            if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {
                return "J";
            }
            if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {
                return "K";
            }
            if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {
                return "L";
            }
            if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {
                return "M";
            }
            if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {
                return "N";
            }
            if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {
                return "O";
            }
            if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {
                return "P";
            }
            if ((iCnChar >= 50906) && (iCnChar <= 51386))
            {
                return "Q";
            }
            if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {
                return "R";
            }
            if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {
                return "S";
            }
            if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {
                return "T";
            }
            if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {
                return "W";
            }
            if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {
                return "X";
            }
            if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {
                return "Y";
            }
            if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {
                return "Z";
            }
            return ("?");
        }
    }
}
