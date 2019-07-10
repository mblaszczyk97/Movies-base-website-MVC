using ProjektMovie.Models;
using ProjektMovie.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektMovie.Repositories.Fakes
{
    public class FakeMovieRepository : IMovieRepository
    {
        private List<Movie> _movies = new List<Movie>();

        public IEnumerable<Movie> GetMovies()
        {
            return _movies.ToList();
        }

        public Movie GetMovieById(int? movieId)
        {
            return _movies.FirstOrDefault(x => x.Id == movieId);
        }

        public void AddMovie(Movie movie)
        {
            _movies.Add(movie);
        }

        public void DeleteMovie(int movieId)
        {
            Movie movie = _movies.FirstOrDefault(x => x.Id == movieId);
            _movies.Remove(movie);
        }

        public void UpdateMovie(Movie movie)
        {
            Movie modified = _movies.FirstOrDefault(x => x.Id == movie.Id);
            modified = movie;
        }

        public void Save()
        {
        }

        public void Dispose()
        {
        }
    }
}