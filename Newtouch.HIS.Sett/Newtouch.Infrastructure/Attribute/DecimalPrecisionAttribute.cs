using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class DecimalPrecisionAttribute:Attribute
    {
        private byte _precision;

        /// <summary>
        /// chính xác đ ộ.
        /// </summary>
        public byte Precision
        {

            get { return _precision; }
            set { _precision = value; }
        }
        
        private byte _scale;

        /// <summary>
        /// số thập phân giữ lại vị số.
        /// </summary>
        public byte Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        
        /// <summary>
        /// theo chỉ đ ịnh củachính xác đ ộ vàsố thập phân giữ lại vị số,khởi tạo DecimalPrecisionAttribute củathể hiện.
        /// </summary>
        /// <param name="precision">chính xác đ ộ.</param>
        /// <param name="scale">số thập phân giữ lại vị số.</param>
        public DecimalPrecisionAttribute(byte precision, byte scale)
        {
            this.Precision = precision;
            this.Scale = scale;
        }
    }
}
