using System.Diagnostics.CodeAnalysis;

namespace CarServices.Api.Configure;

public class Settings
{
    [NotNull] public string? DbConnectionString { get; init; }
}