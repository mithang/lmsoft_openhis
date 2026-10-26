/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description: Quản lý nội trú》Quản lý tài khoản》Thực thể giao diện trả về
// Author:HLF
// CreateDate: 2016/12/8 10:12:01 
//**********************************************************/

using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// Tiền tạm ứng
    /// </summary>
   public class ReserveDto
    {
        /// <summary>
        /// Thông tin cơ bản
        /// </summary>
        public ReservePatientInfoDto patInfo;

        /// <summary>
        /// Thông tin tài khoản
        /// </summary>
        public List<PatAccPayVO> accPayInfoList;
    }
    [NotMapped]
    public class ReservePatientInfoDto: OutpatAccInfoDto
    {
        public int zhCode { get; set; }
        /// <summary>
        /// Tính chất tài khoản Dùng khi phân biệt tài khoản trong viện
        /// </summary>
        public int? zhxz { get; set; }
    }
}
