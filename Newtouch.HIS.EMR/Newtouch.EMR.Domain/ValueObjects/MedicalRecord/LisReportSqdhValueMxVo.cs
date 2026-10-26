using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MedicalRecord
{
    public class LisReportSqdhValueMxVo
    {
        public string xmmc { get; set; } //Tên hạng mục
        public string jyjg { get; set; } //Kết quả xét nghiệm
        public string gdbj { get; set; } //Cờ cao/thấp
        public string ckzfw { get; set; } //Khoảng giá trị tham khảo
        public string dw { get; set; } //Đơn vị
    }
}
