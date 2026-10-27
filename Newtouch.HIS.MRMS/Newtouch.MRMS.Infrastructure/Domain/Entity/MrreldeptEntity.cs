using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Infrastructure;

namespace Newtouch.MR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// Tạo: chl
    /// Ngày: 2019-11-20 13:45
    /// Mô tả: Bảng quan hệ khoa bệnh án và khoa HIS
    /// </summary>
    [SugarTable("mr_rel_dept")]
    public class MrreldeptEntity : EntityBase
    {
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// baksId
        /// </summary>
        /// <returns></returns>
        public string baksId { get; set; }

        /// <summary>
        /// hisdept
        /// </summary>
        /// <returns></returns>
        public string hisdept { get; set; }

        /// <summary>
        /// hisdeptname
        /// </summary>
        /// <returns></returns>
        public string hisdeptname { get; set; }

        /// <summary>
        /// Trạng thái     1:Hiệu lực  0: Vô hiệu
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }

    }
}