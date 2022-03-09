using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storied.CustomClaimsProvider.API.Models
{
    public class AppSettingsModel
    {
        public string BlobStorageConnectionString { get; set; }
        public string EncryptionKey { get; set; }
    }
}
