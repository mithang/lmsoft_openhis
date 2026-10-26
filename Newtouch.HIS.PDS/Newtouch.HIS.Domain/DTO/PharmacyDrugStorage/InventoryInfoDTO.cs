using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    public class InventoryInfoDTO
    {
        /// <summary>
        /// thời gian kiểm kê hộp thả xuống
        /// </summary>
         public InventoryDateDropDownVO DateDropDownVO { get; set; }
        /// <summary>
        /// quay lại của kiểm kê thông tin
        /// </summary>
         public IList<InventoryInfoVO> InventoryInfoList { get; set; }
    }
}
