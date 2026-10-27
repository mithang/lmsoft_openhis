using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Clinic
{
    public class APIResponseVO
    {
    }

    public class MeettingResponse
    {
        /// <summary>
        /// là音xemtần suấtTài khoảnlấy Secret làkhóa bí mậtvàodòng AES đốigọi làthêmmậtsaucủakếtnếu，gọi音xemtần suấtphụcvụcầndùngđến，vui lònggiảimậtsauSử dụng
        /// </summary>
        public string rtcuserid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roomid { get; set; }
        public string username { get; set; }
        public string roompath { get; set; }
        public string organization { get; set; }
        public string device { get; set; }
    }
}
