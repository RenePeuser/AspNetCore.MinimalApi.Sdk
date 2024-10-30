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
