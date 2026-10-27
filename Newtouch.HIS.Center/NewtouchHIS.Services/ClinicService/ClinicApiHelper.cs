using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Services.ClinicService;
using static NewtouchHIS.Lib.Common.HisEnum;

namespace NewtouchHIS.Services
{
    /// <summary>
    /// Lớp hỗ trợ giao diện phòng khám
    /// </summary>
    public class ClinicApiHelper
    {
        /// <summary>
        /// Địa chỉ dịch vụ phòng khám điện toán đám mây
        /// </summary>
        public static string OuterClinicSystemAPIHost
        {
            get
            {
                string hostconfig = AppSettings.GetConfig("BusinessConfig:AppAPIHost:OuterClinicSystemAPIHost");
                if (hostconfig.EndsWith("/"))
                {
                    return hostconfig;
                }
                return $"{hostconfig}/";
            }
        }

        /// <summary>
        /// Lắp ráp header yêu cầu Header
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> HeaderItems(string? token)
        {
            string cacheTokenKey = RedisKey.TokenOfOhClinic;
            //Truyền vàotoken không rỗng thì đặt lạitoken Cache
            if (!string.IsNullOrWhiteSpace(token))
            {
                RedisHelper.SetString(cacheTokenKey, token);
            }
            else
            {
                token = RedisHelper.GetString(cacheTokenKey);
            }
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "X-Token", token }
            };
            return dic;
        }


        #region ApiRoute
        /// <summary>
        /// Lấy phòng khámTokenPhương thức
        /// </summary>
        public readonly static string GetTokenRoute = $"{OuterClinicSystemAPIHost}lease-backend-test/auth/getToken";
        /// <summary>
        /// Đăng ký khám chữa bệnhThông báo kết quả
        /// </summary>
        public readonly static string TreatedApplyResultNotice = $"{OuterClinicSystemAPIHost}lease-backend-test/outpatient/remoteDiagnosisTreatment/modifiedState";
        /// <summary>
        /// Lấy phòng khámBệnh nhânBệnh án
        /// </summary>
        public readonly static string GetPatMedicalRecord = $"{OuterClinicSystemAPIHost}lease-backend-test/outpatient/medicalRecord/medical/";
        /// <summary>
        /// Đồng bộHISBệnh nhânBệnh án
        /// </summary>
        public readonly static string SendHisPatMedicalRecord = $"{OuterClinicSystemAPIHost}lease-backend-test/outpatient/medicalRecord/saveAdd";
        /// <summary>
        /// Đồng bộHISDữ liệu đơn thuốc
        /// </summary>
        public readonly static string SendHisRecipelData = $"{OuterClinicSystemAPIHost}lease-backend-test/outpatient/medicalRecord/addRecipel";
        #endregion

        #region Method
        public static int ApplyStuExChange(int sqzt)
        {
            switch(sqzt)
            {
                case (int)EmunRemoteTreatedStu.dqr:
                    return (int)EmunClinicRemoteTreatedStu.dqr;
                case (int)EmunRemoteTreatedStu.jzz:
                    return (int)EmunClinicRemoteTreatedStu.jzz;
                case (int)EmunRemoteTreatedStu.yjs:
                    return (int)EmunClinicRemoteTreatedStu.yjs;
                case (int)EmunRemoteTreatedStu.yfy:
                    return (int)EmunClinicRemoteTreatedStu.yfy;
                case (int)EmunRemoteTreatedStu.yth:
                    return (int)EmunClinicRemoteTreatedStu.yth;
                case (int)EmunRemoteTreatedStu.ycx:
                    return (int)EmunClinicRemoteTreatedStu.wtj;
            }
            return -1;
        }
        #endregion

    }
}
