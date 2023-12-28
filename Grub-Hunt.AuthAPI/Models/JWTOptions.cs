﻿namespace Grub_Hunt.AuthAPI.Models
{
    public class JWTOptions
    {
        public string? Secret { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}
