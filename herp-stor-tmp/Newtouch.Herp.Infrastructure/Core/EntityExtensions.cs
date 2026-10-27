using System;
using System.Reflection;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Create/Modify audit-stamp helpers replacing the FrameworkBase IEntity
    /// extension methods (binary-only). The originals stamped Id/CreatorCode/
    /// CreateTime on create and LastModifierCode/LastModifyTime on modify;
    /// OrganizeId is set on create only when empty (callers that set it
    /// explicitly, e.g. SubmitForm, keep their value). Properties are set by
    /// reflection only when present, so VEntities without audit columns work too.
    /// </summary>
    public static class EntityExtensions
    {
        private static void SetProp(object entity, string name, object value)
        {
            var prop = entity.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (prop != null && prop.CanWrite)
            {
                try
                {
                    var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    prop.SetValue(entity, value == null ? null : Convert.ChangeType(value, targetType));
                }
                catch { /* type mismatch: leave as-is */ }
            }
        }

        private static object GetProp(object entity, string name)
        {
            var prop = entity.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            return prop != null && prop.CanRead ? prop.GetValue(entity) : null;
        }

        /// <summary>Stamp a new entity. newId=true generates a Guid Id unless an explicit id is given.</summary>
        public static void Create(this object entity, bool newId, string id = null)
        {
            if (newId)
            {
                SetProp(entity, "Id", !string.IsNullOrWhiteSpace(id) ? id : Guid.NewGuid().ToString());
            }
            else if (!string.IsNullOrWhiteSpace(id))
            {
                SetProp(entity, "Id", id);
            }
            var opr = OperatorProvider.GetCurrent();
            SetProp(entity, "CreatorCode", opr?.UserCode);
            SetProp(entity, "CreateTime", DateTime.Now);
            var org = GetProp(entity, "OrganizeId") as string;
            if (string.IsNullOrWhiteSpace(org))
            {
                SetProp(entity, "OrganizeId", opr?.OrganizeId);
            }
        }

        /// <summary>Stamp a modified entity. An explicit id overwrites Id (the original Modify(keyWord) form).</summary>
        public static void Modify(this object entity, string id = null)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                SetProp(entity, "Id", id);
            }
            var opr = OperatorProvider.GetCurrent();
            SetProp(entity, "LastModifierCode", opr?.UserCode);
            SetProp(entity, "LastModifyTime", DateTime.Now);
        }
    }
}
