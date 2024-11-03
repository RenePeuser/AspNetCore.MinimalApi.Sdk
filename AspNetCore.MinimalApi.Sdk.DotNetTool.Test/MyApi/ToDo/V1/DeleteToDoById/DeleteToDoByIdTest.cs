namespace AspNetCore.MinimalApi.Sdk.DotNetTool.Test
{
    [TestClass]
    [TestCategory("MyApi")]
    [TestCategory("MyApi ToDo V1 DeleteToDoById")]
    public class DeleteToDoByIdTest : GlobalSetup
    {
        /// <summary>
        /// Tests the 'myapi todo v1 deletetodobyid' command with different output formats and writes the output to a specified file.
        /// Asserts the output against the expected JSON file.
        /// </summary>
        /// <param name="format">The format in which the configuration should be output (e.g., Json, JsonIndented, JsonAsString).</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [DataTestMethod]
        [DataRow("Json")]
        [DataRow("JsonIndented")]
        [DataRow("JsonAsString")]
        public Task MyApi_ToDo_V1_DeleteToDoById_To_Console_Out_With_Format(string format)
        {
            // 1. Executes the CLI command and asserts the output against the expected JSON file.
            return Cli.AssertRunAsync($"myapi todo v1 deletetodobyid MyApi.ToDo.V1.DeleteToDoById.Parameters.DeleteToDoById.json --format {format}",
                                      "MyApi.ToDo.V1.DeleteToDoById.Output.DeleteToDoById.json");
        }
        /// <summary>
        /// Tests the 'myapi todo v1 deletetodobyid' command with different output formats and writes the output to a specified file.
        /// Asserts the output against the expected JSON file.
        /// </summary>
        /// <param name="format">The format in which the configuration should be output (e.g., Json, JsonIndented, JsonAsString).</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [DataTestMethod]
        [DataRow("Json")]
        [DataRow("JsonIndented")]
        [DataRow("JsonAsString")]
        public Task MyApi_ToDo_V1_DeleteToDoById_To_File_Output_With_Format(string format)
        {
            // 1. Constructs the full path to the output file.
            var file = Path.Combine(Environment.CurrentDirectory, "MyApi", "ToDo", "V1", "DeleteToDoById", $"OutputAs{format}.json");
        
            // 2. Executes the CLI command and asserts the output against the expected JSON file.
            return Cli.AssertRunAsync($"myapi todo v1 deletetodobyid MyApi.ToDo.V1.DeleteToDoById.Parameters.DeleteToDoById.json --format {format} --output {file}",
                                      "MyApi.ToDo.V1.DeleteToDoById.Output.DeleteToDoById.json");
        }
                                               
    }
}
