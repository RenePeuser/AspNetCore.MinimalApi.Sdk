namespace AspNetCore.MinimalApi.Sdk.DotNetTool.Test.MyApi.Config.Get
{
    [TestClass]
[TestCategory("myapi")]
[TestCategory("myapi config get")]
public class GetConfigTest : GlobalSetup
{
    /// <summary>
    /// Tests the 'myapi config get' command with different output formats.
    /// </summary>
    /// <param name="format">The format in which the configuration should be output (e.g., Json, JsonIndented, JsonAsString).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [DataTestMethod]
    [DataRow("Json")]
    [DataRow("JsonIndented")]
    [DataRow("JsonAsString")]
    public Task Get_Config_Test_With_Specific_Format(string format)
    {
        // Executes the CLI command and asserts the output against the expected JSON file.
        return Cli.AssertRunAsync($"myapi config get --format {format}",
                                  $"MyApi.Config.Get.Output.GetAs{format}.json");
    }

    /// <summary>
    /// Tests the 'myapi config get' command with different output formats and writes the output to a specified file.
    /// Asserts that the file exists after the command is executed.
    /// </summary>
    /// <param name="format">The format in which the configuration should be output (e.g., Json, JsonIndented, JsonAsString).</param>
    /// <param name="fileName">The name of the file to which the output should be written.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [DataTestMethod]
    [DataRow("Json", "simpleJson.json")]
    [DataRow("JsonIndented", "jsonIntended.json")]
    [DataRow("JsonAsString", "jsonAsString.json")]
    public async Task Get_Config_And_Write_It_Into_Given_File_With_Given_Format_Simple_Exist_Check(string format,
                                                                                                   string fileName)
    {
        // 1. Constructs the full path to the output file.
        var file = Path.Combine(Environment.CurrentDirectory, "MyApi", "Config", "Get", "Output", fileName);

        // 2. Executes the CLI command and asserts the output file exists.
        var outputFile = await Cli.AssertRunAsync<FileInfo>($"myapi config get --format {format} --output {file}").ConfigureAwait(false);

        // 3. Asserts that the output file exists.
        Assert.IsTrue(outputFile.Exists, $"File: {outputFile.FullName} should exist, but it does not");
    }

    /// <summary>
    /// Tests the 'myapi config get' command with different output formats and writes the output to a specified file.
    /// Asserts the output against the expected JSON file.
    /// </summary>
    /// <param name="format">The format in which the configuration should be output (e.g., Json, JsonIndented, JsonAsString).</param>
    /// <param name="fileName">The name of the file to which the output should be written.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [DataTestMethod]
    [DataRow("Json", "simpleJson.json")]
    [DataRow("JsonIndented", "jsonIntended.json")]
    [DataRow("JsonAsString", "jsonAsString.json")]
    public async Task Get_Config_And_Write_It_Into_Given_File_With_Given_Format(string format,
                                                                                string fileName)
    {
        // 1. Constructs the full path to the output file.
        var file = Path.Combine(Environment.CurrentDirectory, "MyApi", "Config", "Get", "Output", fileName);

        // 2. Executes the CLI command and asserts the output against the expected JSON file.
        await Cli.AssertRunAsync<FileInfo>($"myapi config get --format {format} --output {file}",
                                           $"MyApi.Config.Get.Output.GetAs{format}.json").ConfigureAwait(false);
    }
}
}
