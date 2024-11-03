namespace MaterialPurchase.Configuration;

public record SwaggerAuthOptions
{
    public required Uri AuthorizationUrl { get; init; }
    public required Uri TokenUrl { get; init; }
    public required Uri OpenIdConnectUrl { get; init; }
    public required Uri ContactUrl { get; init; }
    public string Scope { get; init; } = string.Empty;
    public string ClientId { get; init; } = string.Empty;
}