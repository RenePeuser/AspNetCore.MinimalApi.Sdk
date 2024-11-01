namespace AspNetCore.MinimalApi.Sdk.DotNetTool.Test
{
    [TestClass]
    [TestCategory("myapi")]
    [TestCategory("myapi config get")]
    public class AddToDoTest : GlobalSetup
    {
        [TestMethod]
        public Task MyApi_ToDo_V1_AddToDo_v1()
        {
            return Cli.AssertRunAsync("myapi todo v1 addtodo AddToDo.json",
                                      "AddToDo.json");
        }

        [TestMethod]
        public Task MyApi_ToDo_V1_AddToDo_v2()
        {
            return Cli.AssertRunAsync("myapi todo v1 addtodo MyApi.ToDo.V1.AddToDo.Parameters.AddToDo.json",
                                      "MyApi.ToDo.V1.AddToDo.Output.AddToDo.json");
        }
    }
}
