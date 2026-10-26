namespace Newtouch.CIS.Proxy
{
    /// <summary>
    /// Lớp công cụ
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// Lấy mã giới tính
        /// </summary>
        /// <param name="gender"></param>
        /// <returns>Trả về mã giới tính tương ứng của Đông y</returns>
        public static string GetGenderCode(string gender)
        {
            switch (gender.Trim())
            {
                case "男":
                case "男姓":
                case "男人":
                    return "1";
                case "女":
                case "女性":
                case "女人":
                    return "2";
                case "":
                    return "9";
                default:
                case "人妖":
                    return "0";
            }
        }

        /// <summary>
        /// Chuyển đổi thành mã loại giấy tờ tương ứng của Đông y
        /// </summary>
        /// <param name="zjlx">Loại giấy tờ</param>
        /// <returns>Mã loại giấy tờ tương ứng của Đông y</returns>
        public static string CardTypeCodeChange(int zjlx)
        {
            switch (zjlx)
            {
                case 1://Chứng minh nhân dân
                    return "01";
                case 2://Hộ chiếu
                    return "03";
                case 3://Chứng nhận sĩ quan
                    return "04";
                case 9://Khác
                default:
                    return "99";
            }
        }

        /// <summary>
        /// Lấy tên giấy tờ
        /// </summary>
        /// <param name="zjlx">Loại giấy tờ cis</param>
        /// <returns>Tên loại giấy tờ tương ứng của Đông y</returns>
        public static string GetCardType(int zjlx)
        {
            var code = CardTypeCodeChange(zjlx);
            switch (code)
            {
                case "01":
                    return "居民身份证";
                case "02":
                    return "居民户口本";
                case "03":
                    return "护照";
                case "04":
                    return "军官证";
                case "05":
                    return "驾驶证";
                case "06":
                    return "港澳居民来往内地通行证";
                case "07":
                    return "太晚居民来往内地通行证";
                case "09":
                default:
                    return "其他法定有效证件";

            }
        }

        /// <summary>
        /// Chuyển đổi thành mã tình trạng hôn nhân tương ứng của Đông y
        /// </summary>
        /// <param name="hf"></param>
        /// <returns></returns>
        public static string ChangeMarriedCode(int hf)
        {
            switch (hf)
            {
                case 0://Chưa kết hôn
                    return "10";
                case 1://Đã kết hôn
                    return "20";
                case 9:
                default:
                    return "90";
            }
        }

        /// <summary>
        /// Chuyển đổi thành mã tình trạng hôn nhân tương ứng của Đông y
        /// </summary>
        /// <param name="hf"></param>
        /// <returns></returns>
        public static string GetMarried(int hf)
        {
            var code = ChangeMarriedCode(hf);
            switch (code)
            {
                case "10":
                    return "未婚";
                case "20":
                    return "已婚";
                case "21":
                    return "初婚";
                case "22":
                    return "再婚";
                case "23":
                    return "复婚";
                case "30":
                    return "丧偶";
                case "40":
                    return "离婚";
                case "90":
                default:
                    return "未知";
            }
        }

        /// <summary>
        /// Chuyển tính chất bệnh nhân nội bộ thành tính chất bệnh nhân phòng khám Đông y
        /// </summary>
        /// <param name="brxz"></param>
        /// <returns></returns>
        public static string ChangeBrxzCode(string brxz)
        {
            switch (brxz)
            {
                case "1"://Nội trú Tự chi trả
                    return "2";// Phòng khám Đông y Tự chi trả
                case "0"://Nội trú Tự chi trả
                default:
                    return "1";// Phòng khám Đông y Tự chi trả
            }
        }
    }
}