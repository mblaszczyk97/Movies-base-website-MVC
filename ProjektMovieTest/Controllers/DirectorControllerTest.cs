using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjektMovie.Controllers;
using ProjektMovie.Models;
using ProjektMovie.Repositories;
using ProjektMovie.Repositories.Fakes;
using ProjektMovie.Repositories.Interfaces;

namespace ProjektMovieTest.Controllers
{
    [TestClass]
    public class DirectorControllerTest
    {
        FakeDirectorRepository _fakeDirectorRepository;
        DirectorRepository _directorRepository;
        DirectorsController _directorController;

        [TestInitialize]
        public void SetUp()
        {
            _fakeDirectorRepository = new FakeDirectorRepository();
            _directorRepository = new DirectorRepository();
        }

        [TestMethod]
        public void DirectorReturnsSomething()
        {
            _directorController = new DirectorsController(_directorRepository);

            var value = _directorController.Index() as ViewResult;
            Assert.IsNotNull(value);
        }

        [TestMethod]
        public void DirectorCreateisCreated()
        {
            _directorController = new DirectorsController(_directorRepository);

            var type = typeof(ViewResult);
            var real = _directorController.Create();
            Assert.IsInstanceOfType(real, type);
        }


        [TestMethod]
        public void DirectorCreateisCreatedOnFake()
        {
            _directorController = new DirectorsController(_fakeDirectorRepository);

            var type = typeof(ActionResult);
            var real = _directorController.Create(new Director());
            Assert.IsInstanceOfType(real, type);
        }

        [TestMethod]
        public void MockDirectorEdit()
        {
            var director = new Director();
            var service = new Mock<IDirectorRepository>();
            service.Setup(x => x.GetDirectorById(1)).Returns(director);
            _directorController = new DirectorsController(service.Object);

            var expected = director;
            var result = _directorController.Edit(1);
            var actual = ((ViewResult)result).Model as Director;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FakeDirectorEdit()
        {
            _directorController = new DirectorsController(_fakeDirectorRepository);

            var type = typeof(ActionResult);
            var real = _directorController.Edit(new Director());
            Assert.IsInstanceOfType(real, type);
        }

        [TestMethod]
        public void MockDeleteFromDirector()
        {
            var director = new Director();
            var service = new Mock<IDirectorRepository>();
            service.Setup(x => x.GetDirectorById(1)).Returns(director);
            _directorController = new DirectorsController(service.Object);

            var expected = director;
            var result = _directorController.Delete(1);
            var actual = ((ViewResult)result).Model as Director;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void MockEverythingDeletedDirector()
        {
            var director = new Director();
            var service = new Mock<IDirectorRepository>();
            service.Setup(x => x.GetDirectorById(1)).Returns(director);
            _directorController = new DirectorsController(service.Object);

            var type = typeof(RedirectToRouteResult);
            var real = _directorController.DeleteConfirmed(1);
            Assert.IsInstanceOfType(real, type);
        }

        [TestMethod]
        public void MockMovieIncorrectID()
        {
            var director = new Director();
            var service = new Mock<IDirectorRepository>();
            service.Setup(x => x.GetDirectorById(1)).Returns(director);
            _directorController = new DirectorsController(service.Object);

            var expectedErrorCode = 404;
            var result = _directorController.Details(1000000);
            var viewResult = (HttpNotFoundResult)result;
            var actual = viewResult.StatusCode;
            Assert.AreEqual(expectedErrorCode, actual);
        }

        [TestCleanup]
        public void EndUp()
        {
            _directorRepository = null;
            _directorController = null;
        }
    }
}
