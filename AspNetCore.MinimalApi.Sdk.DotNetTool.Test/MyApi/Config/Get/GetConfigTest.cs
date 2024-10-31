using System.Text.Json.Nodes;
using AspNetCore.Simple.MsTest.Sdk;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.Test.MyApi.Config.Get
{
    [TestClass]
    [TestCategory("myapi config get")]
    public class GetConfigTest : GlobalSetup
    {
        [DataTestMethod]
        [DataRow("Json")]
        [DataRow("JsonIndented")]
        [DataRow("JsonAsString")]
        public Task Get_Config_Test_With_Specific_Format(string format)
        {
            return Cli.AssertRunAsync($"myapi config get --format {format}",
                                      $"MyApi.Config.Get.Output.GetAs{format}.json");
        }

        [DataTestMethod]
        [DataRow("Json", "simpleJson.json")]
        [DataRow("JsonIndented", "jsonIntended.json")]
        [DataRow("JsonAsString", "jsonAsString.json")]
        public async Task Get_Config_And_Write_It_Into_Given_File_With_Given_Format_Simple_Exist_Check(string format,
                                                                                                       string fileName)
        {
            var file = Path.Combine(Environment.CurrentDirectory, "MyApi", "Config", "Get", "Output", fileName);

            var outputFile = await Cli.AssertRunAsync<FileInfo>($"myapi config get --format {format} --output {file}").ConfigureAwait(false);

            Assert.IsTrue(outputFile.Exists, $"File: {outputFile.FullName} should exists, but it does not");
        }

        [DataTestMethod]
        [DataRow("Json", "simpleJson.json")]
        [DataRow("JsonIndented", "jsonIntended.json")]
        [DataRow("JsonAsString", "jsonAsString.json")]
        public async Task Get_Config_And_Write_It_Into_Given_File_With_Given_Format(string format,
                                                                                    string fileName)
        {
            var file = Path.Combine(Environment.CurrentDirectory, "MyApi", "Config", "Get", "Output", fileName);

            await Cli.AssertRunAsync<FileInfo>($"myapi config get --format {format} --output {file}",
                                               $"MyApi.Config.Get.Output.GetAs{format}.json").ConfigureAwait(false);
        }
    }

    //internal sealed record GetConfig() : IRequest;

    //internal sealed class LocalizeAllStringsHandler(MyAppStarter myAppStarter) : IRequestHandler<GetConfig>
    //{
    //    public Task Handle(GetConfig request,
    //                       CancellationToken cancellationToken)
    //    {
    //        var consoleArguments = CollectConsoleParameters(request).ToArray();

    //        return myAppStarter.StartAsync(consoleArguments, cancellationToken);
    //    }

    //    private IEnumerable<string> CollectConsoleParameters(GetConfig parameters)
    //    {
    //        // 1. Parameter solution file from the backend to parse
    //        yield return "myapi";
    //        yield return "config";
    //        yield return "get";
    //    }
    //}
}
