using System.Text.Json.Serialization;

namespace Example.AI.App02;

internal sealed class IntentModel
{
    [JsonRequired]
    public required string Theme { get; set; }
}
