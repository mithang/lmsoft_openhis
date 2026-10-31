using System;
using System.Collections.Generic;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Domain
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class IEntity<TEntity> : Newtouch.Infrastructure.EF.IEntity<TEntity>
    {
        private static readonly IList<string> creatorUserCodePropertyNameList = new List<string> { "CreatorCode" };
        private static readonly IList<string> createTimePropertyNameList = new List<string> { "CreateTime" };
        private static readonly IList<string> lastModifyUserCodePropertyNameList = new List<string> { "LastModifierCode" };
        private static readonly IList<string> lastModifyTimePropertyNameList = new List<string> { "LastModifyTime" };
        private static readonly IList<string> ztPropertyNameList = new List<string> { "zt" };
        private static readonly IList<string> orgIdPropertyNameList = new List<string> { "OrganizeId" };
        private static readonly IList<string> topOrgIdPropertyNameList = new List<string> { "TopOrganizeId" };

        static IEntity()
        {

        }

        /// <summary>
        /// Khi tạo thực thể,Cập nhật thực thể của Trường khóa chính,Người tạo,Thời gian tạo
        /// Ví dụ gọi
        /// Guid/stringKhóa chính  Create() hoặc Create(true) hoặc Create(true, '73FD1267-79BA-4E23-A152-744AF73117E9')
        /// Khóa chính tự tăng  Create()
        /// int/longKhóa chính không tự tăng  Create() hoặc Create(true, 9999)
        /// </summary>
        public void Create(bool isSetPrimaryKeyValue = false, object keyValue = null, bool isSetOrgId = false, bool isSetTopOrgId = false)
        {
            var loginInfo = OperatorProvider.GetCurrent();
            if (isSetPrimaryKeyValue)
            {
                setPropertyValue("Key", null
                    , (keyValue == null ? Guid.NewGuid().ToString() : keyValue), isPrimaryKey: true);
            }
            if (loginInfo != null)
            {
                setPropertyValue("creatorUserCode", creatorUserCodePropertyNameList, loginInfo.UserCode);
            }
            setPropertyValue("CreateTime", createTimePropertyNameList, DateTime.Now);
            setPropertyValue("zt", ztPropertyNameList, "1");
            if (isSetOrgId)
            {
                setPropertyValue("orgId", orgIdPropertyNameList, loginInfo.OrganizeId);
            }
            if (isSetTopOrgId)
            {
                setPropertyValue("topOrgId", topOrgIdPropertyNameList, Constants.TopOrganizeId);
            }
        }

        /// <summary>
        /// Khi cập nhật thực thể,Cập nhật Thực thể hiện tại của Trường khóa chính,Người cập nhật,Thời gian cập nhật
        /// Ví dụ gọi
        /// Modify() , Modify('73FD1267-79BA-4E23-A152-744AF73117E9'), Modify(9999)
        /// </summary>
        /// <param name="keyValue">!=nullCập nhật trường khóa chính,Nếu không thì không cập nhật trường khóa chính</param>
        public void Modify(object keyValue = null, bool isSetOrgId = false, bool isSetTopOrgId = false)
        {
            var loginInfo = OperatorProvider.GetCurrent();
            if (keyValue != null)
            {
                setPropertyValue("Key", null, keyValue, isPrimaryKey: true);
            }
            if (loginInfo != null)
            {
                setPropertyValue("LastModifyUserCode", lastModifyUserCodePropertyNameList, loginInfo.UserCode);
            }
            setPropertyValue("LastModifyTime", lastModifyTimePropertyNameList, DateTime.Now);
            if (isSetOrgId)
            {
                setPropertyValue("orgId", orgIdPropertyNameList, loginInfo.OrganizeId);
            }
            if (isSetTopOrgId)
            {
                setPropertyValue("topOrgId", topOrgIdPropertyNameList, Constants.TopOrganizeId);
            }
        }

    }
}
