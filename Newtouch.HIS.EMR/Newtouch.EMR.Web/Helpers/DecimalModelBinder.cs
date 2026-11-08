using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Newtouch.EMR.Web.Helpers
{
    /// <summary>
    /// decimal/decimal? model binder for EMR Web (cycle 56, mirrors the
    /// DateTime binder from cycle 21).
    /// Stock ASP.NET Core binds form bodies with CurrentCulture (vi-VN since
    /// commit 1d15b68c), so a dot-decimal value such as "1234.56" — what the
    /// views send via toFixed(2) strings and regex-validated inputs — binds
    /// as 123456 ('.' is the vi-VN group separator). Query strings bind
    /// invariant, so the same value bound correctly on GET. This binder
    /// accepts BOTH live value classes, in this order, via
    /// <see cref="DecimalParseHelper"/>:
    ///   1. Invariant dot-decimal ("1234.56" -&gt; 1234.56), never 123456.
    ///   2. vi-VN comma-decimal ("1234,56" -&gt; 1234.56), for Razor-rendered
    ///      quoted strings such as '@(Model.zhye)' that parse under vi-VN
    ///      today — a naive invariant-only parse would break them.
    /// Anything else fails exactly as the framework binder fails today: a
    /// model-state error and the value left unbound (default for non-nullable,
    /// null for nullable). No fallback value is ever invented, so no silent
    /// wrong amount can be introduced. Applies to decimal parameters,
    /// decimal properties of complex types, and decimal dictionary values.
    /// </summary>
    public class DecimalModelBinder : IModelBinder
    {
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

            if (DecimalParseHelper.TryParseDecimal(value, out var parsed))
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

    /// <summary>Returns <see cref="DecimalModelBinder"/> for decimal and decimal? models.</summary>
    public class DecimalModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var modelType = Nullable.GetUnderlyingType(context.Metadata.ModelType)
                ?? context.Metadata.ModelType;
            if (modelType == typeof(decimal))
            {
                return new DecimalModelBinder();
            }
            return null;
        }
    }
}
