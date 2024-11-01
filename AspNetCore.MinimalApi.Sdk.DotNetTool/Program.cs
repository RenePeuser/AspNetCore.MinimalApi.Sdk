namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    // Needed public to run automated tests which are debugable
    public static class Program
    {
        public static Task<int> Main(string[] args)
        {
            return new AppBuilder().Build().RunAsync(args);
        }
    }
}