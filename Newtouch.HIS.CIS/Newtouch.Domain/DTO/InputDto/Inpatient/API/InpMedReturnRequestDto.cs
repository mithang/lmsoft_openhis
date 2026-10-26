using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto
{
    public class InpMedReturnRequestDto
    {
        /// <summary>
        /// Số y lệnh
        /// </summary>
        public string yzId { get; set; }
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }
       
        /// <summary>
        /// Số thứ tự nhóm
        /// </summary>
        public int? zh { get; set; }
      

        public string zyh { get; set; }

        public int tysl { get; set; }

        public int? zhyz { get; set; }
        /// <summary>
        /// Người yêu cầu trả thuốc
        /// </summary>
        public string tysqr { get; set; }
        /// <summary>
        /// Ngày thực hiện
        /// </summary>
        public DateTime zxrq { get; set; }
        public string ksCode { get; set; }
        public string bqCode { get; set; }
        public string fyyf { get; set; }
        public long lyxh { get; set; }

    }

    public class InMedReturnRequestslDto {
        public string Id { get; set; }
        public int tsl { get; set; }
        public int? yzlx { get; set; }
    }
}
