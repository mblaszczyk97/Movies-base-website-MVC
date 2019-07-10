using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjektMovie.Controllers;
using ProjektMovie.Models;
using ProjektMovie.Repositories;
using ProjektMovie.Repositories.Fakes;
using Moq;
using ProjektMovie.Repositories.Interfaces;

namespace ProjektMovieTest.Controllers
{
    [TestClass]
    public class MovieControllerTest
    {
        FakeMovieRepository _fakeMovieRepository;
        MovieRepository _movieRepository;
        MoviesController _moviesController;

        [TestInitialize]
        public void SetUp()
        {
            _fakeMovieRepository = new FakeMovieRepository();
            _movieRepository = new MovieRepository();
        }

        [TestMethod]
        public void MovieReturnsSomething()
        {
            _moviesController = new MoviesController(_movieRepository);

            var value = _moviesController.Index() as ViewResult;
            Assert.IsNotNull(value);
        }

        [TestMethod]
        public void MockMovieRepisotoryOnDetails()
        {
            var movie = new Movie();
            var service = new Mock<IMovieRepository>();
            service.Setup(x => x.GetMovieById(1)).Returns(movie);
            _moviesController = new MoviesController(service.Object);

            var expected = movie;
            var result = _moviesController.Details(1);
            var actual = ((ViewResult)result).Model as Movie;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void MockMovieIncorrectID()
        {
            var movie = new Movie();
            var service = new Mock<IMovieRepository>();
            service.Setup(x => x.GetMovieById(1)).Returns(movie);
            _moviesController = new MoviesController(service.Object);

            var expectedErrorCode = 404;
            var result = _moviesController.Details(1000000);
            var viewResult = (HttpNotFoundResult)result;
            var actual = viewResult.StatusCode;
            Assert.AreEqual(expectedErrorCode, actual);
        }

        [TestMethod]
        public void MovieCreateisCreated()
        {
            _moviesController = new MoviesController(_movieRepository);

            var expectedType = typeof(ViewResult);
            var value = _moviesController.Create();
            Assert.IsInstanceOfType(value, expectedType);
        }


        [TestMethod]
        public void MovieCreateisCreatedOnFake()
        {
            _moviesController = new MoviesController(_fakeMovieRepository);

            var expectedType = typeof(ActionResult);
            var value = _moviesController.Create(new Movie());
            Assert.IsInstanceOfType(value, expectedType);
        }

        [TestMethod]
        public void MockMovieEdit()
        {
            var movie = new Movie();
            var service = new Mock<IMovieRepository>();
            service.Setup(x => x.GetMovieById(1)).Returns(movie);
            _moviesController = new MoviesController(service.Object);

            var expected = movie;
            var result = _moviesController.Edit(1);
            var actual = ((ViewResult)result).Model as Movie;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FakeMovieEdit()
        {
            _moviesController = new MoviesController(_fakeMovieRepository);

            var expectedType = typeof(ActionResult);
            var value = _moviesController.Edit(new Movie());
            Assert.IsInstanceOfType(value, expectedType);
        }

        [TestMethod]
        public void MockDeleteFromMovie()
        {
            var movie = new Movie();
            var service = new Mock<IMovieRepository>();
            service.Setup(x => x.GetMovieById(1)).Returns(movie);
            _moviesController = new MoviesController(service.Object);

            var expected = movie;
            var result = _moviesController.Delete(1);
            var actual = ((ViewResult)result).Model as Movie;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void MockEverythingDeletedMovie()
        {
            var movie = new Movie();
            var service = new Mock<IMovieRepository>();
            service.Setup(x => x.GetMovieById(1)).Returns(movie);
            _moviesController = new MoviesController(service.Object);

            var expectedType = typeof(RedirectToRouteResult);
            var result = _moviesController.DeleteConfirmed(1);
            Assert.IsInstanceOfType(result, expectedType);
        }

        [TestCleanup]
        public void EndUp()
        {
            _movieRepository = null;
            _moviesController = null;
        }

    }
}
