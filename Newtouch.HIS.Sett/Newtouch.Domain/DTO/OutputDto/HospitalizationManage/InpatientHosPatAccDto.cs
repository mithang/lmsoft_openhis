using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage
{

    /// <summary>
    /// Tiền tạm ứng
    /// </summary>
    public class InpatientReserveDto
    {
        /// <summary>
        /// Thông tin cơ bản
        /// </summary>
        public InpatientReservePatientInfoDto patInfo;

        /// <summary>
        /// Thông tin tài khoản
        /// </summary>
        public List<InpatientPatAccPayVO> accPayInfoList;
    }
    [NotMapped]
    public class InpatientReservePatientInfoDto : InpatientOutpatAccInfoDto
    {
        public int zhCode { get; set; }
        /// <summary>
        /// Tính chất tài khoản Dùng khi phân biệt tài khoản trong viện
        /// </summary>
        public int? zhxz { get; set; }
    }
}
