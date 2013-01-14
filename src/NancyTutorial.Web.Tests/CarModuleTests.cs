using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace NancyTutorial.Web.Tests
{
    [TestFixture]
    public class CarModuleTests
    {
        [Test]
        public void Should_return_car_as_xml_when_present()
        {
            //Given
            var mockRepository = new Mock<ICarRepository>();
            mockRepository.Setup(x => x.GetById(123)).Returns(new Car
                                                                  {
                                                                      Id = 123,
                                                                      Make = "Tesla",
                                                                      Model = "Model S"
                                                                  });           
            var browser = new Browser((c) => c.Module<CarModule>()
                                                 .Dependency<ICarRepository>(mockRepository.Object));
            //When
            var response = browser.Get("/car/123", with =>
                                                       {
                                                           with.HttpRequest();
                                                           with.Header("accept", "application/xml");
                                                       });
            var responseBody = response.BodyAsXml();
            //Then
            Assert.That(response,Is.Not.Null);
            
            string make = responseBody.Element("Car").Element("Make").Value;

            Assert.That(make,Is.EqualTo("Tesla"));
        }

        [Test]
        public void Should_return_404_NotFound_when_CarRespository_throws_NotFoundException()
        {
            //Given
            var mockRepository = new Mock<ICarRepository>();
            mockRepository.Setup(x => x.GetById(111)).Throws(new CarNotFoundException("Car with Id 111 Not Found"));

            var browser = new Browser((c) => c.Module<CarModule>()
                                                 .Dependency<ICarRepository>(mockRepository.Object));
            //When            
            var response = browser.Get("/car/111", with =>
                                                       {
                                                           with.HttpRequest();
                                                           with.Header("accept", "application/xml");
                                                       });

            var responseBody = response.Body.AsString();
            //Then
            Assert.That(response.StatusCode,Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(responseBody, Is.EqualTo("Car with Id 111 Not Found"));



        }
    }
}
