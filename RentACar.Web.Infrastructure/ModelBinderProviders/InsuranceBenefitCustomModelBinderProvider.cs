﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using RentACar.Web.ViewModels.ModelBinders;

namespace RentACar.Web.Infrastructure.ModelBinderProviders
{
    public class InsuranceBenefitCustomModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(bool))
            {
                return new InsuranceBenefitModelBinder();
            }

            return null;
        }
    }
}
