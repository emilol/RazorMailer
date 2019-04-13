using System.Threading.Tasks;
using RazorMailer.Engine;

namespace RazorMailer.Parsing
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