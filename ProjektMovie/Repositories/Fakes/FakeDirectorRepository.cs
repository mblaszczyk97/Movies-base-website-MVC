using ProjektMovie.Models;
using ProjektMovie.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektMovie.Repositories.Fakes
{
    public class FakeDirectorRepository : IDirectorRepository
    {
        private List<Director> _directors = new List<Director>();

        public IEnumerable<Director> GetDirectors()
        {
            return _directors.ToList();
        }

        public Director GetDirectorById(int? directorId)
        {
            return _directors.FirstOrDefault(x => x.Id == directorId);
        }

        public void AddDirector(Director director)
        {
            _directors.Add(director);
        }

        public void DeleteDirector(int directorId)
        {
            Director director = _directors.FirstOrDefault(x => x.Id == directorId);
            _directors.Remove(director);
        }

        public void UpdateDirector(Director director)
        {
            Director modified = _directors.FirstOrDefault(x => x.Id == director.Id);
            modified = director;
        }

        public void Save()
        {
        }

        public void Dispose()
        {
        }
    }
}