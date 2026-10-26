/*******************************************************************************
 * Copyright © 2016 Newtouch Bản quyền thuộc về
 * Author: Newtouch
 * Description: 
*********************************************************************************/

using Newtouch.HIS.API.Common.Models;

namespace Newtouch.MRQC.API.Models
{
    /// <summary>
    /// danh tính người dùng（theo nhu cầu của dự án trường mở rộng của danh tính người dùng）
    /// </summary>
    public class UserIdentity : Identity
    {
        /// <summary>
        /// cơ sở y tếId
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// cơ sở y tế cấp cao nhấtId
        /// </summary>
        public string TopOrganizeId { get; set; }

    }
}