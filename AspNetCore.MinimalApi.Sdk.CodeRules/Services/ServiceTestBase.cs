using System.Collections.Immutable;
using AspNetCore.MinimalApi.Sdk.CodeRules.Mediator;
using Extensions.Pack;
using Solution.Parser.CSharp;

namespace AspNetCore.MinimalApi.Sdk.CodeRules.Services
{
    [TestClass]
    public class ServiceTestBase : MsTestBase
    {
        public ServiceTestBase()
        {
            Services = FindServices().ToImmutableList();

            IEnumerable<(Class Class, Class? Registration, CSharpSyntaxTree syntaxTree)> FindServices()
            {
                foreach (var cSharpSyntaxTree in ProductiveSyntaxTrees)
                {
                    var services = cSharpSyntaxTree.Classes.Where(@class => @class.Modifiers.Any(m => m is Modifier.Static or Modifier.Abstract).IsFalse() &&
                                                                            MediatorTestBase.HandlerInterfaces.Any(type => @class.BaseTypes.Any(t => t.TypeName.StartWith(type))).IsFalse() &&
                                                                            @class.Methods.Any() &&
                                                                            (@class.BaseTypes.IsEmpty() || @class.BaseTypes.Any(t => t.TypeName.StartWith("I")))).ToImmutableList();

                    foreach (var service in services)
                    {
                        var registrationClass = cSharpSyntaxTree.Classes.FirstOrDefault(@class => @class.Name.Contains(service.Name) && @class.Name.Contains("Extension"));
                        yield return (service, registrationClass, cSharpSyntaxTree);
                    }
                }
            }
        }

        protected ImmutableList<(Class Class, Class? Registration, CSharpSyntaxTree syntaxTree)> Services { get; set; } = ImmutableList<(Class Class, Class? Registration, CSharpSyntaxTree syntaxTree)>.Empty;
    }
}
