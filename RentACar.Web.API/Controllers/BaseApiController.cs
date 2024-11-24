﻿using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;

namespace RentACar.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected bool ValidatePhoneNumber(string phoneNumber)
        {
            if (String.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            if (!Regex.IsMatch(phoneNumber, InternationalPhoneNumberRegex))
            {
                return false;
            }

            return true;
        }
    }
}
