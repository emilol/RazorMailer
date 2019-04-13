using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using RazorMailer.Models;
using RazorMailer.Parsing;
using SendGrid.Helpers.Mail;

namespace RazorMailer.WebJob
{
    public class Functions
    {
        private readonly ITemplateParser _parser;

        public Functions(ITemplateParser parser)
        {
            _parser = parser;
        }

        public async Task HelloWorldEmailHandler(
            [QueueTrigger("hello-world")] HelloWorldModel model,
            [SendGrid(
                To = "{Email}",
                Subject = "Hello from RazorMailer"
            )]
            IAsyncCollector<SendGridMessage> messages,
            ILogger logger)
        {
            logger.LogInformation($"Sending email to {model.FirstName} {model.LastName}");

            var html = await _parser.RenderAsync("/Templates/HelloWorld.cshtml", model);
            var message = new SendGridMessage
            {
                HtmlContent = html
            };

            await messages.AddAsync(message);
        }
    }
}