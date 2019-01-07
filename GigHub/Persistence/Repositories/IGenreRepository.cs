using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Persistence.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}