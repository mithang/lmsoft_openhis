using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    ///Tải lên đối chiếu danh mục
    /// </summary>
    public class SysMedicineMldzxxVO : EntityBase
    {
        public string code { get; set; }
        public string name { get; set; }
        public string gg { get; set; }
        public string gjybdm { get; set; }
        public string isdz { get; set; }
    }
}
