using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RentACar.Web.ViewModels.Admin;

namespace RentACar.Web.ViewModels.ModelBinders
{
    public class AddCarModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(AddCarViewModel))
            {
                return;
            }

            HttpRequest? request = bindingContext.HttpContext.Request;

            AddCarViewModel model = new AddCarViewModel();
            model.Brand = bindingContext.ValueProvider.GetValue("Brand").FirstValue;
            model.Model = bindingContext.ValueProvider.GetValue("Model").FirstValue;
            model.YearOfManifacture = int.Parse(bindingContext.ValueProvider.GetValue("YearOfManifacture").FirstValue);
            model.HorsePower = int.Parse(bindingContext.ValueProvider.GetValue("HorsePower").FirstValue);
            model.RegistrationNumber = bindingContext.ValueProvider.GetValue("RegistrationNumber").FirstValue;
            model.PricePerDay = decimal.Parse(bindingContext.ValueProvider.GetValue("PricePerDay").FirstValue);
            model.CategoryId = bindingContext.ValueProvider.GetValue("CategoryId").FirstValue;
            model.LocationId = bindingContext.ValueProvider.GetValue("LocationId").FirstValue;

            if (request.Form.Files.Count > 0)
            {
                model.CarImage = request.Form.Files["CarImage"];
            }


            bindingContext.Result = ModelBindingResult.Success(model);
        }
    }
}
