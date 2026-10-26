using Newtouch.HIS.Proxy.guian.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.HIS.Proxy.guian.DTO.S16;

namespace Newtouch.HIS.Proxy.guian
{
    /// <summary>
    /// Nhập viện quản lý liên quan 
    /// </summary>
    public class HospitalizationProxy
    {
        #region 单例
        private static readonly HospitalizationProxy _hospitalizationProxy = new HospitalizationProxy();

        private HospitalizationProxy()
        {
        }

        public static HospitalizationProxy GetInstance(string organizeId)
        {
            OrganizeId = organizeId;
            return _hospitalizationProxy;
        }

        #endregion


        /// <summary>
        /// ID tổ chức
        /// </summary>
        public static string OrganizeId { get; set; }

        ///// <summary>
        ///// Nhập viện làm thủ tục 
        ///// </summary>
        ///// <param name="inBody"></param>
        ///// <returns></returns>
        //public ResponseBaseDTO<S04ResponseDTO> S04(S04RequestDTO inBody)
        //{
        //    return WebServiceHelper.xnh_HospitalCall<S04RequestDTO, S04ResponseDTO>(inBody, "S04");
        //    #region
        //    //try
        //    //{
        //    //    CommonProxy _commonProxy = CommonProxy.GetInstance();
        //    //    string billCode = string.Empty;
        //    //    if (_commonProxy.InterfaceS02(out billCode))
        //    //    {
        //    //        RequestHeadDTO inHead = new RequestHeadDTO
        //    //        {
        //    //            operCode = "S04",
        //    //            billCode = billCode
        //    //        };
        //    //        string responseDtoStr = WebServiceHelper.xnh_InterfaceCall<RequestHeadDTO, S04RequestDTO>(inHead, inBody);
        //    //        return XMLHelper.XmlDeserialize<ResponseBaseDTO<S04ResponseDTO>>(responseDtoStr);
        //    //    }
        //    //    else
        //    //    {
        //    //        return new ResponseBaseDTO<S04ResponseDTO>
        //    //        {
        //    //            state = false,
        // // message = "S02认证鉴权giao diện gọi thất bại",
        //    //            data = null
        //    //        };
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return new ResponseBaseDTO<S04ResponseDTO>
        //    //    {
        //    //        state = false,
        //    //        message = ex.Message,
        //    //        data = null
        //    //    };
        //    //}
        //    #endregion
        //}
        /// <summary>
        /// Nhập viện làm thủ tục trả lại 
        /// </summary>
        /// <param name="inBody"></param>
        /// <returns></returns>
        public Response<string> S05(S05RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<S05RequestDTO, Response<string>>(request, OrganizeId).Call() as Response<string>;
            return response;
            //return WebServiceHelper.xnh_HospitalCall<S05RequestDTO, string>(inBody, "S05");

            #region
            //try
            //{
            //    CommonProxy _commonProxy = CommonProxy.GetInstance(OrganizeId);
            //    string billCode = string.Empty;
            //    if (_commonProxy.InterfaceS02(out billCode))
            //    {
            //        RequestHeadDTO inHead = new RequestHeadDTO
            //        {
            //            operCode = "S05",
            //            billCode = billCode
            //        };
            //        string responseDtoStr = WebServiceHelper.xnh_InterfaceCall<RequestHeadDTO, S05RequestDTO>(inHead, inBody);
            //        return XMLHelper.XmlDeserialize<ResponseBaseDTO<string>>(responseDtoStr);
            //    }
            //    else
            //    {
            //        return new ResponseBaseDTO<string>
            //        {
            //            state = false,
            // message = "S02认证鉴权giao diện gọi thất bại",
            //            data = null
            //        };
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return new ResponseBaseDTO<string>
            //    {
            //        state = false,
            //        message = ex.Message,
            //        data = null
            //    };
            //}
            #endregion
        }

        ///// <summary>
        ///// Nhập viện làm thủ tục thông tin sửa
        ///// </summary>
        ///// <param name="inBody"></param>
        ///// <returns></returns>
        //public ResponseBaseDTO<string> S06(S06RequestDTO inBody)
        //{
        //    return WebServiceHelper.xnh_HospitalCall<S06RequestDTO, string>(inBody, "S06");
        //}

        /// <summary>
        /// Xuất viện làm thủ tục 
        /// </summary>
        /// <param name="inBody"></param>
        /// <returns></returns>
        public Response<S07ResponseDTO> S07(S07RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<S07RequestDTO, Response<S07ResponseDTO>>(request, OrganizeId).Call() as Response<S07ResponseDTO>;
            return response;
            //return WebServiceHelper.xnh_HospitalCall<S07RequestDTO, string>(inBody, "S07");
        }

        /// <summary>
        /// Xuất viện làm thủ tục trả lại 
        /// </summary>
        /// <param name="inBody"></param>
        /// <returns></returns>
        public Response<string> S08(S08RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<S08RequestDTO, Response<string>>(request, OrganizeId).Call() as Response<string>;
            return response;
            //return WebServiceHelper.xnh_HospitalCall<S08RequestDTO, string>(inBody, "S08");
        }
        /// <summary>
        /// Nhập việnchi phí chi tiết tải lên
        /// </summary>
        /// <param name="inBody"></param>
        /// <returns></returns>
        public Response<S10ResponseDTO> S10(S10RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<S10RequestDTO, Response<S10ResponseDTO>>(request, OrganizeId).Call() as Response<S10ResponseDTO>;
            return response;
            //return WebServiceHelper.xnh_HospitalCall<S10RequestDTO,S10ResponseDTO>(inBody, "S10");
        }
        /// <summary>
        /// Nhập việnchi phí chi tiếttra cứu
        /// </summary>
        /// <param name="inBody"></param>
        /// <returns></returns>
        public Response<List<detail>> S11(S11RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<S11RequestDTO, Response<List<detail>>>(request, OrganizeId).Call() as Response<List<detail>>;
            return response;
            //ResponseBaseDetailDTO<List<S11ResponseDTO>> dto= WebServiceHelper.xnh_HospitalDetailAttrCall<S11RequestDTO,List<S11ResponseDTO>>(inBody, "S11");
            //return new ResponseBaseDTO<List<S11ResponseDTO>>
            //{
            //    state = dto.state,
            //    message = dto.message,
            //    data = dto.data
            //};
        }
        /// <summary>
        /// Nhập việnchi phí chi tiết trả lại đ ơn 
        /// </summary>
        /// <param name="inBody"></param>
        /// <returns></returns>
        public Response<string> S12(S12RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<S12RequestDTO, Response<string>>(request, OrganizeId).Call() as Response<string>;
            return response;
            //return WebServiceHelper.xnh_HospitalCall<S12RequestDTO, string>(inBody, "S12");
        }
        /// <summary>
        /// Nhập viện mô phỏng quyết toán
        /// </summary>
        /// <param name="inBody"></param>
        /// <returns></returns>
        public Response<S13ResponseDTO> S13(S13RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<S13RequestDTO, Response<S13ResponseDTO>>(request, OrganizeId).Call() as Response<S13ResponseDTO>;
            return response;
            //return WebServiceHelper.xnh_HospitalCall<S13RequestDTO, S13ResponseDTO>(inBody, "S13");
        }
        /// <summary>
        /// Quyết toán nhập viện
        /// </summary>
        /// <param name="inBody"></param>
        /// <returns></returns>
        public Response<S14ResponseDTO> S14(S14RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<S14RequestDTO, Response<S14ResponseDTO>>(request, OrganizeId).Call() as Response<S14ResponseDTO>;
            return response;
            //return WebServiceHelper.xnh_HospitalCall<S14RequestDTO, S14ResponseDTO>(inBody, "S14");
        }
        /// <summary>
        /// Nhập viện ghi đ ỏ 
        /// </summary>
        /// <param name="inBody"></param>
        /// <returns></returns>
        public Response<string> S15(S15RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<S15RequestDTO, Response<string>>(request, OrganizeId).Call() as Response<string>;
            return response;
            //return WebServiceHelper.xnh_HospitalCall<S15RequestDTO, string>(inBody, "S15");
        }
        /// <summary>
        /// 16 lấy quyết toán nhập viện đ ơn thông tin
        /// </summary>
        /// <param name="inBody"></param>
        /// <returns></returns>
        public Response<S16ResponseDTO> S16(S16RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<S16RequestDTO, Response<S16ResponseDTO>>(request, OrganizeId).Call() as Response<S16ResponseDTO>;
            return response;
            //return WebServiceHelper.xnh_HospitalCall<S16RequestDTO, S16ResponseDTO>(inBody, "S16");
        }

        public Response<List<inp>> S17(S17RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<S17RequestDTO, Response<List<inp>>>(request, OrganizeId).Call() as Response<List<inp>>;
            return response;
            //ResponseBaseInpDTO<List<S17ResponseDTO>> dto = WebServiceHelper.xnh_HospitalInpAttrCall<S17RequestDTO, List<S17ResponseDTO>>(inBody, "S17");
            //return new ResponseBaseDTO<List<S17ResponseDTO>>
            //{
            //    state = dto.state,
            //    message = dto.message,
            //    data = dto.data
            //};
        }

        /// <summary>
        /// Nhập viện làm thủ tục 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S04ResponseDTO> S04(S04RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<S04RequestDTO, Response<S04ResponseDTO>>(request, OrganizeId).Call() as Response<S04ResponseDTO>;
            return response;
        }

        /// <summary>
        /// Nhập viện làm thủ tục sửa
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<string> S06(S06RequestDTO request)
        {
            var response = new GuiAnWebServiceDefaultFactory<S06RequestDTO, Response<string>>(request, OrganizeId).Call() as Response<string>;
            return response;
        }
    }
}
