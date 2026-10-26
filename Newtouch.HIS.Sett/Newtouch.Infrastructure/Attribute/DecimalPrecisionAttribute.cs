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
        /// 精确 đ ộ.
        /// </summary>
        public byte Precision
        {

            get { return _precision; }
            set { _precision = value; }
        }
        
        private byte _scale;

        /// <summary>
        /// 小数保留 vị 数.
        /// </summary>
        public byte Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        
        /// <summary>
        /// theo chỉ đ ịnh của精确 đ ộ và小数保留 vị 数,初始化 DecimalPrecisionAttribute của实例.
        /// </summary>
        /// <param name="precision">精确 đ ộ.</param>
        /// <param name="scale">小数保留 vị 数.</param>
        public DecimalPrecisionAttribute(byte precision, byte scale)
        {
            this.Precision = precision;
            this.Scale = scale;
        }
    }
}
