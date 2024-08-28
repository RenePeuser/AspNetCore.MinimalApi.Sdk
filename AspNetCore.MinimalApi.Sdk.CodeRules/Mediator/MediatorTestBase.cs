using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Extensions.Pack;
using Solution.Parser.CSharp;

namespace AspNetCore.MinimalApi.Sdk.CodeRules.Mediator
{
    public record MediatorImplementation(
        Class Handler,
        Class? Request,
        Class? Response,
        string RequestType,
        string ResponseType,
        CSharpSyntaxTree SyntaxTree);

    [TestClass]
    public abstract class MediatorTestBase : MsTestBase
    {
        internal static readonly IImmutableList<string> HandlerInterfaces = ImmutableList.Create("IRequestHandler", "IQueryHandler", "ICommandHandler");

        public MediatorTestBase()
        {
            MediatorImplementations = FindAllHandlers().ToImmutableList();

            IEnumerable<MediatorImplementation> FindAllHandlers()
            {
                foreach (var cSharpSyntaxTree in SyntaxTreesToAnalyze)
                {
                    foreach (var @class in cSharpSyntaxTree.Classes)
                    {
                        foreach (var classBaseType in @class.BaseTypes)
                        {
                            if (HandlerInterfaces.Any(type => classBaseType.TypeName.StartWith(type)))
                            {
#pragma warning disable SYSLIB1045
                                var genericParameters =
                                    Regex.Matches(classBaseType.TypeName, @"(?<=\<)(.*?)(?=\>)",
                                             RegexOptions.Singleline | RegexOptions.IgnoreCase).Select(m => m.Value)
                                         .First().Split(',');
#pragma warning restore SYSLIB1045
                                var requestType = genericParameters.First().Trim();
                                var responseType = genericParameters.Length > 1 ? genericParameters.Last().Trim() : null;

                                var request = cSharpSyntaxTree.Records.FirstOrDefault(c => c.Name == requestType) ??
                                              cSharpSyntaxTree.Classes.FirstOrDefault(c => c.Name == requestType);

                                var response = responseType.IsNull()
                                    ? null
                                    : cSharpSyntaxTree.Records.FirstOrDefault(c => c.Name == responseType) ??
                                      cSharpSyntaxTree.Classes.FirstOrDefault(c => c.Name == responseType);

                                yield return new MediatorImplementation(@class, request, response, requestType, responseType ?? string.Empty,
                                    cSharpSyntaxTree);
                            }
                        }
                    }
                }
            }
        }

        protected static IImmutableList<MediatorImplementation> MediatorImplementations { get; private set; } = ImmutableList<MediatorImplementation>.Empty;
    }
}
