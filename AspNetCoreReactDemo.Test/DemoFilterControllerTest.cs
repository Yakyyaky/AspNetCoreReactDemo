using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreReactDemo.Controllers;
using AspNetCoreReactDemo.Extensions;
using AspNetCoreReactDemo.Model;
using AspNetCoreReactDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Xunit;

namespace AspNetCoreReactDemo.Test
{
    public class DemoFilterControllerTest
    {
        [Fact]
        public async Task FailedToSave()
        {
            // arrange
            var data = "some fake data";
            var storage = new Mock<IDemoModelStorage>();
            storage.Setup(modelStorage => modelStorage.Update(data)).ReturnsAsync(true);
            storage.Setup(modelStorage => modelStorage.SaveChanges()).ReturnsAsync(false);

            var problemDetails = new ProblemDetails();
            var problemFactory = new Mock<ProblemDetailsFactory>();
            problemFactory
                .Setup(_ => _.CreateProblemDetails(
                    It.IsAny<HttpContext>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>())
                )
                .Returns(problemDetails).Verifiable();

            var controller = new DemoFilterController(storage.Object)
            {
                ProblemDetailsFactory = problemFactory.Object
            };

            // test
            var result = await controller.HandleModel(new ModelType()
            {
                SomeOtherField = data
            });

            // Validate
            Assert.Null(result.Value);
            Assert.IsType<ObjectResult>(result.Result);
            problemFactory.Verify();
        }
    }
}
