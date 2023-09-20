using Codecool.BruteForce.Users.Model;
using Codecool.BruteForce.Users.Repository;

namespace Codecool.BruteForce.Authentication;
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public bool Authenticate(string userName, string password)
    {
        List<User> users = _userRepository.GetAll().ToList();
        foreach (var user in users)
        {
            if (user.UserName == userName && user.Password == password) return true;
        }
        return false;
    }
}