using System.Threading.Tasks;

namespace RazorMailer.WebJob.Parsing
{
    public interface ITemplateParser
    {
        Task<string> RenderAsync(string name);
        Task<string> RenderAsync<TModel>(string name, TModel model);
    }
}