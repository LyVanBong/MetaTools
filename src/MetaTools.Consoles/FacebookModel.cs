namespace MetaTools.Consoles;

public class FacebookModel
{
    [JsonPropertyName("accounts")] public Accounts Accounts { get; set; } = new();

    [JsonPropertyName("conversations")]
    public Conversations Conversations { get; set; } = new();

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("data")]
    public List<Datum> Data { get; set; } = new();

    [JsonPropertyName("paging")] public Paging Paging { get; set; } = new();
}

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public class Accounts
{
    [JsonPropertyName("data")]
    public List<Datum> Data { get; set; } = new();

    [JsonPropertyName("paging")] public Paging Paging { get; set; } = new();
}

public class CategoryList
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class Cursors
{
    [JsonPropertyName("before")]
    public string Before { get; set; }

    [JsonPropertyName("after")]
    public string After { get; set; }
}

public class Datum
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("category_list")]
    public List<CategoryList> CategoryList { get; set; } = new();

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("perms")]
    public List<string> Perms { get; set; } = new();

    [JsonPropertyName("tasks")]
    public List<string> Tasks { get; set; } = new();

    [JsonPropertyName("link")]
    public string Link { get; set; }

    [JsonPropertyName("updated_time")]
    public string UpdatedTime { get; set; }

}

public class Paging
{
    [JsonPropertyName("cursors")]
    public Cursors Cursors { get; set; } = new();
    [JsonPropertyName("next")]
    public string Next { get; set; }

    [JsonPropertyName("previous")]
    public string Previous { get; set; }
}

public class Conversations
{
    [JsonPropertyName("data")]
    public List<Datum> Data { get; set; } = new();

    [JsonPropertyName("paging")] public Paging Paging { get; set; } = new();
}


// Test



