using Movies.API.Models;
using Movies.API.Requests.Movies;

namespace Movies.API.Interface.Repository;

public interface IRepositoryMovie
{
    bool Create(MovieCreateRequest movie);
    Movie? GetById(int id);
    bool Update(int id,MovieUpdateRequest movie);

    bool Delete(int id);

    List<Movie> GetAll();
}
