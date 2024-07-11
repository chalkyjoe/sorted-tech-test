using System.Text.Json.Serialization;

namespace Domain.Models;

public class Item
{
    [JsonPropertyName( "@id" )]
    public string Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Measure { get; set; }
    public double Value { get; set; }
}