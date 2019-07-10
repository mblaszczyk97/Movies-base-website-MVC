using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjektMovie.Models;
using ProjektMovieTest.Helpers;

namespace ProjektMovieTest.Models
{
    [TestClass]
    public class MovieModelTest
    {
        Movie movie;
        string _nazwa;
        string _rodzaj;
        DateTime _rok_produkcji;

        [TestInitialize]
        public void InitializeTests()
        {
            _nazwa = "Władca Pierścieni";
            _rodzaj = "Fantasy";
            _rok_produkcji = new DateTime(2019, 1, 1);

       
        }

        [TestMethod]
        [TestCategory("Movie")]
        public void DataValidMovie()
        {
            var movie = new Movie()
            {
                nazwa = _nazwa,
                rodzaj = _rodzaj,
                rok_produkcji = _rok_produkcji
            };
            var context = new ValidationContext(movie);
            var result = new List<ValidationResult>();

            var condition = Validator.TryValidateObject(movie, context, result, true);
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void MinimalMovieName()
        {
            var movie = new Movie()
            {
                nazwa = "a",
                rodzaj = _rodzaj,
                rok_produkcji = _rok_produkcji
            };
            var result = TestModelHelper.Validate(movie);

            var expected = "Pole Tytuł musi być ciągiem o długości minimalnej 2 i maksymalnej 60.";
            var actual = result[0].ErrorMessage;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MaximalMovieName()
        {
            var movie = new Movie()
            {
                nazwa = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                rodzaj = _rodzaj,
                rok_produkcji = _rok_produkcji
            };
            var result = TestModelHelper.Validate(movie);

            var expected = "Pole Tytuł musi być ciągiem o długości minimalnej 2 i maksymalnej 60.";
            var actual = result[0].ErrorMessage;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MinimalGenreName()
        {
            var movie = new Movie()
            {
                nazwa = _nazwa,
                rodzaj = "ss",
                rok_produkcji = _rok_produkcji
            };
            var result = TestModelHelper.Validate(movie);

            var expected = "Pole Rodzaj musi być ciągiem o długości minimalnej 4 i maksymalnej 50.";
            var actual = result[0].ErrorMessage;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MaximalGenreName()
        {
            var movie = new Movie()
            {
                nazwa = _nazwa,
                rodzaj = "asdddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd",
                rok_produkcji = _rok_produkcji
            };
            var result = TestModelHelper.Validate(movie);

            var expected = "Pole Rodzaj musi być ciągiem o długości minimalnej 4 i maksymalnej 50.";
            var actual = result[0].ErrorMessage;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DateIsDate()
        {
            var movie = new Movie()
            {
                nazwa = _nazwa,
                rodzaj = _rodzaj,
                rok_produkcji = new DateTime(2018, 10, 12),
            };

            var value = movie.rok_produkcji;
            var expectedType = typeof(DateTime);
            Assert.IsInstanceOfType(value, expectedType);
        }

    }
}
