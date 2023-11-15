using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace MetaTools.Messager.Models;

public class MessagesModel
{
    [JsonPropertyName("data")]
    public List<Datum> Data { get; set; }

    [JsonPropertyName("paging")]
    public Paging Paging { get; set; }
}


// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public class Cursors
{
    [JsonPropertyName("before")]
    public string Before { get; set; }

    [JsonPropertyName("after")]
    public string After { get; set; }
}

public class Datum
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("link")]
    public string Link { get; set; }

    [JsonPropertyName("updated_time")]
    public string UpdatedTime { get; set; }
}

public class Paging
{
    [JsonPropertyName("cursors")]
    public Cursors Cursors { get; set; }

    [JsonPropertyName("next")]
    public string Next { get; set; }

    [JsonPropertyName("previous")]
    public string Previous { get; set; }
}