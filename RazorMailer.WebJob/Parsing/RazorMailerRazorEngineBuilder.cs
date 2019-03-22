using System.IO;
using System.Reflection;

namespace RazorMailer.WebJob.Parsing
{
    public class RazorMailerRazorEngineBuilder
    {
        private Assembly _viewAssembly;

        public RazorMailerRazorEngineBuilder UseEmbeddedResourcesProject(Assembly viewAssembly)
        {
            _viewAssembly = viewAssembly;
            return this;
        }

        public RazorMailerRazorEngineBuilder UseEmbeddedResourcesProject(Assembly referencedAssembly, string viewAssemblyName)
        {
            if (viewAssemblyName == null)
            {
                UseEmbeddedResourcesProject(referencedAssembly);
                return this;
            }

            var viewAssemblyLocation = Path.GetDirectoryName(referencedAssembly.Location);
            UseEmbeddedResourcesProject(
                Assembly.LoadFile(Path.Combine(viewAssemblyLocation, $"{viewAssemblyName}.dll")));

            return this;
        }

        public RazorMailerRazorEngine Build()
        {
            return new RazorMailerRazorEngine(_viewAssembly);
        }
    }
}