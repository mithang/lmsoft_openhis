using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Entity.PatientCenter
{
    [Tenant(DBEnum.SettDb)]
    [SugarTable("order_info", "OrderInfoEntity")]
    public class OrderInfoEntity : IEntity
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// số đơn hàng
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// loại đơn hàng EnumOrderType
        /// </summary>
        public int? OrderType { get; set; }
        /// <summary>
        /// số thẻ khám bệnh
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// định danh lần khám này  khám ngoại trú：mzh；nội trú：zyh
        /// </summary>
        public string VisitNo { get; set; }
        /// <summary>
        /// trạng thái đơn hàng EnumOrderStatus
        /// </summary>
        public int OrderStu { get; set; }
        /// <summary>
        /// số tiền đơn hàng
        /// </summary>
        public decimal OrderAmt { get; set; }
        /// <summary>
        /// số tiền thanh toán
        /// </summary>
        public decimal? PayFee { get; set; }
        /// <summary>
        /// số thứ tự thanh toán
        /// </summary>
        public string PayLsh { get; set; }
        /// <summary>
        /// thông tin danh tính người thanh toán
        /// </summary>
        public string PayerId { get; set; }
        /// <summary>
        /// thời gian thanh toán đơn hàng
        /// </summary>
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// phương thức thanh toán
        /// </summary>
        public int? PayWay { get; set; }
        /// <summary>
        /// HISsố thanh toán nội bộ
        /// </summary>
        public string SettTradeNo { get; set; }
        public string Memo { get; set; }
        /// <summary>
        /// người tạoID
        /// </summary>
       // public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian tạo
        /// </summary>
       // public DateTime CreateTime { get; set; }

        /// <summary>
        /// thời gian sửa đổi lần cuối
        /// </summary>
       // public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// người sửa đổi lần cuốiID
        /// </summary>
       // public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string zt { get; set; }
        public int? patid { get; set; }
        public string dzId { get; set; }
    }
}
