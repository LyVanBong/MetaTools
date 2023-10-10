using Newtonsoft.Json;

namespace MetaTools.Models;

public class UserAgentModel
{
    [JsonProperty("ua")]
    public string Ua { get; set; }

    [JsonProperty("type")]
    public Type Type { get; set; }

    [JsonProperty("browser")]
    public Browser Browser { get; set; }

    [JsonProperty("os")]
    public Os Os { get; set; }

    [JsonProperty("device")]
    public Device Device { get; set; }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Browser
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("version_major")]
    public int VersionMajor { get; set; }

    [JsonProperty("version")]
    public string Version { get; set; }
}

public class Device
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("brand")]
    public object Brand { get; set; }

    [JsonProperty("model")]
    public object Model { get; set; }
}

public class Os
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("version_major")]
    public object VersionMajor { get; set; }

    [JsonProperty("version")]
    public object Version { get; set; }
}

public class Type
{
    [JsonProperty("mobile")]
    public bool Mobile { get; set; }

    [JsonProperty("tablet")]
    public bool Tablet { get; set; }

    [JsonProperty("touch_capable")]
    public bool TouchCapable { get; set; }

    [JsonProperty("pc")]
    public bool Pc { get; set; }

    [JsonProperty("bot")]
    public bool Bot { get; set; }
}

