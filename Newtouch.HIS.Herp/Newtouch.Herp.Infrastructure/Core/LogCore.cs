using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Newtouch.Herp.Infrastructure.Log
{
    /// <summary>
    /// Minimal LogCore port. The original wrote structured error logs through
    /// NLog client infrastructure (binary-only); here errors go to Trace so
    /// they remain visible in diagnostics without a logging dependency in the
    /// infra assembly. Call sites (error paths that return null) are unchanged.
    /// </summary>
    public static class LogCore
    {
        public static void Error(string message, Exception ex, Dictionary<string, string> addInfo = null)
        {
            try
            {
                var info = addInfo != null ? string.Join(";", addInfo) : string.Empty;
                Trace.TraceError("[Herp] {0} {1} {2}", message, info, ex);
            }
            catch { /* logging must never throw */ }
        }
    }
}
