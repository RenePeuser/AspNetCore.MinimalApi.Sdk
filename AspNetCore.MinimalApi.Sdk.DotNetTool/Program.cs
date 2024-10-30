namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    public static class Program
    {
        public static Task<int> Main(string[] args)
        {
            return new AppBuilder().Build().RunAsync(args);
        }
    }
}
