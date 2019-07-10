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
    public class DirectorRepository : GenericRepository<ProjektMovieContext, Movie>, IDirectorRepository
    {
        public IEnumerable<Director> GetDirectors()
        {
            return Context.Directors.ToList();
        }

        public Director GetDirectorById(int? directorId)
        {
            return Context.Directors.FirstOrDefault(x => x.Id == directorId);
        }

        public void AddDirector(Director director)
        {
            Context.Directors.Add(director);
        }

        public void UpdateDirector(Director director)
        {
            Context.Entry(director).State = EntityState.Modified;
        }

        public void DeleteDirector(int directorId)
        {
            Director director = Context.Directors.Find(directorId);
            Context.Directors.Remove(director);
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

    }
}