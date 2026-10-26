/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description:Tập hợp thông tin thu phí ngoại trú
// Author:HLF
// CreateDate: 2016/12/26 18:38:25 
//**********************************************************/

using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    public class OutPatChargeDto
    {
        /// <summary>
        /// Thông tin cơ bản bệnh nhân
        /// </summary>
        public OutPatChargeInfoVO patInfo;

        /// <summary>
        /// Thông tin đăng ký khám
        /// </summary>
        public List<OutPatChargeItemVO> ghItemList;

        /// <summary>
        /// Thông tin chuyên gia và khoa đăng ký khám
        /// </summary>
        public OutPatChargeDoctorVO ghDocVo;
    }
}
