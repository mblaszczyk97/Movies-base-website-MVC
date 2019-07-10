using ProjektMovie.Context;
using ProjektMovie.Models;
using ProjektMovie.Repositories.Generics;
using ProjektMovie.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjektMovie.Repositories
{
    public class MovieRepository : GenericRepository<ProjektMovieContext, Movie>, IMovieRepository
    {
        public IEnumerable<Movie> GetMovies()
        {
            return Context.Movies.ToList();
        }

        public Movie GetMovieById(int? movieId)
        {
            return Context.Movies.FirstOrDefault(x => x.Id == movieId);
        }

        public void AddMovie(Movie movie)
        {
            Context.Movies.Add(movie);
        }

        public void UpdateMovie(Movie movie)
        {
            Context.Entry(movie).State = EntityState.Modified;
        }

        public void DeleteMovie(int movieId)
        {
            Movie movie = Context.Movies.Find(movieId);
            Context.Movies.Remove(movie);
        }

        public override void Save()
        {
            Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Movie> GetMovie()
        {
            throw new NotImplementedException();
        }
    }
}