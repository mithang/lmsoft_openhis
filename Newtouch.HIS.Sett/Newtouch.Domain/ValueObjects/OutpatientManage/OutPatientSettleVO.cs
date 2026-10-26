using Newtouch.HIS.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Đối tượng thu phí ngoại trú
    /// </summary>
    [NotMapped]
    public class OutPatientSettleVO: OutpatientRegistEntity
    {
        /// <summary>
        /// Tên tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// Tên hạng mục bệnh nặng
        /// </summary>
        public string dbxmmc { get; set; }
        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public decimal zje { get; set; }
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string rymc { get; set; }
        /// <summary>
        /// Ngày hủy
        /// </summary>
        public DateTime? zfrq { get; set; }
        /// <summary>
        /// Tên hạng mục thu phí
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// Hủy
        /// </summary>
        public string iszf { get; set; }
        /// <summary>
        /// Trả lại
        /// </summary>
        public string isth { get; set; }


    }
}
