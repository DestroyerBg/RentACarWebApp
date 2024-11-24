using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace RentACar.Web.Infrastructure.InputFormaters
{
    public class TextPlainInputFormatter : InputFormatter
    {
        public TextPlainInputFormatter()
        {
            SupportedMediaTypes.Add("text/plain");
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(string);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            HttpRequest request = context.HttpContext.Request;
            using (StreamReader reader = new StreamReader(request.Body))
            {
                string content = await reader.ReadToEndAsync();
                return await InputFormatterResult.SuccessAsync(content);
            }

        }
    }
}
