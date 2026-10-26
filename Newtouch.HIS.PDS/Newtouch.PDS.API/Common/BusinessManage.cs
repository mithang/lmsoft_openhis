using Newtouch.HIS.API.Common;
using Newtouch.PDS.Requset;

namespace Newtouch.PDS.API.Common
{
    /// <summary>
    /// Xử lý nghiệp vụ đơn giản
    /// </summary>
    public class BusinessManage
    {
        #region instance

        private static readonly BusinessManage _businessManage = new BusinessManage();

        private BusinessManage()
        {
        }

        /// <summary>
        /// Truy cập bên ngoài
        /// </summary>
        /// <returns></returns>
        public static BusinessManage GetInstance()
        {
            return _businessManage;
        }

        #endregion

        /// <summary>
        /// Lắp ráp thông điệp trả về
        /// </summary>
        /// <param name="result"></param>
        /// <param name="response"></param>
        public void BuildResponse(ActResult result, DefaultResponse response)
        {
            response = new DefaultResponse();
            if (result == null)
            {
                response.data = null;
                response.code = ResponseResultCode.ERROR;
                return;
            }
            response.data = result.Data;
            response.code = result.IsSucceed ? ResponseResultCode.SUCCESS : ResponseResultCode.FAIL;
        }
    }
}