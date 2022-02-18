using System;
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
