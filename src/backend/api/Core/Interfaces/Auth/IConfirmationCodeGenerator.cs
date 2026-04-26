namespace api.Core.Interfaces.Auth;

public interface IConfirmationCodeGenerator
{
    string GenerateCode();
}
