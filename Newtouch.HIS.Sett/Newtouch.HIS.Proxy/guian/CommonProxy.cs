using System;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Repository.SystemManage;
using Newtouch.Core.Redis.Cache;
using Newtouch.HIS.Proxy.Common;
using Newtouch.HIS.Proxy.guian.DTO;
using Newtouch.HIS.Proxy.guian.DTO.S02;
using Newtouch.HIS.Proxy.guian.DTO.S03;
using Newtouch.HIS.Proxy.guian.DTO.S29;
using Newtouch.HIS.Proxy.GuiAnXnhReference;
using Newtouch.HIS.Proxy.Log;
using NLog.Client.Util;

namespace Newtouch.HIS.Proxy.guian
{
    /// <summary>
    /// côngtổng cộnggiao diện
    /// </summary>
    public class CommonProxy
    {
        #region 单例
        private static readonly CommonProxy _commonProxy = new CommonProxy();

        private CommonProxy()
        {
        }

        public static CommonProxy GetInstance(string organizeId)
        {
            OrganizeId = organizeId;
            return _commonProxy;
        }

        #endregion

        /// <summary>
        /// ID tổ chức
        /// </summary>
        public static string OrganizeId { get; set; }

        /// <summary>
        /// xác thực và phân quyền
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool InterfaceS02(out string billCode)
        {
            //return PerformanceMonitoring.RequestMonitor(_ => WebServiceHelper<RequestBaseHeadDTO>.xnh_InterfaceCall(request, OrganizeId), request, "CommonProxy S02");
            try
            {
                var sysConfigRepo = new SysConfigRepo(new DefaultDatabaseFactory(), new SERedisCache());
                var userInfo = sysConfigRepo.GetValueByCode("guian_xnh_userInfo", OrganizeId);
                var s02Body = new S02RequestDTO();
                if (!string.IsNullOrWhiteSpace(userInfo))
                {
                    s02Body = userInfo.ToObject<S02RequestDTO>();
                }

                s02Body = s02Body ?? new S02RequestDTO();
                RequestBaseHeadDTO inHead = new RequestBaseHeadDTO
                {
                    operCode = "S02"
                };
                S02RequestDTO inBody = new S02RequestDTO
                {
                    userName = "gaxqzd",
                    passWord = "08A73F723AE68F27945DB81EC8DB45CB"
                };
                string responseDtoStr = WebServiceHelper.xnh_InterfaceCall<RequestBaseHeadDTO, S02RequestDTO>(inHead, inBody);
                ResponseBaseDTO<S02ResponseDTO> resDto = XMLHelper.XmlDeserialize<ResponseBaseDTO<S02ResponseDTO>>(responseDtoStr);
                if (resDto != null && resDto.state)
                {
                    billCode = resDto.data.billCode;
                    return true;
                }
                else
                {
                    billCode = null;
                    return false;
                }
            }
            catch (Exception e)
            {
                billCode = null;
                return false;
            }

        }

        /// <summary>
        /// S02 máy khách truyền vào ngư ời dùng tên mật khẩu tiến hành thân phận xác thựcquay lạichứng từmã, mật khẩu cầnMD5 mã hóa 32 vị chữ hoa
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public Response<S02ResponseDTO> S02(string userName, string passWord)
        {
            try
            {
                var requestBody = new S02RequestDTO
                {
                    userName = userName,
                    passWord = passWord
                };
                var inBodyXml = XMLSerializer.Serialize(requestBody);
                var requestHead = new RequestHeaderBase
                {
                    operCode = "S02",
                    rsa = MD5Helper.GetMd5Hash(inBodyXml)
                };
                var inHeadXml = XMLSerializer.Serialize(requestHead);
                var requestXml = string.Format("inHeadXml：{0}；inBodyXml：{1}", inHeadXml, inBodyXml);
                var responseStr = PerformanceMonitoring.RequestMonitor(_ => new HisServiceClient().request(inHeadXml, inBodyXml), requestXml, "S02");
                return XMLSerializer.DeSerialize(responseStr, typeof(Response<S02ResponseDTO>)) as Response<S02ResponseDTO>;
            }
            catch (Exception e)
            {
                LogCore.Error("S02 error", e);
                throw;
            }
        }

        /// <summary>
        /// theo truyền vàocủa số chứng nhận y tế lấy gia đ ình của tham gia hợp tác thành viên danh sách
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<List<member>> S03(S03RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<S03RequestDTO, Response<List<member>>>(request, OrganizeId).Call() as Response<List<member>>;
            return response;
        }

        /// <summary>
        /// Tải thông tin từ điển bệnh theo dấu thời gian
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<List<item>> S29(S29RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<S29RequestDTO, Response<List<item>>>(request, OrganizeId).Call() as Response<List<item>>;
            return response;
        }

        /// <summary>
        /// Tải thông tin từ điển bệnh theo dấu thời gian
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<List<DTO.S30.item>> S30(Newtouch.HIS.Proxy.guian.DTO.S30.S30RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<DTO.S30.S30RequestDTO, Response<List<DTO.S30.item>>>(request, OrganizeId).Call() as Response<List<DTO.S30.item>>;
            return response;
        }
    }
}
