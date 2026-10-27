using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.ValueObjects
{
    /// <summary>
    /// Thông tin ứng dụng đã đăng ký
    /// </summary>
    public class AuthAppVO
    {
        /// <summary>
        /// Desc:Ủy quyềnID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? AuthId { get; set; }

        /// <summary>
        /// Desc:Dùng chung cho ứng dụngId
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? AppId { get; set; }

        /// <summary>
        /// Desc:Tên ứng dụng
        /// Default:
        /// Nullable:False
        public string? AppName { get; set; }

        /// <summary>
        /// Desc:Địa chỉ tên miền
        /// Default:
        /// Nullable:False
        /// </summary>   
        public string? Domain { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? Host { get; set; }

        /// <summary>
        /// Desc:Loại cấp quyền AuthType
        /// Default:Web
        /// Nullable:True
        /// </summary>    
        public int? AuthType { get; set; }
    }

    
}
