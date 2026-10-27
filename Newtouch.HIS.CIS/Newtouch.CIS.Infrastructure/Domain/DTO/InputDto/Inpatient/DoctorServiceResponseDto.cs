using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto.Inpatient
{
    /// <summary>
    /// kiểm traY lệnhlàKhôngtrùng lặp
    /// </summary>
    public class DSrepeatResponseDto
    {
        public string Id { get; set; }
        //0 Y lệnh tạm thời 1 Y lệnh dài hạn
        public int clbz { get; set; }
        public string xmdm { get; set; }
        public string  xmmc { get; set; }
    }
}
