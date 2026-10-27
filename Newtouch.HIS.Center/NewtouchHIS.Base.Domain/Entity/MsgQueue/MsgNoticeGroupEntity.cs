using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    /// Nhóm tin nhắn配置表
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Msg_NoticeGroup", "MsgNoticeGroupEntity")]
    public class MsgNoticeGroupEntity : IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        
        /// <summary>
        /// 组标签
        /// 系统Tin nhắn / 院内Tin nhắn /科室内部Tin nhắn  质控Tin nhắn
        /// </summary>
        [Required(ErrorMessage = "GroupTag không được để trống")]
        public string GroupTag { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        [Required(ErrorMessage = "GroupName không được để trống")]
        public string GroupName { get; set; }
        /// <summary>
        /// Nhóm tin nhắn说明
        /// </summary>
        public string? GroupDesc { get; set; }
        /// <summary>
        /// 相关业务类型 GroupYwlxEnum
        /// </summary>
        [Required(ErrorMessage = "GroupYwlx không được để trống")]
        public int GroupYwlx { get; set; }
        /// <summary>
        /// 决定了通知对象范围  MsgNoticeRangeEnum
        /// </summary>
        [Required(ErrorMessage = "NoticeRange không được để trống")]
        public int NoticeRange { get; set; }
        /// <summary>
        /// 显示主题 
        /// 如 您有新的质控Tin nhắn，请即时查看
        /// </summary>
        public string? DefaultTitle { get; set; }
        /// <summary>
        /// 详细内容
        /// 展开显示
        /// </summary>
        public string? DefaultContent { get; set; }
        /// <summary>
        /// 内容关联数据 Json格式 可转化对象
        /// </summary>
        public string? DefaultContentData { get; set; }
        /// <summary>
        /// 默认跳转路径 
        /// 不为空时要求 AppId  không được để trống
        /// </summary>
        public string? DefaultOpenPath { get; set; }
        /// <summary>
        /// 路径参数
        /// 格式： zyh={0}，id={1}
        /// </summary>
        public string? DefaultOpenPathPara { get; set; }
        /// <summary>
        /// 应用Id
        /// </summary>
        public string? AppId { get; set; }
        ///// <summary>
        ///// Desc:
        ///// Default:
        ///// Nullable:False
        ///// </summary>           
        //[Required(ErrorMessage = "CreatorCode không được để trống")]
        //[StringLength(50, ErrorMessage = "CreatorCode giới hạn độ dài là 50")]
        //public string CreatorCode { get; set; }

        ///// <summary>
        ///// Desc:
        ///// Default:
        ///// Nullable:True
        ///// </summary>           
        //[StringLength(23, ErrorMessage = "LastModifyTime giới hạn độ dài là 23")]
        //public DateTime? LastModifyTime { get; set; }

        ///// <summary>
        ///// Desc:
        ///// Default:
        ///// Nullable:True
        ///// </summary>           
        //[StringLength(50, ErrorMessage = "LastModifierCode giới hạn độ dài là 50")]
        //public string? LastModifierCode { get; set; }

        ///// <summary>
        ///// Desc:
        ///// Default:
        ///// Nullable:False
        ///// </summary>           
        //[Required(ErrorMessage = "zt không được để trống")]
        //[StringLength(1, ErrorMessage = "zt giới hạn độ dài là 1")]
        //public string zt { get; set; }

    }
}
