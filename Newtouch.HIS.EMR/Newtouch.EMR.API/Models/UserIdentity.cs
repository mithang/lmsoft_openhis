/*****************************************************************************
Copyright © 2016 Newtouch. Bảo lưu mọi quyền
Author: Newtouch
Description: 
*******************************************************************************/

using Newtouch.HIS.API.Common.Models;

namespace Newtouch.EMR.API.Models
{
    /// <summary>
    /// Danh tính người dùng (mở rộng trường danh tính người dùng theo nhu cầu dự án)
    /// </summary>
    public class UserIdentity : Identity
    {
        /// <summary>
        /// Id cơ sở y tế
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Id cơ sở y tế cấp cao nhất
        /// </summary>
        public string TopOrganizeId { get; set; }

    }
}