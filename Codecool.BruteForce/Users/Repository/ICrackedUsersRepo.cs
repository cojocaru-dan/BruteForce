using Codecool.BruteForce.Users.Model;

namespace Codecool.BruteForce.Users.Repository;
public interface ICrackedUsersRepo
{
    void Add(string userName, string password);
    void Update(int id, string userName, string password);
    void Delete(int id);
    void DeleteAll();

    User Get(int id);
    IEnumerable<User> GetAll();
}