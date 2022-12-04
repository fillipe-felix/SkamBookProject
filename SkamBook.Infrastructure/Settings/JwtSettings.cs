﻿namespace SkamBook.Infrastructure.Settings;

public class JwtSettings
{
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
    public string Secret { get; set; }
    public int Expiration { get; set; }
}
