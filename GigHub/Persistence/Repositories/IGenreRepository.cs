using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Persistence.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}