using ProjektMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektMovie.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies();
        Movie GetMovieById(int? movieId);
        void AddMovie(Movie movie);
        void DeleteMovie(int movieId);
        void UpdateMovie(Movie movie);
        void Save();
        void Dispose();
    }
}