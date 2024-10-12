using System.Collections.Immutable;
using System.Text;
using System.Text.RegularExpressions;
using Extensions.Pack;
using Solution.Parser.CSharp;

namespace AspNetCore.MinimalApi.Sdk.CodeRules.Fixtures.Basic.Records._00003
{
    [TestCategory("Coding-Rules Fixtures")]
    // [TestCategory("Basic")]
    [TestClass]
    public class RJIT_CR_BC_R_00003 : MsTestBase
    {
        private static ImmutableList<AnalyzedCodeResult> GetAnalyzedCodeResult()
        {
            var analyzedCode = (from record in Records
                                where record.SyntaxTree.DoesNotContain("partial Regex")
                                from modifier in record.Modifiers
                                where modifier == Modifier.Partial
                                select new AnalyzedCodeResult(record.FullQualifiedName, record.Name, record.SyntaxTree)).ToImmutableList();
            return analyzedCode;
        }

        [TestMethod]
        public async Task Should_SelfHealAsync()
        {
            var codeRuleResult = GetAnalyzedCodeResult();
            foreach (var errors in codeRuleResult)
            {
                var cSharpFiles = Solution.Projects.SelectMany(p => p.CSharpFileInfos).ToList();
                foreach (var allSyntax in cSharpFiles)
                {
                    var codeToAnalyze = await File.ReadAllTextAsync(allSyntax.Value.FullName).ConfigureAwait(false);
                    if (codeToAnalyze.Contains(errors.Name))
                    {
                        var pattern = errors.ErrorSyntaxTree;
                        var replacement = errors.ErrorSyntaxTree.Replace("set;", "init;");
                        var modifiedContent = Regex.Replace(codeToAnalyze, pattern, replacement);
                        await File.WriteAllTextAsync(allSyntax.Value.FullName, modifiedContent, new UTF8Encoding())
                                  .ConfigureAwait(false);
                    }
                }
            }
        }

        private sealed record AnalyzedCodeResult(string FullName, string Name, string ErrorSyntaxTree);
    }
}
