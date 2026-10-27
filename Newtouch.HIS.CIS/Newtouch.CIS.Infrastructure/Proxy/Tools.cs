using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy
{
    /// <summary>
    /// côngcụ thểloại
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// LấyGiới tínhMã
        /// </summary>
        /// <param name="gender"></param>
        /// <returns>Quay lạitrongytương ứngcủaGiới tínhMã</returns>
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
        /// chuyểnđổithànhtrongytương ứngcủachứngmụcMã loại
        /// </summary>
        /// <param name="zjlx">chứngmụcLoại</param>
        /// <returns>trongytương ứngcủachứngmụcMã loại</returns>
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
        /// LấychứngmụcTên
        /// </summary>
        /// <param name="zjlx">cischứngmụcLoại</param>
        /// <returns>trongytương ứngcủachứngmụcTên loại</returns>
        public static string GetCardType(int zjlx)
        {
            var code = CardTypeCodeChange(zjlx);
            switch (code)
            {
                case "01":
                    return "cư dânchứng minh nhân dân";
                case "02":
                    return "cư dân户miệngbản";
                case "03":
                    return "Hộ chiếu";
                case "04":
                    return "Giấy chứng nhận sĩ quan";
                case "05":
                    return "驾驶chứng";
                case "06":
                    return "港澳cư dânđếntớitrongđịathôngdòngchứng";
                case "07":
                    return "太晚cư dânđếntớitrongđịathôngdòngchứng";
                case "09":
                default:
                    return "khácphương phápđịnhhiệu lựcchứngmục";

            }
        }

        /// <summary>
        /// chuyểnđổithànhtrongytương ứngcủaTình trạng hôn nhânMã
        /// </summary>
        /// <param name="hf"></param>
        /// <returns></returns>
        public static string ChangeMarriedCode(int hf)
        {
            switch (hf)
            {
                case 0://chưa婚
                    return "10";
                case 1://đã kết hôn
                    return "20";
                case 9:
                default:
                    return "90";
            }
        }

        /// <summary>
        /// chuyểnđổithànhtrongytương ứngcủaTình trạng hôn nhânMã
        /// </summary>
        /// <param name="hf"></param>
        /// <returns></returns>
        public static string GetMarried(int hf)
        {
            var code = ChangeMarriedCode(hf);
            switch (code)
            {
                case "10":
                    return "chưa婚";
                case "20":
                    return "đã kết hôn";
                case "21":
                    return "ban đầu婚";
                case "22":
                    return "lại婚";
                case "23":
                    return "lại婚";
                case "30":
                    return "丧chẵn";
                case "40":
                    return "离婚";
                case "90":
                default:
                    return "chưa biết";
            }
        }

        /// <summary>
        /// bảnđịabệnh nhânTính chấtchuyểntrongyquánbệnh nhânTính chất
        /// </summary>
        /// <param name="brxz"></param>
        /// <returns></returns>
        public static string ChangeBrxzCode(string brxz)
        {
            switch (brxz)
            {
                case "1"://cục bộ tự chi trả
                    return "2";// trongyquán tự chi trả
                case "0"://cục bộ tự chi trả
                default:
                    return "1";// trongyquán tự chi trả
            }
        }
    }
}