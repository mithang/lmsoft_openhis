using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Apply
{
    public class YzbindingfeeVo
    {
        public string yfdm { get; set; }
        public string ypbzdm { get; set; }
        public DateTime cyrq { get; set; }
        public DateTime ryrq { get; set; }
        public decimal je { get; set; }
        public string dw { get; set; }
        public decimal sl { get; set; }
        public string brxz { get; set; }
        public DateTime tdrq { get; set; }
        public string zyh { get; set; }
        public int? bqbh { get; set; }
        public string bq { get; set; }
        public string cw { get; set; }
        public string yzlx { get; set; }
        public int sfxmbh { get; set; }

        public string sfxm { get; set; }
        public decimal dj { get; set; }
        public string dl { get; set; }
        public string jfdw { get; set; }
        public decimal zfbl { get; set; }
        public string zfxz { get; set; }
        public string ybbm { get; set; }
        public int? ysbh { get; set; }
        public string ys { get; set; }
        public int? ksbh { get; set; }

        public string ks { get; set; }
        public string zxksbh { get; set; }
        public string zxks { get; set; }
        public string ysmc { get; set; }
        public string ksmc { get; set; }

        /// <summary>
        /// tháokhôngsố
        /// </summary>
        public int? cls { get; set; }
        public decimal ztsl { get; set; }
        public string dlmc { get; set; }
        public string sfxmmc { get; set; }
        public string pcCode { get; set; }
        /// <summary>
        /// Số lần thực hiện
        /// </summary>
        /// <returns></returns>
        public int? zxcs { get; set; }
        /// <summary>
        /// Chu kỳ thực hiện
        /// </summary>
        /// <returns></returns>
        public int? zxzq { get; set; }
        /// <summary>
        /// Chu kỳ thực hiệnĐơn vị -1:khôngquythìchu kỳ,0:ngày,1:giờ,2:Phút.Lưu ý:khilà-1khi,tuầnMã(zdm)làsốchữ(1234567)trongcủaBất kỳ vàicáisốchữ
        /// </summary>
        /// <returns></returns>
        public int? zxzqdw { get; set; }

        public string gg { get; set; }
        public string pcmc { get; set; }
        public string yzxz { get; set; }
    }
}
