using System.Text.Json.Serialization;

namespace MetaTools.Models;

public class UserAgentModel
{
    [JsonPropertyName("ua")]
    public string Ua { get; set; }

    [JsonPropertyName("type")]
    public Type Type { get; set; }

    [JsonPropertyName("browser")]
    public Browser Browser { get; set; }

    [JsonPropertyName("os")]
    public Os Os { get; set; }

    [JsonPropertyName("device")]
    public Device Device { get; set; }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Browser
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("version_major")]
    public int VersionMajor { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; }
}

public class Device
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("brand")]
    public object Brand { get; set; }

    [JsonPropertyName("model")]
    public object Model { get; set; }
}

public class Os
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("version_major")]
    public object VersionMajor { get; set; }

    [JsonPropertyName("version")]
    public object Version { get; set; }
}

public class Type
{
    [JsonPropertyName("mobile")]
    public bool Mobile { get; set; }

    [JsonPropertyName("tablet")]
    public bool Tablet { get; set; }

    [JsonPropertyName("touch_capable")]
    public bool TouchCapable { get; set; }

    [JsonPropertyName("pc")]
    public bool Pc { get; set; }

    [JsonPropertyName("bot")]
    public bool Bot { get; set; }
}

