using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Text;

namespace Jarvis.CodeGeneration
{
    [Generator]
    public class ControllerGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
#if DEBUG
            if (!Debugger.IsAttached)
            {
                //Debugger.Launch();
            }
#endif

            string[] controllerNames = new[]
            {
                "Container",
                "Document",
                "Tag"
            };

            int actualVersion = 3;
            bool lastVersionIsPreview = true;

            for (int i = 1; i <= actualVersion; i++)
            {
                string suffix = (i == actualVersion && lastVersionIsPreview) ? "-preview" : "";

                foreach (var controllerName in controllerNames)
                {
                    string fileName = $"{controllerName}{i}Controller";
                    string content = $@"using Microsoft.AspNetCore.Mvc;

namespace Jarvis.HostApiVersion3.Controllers
{{
    [ApiController]
    [ApiVersion(""{i}.0{suffix}"")]
    [Route(""/api/{controllerName}"")]
    public partial class {controllerName}{i}Controller : {GetParentControllerName(i, controllerName)}
    {{
       
    }}
}}
";
                    context.AddSource(fileName, SourceText.From(content, Encoding.UTF8));
                }
            }
        }

        private object GetParentControllerName(int i, string controllerName)
        {
            return i == 1 ? "Controller" : $"{controllerName}{i - 1}Controller";
        }

        public void Initialize(GeneratorInitializationContext context)
        {
           
        }
    }
}
