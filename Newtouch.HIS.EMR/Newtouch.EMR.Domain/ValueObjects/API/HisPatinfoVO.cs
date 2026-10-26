using Newtouch.Core.Common;
using Newtouch.EMR.Domain.DTO.InputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects
{
    public class HisPatinfoVO
    {
        /// <summary>
        /// Phân trang
        /// </summary>
        public Pagination pagination { get; set; }
        /// <summary>
        /// Đồng bộ danh sách thông tin bệnh nhân his
        /// </summary>
        public List<HisPatinfoDto> List { get; set; }
    }
}
