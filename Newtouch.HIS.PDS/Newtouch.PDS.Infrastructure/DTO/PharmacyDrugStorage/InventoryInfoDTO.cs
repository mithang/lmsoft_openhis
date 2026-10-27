using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    public class InventoryInfoDTO
    {
        /// <summary>
        /// Hộp thả xuống thời gian kiểm kê
        /// </summary>
         public InventoryDateDropDownVO DateDropDownVO { get; set; }
        /// <summary>
        /// Thông tin kiểm kê trả về
        /// </summary>
         public IList<InventoryInfoVO> InventoryInfoList { get; set; }
    }
}
