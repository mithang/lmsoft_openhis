using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using SqlSugar;

namespace Newtouch.MR.ManageSystem.Infrastructure
{
    /// <summary>
    /// Audit stamping for MRMS entities. Replaces FrameworkBase IEntity&lt;T&gt;.Create/Modify.
    /// Uses reflection so every entity keeps its own property declarations untouched.
    /// </summary>
    public abstract class EntityBase
    {
        public virtual void Create(bool isNew)
        {
            SetProp("Id", Guid.NewGuid().ToString(), onlyIfEmpty: true);
            SetProp("CreateTime", DateTime.Now);
            SetProp("CreatorCode", CurrentUserCode());
            SetProp("zt", "1", onlyIfEmpty: true);
            SetProp("OrganizeId", CurrentOrganizeId(), onlyIfEmpty: true);
        }

        public virtual void Modify(string keyValue)
        {
            SetProp("Id", keyValue);
            SetProp("LastModifyTime", DateTime.Now);
            SetProp("LastModifierCode", CurrentUserCode());
        }

        private void SetProp(string name, object value, bool onlyIfEmpty = false)
        {
            var prop = GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (prop == null || !prop.CanWrite) return;
            if (onlyIfEmpty)
            {
                var cur = prop.GetValue(this);
                if (cur != null && !string.IsNullOrWhiteSpace(cur.ToString())) return;
            }
            try { prop.SetValue(this, value); } catch { /* type mismatch: leave as-is */ }
        }

        private static string CurrentUserCode()
        {
            try { return OperatorProvider.GetCurrent()?.UserCode ?? ""; } catch { return ""; }
        }

        private static string CurrentOrganizeId()
        {
            try { return OperatorProvider.GetCurrent()?.OrganizeId ?? ""; } catch { return ""; }
        }
    }
}
