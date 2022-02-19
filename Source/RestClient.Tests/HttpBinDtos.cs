using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RestClient.Tests;

[ExcludeFromCodeCoverage]
public class Uuid
{
    public Guid uuid { get; set; }
}

[ExcludeFromCodeCoverage]
public class MyIp
{
    public string origin { get; set; }
}

public class MethodResponse
{
    public Dictionary<string, string> args { get; set; }
    public string data { get; set; }
    public Dictionary<string, string> files { get; set; }
    public Dictionary<string, string> form { get; set; }
    public Dictionary<string, string> headers { get; set; }
    public RequestObject json { get; set; }
    public string origin { get; set; }
    public string url { get; set; }
}

public class RequestObject
{
    public int Id { get; set; }
    public string Name { get; set; }
}
