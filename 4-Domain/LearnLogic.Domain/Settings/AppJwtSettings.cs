﻿using System.Collections.Generic;

namespace LearnLogic.Domain.Settings
{
    public class AppJwtSettings
    {
        public string SecretKey { get; set; }
        public int Expiration { get; set; }
        public string Issuer { get; set; }
        public IList<string> Issuers { get; set; }
        public string Audience { get; set; }
        public IList<string> Audiences { get; set; }
    }
}
