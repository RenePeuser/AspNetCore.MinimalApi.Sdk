using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.Test.MyApi.Config.Get
{
    [TestClass]
    [TestCategory("myapi")]
    [TestCategory("myapi config set")]
    public class SetConfigTest : GlobalSetup
    {
        [TestInitialize]
        public Task InitAsync()
        {
            return CleanupAsync();
        }

        [TestMethod]
        public async Task Get_Config_Test_With_Specific_Format()
        {
            // 1. get config
            var currentConfig = await Cli.AssertRunAsync("myapi config get --format Json",
                                                         "MyApi.Config.Set.Output.GetConfig.json");

            // 2. update the expected settings
            await Cli.AssertRunAsync($"myapi config set {currentConfig.Replace("7", "5")}");

            // 3. get config again to check if it is uo to date
            await Cli.AssertRunAsync("myapi config get --format Json",
                                     "MyApi.Config.Set.Output.GetConfigAfterUpdate.json");
        }

        [TestCleanup]
        public async Task CleanupAsync()
        {
            var originalAppSettings = EmbeddedFile.GetFileContentFrom("MyApi.Config.Set.Parameters.OriginalAppSettings.json");

            await Cli.AssertRunAsync($"myapi config set {originalAppSettings}");
        }
    }
}
