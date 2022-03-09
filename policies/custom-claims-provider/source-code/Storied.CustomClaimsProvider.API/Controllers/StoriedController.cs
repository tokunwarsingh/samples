using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Storied.CustomClaimsProvider.API.Models;
using System.Net;

namespace Storied.CustomClaimsProvider.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriedController : ControllerBase
    {
        private readonly ILogger<StoriedIdentityClaimsController> _logger;
        private readonly IConfiguration _configuration;

        public StoriedController(ILogger<StoriedIdentityClaimsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            try
            {
                 string content = await new StreamReader(Request.Body).ReadToEndAsync();
                _logger.LogInformation(content);
                var requestConnector = JsonSerializer.Deserialize<InputClaimsModel>(content);
                
                var b2CResponseModel = new B2CResponseModel
                {
                    CustomClaim = $"everything awesome {requestConnector?.Email}",
                    RecurlySubscriptionUuid   = "A2323ddd",
                    PlanId = "19580rrr",
                    StartDate = "20122023dd",
                    EndDate = "20122023dd",
                    AccountImagePath = "AccountImagePath-STATIC"

                };
                return Ok(b2CResponseModel);
               
            }
            catch (Exception ex)
            {                
                return StatusCode((int)HttpStatusCode.Conflict, new B2CResponseModel(ex.Message, ""));
            }           
        }

        private bool IsAuthorized()
        {
            return true;

        //    if (!Request.Headers.ContainsKey("Authorization"))
        //    {
        //        ErrorInfo errorInfo = CommonLogic.GetExceptionErrorInfo(new Exception("Missing HTTP basic authentication header."));
        //        _errorLogService.InsertErrorLog(errorInfo);
        //        return false;
        //    }
        //    var auth = Request.Headers["Authorization"].ToString();

        //    if (!auth.StartsWith("Basic "))
        //    {
        //        ErrorInfo errorInfo = CommonLogic.GetExceptionErrorInfo(new Exception("HTTP basic authentication header must start with 'Basic '."));
        //        _errorLogService.InsertErrorLog(errorInfo);
        //        return false;
        //    }
        //    var credential = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
        //    return credential[0] == _appSettings.BasicAuthUsername && credential[1] == _appSettings.BasicAuthPassword;
        }
    }
}
