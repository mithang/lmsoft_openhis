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
        /// Số đơn hàng
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// Loại đơn hàng EnumOrderType
        /// </summary>
        public int? OrderType { get; set; }
        /// <summary>
        /// Khám bệnhSố thẻ
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// Mã định danh lần khám này  Ngoại trú:mzh;Nội trú:zyh
        /// </summary>
        public string VisitNo { get; set; }
        /// <summary>
        /// Trạng thái đơn hàng EnumOrderStatus
        /// </summary>
        public int OrderStu { get; set; }
        /// <summary>
        /// Số tiền đơn hàng
        /// </summary>
        public decimal OrderAmt { get; set; }
        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        public decimal? PayFee { get; set; }
        /// <summary>
        /// Số giao dịch thanh toán
        /// </summary>
        public string PayLsh { get; set; }
        /// <summary>
        /// Thông tin danh tính người thanh toán
        /// </summary>
        public string PayerId { get; set; }
        /// <summary>
        /// Thời gian thanh toán đơn hàng
        /// </summary>
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// Phương thức thanh toán
        /// </summary>
        public int? PayWay { get; set; }
        /// <summary>
        /// HISSố quyết toán nội bộ
        /// </summary>
        public string SettTradeNo { get; set; }
        public string Memo { get; set; }
        /// <summary>
        /// Người tạoID
        /// </summary>
       // public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
       // public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối
        /// </summary>
       // public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuốiID
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
