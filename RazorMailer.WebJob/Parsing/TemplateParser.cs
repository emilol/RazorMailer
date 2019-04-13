using System.Threading.Tasks;

namespace RazorMailer.WebJob.Parsing
{
    public class TemplateParser : ITemplateParser
    {
        public async Task<string> RenderAsync(string name)
        {
            return await RenderAsync<object>(name, null);
        }

        public async Task<string> RenderAsync<TModel>(string name, TModel model)
        {
            var engine = new RazorMailerRazorEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(IAmARazorAssembly).Assembly)
                .Build();

            return await engine.RenderAsync(name, model);
        }
    }
}