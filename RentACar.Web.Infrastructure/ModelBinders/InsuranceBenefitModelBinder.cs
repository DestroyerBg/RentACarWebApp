using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RentACar.Web.Infrastructure.ModelBinders
{
    public class InsuranceBenefitModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            string? value = valueProviderResult.FirstValue;

            
            if (bool.TryParse(value, out bool result))
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Success(false);
            }

            return Task.CompletedTask;
        }
    }
}
