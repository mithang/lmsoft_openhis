using Newtouch.EMR.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects
{
    public class BlTemplateVo
    {
        /// <summary>
        /// Id văn bản, mẫu
        /// </summary>
        public string templateId { get; set; }
        /// <summary>
        /// Tên văn bản, mẫu
        /// </summary>
        public string templateName { get; set; }
        /// <summary>
        /// Nội dung văn bản
        /// </summary>
        public string xmldata { get; set; }
        /// <summary>
        /// Đường dẫn văn bản bệnh án
        /// </summary>
        public string bllj { get; set; }
        /// <summary>
        /// Cờ khóa chiếm dụng văn bản bệnh án
        /// </summary>
        public bool lockStatus { get; set; }

        //Thông tin cơ bản bệnh án ngoại trú
        public TreatEntityVO mzjbxx { get; set; }
        //Thông tin cơ bản bệnh án của bệnh nhân
        public blzybrjbxxVO zybrxx { get; set;}
        /// <summary>
        /// Thông tin cơ bản trang đầu hồ sơ bệnh án của văn bản bệnh án
        /// </summary>
        public BabasyVO basyVo { get; set; }
    }
}
