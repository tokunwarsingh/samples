using Microsoft.AspNetCore.Mvc;
using Storied.CustomClaimsProvider.API.Models;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storied.CustomClaimsProvider.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriedIdentityClaimsController : ControllerBase
    {
        private readonly ILogger<StoriedIdentityClaimsController> _logger;
        private readonly IConfiguration _configuration;

        public StoriedIdentityClaimsController(ILogger<StoriedIdentityClaimsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {

            _logger.Log(LogLevel.Critical, "This is test message");
            string input = null;
            /*
            // Check HTTP basic authorization
            if (!IsAuthorizedUsingUnsecureBasicAuth(Request))
            {
                _logger.LogWarning("HTTP basic authentication validation failed.");
                return Unauthorized();
            } 

            // If not data came in, then return
            if (this.Request.Body == null)
            {
                return StatusCode((int)HttpStatusCode.Conflict, new B2CResponseModel("Request content is null", HttpStatusCode.Conflict));
            }

            // Read the input claims from the request body
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                input = await reader.ReadToEndAsync();
            }

            // Check input content value
            if (string.IsNullOrEmpty(input))
            {
                return StatusCode((int)HttpStatusCode.Conflict, new B2CResponseModel("Request content is empty", HttpStatusCode.Conflict));
            }

            // Convert the input string into InputClaimsModel object
            InputClaimsModel inputClaims = InputClaimsModel.Parse(input);
            /*
            if (inputClaims == null)
            {
                return StatusCode((int)HttpStatusCode.Conflict, new B2CResponseModel("Can not deserialize input claims", HttpStatusCode.Conflict));
            }

            if (string.IsNullOrEmpty(inputClaims.correlationId))
            {
                return StatusCode((int)HttpStatusCode.Conflict, new B2CResponseModel("User 'requestId' is null or empty", HttpStatusCode.Conflict));
            }

            if (string.IsNullOrEmpty(inputClaims.userId))
            {
                return StatusCode((int)HttpStatusCode.Conflict, new B2CResponseModel("User 'userId' is null or empty", HttpStatusCode.Conflict));
            }

            if (string.IsNullOrEmpty(inputClaims.ipAddress))
            {
                return StatusCode((int)HttpStatusCode.Conflict, new B2CResponseModel("User 'ipAddress' is null or empty", HttpStatusCode.Conflict));
            }

            if (string.IsNullOrEmpty(inputClaims.appId))
            {
                return StatusCode((int)HttpStatusCode.Conflict, new B2CResponseModel("User 'appId' is null or empty", HttpStatusCode.Conflict));
            }*/

            //string requestId = Guid.NewGuid().ToString();

            try
            {

                string content = await new StreamReader(Request.Body).ReadToEndAsync();
                _logger.LogInformation(content);
                var requestConnector = JsonSerializer.Deserialize<InputClaimsModel>(content);
                var b2CResponseModel = new B2CResponseModel
                {
                    // use the objectId of the email to get the user specfic claims
                    CustomClaim = $"everything awesome {requestConnector?.Email}"
                };


                return Ok(b2CResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.Conflict, new B2CResponseModel(ex.Message, ""));
            }

        }

        private bool IsAuthorizedUsingUnsecureBasicAuth(HttpRequest req)
        {
            string username = _configuration["BasicAuthUsername"];
            string password = _configuration["BasicAuthPassword"];

            // Check if the HTTP Authorization header exist
            if (!req.Headers.ContainsKey("Authorization"))
            {
                _logger.LogWarning("Missing HTTP basic authentication header.");
                return false;
            }

            // Read the authorization header
            var auth = req.Headers["Authorization"].ToString();

            // Ensure the type of the authorization header id `Basic`
            if (!auth.StartsWith("Basic "))
            {
                _logger.LogWarning("HTTP basic authentication header must start with 'Basic '.");
                return false;
            }

            // Get the the HTTP basinc authorization credentials
            var cred = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');

            // Evaluate the credentials and return the result
            return (cred[0] == username && cred[1] == password);
        }


    }
}