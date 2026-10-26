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
    /// đăng ký thông tin ứng dụng
    /// </summary>
    public class AuthAppVO
    {
        /// <summary>
        /// Desc:ủy quyềnID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? AuthId { get; set; }

        /// <summary>
        /// Desc:ứng dụng chungId
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? AppId { get; set; }

        /// <summary>
        /// Desc:tên ứng dụng
        /// Default:
        /// Nullable:False
        public string? AppName { get; set; }

        /// <summary>
        /// Desc:địa chỉ tên miền
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
        /// Desc:loại ủy quyền AuthType
        /// Default:Web
        /// Nullable:True
        /// </summary>    
        public int? AuthType { get; set; }
    }

    
}
