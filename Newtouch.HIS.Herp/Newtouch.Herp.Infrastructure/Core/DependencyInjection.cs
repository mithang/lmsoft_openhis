using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Newtouch.Herp.Infrastructure
{
    /// <summary>
    /// Replaces Autofac assembly scanning (DependencyResolver) with MS DI scanning:
    /// every *Repo / *DmnService / *App class in this assembly is registered scoped
    /// as the Herp interface named "I" + class name, plus any other Herp interface
    /// it alone implements. Controllers inject interfaces, so ctor signatures of
    /// services stay flexible (parameterless base; repos/services take only the
    /// interfaces they actually use).
    /// </summary>
    public static class HerpServiceRegistration
    {
        public static void AddHerpServices(this IServiceCollection services)
        {
            var asm = typeof(HerpDb).Assembly;
            var candidates = asm.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract
                    && (t.Name.EndsWith("Repo") || t.Name.EndsWith("DmnService") || t.Name.EndsWith("App")))
                .ToList();

            foreach (var type in candidates)
            {
                var herpInterfaces = type.GetInterfaces()
                    .Where(i => i.Namespace != null && i.Namespace.StartsWith("Newtouch.Herp"))
                    .ToList();
                var exact = herpInterfaces.FirstOrDefault(i => i.Name == "I" + type.Name);
                if (exact != null)
                {
                    services.AddScoped(exact, type);
                }
                foreach (var iface in herpInterfaces)
                {
                    if (iface == exact) continue;
                    // Only auto-register an extra interface when a single class provides it.
                    if (candidates.Count(c => iface.IsAssignableFrom(c)) == 1)
                    {
                        services.AddScoped(iface, type);
                    }
                }
            }
        }
    }
}
