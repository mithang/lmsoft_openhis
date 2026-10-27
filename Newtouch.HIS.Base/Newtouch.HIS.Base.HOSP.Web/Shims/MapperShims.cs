using System.Collections.Generic;
using Mapster;

namespace Newtouch.Tools
{
    /// <summary>
    /// MapperTo helpers used by the Framework HOSP web (originally a Tools wrapper over
    /// an object mapper). Implemented here over Mapster (already referenced by the web
    /// project). Two shapes exist at the call sites:
    ///   entity = srcEntity.MapperTo(destEntity);          // copy src properties onto dest
    ///   vo = entity.MapperTo&lt;TEntity, TVO&gt;();         // map to a new instance
    /// </summary>
    public static class MapperExtensions
    {
        public static TDest MapperTo<TDest>(this object src, TDest dest)
        {
            if (src == null || dest == null) return dest;
            return src.Adapt(dest);
        }

        public static TDest MapperTo<TSrc, TDest>(this TSrc src)
        {
            if (src == null) return default(TDest);
            return src.Adapt<TSrc, TDest>();
        }
    }
}
