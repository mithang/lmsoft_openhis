using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    public class QHDSmartCheckInput
    {
        public string jsondata { get; set; }
        public string jydm { get; set; }
    }
    public class YBRequestBase<T>
    {
        public YBRequest<T> REQUESTDATA { get; set; }
    }

    public class YBRequest<T>
    {
        /// <summary>
        /// Mã giao dịch
        /// </summary>
        public string MSGNO { get; set; }
        /// <summary>
        /// Mã cơ sở y tế chỉ định
        /// </summary>
        public string AKB020 { get; set; }
        /// <summary>
        /// Mã nhân viên thao tác
        /// </summary>
        public string OPERID { get; set; }
        /// <summary>
        /// Tên nhân viên thao tác
        /// </summary>
        public string OPERNAME { get; set; }
        /// <summary>
        /// Số chu kỳ nghiệp vụ khi nhân viên thao tác đăng nhập，trung tâm trả về cho his số chu kỳ nghiệp vụ
        /// </summary>
        public string PERIODNO { get; set; }
        /// <summary>
        /// Số thứ tự giao dịch của bên gửi do HIS điền vào，sử dụng mã cơ sở y tế chỉ định+Thời gian yyyymmddhh24miss+dãy số
        /// </summary>
        public string MSGID { get; set; }
        /// <summary>
        /// Neusoft cung cấp(tạm thời để trống)
        /// </summary>
        public string GRANTID { get; set; }
    }

    public class YBRequestSingle<T> : YBRequest<T>
    {
        public T INPUT{ get; set; }
    }

    public class YBRequestRows<T> : YBRequest<T>
    {
        public List<T> INPUT { get; set; }
    }

    public class ROWS<T>
    {
        public T INPUTROW { get; set; }
    }
}
