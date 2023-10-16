namespace MetaTools.Models;

public class FacebookeInfoModel
{
    public string name { get; set; }
    public string gender { get; set; }
    public Friends friends { get; set; }
    public string id { get; set; }
    public string __fb_trace_id__ { get; set; }
    public string __www_request_id__ { get; set; }
}
public class Friends
{
    public object[] data { get; set; }
    public Summary summary { get; set; }
}

public class Summary
{
    public int total_count { get; set; }
}
