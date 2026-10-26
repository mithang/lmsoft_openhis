using System.Collections.Generic;
using Newtouch.HIS.Proxy.guian.DTO;
using Newtouch.HIS.Proxy.guian.DTO.S18;
using Newtouch.HIS.Proxy.guian.DTO.S19;
using Newtouch.HIS.Proxy.guian.DTO.S20;
using Newtouch.HIS.Proxy.guian.DTO.S21;
using Newtouch.HIS.Proxy.guian.DTO.S22;
using Newtouch.HIS.Proxy.guian.DTO.S23;
using Newtouch.HIS.Proxy.guian.DTO.S24;
using Newtouch.HIS.Proxy.guian.DTO.S25;
using Newtouch.HIS.Proxy.guian.DTO.S26;
using Newtouch.HIS.Proxy.guian.DTO.S27;

namespace Newtouch.HIS.Proxy.guian
{
    /// <summary>
    /// Ngoại trú liên quan 
    /// </summary>
    public class OutpatientProxy
    {
        #region 单例
        private static readonly OutpatientProxy _outpatientProxy = new OutpatientProxy();

        private OutpatientProxy()
        {
        }

        public static OutpatientProxy GetInstance(string organizeId)
        {
            OrganizeId = organizeId;
            return _outpatientProxy;
        }

        #endregion

        /// <summary>
        /// ID tổ chức
        /// </summary>
        public static string OrganizeId { get; set; }

        /// <summary>
        /// theo lấy của gia đ ình tham gia hợp tác danh sách tiến hành ngoại trú tải lên liên tỉnh đăng kýsửa cũng là này giao diện,sử dụnguploadType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S18ResponseDTO> S18(S18RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S18RequestDTO, Response<S18ResponseDTO>>(request, OrganizeId).Call() as Response<S18ResponseDTO>;
        }

        /// <summary>
        /// theo S18 số thứ tự bồi thư ờng ngoại trú trả về sau khi tải lênoutpid tiến hành trả lại ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S19ResponseDTO> S19(S19RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S19RequestDTO, Response<S19ResponseDTO>>(request, OrganizeId).Call() as Response<S19ResponseDTO>;
        }

        /// <summary>
        /// theo S18 số thứ tự bồi thư ờng ngoại trú trả về sau khi tải lênoutpid tiến hành ngoại trú thông tin sửa
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S20ResponseDTO> S20(S20RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S20RequestDTO, Response<S20ResponseDTO>>(request, OrganizeId).Call() as Response<S20ResponseDTO>;
        }

        /// <summary>
        /// theo S18 số thứ tự bồi thư ờng trả về sau khi tải lên ngoại trúoutpid tiến hành ngoại trú chi phí tải lên,quyết toán sau tải lên không hiệu lực
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S21ResponseDTO> S21(S21RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S21RequestDTO, Response<S21ResponseDTO>>(request, OrganizeId).Call() as Response<S21ResponseDTO>;
        }

        /// <summary>
        /// Tra cứu chi tiết chi phí ngoại trú lần này đ ã tải lên
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<List<DTO.S22.item>> S22(S22RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S22RequestDTO, Response<List<DTO.S22.item>>>(request, OrganizeId).Call() as Response<List<DTO.S22.item>>;
        }

        /// <summary>
        /// theo S18 số thứ tự bồi thư ờng trả về sau khi tải lên ngoại trúoutpid ngoại trú chi phí chi tiếttra cứuS22 tiến hành ngoại trú chi phí chi tiết trả lại đ ơn; liên tỉnh nếuchỉ truyền số thứ tự bồi thư ờng, không truyền chi tiếtID thì là tất cả các chi tiết xóa
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S23ResponseDTO> S23(S23RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S23RequestDTO, Response<S23ResponseDTO>>(request, OrganizeId).Call() as Response<S23ResponseDTO>;
        }

        /// <summary>
        /// theo S18 số thứ tự bồi thư ờng trả về sau khi tải lên ngoại trúoutpid tiến hành ngoại trú mô phỏng quyết toán
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S24ResponseDTO> S24(S24RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S24RequestDTO, Response<S24ResponseDTO>>(request, OrganizeId).Call() as Response<S24ResponseDTO>;
        }

        /// <summary>
        /// theo S18 số thứ tự bồi thư ờng trả về sau khi tải lên ngoại trúoutpid tiến hành quyết toán ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S25ResponseDTO> S25(S25RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S25RequestDTO, Response<S25ResponseDTO>>(request, OrganizeId).Call() as Response<S25ResponseDTO>;
        }

        /// <summary>
        /// theo S18 số thứ tự bồi thư ờng trả về sau khi tải lên ngoại trúoutpid tiến hành ngoại trú ghi đ ỏ 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S26ResponseDTO> S26(S26RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S26RequestDTO, Response<S26ResponseDTO>>(request, OrganizeId).Call() as Response<S26ResponseDTO>;
        }

        /// <summary>
        /// theo S18 số thứ tự bồi thư ờng trả về sau khi tải lên ngoại trúoutpid tiến hành lấy quyết toán ngoại trú đ ơn thông tin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S27ResponseDTO> S27(S27RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S27RequestDTO, Response<S27ResponseDTO>>(request, OrganizeId).Call() as Response<S27ResponseDTO>;
        }
    }
}
