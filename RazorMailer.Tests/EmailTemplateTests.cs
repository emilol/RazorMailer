using System.Threading.Tasks;
using Assent;
using RazorMailer.Models;
using RazorMailer.Parsing;
using Xunit;

namespace RazorMailer.Tests
{
    public class EmailTemplateTests
    {
        [Fact]
        public async Task HelloWorldModel()
        {
            // Arrange
            var template = "/Templates/HelloWorld.cshtml";
            var model = new HelloWorldModel
            {
                FirstName = "Joe",
                LastName = "Smith"
            };
            var templateParser = new TemplateParser();

            // Act
            var result = await templateParser.RenderAsync(template, model);

            // Assert
            this.Assent(result);
        }

        [Fact]
        public async Task NoModel()
        {
            // Arrange
            var template = "/Templates/NoModel.cshtml";
            var templateParser = new TemplateParser();

            // Act
            var result = await templateParser.RenderAsync(template);

            // Assert
            this.Assent(result);
        }
    }
}