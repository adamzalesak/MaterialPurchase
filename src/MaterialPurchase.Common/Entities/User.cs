namespace MaterialPurchase.Common.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;
    public string ExternalId { get; set; } = string.Empty;
    public string SId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
}