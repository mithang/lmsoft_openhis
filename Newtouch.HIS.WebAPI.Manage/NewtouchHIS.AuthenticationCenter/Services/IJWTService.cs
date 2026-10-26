using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.AuthenticationCenter.Services
{
    public interface IJWTService
    {
        Task<BusResult<JWTToken>> GetTokenAsync(JWTTokenRequest request);
        Task<BusResult<JWTToken>> TokenRefreshAsync(JWTTokenRequest request);
        #region auth
        ///// <summary>
        ///// lấy mã ủy quyền
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        ///// <exception cref="NotImplementedException"></exception>
        //Task<BusResult<string>> GetCode(string clientId, string userName, string password);
        ///// <summary>
        ///// theo phiênCodelấy mã ủy quyền
        ///// </summary>
        ///// <param name="clientId"></param>
        ///// <param name="sessionCode"></param>
        ///// <returns></returns>
        //Task<BusResult<string>> GetCodeBySessionCode(string clientId, string sessionCode);

        ///// <summary>
        ///// lấy theo mã ủy quyềnToken+RefreshToken
        ///// </summary>
        ///// <param name="authCode"></param>
        ///// <returns>Token+RefreshToken</returns>
        //Task<BusResult<JWTToken>> GetTokenWithRefresh(string authCode);

        ///// <summary>
        ///// theoRefreshTokenlàm mớiToken
        ///// </summary>
        ///// <param name="refreshToken"></param>
        ///// <param name="clientId"></param>
        ///// <returns></returns>
        //Task<string> GetTokenByRefresh(string refreshToken, string clientId);
        #endregion
    }
}
