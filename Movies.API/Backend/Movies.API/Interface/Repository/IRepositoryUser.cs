using Movies.API.Models;
using Movies.API.Requests.Users;
namespace Movies.API.Interface.Repository;
public interface IRepositoryUser
{
    bool Create(UserCreateRequest user);
    User? GetById(int id);
    bool Update(int id, UserUpdateRequest user);
    bool Delete(int id);
    IEnumerable<User> GetAll();
}