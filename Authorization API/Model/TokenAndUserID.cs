﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Model
{
    public class TokenAndUserID
    {
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
