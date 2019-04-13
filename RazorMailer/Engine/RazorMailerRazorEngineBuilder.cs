using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace RazorMailer.Engine
{
    public class RazorMailerRazorEngineBuilder
    {
        private Assembly _viewAssembly;

        public RazorMailerRazorEngineBuilder UseEmbeddedResourcesProject(Assembly viewAssembly)
        {
            var relatedAssembly = RelatedAssemblyAttribute.GetRelatedAssemblies(viewAssembly, false).SingleOrDefault();

            if (relatedAssembly == null)
            {
                _viewAssembly = viewAssembly;
                
                return this;
            }

            _viewAssembly = relatedAssembly;

            return this;
        }

        public RazorMailerRazorEngine Build()
        {
            return new RazorMailerRazorEngine(_viewAssembly);
        }
    }
}