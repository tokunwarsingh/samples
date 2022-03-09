﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Storied.CustomClaimsProvider.API.Models
{
    public class B2CResponseModel
    {

        public const string ApiVersion = "1.0.0";

        public B2CResponseModel()
        {
            Version = ApiVersion;
            Action = "Continue";
        }

        public B2CResponseModel(string action, string userMessage)
        {
            Version = ApiVersion;
            Action = action;
            UserMessage = userMessage;
            if (action == "ValidationError")
            {
                Status = "400";
            }
        }

        [JsonPropertyName("version")]
        public string Version { get; }

        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("userMessage")]
        public string? UserMessage { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("extension_CustomClaim")]       
        public string CustomClaim { get; set; } = string.Empty;
        [JsonPropertyName("extension_RecurlySubscriptionUuid")]
        public string RecurlySubscriptionUuid { get; set; } = string.Empty;
        [JsonPropertyName("extension_PlanId")]
        public string PlanId { get; set; } = string.Empty;
        [JsonPropertyName("extension_StartDate")]
        public string StartDate { get; set; } = string.Empty;
        [JsonPropertyName("extension_EndDate")]
        public string EndDate { get; set; } = string.Empty;

        [JsonPropertyName("extension_AccountImagePath")]
        public string AccountImagePath { get; set; } = string.Empty;

        
    }

}
