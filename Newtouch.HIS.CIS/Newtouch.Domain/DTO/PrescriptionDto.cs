using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO
{
   public class PrescriptionDto: PrescriptionQhdDto
    {
        public int? zdlx { get; set; }
    }

    public class PrescriptionQhdDto
    {
        public int zdmxnm { get; set; }
        public string zdmxxz { get; set; }
        public int cfnm { get; set; }
        public DateTime kjrq { get; set; }
        public string sfwym { get; set; }
        public string xmwym { get; set; }
        public string xmmc1 { get; set; }
        public string xmmc2 { get; set; }
        public string xmgg { get; set; }
        public string xmdw { get; set; }
        public int xmsl { get; set; }
        public string xmdldm { get; set; }
        public string xmdlmc { get; set; }
        public string ypbzdm { get; set; }
        public string yfdm { get; set; }
        public string yfmc { get; set; }
        public string bz { get; set; }
        public string ghwym { get; set; }
        public string ysbm { get; set; }
        public string ysgh { get; set; }
        public string ysmc { get; set; }
        public string ksbm { get; set; }
        public string ksmc { get; set; }
        public int? ypfz { get; set; }
        public string txm { get; set; }
        public string ybdb { get; set; }
        public string mxb { get; set; }
        public string jhsy { get; set; }
        public string dpy { get; set; }
        public decimal price { get; set; }
        public string xzyybx { get; set; }
    }



    public class RtnToQhd
    {   // Thuốc
        public List<PrescriptionDto>  ypMessage { get; set; }
        //Xét nghiệm
        public List<PrescriptionDto> lisMessage { get; set; }
        //Kiểm tra
        public List<PrescriptionDto> pacsMessage { get; set; }
        //Điều trị
        public List<PrescriptionDto> zlMessage { get; set; }
        //Phục hồi chức năng
        public List<PrescriptionDto> kfMessage { get; set; }
    }
}
