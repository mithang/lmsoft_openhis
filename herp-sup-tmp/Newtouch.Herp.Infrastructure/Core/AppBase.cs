using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.Herp.Application
{
    /// <summary>
    /// Application-layer base replacing FrameworkBase AppBase (binary-only).
    /// OrganizeId/UserIdentity come from the current operator, exactly as the
    /// Framework base exposed them to the Herp app services.
    /// </summary>
    public class AppBase
    {
        protected string OrganizeId => OperatorProvider.GetCurrent()?.OrganizeId;

        protected OperatorModel UserIdentity => OperatorProvider.GetCurrent();
    }
}
