using ProjektMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektMovie.Repositories.Interfaces
{
    public interface IDirectorRepository
    {
        IEnumerable<Director> GetDirectors();
        Director GetDirectorById(int? directorId);
        void AddDirector(Director director);
        void DeleteDirector(int directorId);
        void UpdateDirector(Director director);
        void Save();
        void Dispose();
    }
}
