using Codecool.BruteForce.Passwords.Generator;

namespace Codecool.BruteForce.Users.Generator;

public class UserGenerator : IUserGenerator
{
    private static readonly Random random = new();
    private readonly List<IPasswordGenerator> _passwordGenerators;

    private int _userCount;

    public UserGenerator(IEnumerable<IPasswordGenerator> passwordGenerators)
    {
        _passwordGenerators = passwordGenerators.ToList();
    }

    public IEnumerable<(string userName, string password)> Generate(int count, int maxPasswordLength)
    {
        List<(string, string)> usersData = new List<(string, string)>();

        for (int i = 0; i < count; i++)
        {
            string userName = $"user{i + 1}";
            int randomPassLength = GetRandomPasswordLength(maxPasswordLength);
            IPasswordGenerator randomPassGenerator = GetRandomPasswordGenerator();
            string password = randomPassGenerator.Generate(randomPassLength);
            usersData.Add((userName, password));
        }
        
        return usersData;
    }

    private IPasswordGenerator GetRandomPasswordGenerator()
    {
        return _passwordGenerators[random.Next(_passwordGenerators.Count)];
    }

    private static int GetRandomPasswordLength(int maxPasswordLength)
    {
        return random.Next(1, maxPasswordLength + 1);
    }
}
