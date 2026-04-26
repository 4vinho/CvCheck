using api.Core.Interfaces.Auth;

namespace backend.Tests.Auth;

public sealed class TestConfirmationCodeGenerator : IConfirmationCodeGenerator
{
    private readonly Queue<string> codes;
    private int nextCounter;

    public TestConfirmationCodeGenerator(params string[] codes)
    {
        this.codes = new Queue<string>(codes.Length == 0 ? ["ABC123", "DEF456", "GHI789", "JKL012"] : codes);
    }

    public string GenerateCode()
    {
        if (codes.Count > 0)
        {
            return codes.Dequeue();
        }

        nextCounter++;
        return $"C{nextCounter:00000}";
    }
}
