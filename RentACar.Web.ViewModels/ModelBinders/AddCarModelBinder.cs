using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RentACar.Web.ViewModels.Admin;
using RentACar.Web.ViewModels.Category;
using RentACar.Web.ViewModels.Feature;
using RentACar.Web.ViewModels.Location;

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

            IEnumerable<string> categoryKeys = request.Form.Keys.Where(key => key.StartsWith("Categories["));
            foreach (string key in categoryKeys)
            {
                string index = key.Split('[', ']')[1];
                string idKey = $"Categories[{index}].Id";
                string nameKey = $"Categories[{index}].Name";

                string id = request.Form[idKey];
                string name = request.Form[nameKey];

                model.Categories.Add(new CategoryViewModel { Id = id, Name = name });
            }

            IEnumerable<string> locationKeys = request.Form.Keys.Where(key => key.StartsWith("Locations["));
            foreach (string key in locationKeys)
            {
                string index = key.Split('[', ']')[1]; 
                string idKey = $"Locations[{index}].Id";
                string cityKey = $"Locations[{index}].City";

                string id = request.Form[idKey];
                string city = request.Form[cityKey];

                model.Locations.Add(new LocationViewModel { Id = id, City = city });
            }

            IEnumerable<string> featureKeys = request.Form.Keys.Where(key => key.StartsWith("Features["));
            foreach (string key in featureKeys)
            {
                string index = key.Split('[', ']')[1];
                string idKey = $"Features[{index}].Id";
                string nameKey = $"Features[{index}].Name";
                string isCheckedKey = $"Features[{index}].IsChecked";

                string id = request.Form[idKey];
                string name = request.Form[nameKey];
                string isChecked = request.Form[isCheckedKey];
                bool isCheckedValue;

                if (isChecked.Contains(','))
                {
                    isCheckedValue = bool.Parse(isChecked.Split(',')[0]);
                }
                else
                {
                    isCheckedValue = bool.Parse(isChecked);
                }

                model.Features.Add(new FeatureCheckboxViewModel(){ Id = id, Name = name, IsChecked = isCheckedValue});
            }

            bindingContext.Result = ModelBindingResult.Success(model);
        }
    }
}
