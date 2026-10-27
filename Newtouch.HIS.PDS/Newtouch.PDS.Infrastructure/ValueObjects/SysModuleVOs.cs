using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class SysModuleVO : SysModuleEntity
    {
        public bool IsOpenToOrg { get; set; }
    }

    public class SysModuleButtonVO : SysModuleButtonEntity
    {
        public bool IsOpenToOrg { get; set; }
    }
}
