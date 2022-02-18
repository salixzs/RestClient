namespace RestClient.Sample;

// Dummy DTOs for httpbin.org test service to get typed data from its endpoints.

public class BinUuid
{
    public Guid uuid { get; set; }
}

public class IpAddress
{
    public string origin { get; set; }
}
