using System.Collections.Immutable;
using Solution.Parser.CSharp;

namespace AspNetCore.MinimalApi.Sdk.CodeRules.Api.Controllers
{
    [TestClass]
    public class ControllerTestBase : MsTestBase
    {
        public ControllerTestBase()
        {
            Controllers = (from syntaxTree in ProductiveSyntaxTrees
                           from @class in syntaxTree.Classes
                           where @class.BaseTypes.Any(baseType => baseType.TypeName == "ControllerBase")
                           select @class).ToImmutableList();


            ActionControllers = Controllers.Where(controller => controller.Methods.All(m => m.Attributes.Any(a => a.Name.Contains("HttpPost")))).ToImmutableList();
            RestControllers = Controllers.Except(ActionControllers).ToImmutableList();
        }

        public IImmutableList<Class> Controllers { get; set; } = ImmutableList<Class>.Empty;
        public ImmutableList<Class> RestControllers { get; set; } = ImmutableList<Class>.Empty;
        public ImmutableList<Class> ActionControllers { get; set; } = ImmutableList<Class>.Empty;
    }
}
