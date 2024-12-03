using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentACar.Web.ViewModels.Admin;
using RentACar.Web.ViewModels.Category;
using RentACar.Web.ViewModels.Feature;
using RentACar.Web.ViewModels.Location;

namespace RentACar.Web.ViewModels.ModelBinders
{
    public class EditCarModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(EditCarViewModel))
            {
                return;
            }

            HttpRequest? request = bindingContext.HttpContext.Request;

            EditCarViewModel model = new EditCarViewModel();
            model.Id = bindingContext.ValueProvider.GetValue("Id").FirstValue;
            model.Brand = bindingContext.ValueProvider.GetValue("Brand").FirstValue;
            model.Model = bindingContext.ValueProvider.GetValue("Model").FirstValue;
            model.YearOfManufacture = int.Parse(bindingContext.ValueProvider.GetValue("YearOfManufacture").FirstValue);
            model.HorsePower = int.Parse(bindingContext.ValueProvider.GetValue("HorsePower").FirstValue);
            model.RegistrationNumber = bindingContext.ValueProvider.GetValue("RegistrationNumber").FirstValue;
            model.PricePerDay = decimal.Parse(bindingContext.ValueProvider.GetValue("PricePerDay").FirstValue);
            model.CarImageUrl = bindingContext.ValueProvider.GetValue("CarImageUrl").FirstValue;
            if (request.Form.Files.Count > 0)
            {
                model.CarImage = request.Form.Files["CarImage"];
            }

            IEnumerable<string> categoryKeys = request.Form.Keys
                .Where(key => key.StartsWith("Categories["))
                .Select(key => key.Split('[', ']')[1])
                .Distinct();

            foreach (string index in categoryKeys)
            {

                string idKey = $"Categories[{index}].Value";
                string nameKey = $"Categories[{index}].Text";
                string isSelected = $"Categories[{index}].Selected";

                string id = request.Form[idKey];
                string name = request.Form[nameKey];
                string selected = request.Form[isSelected];

                model.Categories.Add(new SelectListItem()
                {
                    Value = id,
                    Text = name,
                    Selected = selected.Contains("true")
                });
            }

            IEnumerable<string> locationKeys = request.Form.Keys
                .Where(key => key.StartsWith("Locations["))
                .Select(key => key.Split('[', ']')[1])
                .Distinct();

            foreach (string index in locationKeys)
            {
                string idKey = $"Locations[{index}].Value";
                string cityKey = $"Locations[{index}].Text";
                string isSelected = $"Locations[{index}].Selected";

                string id = request.Form[idKey];
                string city = request.Form[cityKey];
                string selected = request.Form[isSelected];

                model.Locations.Add(new SelectListItem()
                {
                    Value = id, 
                    Text = city,
                    Selected = selected != null
                });
            }

            IEnumerable<string> featureKeys = request.Form.Keys
                .Where(key => key.StartsWith("Features["))
                .Select(key => key.Split('[', ']')[1])
                .Distinct();

            foreach (string index in featureKeys)
            {
                string idKey = $"Features[{index}].Value";
                string nameKey = $"Features[{index}].Text";
                string isCheckedKey = $"Features[{index}].Selected";

                string id = request.Form[idKey];
                string name = request.Form[nameKey];

                string isCheckedRaw = request.Form[isCheckedKey];
                bool isChecked = isCheckedRaw == "true";

                model.Features.Add(new SelectListItem()
                {
                    Value = id,
                    Text = name,
                    Selected = isChecked
                });
            }

            bindingContext.Result = ModelBindingResult.Success(model);
        }
    }
}
