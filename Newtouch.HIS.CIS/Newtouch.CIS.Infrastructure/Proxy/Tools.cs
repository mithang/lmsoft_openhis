using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy
{
    /// <summary>
    /// công cụ thể loại
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// Lấy Giới tính Mã
        /// </summary>
        /// <param name="gender"></param>
        /// <returns>Quay lại trong y tương ứng của Giới tính Mã</returns>
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
        /// chuyển đổi thành trong y tương ứng của chứng mục Mã loại
        /// </summary>
        /// <param name="zjlx">chứng mục Loại</param>
        /// <returns>trong y tương ứng của chứng mục Mã loại</returns>
        public static string CardTypeCodeChange(int zjlx)
        {
            switch (zjlx)
            {
                case 1://chứng minh nhân dân
                    return "01";
                case 2://Hộ chiếu
                    return "03";
                case 3://Giấy chứng nhận sĩ quan
                    return "04";
                case 9://khác
                default:
                    return "99";
            }
        }

        /// <summary>
        /// Lấy chứng mục Tên
        /// </summary>
        /// <param name="zjlx">cis chứng mục Loại</param>
        /// <returns>trong y tương ứng của chứng mục Tên loại</returns>
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
        /// chuyển đổi thành trong y tương ứng của Tình trạng hôn nhân Mã
        /// </summary>
        /// <param name="hf"></param>
        /// <returns></returns>
        public static string ChangeMarriedCode(int hf)
        {
            switch (hf)
            {
                case 0:// chưa kết hôn
                    return "10";
                case 1://đã kết hôn
                    return "20";
                case 9:
                default:
                    return "90";
            }
        }

        /// <summary>
        /// chuyển đổi thành trong y tương ứng của Tình trạng hôn nhân Mã
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
        /// bản địa bệnh nhân Tính chất chuyển trong y quán bệnh nhân Tính chất
        /// </summary>
        /// <param name="brxz"></param>
        /// <returns></returns>
        public static string ChangeBrxzCode(string brxz)
        {
            switch (brxz)
            {
                case "1"://cục bộ tự chi trả
                    return "2";// trong y quán tự chi trả
                case "0"://cục bộ tự chi trả
                default:
                    return "1";// trong y quán tự chi trả
            }
        }
    }
}