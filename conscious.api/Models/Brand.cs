using System;

namespace conscious.api.Models;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsBoycotted { get; set; }
    public string Reason { get; set; } = string.Empty;
    public bool Visibility { get; set; }

}
