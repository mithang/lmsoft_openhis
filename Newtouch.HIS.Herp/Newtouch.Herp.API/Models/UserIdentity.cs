/*******************************************************************************
 * Copyright © 2022 Ủy quyền y tế nguồn mở Bản quyền thuộc về
 * Author: Newtouch
 * Description: 
*********************************************************************************/

using Newtouch.HIS.API.Common.Models;

namespace Newtouch.Herp.API.Models
{
    /// <summary>
    /// Danh tính người dùng(Theo nhu cầu dự án Trường mở rộng danh tính người dùng)
    /// </summary>
    public class UserIdentity : Identity
    {
        /// <summary>
        /// Cơ sở y tế Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Cơ sở y tế cấp cao nhất Id
        /// </summary>
        public string TopOrganizeId { get; set; }

    }
}