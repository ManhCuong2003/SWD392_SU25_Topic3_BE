﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Config
{
    public class PayOSSettings
    {
        public string ClientId { get; set; }
        public string ApiKey { get; set; }
        public string ChecksumKey { get; set; }
        public string BaseUrl { get; set; }
    }
}
