using NewtouchHIS.Lib.Framework.Operator;
using SqlSugar;
using System;
using System.Linq;
using System.Reflection;

namespace Newtouch.HIS.Domain
{
    /// <summary>
    /// Entity base replacing FrameworkBase IEntity&lt;T&gt; (EF6, binary-only).
    /// Replicates the original Create()/Modify() semantics: primary key, creator,
    /// create time, zt flag, organize/top-organize stamps — via reflection so
    /// ported entity classes keep calling entity.Create()/Modify() unchanged.
    /// </summary>
    public abstract class EntityBase
    {
        private static PropertyInfo FindPkProperty(Type t)
        {
            foreach (var p in t.GetProperties())
            {
                foreach (var a in p.GetCustomAttributes(false))
                {
                    var at = a.GetType();
                    if (at.Name == "SugarColumnAttribute")
                    {
                        var prop = at.GetType().GetProperty("IsPrimaryKey");
                        if (prop != null && prop.GetValue(a) is bool b && b)
                        {
                            return p;
                        }
                    }
                }
            }
            return t.GetProperty("Id");
        }

        private void SetProp(string name, object value)
        {
            var p = GetType().GetProperty(name);
            if (p == null || !p.CanWrite)
            {
                return;
            }
            try
            {
                var target = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                p.SetValue(this, value == null ? null : Convert.ChangeType(value, target));
            }
            catch
            {
                // Same tolerance as the original reflection-based setter.
            }
        }

        private void SetPk(object value)
        {
            var pk = FindPkProperty(GetType());
            if (pk == null || !pk.CanWrite)
            {
                return;
            }
            try
            {
                var target = Nullable.GetUnderlyingType(pk.PropertyType) ?? pk.PropertyType;
                pk.SetValue(this, value == null ? null : Convert.ChangeType(value, target));
            }
            catch
            {
            }
        }

        public void Create(bool isSetPrimaryKeyValue = false, object keyValue = null, bool isSetOrgId = false, bool isSetTopOrgId = false)
        {
            var loginInfo = OperatorProvider.GetCurrent();
            if (isSetPrimaryKeyValue)
            {
                SetPk(keyValue ?? Guid.NewGuid().ToString());
            }
            if (loginInfo != null)
            {
                SetProp("CreatorCode", loginInfo.UserCode);
            }
            SetProp("CreateTime", DateTime.Now);
            SetProp("zt", "1");
            if (isSetOrgId && loginInfo != null)
            {
                SetProp("OrganizeId", loginInfo.OrganizeId);
            }
            if (isSetTopOrgId)
            {
                SetProp("TopOrganizeId", Newtouch.Infrastructure.Constants.TopOrganizeId);
            }
        }

        public void Modify(object keyValue = null, bool isSetOrgId = false, bool isSetTopOrgId = false)
        {
            var loginInfo = OperatorProvider.GetCurrent();
            if (keyValue != null)
            {
                SetPk(keyValue);
            }
            if (loginInfo != null)
            {
                SetProp("LastModifierCode", loginInfo.UserCode);
            }
            SetProp("LastModifyTime", DateTime.Now);
            if (isSetOrgId && loginInfo != null)
            {
                SetProp("OrganizeId", loginInfo.OrganizeId);
            }
            if (isSetTopOrgId)
            {
                SetProp("TopOrganizeId", Newtouch.Infrastructure.Constants.TopOrganizeId);
            }
        }
    }
}
