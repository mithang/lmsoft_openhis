using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Services.ClinicService;
using static NewtouchHIS.Lib.Common.HisEnum;

namespace NewtouchHIS.Services
{
    /// <summary>
    /// lớp hỗ trợ giao diện phòng khám
    /// </summary>
    public class ClinicApiHelper
    {
        /// <summary>
        /// địa chỉ dịch vụ phòng khám trên đám mây
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
        /// lắp ráp tiêu đề yêu cầu Header
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> HeaderItems(string? token)
        {
            string cacheTokenKey = RedisKey.TokenOfOhClinic;
            //truyền vàotoken không trống thì đặt lạitoken bộ nhớ đệm
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
        /// lấy phòng khámTokenphương thức
        /// </summary>
        public readonly static string GetTokenRoute = $"{OuterClinicSystemAPIHost}lease-backend-test/auth/getToken";
        /// <summary>
        /// thông báo kết quả đăng ký khám chữa bệnh
        /// </summary>
        public readonly static string TreatedApplyResultNotice = $"{OuterClinicSystemAPIHost}lease-backend-test/outpatient/remoteDiagnosisTreatment/modifiedState";
        /// <summary>
        /// lấy bệnh án bệnh nhân phòng khám
        /// </summary>
        public readonly static string GetPatMedicalRecord = $"{OuterClinicSystemAPIHost}lease-backend-test/outpatient/medicalRecord/medical/";
        /// <summary>
        /// đồng bộHISbệnh án bệnh nhân
        /// </summary>
        public readonly static string SendHisPatMedicalRecord = $"{OuterClinicSystemAPIHost}lease-backend-test/outpatient/medicalRecord/saveAdd";
        /// <summary>
        /// đồng bộHISdữ liệu đơn thuốc
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
