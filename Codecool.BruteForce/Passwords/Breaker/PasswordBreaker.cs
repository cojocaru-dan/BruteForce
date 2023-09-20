namespace Codecool.BruteForce.Passwords.Breaker;

public class PasswordBreaker : IPasswordBreaker
{
    public IEnumerable<string> GetCombinations(int passwordLength)
    {
        List<List<string>> strings = new List<List<string>>();

        for (int i = 0; i < passwordLength; i++)
        {
            List<string> characters = new List<string>();
            for (int j = 0; j < 128; j++)
            {
                string character = ((char) j).ToString();
                characters.Add(character);
            }
            strings.Add(characters);
        }
    
        return GetAllPossibleCombos(strings);
    }

    private static IEnumerable<string> GetAllPossibleCombos(IEnumerable<IEnumerable<string>> strings)
    {
        IEnumerable<string> combos = new[] { "" };

        combos = strings.Aggregate(combos, (current, inner) => current.SelectMany(c => inner, (c, i) => c + i));

        return combos;
    }
}
