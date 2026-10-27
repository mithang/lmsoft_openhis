using NewtouchHIS.Lib.Framework.Operator;
using System;
using System.Reflection;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// Marker for ported OR entities (replaces FrameworkBase IEntity&lt;T&gt;).
    /// All OR entities carry a string Id; the remaining audit columns are set
    /// by EntityHelper via reflection because the originals declare them
    /// individually rather than inheriting them.
    /// </summary>
    public interface IEntityBase
    {
        string Id { get; set; }
    }

    /// <summary>
    /// Replaces FrameworkBase entity Create()/Modify() helpers.
    /// Create: assigns a new Guid Id when empty, CreateTime now, CreatorCode
    /// from the current operator, zt "1" when empty.
    /// Modify: stamps LastModifyTime now and LastModifierCode from the
    /// current operator (when those properties exist on the entity).
    /// </summary>
    public static class EntityHelper
    {
        private static string CurrentUserCode()
        {
            try
            {
                return OperatorProvider.GetCurrent()?.UserCode ?? "";
            }
            catch
            {
                return "";
            }
        }

        private static void SetProp(object entity, string name, object value)
        {
            var prop = entity.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                try
                {
                    var target = prop.PropertyType;
                    object v = value;
                    if (value != null && target != value.GetType())
                    {
                        if (target == typeof(DateTime?) && value is DateTime)
                        {
                            v = value;
                        }
                        else
                        {
                            v = Convert.ChangeType(value, Nullable.GetUnderlyingType(target) ?? target);
                        }
                    }
                    prop.SetValue(entity, v);
                }
                catch
                {
                    // audit columns are best-effort; never break a save for them
                }
            }
        }

        public static void Create(IEntityBase entity, bool isNew)
        {
            if (entity == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString();
            }
            SetProp(entity, "CreateTime", DateTime.Now);
            SetProp(entity, "CreatorCode", CurrentUserCode());
            var zt = entity.GetType().GetProperty("zt")?.GetValue(entity) as string;
            if (string.IsNullOrEmpty(zt))
            {
                SetProp(entity, "zt", "1");
            }
        }

        public static void Modify(IEntityBase entity, string keyValue)
        {
            if (entity == null)
            {
                return;
            }
            if (!string.IsNullOrEmpty(keyValue) && string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = keyValue;
            }
            SetProp(entity, "LastModifyTime", DateTime.Now);
            SetProp(entity, "LastModifierCode", CurrentUserCode());
        }
    }
}
