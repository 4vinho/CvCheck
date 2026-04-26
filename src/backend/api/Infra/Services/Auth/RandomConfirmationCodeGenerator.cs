using api.Core.Interfaces.Auth;
using System.Security.Cryptography;

namespace api.Infra.Services.Auth;

public sealed class RandomConfirmationCodeGenerator : IConfirmationCodeGenerator
{
    private const string AllowedChars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

    public string GenerateCode()
    {
        Span<char> buffer = stackalloc char[6];

        for (var i = 0; i < buffer.Length; i++)
        {
            buffer[i] = AllowedChars[RandomNumberGenerator.GetInt32(AllowedChars.Length)];
        }

        return new string(buffer);
    }
}
