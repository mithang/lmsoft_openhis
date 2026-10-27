using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO
{
    public class TemplateCtrlRequestDto
    {
        /// <summary>
        /// Quan hệ quyền mẫu
        /// </summary>
        public string gxId { get; set; }
        /// <summary>
        /// Id vị trí
        /// </summary>
        public string dutyCode { get; set; }
        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string dutyName { get; set; }
        /// <summary>
        /// Mã kiểm soát quyền（EnummbqxFp）
        /// </summary>
        public int ctrlLevel { get; set; }
        /// <summary>
        /// Có bật hay không
        /// </summary>
        public string zt { get; set; }

    }
}
