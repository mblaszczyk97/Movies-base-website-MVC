using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjektMovie.Models;
using ProjektMovieTest.Helpers;

namespace ProjektMovieTest.Models
{
    [TestClass]
    public class DirectorTest
    {
        Movie _movie;
        string _imie;
        string _nazwisko;
        DateTime _rok_urodzenia;
        string _kraj;

        [TestInitialize]
        public void InitializeTests()
        {
            _imie = "Zbigniew";
            _nazwisko = "Kowalski";
            _kraj = "Polska";
            _rok_urodzenia = new DateTime(1982, 8, 6);
            _movie = new Movie();
        }

        [TestMethod]
        [TestCategory("Movie")]
        public void DataValidDirector()
        {
            var director = new Director()
            {
                imie = _imie,
                nazwisko = _nazwisko,
                kraj = _kraj,
                rok_urodzenia = _rok_urodzenia
            };
            var context = new ValidationContext(director);
            var result = new List<ValidationResult>();

            var condition = Validator.TryValidateObject(director, context, result, true);
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void MinimalDirectorName()
        {
            var director = new Director()
            {
                imie = "a",
                nazwisko = _nazwisko,
                kraj = _kraj,
                rok_urodzenia = _rok_urodzenia
            };
            var result = TestModelHelper.Validate(director);

            var expected = "Pole Imie Reżysera musi być ciągiem o długości minimalnej 2 i maksymalnej 50.";
            var actual = result[0].ErrorMessage;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void MaximalDirectorName()
        {
            var director = new Director()
            {
                imie = "assssssssssssssssssssssssssaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                nazwisko = _nazwisko,
                kraj = _kraj,
                rok_urodzenia = _rok_urodzenia
            };
            var result = TestModelHelper.Validate(director);

            var expected = "Pole Imie Reżysera musi być ciągiem o długości minimalnej 2 i maksymalnej 50.";
            var actual = result[0].ErrorMessage;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MinimalDirectorLastName()
        {
            var director = new Director()
            {
                imie = _imie,
                nazwisko = "a",
                kraj = _kraj,
                rok_urodzenia = _rok_urodzenia
            };
            var result = TestModelHelper.Validate(director);

            var expected = "Pole Nazwisko Reżysera musi być ciągiem o długości minimalnej 2 i maksymalnej 50.";
            var actual = result[0].ErrorMessage;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void MaximalDirectorLastName()
        {
            var director = new Director()
            {
                imie = _imie,
                nazwisko = "assssssssssssssssssssssssssaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                kraj = _kraj,
                rok_urodzenia = _rok_urodzenia
            };
            var result = TestModelHelper.Validate(director);

            var expected = "Pole Nazwisko Reżysera musi być ciągiem o długości minimalnej 2 i maksymalnej 50.";
            var actual = result[0].ErrorMessage;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ErrorsOnBadDateDirector()
        {
            var director = new Director()
            {
                imie = _imie,
                nazwisko = _nazwisko,
                kraj = _kraj,
                rok_urodzenia = new DateTime(2020, 1, 1)
            };
            var result = TestModelHelper.Validate(director);

            var expected = 1;
            var actual = result.Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DirectorDate()
        {
            var director = new Director()
            {
                imie = _imie,
                nazwisko = _nazwisko,
                kraj = _kraj,
                rok_urodzenia = new DateTime(2020, 1, 1)
            };
            var result = TestModelHelper.Validate(director);

            var expected = "Data nie może być z przyszłości!";
            var actual = result[0].ErrorMessage;
            Assert.AreEqual(expected, actual);
        }

    }
}
