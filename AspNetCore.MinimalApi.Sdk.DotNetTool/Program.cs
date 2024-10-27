namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var result = await new AppBuilder().Build().RunAsync(args);

            Console.Read();

            return result;
        }
    }
}
