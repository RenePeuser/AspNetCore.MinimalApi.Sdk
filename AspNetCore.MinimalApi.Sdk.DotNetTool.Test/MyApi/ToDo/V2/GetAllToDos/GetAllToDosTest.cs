namespace AspNetCore.MinimalApi.Sdk.DotNetTool.Test
{
    [TestClass]
    [TestCategory("MyApi")]
    [TestCategory("MyApi ToDo V2 GetAllToDos")]
    public class GetAllToDosTest : GlobalSetup
    {
        /// <summary>
        /// Tests the 'myapi todo v2 getalltodos' command with different output formats and writes the output to a specified file.
        /// Asserts the output against the expected JSON file.
        /// </summary>
        /// <param name="format">The format in which the configuration should be output (e.g., Json, JsonIndented, JsonAsString).</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [DataTestMethod]
        [DataRow("Json")]
        [DataRow("JsonIndented")]
        [DataRow("JsonAsString")]
        public Task MyApi_ToDo_V2_GetAllToDos_To_Console_Out_With_Format(string format)
        {
            // 1. Executes the CLI command and asserts the output against the expected JSON file.
            return Cli.AssertRunAsync($"myapi todo v2 getalltodos MyApi.ToDo.V2.GetAllToDos.Parameters.GetAllToDos.json --format {format}",
                                      "MyApi.ToDo.V2.GetAllToDos.Output.GetAllToDos.json");
        }
        /// <summary>
        /// Tests the 'myapi todo v2 getalltodos' command with different output formats and writes the output to a specified file.
        /// Asserts the output against the expected JSON file.
        /// </summary>
        /// <param name="format">The format in which the configuration should be output (e.g., Json, JsonIndented, JsonAsString).</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [DataTestMethod]
        [DataRow("Json")]
        [DataRow("JsonIndented")]
        [DataRow("JsonAsString")]
        public Task MyApi_ToDo_V2_GetAllToDos_To_File_Output_With_Format(string format)
        {
            // 1. Constructs the full path to the output file.
            var file = Path.Combine(Environment.CurrentDirectory, "MyApi", "ToDo", "V2", "GetAllToDos", $"OutputAs{format}.json");
        
            // 2. Executes the CLI command and asserts the output against the expected JSON file.
            return Cli.AssertRunAsync($"myapi todo v2 getalltodos MyApi.ToDo.V2.GetAllToDos.Parameters.GetAllToDos.json --format {format} --output {file}",
                                      "MyApi.ToDo.V2.GetAllToDos.Output.GetAllToDos.json");
        }
                                               
    }
}