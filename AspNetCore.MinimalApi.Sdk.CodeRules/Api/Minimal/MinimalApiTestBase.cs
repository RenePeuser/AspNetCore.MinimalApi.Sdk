using System.Collections.Immutable;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;
using Solution.Parser.CSharp.Models;

namespace AspNetCore.MinimalApi.Sdk.CodeRules.Api.Controllers
{
    [TestClass]
    public class MinimalApiTestBase : MsTestBase
    {
        public MinimalApiTestBase()
        {
            // Without context statements like in program.cs or startup.cs
            var result = GetAllStatements().ToImmutableList();
            MapStatements = result.Select(r => r.Statement).ToImmutableList();
            AllApiVersions = result.Select(r => r.ApiVersion).Distinct().ToImmutableList();

            static IImmutableList<(Statement Statement, ApiVersion ApiVersion)> GetAllStatements()
            {
                var listStatements = ImmutableList<(Statement, ApiVersion)>.Empty;

                foreach (var syntaxTree in ProductiveSyntaxTrees)
                {
                    // All non bound statements
                    foreach (var syntaxTreeStatement in syntaxTree.Statements)
                    {
                        if (syntaxTreeStatement.SyntaxTree.Contains(".Map"))
                        {
                            if (listStatements.Any(s => s.Item1.SyntaxTree.Contains(syntaxTreeStatement.SyntaxTree)))
                            {
                                continue;
                            }

                            var versionMatch = GlobalRegex.MapToApiVersionRegex().Match(syntaxTreeStatement.SyntaxTree);
                            var version = versionMatch.Success ? versionMatch.Groups["version"].Value : "1";

                            listStatements = listStatements.Add(new ValueTuple<Statement, ApiVersion>(syntaxTreeStatement, new ApiVersion(version.ToInt(), 0)));
                        }
                    }

                    foreach (var record in syntaxTree.Records)
                    {
                        foreach (var method in record.Methods)
                        {
                            foreach (var methodStatement in method.Statements)
                            {
                                if (methodStatement.Contains(".Map"))
                                {
                                    if (listStatements.Any(s => s.Item1.SyntaxTree.Contains(methodStatement)))
                                    {
                                        continue;
                                    }

                                    var versionMatch = GlobalRegex.MapToApiVersionRegex().Match(methodStatement);
                                    var version = versionMatch.Success ? versionMatch.Groups["version"].Value : "1";

                                    listStatements = listStatements.Add(new ValueTuple<Statement, ApiVersion>(new Statement(methodStatement, record.FilePath), new ApiVersion(version.ToInt(), 0)));
                                }
                            }
                        }
                    }

                    foreach (var @class in syntaxTree.Classes)
                    {
                        foreach (var method in @class.Methods)
                        {
                            foreach (var methodStatement in method.Statements)
                            {
                                if (methodStatement.Contains(".Map"))
                                {
                                    if (listStatements.Any(s => s.Item1.SyntaxTree.Contains(methodStatement)))
                                    {
                                        continue;
                                    }

                                    var versionMatch = GlobalRegex.MapToApiVersionRegex().Match(methodStatement);
                                    var version = versionMatch.Success ? versionMatch.Groups["version"].Value : "1";

                                    listStatements = listStatements.Add(new ValueTuple<Statement, ApiVersion>(new Statement(methodStatement, @class.FilePath), new ApiVersion(version.ToInt(), 0)));
                                }
                            }
                        }
                    }
                }

                return listStatements;
            }
        }

        public static IImmutableList<Statement> MapStatements { get; set; } = ImmutableList<Statement>.Empty;
        public static IImmutableList<ApiVersion> AllApiVersions { get; set; } = ImmutableList<ApiVersion>.Empty;
    }
}
