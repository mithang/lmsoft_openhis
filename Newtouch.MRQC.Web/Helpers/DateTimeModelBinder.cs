using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Newtouch.MRQC.Web.Helpers
{
    /// <summary>
    /// DateTime/DateTime? model binder for MRQC Web (cycle 33, mirrors the
    /// Sett.Web cycle-21 / CIS Web cycle-24 / PDS Web cycle-27 / Herp Web cycle-30 binder).
    /// Stock ASP.NET Core binds query-string values with InvariantCulture, so
    /// the views' dd/MM/yyyy values mis-bound on GET (20/10/2026 read as Jan 10,
    /// or a MinValue/null filter) while the same values bound vi-VN on form
    /// POST. This binder accepts the exact formats the views send, in this order:
    ///   1. dd/MM/yyyy (+ time) — every MRQC view sends dd/MM/yyyy; tried first so
    ///      ambiguous day/month values resolve the way the views mean them.
    ///   2. ISO yyyy-MM-dd (+ time, + 'T' separator) and year/month-only values.
    ///   3. US MM/dd/yyyy (+ time) — the form the invariant binder used to accept
    ///      on GET, kept so a US-form value keeps binding exactly as before.
    ///   4. Time-only values, which the invariant binder also accepted.
    /// Anything else fails exactly as the framework binder fails today: a
    /// model-state error and the value left unbound (default for non-nullable,
    /// null for nullable). No fallback date is ever invented, so no silent
    /// wrong date can be introduced. Multi-space runs (the 'dd/MM/yyyy  HH:mm:ss'
    /// dateFmt variant) are collapsed to one space before parsing.
    /// </summary>
    public class DateTimeModelBinder : IModelBinder
    {
        private static readonly string[] DateFormats = new[]
        {
            "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy",
            "dd/MM/yyyy HH:mm:ss", "d/M/yyyy H:mm:ss", "dd/MM/yyyy H:mm:ss", "d/M/yyyy HH:mm:ss",
            "dd/MM/yyyy HH:mm", "d/M/yyyy H:mm",
            "yyyy-MM-dd", "yyyy-M-d", "yyyy-MM", "yyyy", "yyyyMM",
            "yyyy-MM-dd HH:mm:ss", "yyyy-M-d H:mm:ss", "yyyy-MM-dd H:mm:ss", "yyyy-M-d HH:mm:ss",
            "yyyy-MM-dd HH:mm", "yyyy-M-d H:mm",
            "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-ddTHH:mm:ss.fff", "yyyy-MM-ddTHH:mm",
            "MM/dd/yyyy", "M/d/yyyy", "MM/d/yyyy", "M/dd/yyyy",
            "MM/dd/yyyy HH:mm:ss", "M/d/yyyy H:mm:ss", "MM/dd/yyyy H:mm:ss", "M/d/yyyy HH:mm:ss",
            "MM/dd/yyyy HH:mm", "M/d/yyyy H:mm",
            "HH:mm:ss", "H:mm:ss", "HH:mm", "H:mm",
        };

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            var isNullable = Nullable.GetUnderlyingType(bindingContext.ModelType) != null;

            if (valueProviderResult == ValueProviderResult.None)
            {
                return MissingValue(bindingContext, modelName, isNullable);
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrWhiteSpace(value))
            {
                return MissingValue(bindingContext, modelName, isNullable);
            }

            value = Regex.Replace(value.Trim(), @"\s+", " ");
            if (DateTime.TryParseExact(value, DateFormats, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var parsed))
            {
                bindingContext.Result = ModelBindingResult.Success(parsed);
                return Task.CompletedTask;
            }

            bindingContext.ModelState.TryAddModelError(modelName,
                $"The value '{value}' is invalid.");
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        private static Task MissingValue(ModelBindingContext bindingContext, string modelName, bool isNullable)
        {
            if (isNullable)
            {
                bindingContext.Result = ModelBindingResult.Success(null);
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(modelName,
                    $"A value for the '{modelName}' parameter or property was not provided.");
                bindingContext.Result = ModelBindingResult.Failed();
            }
            return Task.CompletedTask;
        }
    }

    /// <summary>Returns <see cref="DateTimeModelBinder"/> for DateTime and DateTime? models.</summary>
    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var modelType = Nullable.GetUnderlyingType(context.Metadata.ModelType)
                ?? context.Metadata.ModelType;
            if (modelType == typeof(DateTime))
            {
                return new DateTimeModelBinder();
            }
            return null;
        }
    }
}
