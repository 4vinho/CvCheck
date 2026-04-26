using Microsoft.AspNetCore.Identity;

namespace api.Core.Entities;

public sealed class ApplicationUser : IdentityUser
{
    public ICollection<EmailConfirmationCode> EmailConfirmationCodes { get; set; } = [];
}
