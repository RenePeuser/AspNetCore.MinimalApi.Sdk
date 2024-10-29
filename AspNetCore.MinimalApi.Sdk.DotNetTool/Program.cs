namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal static class Program
    {
        public static Task<int> Main(string[] args)
        {
            return new AppBuilder().Build().RunAsync(args);
        }
    }
}
