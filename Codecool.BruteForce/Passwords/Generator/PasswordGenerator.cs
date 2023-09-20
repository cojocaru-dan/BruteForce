using Codecool.BruteForce.Passwords.Model;

namespace Codecool.BruteForce.Passwords.Generator;

public class PasswordGenerator : IPasswordGenerator
{
    private static readonly Random Random = new();
    private readonly AsciiTableRange[] _characterSets;

    public PasswordGenerator(params AsciiTableRange[] characterSets)
    {
        _characterSets = characterSets;
    }

    public string Generate(int length)
    {
        string newPassword = "";

        for (int i = 0; i < length; i++)
        {
            AsciiTableRange randomAsciiTable = GetRandomCharacterSet();
            char randomCharacter = GetRandomCharacter(randomAsciiTable);
            newPassword += randomCharacter;
        }

        return newPassword;
    }

    private AsciiTableRange GetRandomCharacterSet()
    {
        return _characterSets[Random.Next(_characterSets.Length)];
    }

    private static char GetRandomCharacter(AsciiTableRange characterSet)
    {
        return (char) Random.Next(characterSet.Start, characterSet.End + 1);
    }
}
