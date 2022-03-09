using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Storied.CustomClaimsProvider.API.Models
{
    public class InputClaimsModel
    {      

        [JsonPropertyName("city")]
        public string city { get; set; } = string.Empty;

        [JsonPropertyName("postalCode")]
        public string postalCode { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;   

    }
}
