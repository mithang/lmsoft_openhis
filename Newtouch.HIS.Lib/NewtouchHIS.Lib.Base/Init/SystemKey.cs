using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Lib.Base
{
    /// <summary>
    /// biến hệ thống chung
    /// </summary>
    public sealed class SystemKey
    {
        public static string Cache_UserTokenKey = "HisToken:User_";
        public static string Cache_OrgListKey = "TopOrg_";


        /// <summary>
        /// lắp ráp người dùngTokenbộ nhớ đệmkey
        /// </summary>
        /// <param name="account"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static string AssemblyUserTokenKey(string account,string orgId)
        {
            return $"{Cache_UserTokenKey}{account}_{orgId?.Replace("-", "")}";
        }
        /// <summary>
        /// lắp ráp danh sách tổ chức
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        public static string AssemblyOrgListKey(string topOrgId)
        {
            return $"{Cache_OrgListKey}{topOrgId.Replace("-", "")}";
        }

    }
}
